using System.Reflection;
using SportEventManager.SharedKernel;
using SportEventManager.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using SportEventManager.Core.UserAggregate;
using SportEventManager.Core.TeamAggregate;
using SportEventManager.Core.EventAggregate;
using SportEventManager.Core.MatchAggregate;

namespace SportEventManager.Infrastructure.Data;

public class AppDbContext : DbContext
{
  private readonly IDomainEventDispatcher? _dispatcher;
  public DbSet<Team> Teams => Set<Team>();
  public DbSet<Player> Players => Set<Player>();
  public DbSet<Stadium> Stadiums => Set<Stadium>();
  public DbSet<Match> Matches => Set<Match>();
  public DbSet<Event> Events => Set<Event>();
  public DbSet<FootballStatsBase> Stats => Set<FootballStatsBase>();
  public DbSet<FbPlayerMatchStats> PlayersMatchStats => Set<FbPlayerMatchStats>();
  public DbSet<FbTeamMatchStats> TeamsMatchesStats => Set<FbTeamMatchStats>();
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
  {
  }

  public AppDbContext(DbContextOptions<AppDbContext> options,
    IDomainEventDispatcher? dispatcher)
      : base(options)
  {
    _dispatcher = dispatcher;
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<User>()
      .ToTable("AspNetUsers", t => t.ExcludeFromMigrations());

    modelBuilder.Entity<FbTeamMatchStats>().ToTable("FbTeamMatchStats");
    modelBuilder.Entity<FbPlayerMatchStats>().ToTable("PlayerMatchStats");

    modelBuilder.Entity<Stadium>().HasKey(x => new { x.Id });

    modelBuilder.Entity<Team>()
      .HasMany(t => t.Players)
      .WithMany(p => p.Teams)
      .UsingEntity<TeamPlayer>(
       l => l.HasOne<Player>().WithMany(i => i.TeamPlayers),
       r => r.HasOne<Team>().WithMany(e => e.TeamPlayers));

    modelBuilder.Entity<Team>()
      .HasMany(t => t.Players)
      .WithMany(p => p.Teams)
      .UsingEntity<TeamPlayer>(
       j => j.Property(e => e.JoinOn).HasDefaultValueSql("GETUTCDATE()"));

    modelBuilder.Entity<Match>()
      .HasOne(m => m.HomeTeam)
      .WithMany(t => t.HomeMatches)
      .HasForeignKey(m => m.HomeTeamId)
      .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<Match>()
      .HasOne(m => m.GuestTeam)
      .WithMany(t => t.AwayMatches)
      .HasForeignKey(m => m.GuestTeamId)
      .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<Match>()
      .HasOne(m => m.HomeTeamStats)
      .WithOne()
      .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<Match>()
      .HasOne(m => m.GuestTeamStats)
      .WithOne()
      .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }

  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
  {
    int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

    // ignore events if no dispatcher provided
    if (_dispatcher == null) return result;

    // dispatch events only if save was successful
    var entitiesWithEvents = ChangeTracker.Entries<EntityBase>()
        .Select(e => e.Entity)
        .Where(e => e.DomainEvents.Any())
        .ToArray();

    await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);

    return result;
  }

  public override int SaveChanges()
  {
    return SaveChangesAsync().GetAwaiter().GetResult();
  }
}
