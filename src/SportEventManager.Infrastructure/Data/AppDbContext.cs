using System.Reflection;
using SportEventManager.SharedKernel;
using SportEventManager.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using SportEventManager.Core.UserAggregate;
using SportEventManager.Core.TeamAggregate;
using SportEventManager.Core.TeamAggregate.Stats;
using SportEventManager.Core.EventAggregate;

namespace SportEventManager.Infrastructure.Data;

public class AppDbContext : DbContext
{
  private readonly IDomainEventDispatcher? _dispatcher;

  public DbSet<Team> Teams => Set<Team>();

  public DbSet<Player> Players => Set<Player>();

  public DbSet<Stadium> Stadiums => Set<Stadium>();

  public DbSet<Match> Matches => Set<Match>();

  public DbSet<Event> Events => Set<Event>();

  public DbSet<FBPlayerStats> PlayersStats => Set<FBPlayerStats>();

  public DbSet<FBTeamStats> TeamsStats => Set<FBTeamStats>();

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
    modelBuilder.Entity<FootballStats>()
       .ToTable("FootballStats", t => t.ExcludeFromMigrations());

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
