using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportEventManager.Core.EventAggregate;
using SportEventManager.Core.StatisticsAggregate;
using SportEventManager.Core.TeamAggregate;
using SportEventManager.Core.UserAggregate;
using SportEventManager.Infrastructure.Data.Config.UserAggregate;

namespace SportEventManager.Infrastructure.Data;

public class UserDbContext : IdentityDbContext<User>
{
  public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

  protected override void OnModelCreating(ModelBuilder builder)
  { 
    base.OnModelCreating(builder);
    builder.ApplyConfiguration(new UserConfiguration());

    //builder.Entity<Event>()
    //  .ToTable("Events", t => t.ExcludeFromMigrations());

    //builder.Entity<Event>()
    //  .ToTable("Stadium", t => t.ExcludeFromMigrations());

    //builder.Entity<Team>()
    //  .ToTable("Teams", t => t.ExcludeFromMigrations());

    //builder.Entity<Match>()
    //  .ToTable("Matches", t => t.ExcludeFromMigrations());

    //builder.Entity<Player>()
    //  .ToTable("Players", t => t.ExcludeFromMigrations());

    //builder.Entity<FootballStatsBase>()
    //  .ToTable("Stats", t => t.ExcludeFromMigrations());
    ;
  }
}
