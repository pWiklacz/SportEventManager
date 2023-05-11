using System.Reflection;
using SportEventManager.SharedKernel;
using SportEventManager.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using SportEventManager.Core.UserAggregate;
using SportEventManager.Core.TeamAggregate;
using SportEventManager.Core.EventAggregate;
using SportEventManager.Core.StatisticsAggregate;

namespace SportEventManager.Infrastructure.Data;

public class AppDbContext : DbContext
{
  private readonly IDomainEventDispatcher? _dispatcher;

  public DbSet<Team> Teams => Set<Team>();

  public DbSet<Player> Players => Set<Player>();

  public DbSet<Stadium> Stadiums => Set<Stadium>();

  public DbSet<Match> Matches => Set<Match>();

  public DbSet<Event> Events => Set<Event>();

  public DbSet<FbPlayerStats> PlayersStats => Set<FbPlayerStats>();

  public DbSet<FbTeamStats> TeamsStats => Set<FbTeamStats>();

  public DbSet<FbTeamMatchStats> TeamsMatchesStats => Set<FbTeamMatchStats>();

  public DbSet<Event2Stadium> Event2Stadium => Set<Event2Stadium>();

  public DbSet<Team2Player> Team2Player => Set<Team2Player>();

  public DbSet<Team2User> Team2User => Set<Team2User>();

  public DbSet<Event2Team> Event2Team => Set<Event2Team>();


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

    modelBuilder.Entity<Statistics>().ToView("Statistics").HasNoKey();
    // modelBuilder.Entity<FootballStatsBase>().ToView("FootballStats").HasNoKey();

    modelBuilder.Entity<FbTeamStats>()
      .HasBaseType<FootballStatsBase>()
      .ToTable("FbTeamStats");

    modelBuilder.Entity<FbPlayerStats>()
      .HasBaseType<FootballStatsBase>()
      .ToTable("FbPlayerStats");

    modelBuilder.Entity<FbTeamMatchStats>()
      .HasBaseType<FootballStatsBase>()
      .ToTable("FbTeamMatchStats");

    Event2StadiumConfig(modelBuilder);

    Event2TeamConfig(modelBuilder);

    Team2PlayerConfig(modelBuilder);

    Team2UserConfig(modelBuilder);

    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }

  private static void Team2UserConfig(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Team2User>()
      .HasKey(tu => new { tu.TeamId, tu.OwnerId });

    modelBuilder.Entity<Team2User>()
      .HasOne(tu => tu.User)
      .WithMany(u => u.Teams2Users)
      .HasForeignKey(tu => tu.OwnerId);

    modelBuilder.Entity<Team2User>()
      .HasOne(tu => tu.Team)
      .WithMany(t => t.Teams2Users)
      .HasForeignKey(tu => tu.TeamId);
  }

  private static void Team2PlayerConfig(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Team2Player>()
      .HasKey(tp => new { tp.TeamId, tp.PlayerId });

    modelBuilder.Entity<Team2Player>()
      .HasOne(tp => tp.Player)
      .WithMany(p => p.Teams2Players)
      .HasForeignKey(tp => tp.PlayerId);

    modelBuilder.Entity<Team2Player>()
      .HasOne(tp => tp.Team)
      .WithMany(t => t.Teams2Players)
      .HasForeignKey(tp => tp.TeamId);
  }

  private static void Event2TeamConfig(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Event2Team>()
      .HasKey(et => new { et.EventId, et.TeamId });

    modelBuilder.Entity<Event2Team>()
      .HasOne(et => et.Team)
      .WithMany(t => t.Events2Teams)
      .HasForeignKey(et => et.TeamId);

    modelBuilder.Entity<Event2Team>()
      .HasOne(et => et.Event)
      .WithMany(e => e.Events2Teams)
      .HasForeignKey(et => et.EventId);
  }

  private static void Event2StadiumConfig(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Event2Stadium>()
      .HasKey(es => new { es.EventId, es.StadiumId });

    modelBuilder.Entity<Event2Stadium>()
      .HasOne(es => es.Event)
      .WithMany(e => e.Events2Stadiums)
      .HasForeignKey(es => es.EventId);

    modelBuilder.Entity<Event2Stadium>()
      .HasOne(es => es.Stadium)
      .WithMany(s => s.Events2Stadiums)
      .HasForeignKey(es => es.StadiumId);
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
