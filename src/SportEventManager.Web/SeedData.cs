using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SportEventManager.Infrastructure.Data;
using SportEventManager.Core.UserAggregate;
using SportEventManager.Core.EventAggregate;
using SportEventManager.Core.TeamAggregate;

namespace SportEventManager.Web;

public static class SeedData
{
  public static readonly string[] roles = new[] { "Admin", "EventManager", "TeamManager" };
  public static readonly string[] emails = new[] { "admin@admin.com", "event@event.com", "team@team.com" };
  public static readonly string[] names = new[] { "Admin", "EventManager", "TeamManager" };
  public static readonly string[] passwords = new[] { "Admin1!", "Event1!", "Team1!" };

  public async static Task InitializeAsync(IServiceProvider serviceProvider)
  {
    using (var userDbContext = new UserDbContext(
        serviceProvider.GetRequiredService<DbContextOptions<UserDbContext>>()))
    {
      if(userDbContext.Users.Count() < 3)
      {
        await PopulateTestDataAsync(userDbContext, serviceProvider);
      }
    }

    using (var appDbContext = new AppDbContext(
        serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>(), null))
    {
      if(appDbContext.Events.Where(Event => !Event.IsArchived).Count() >= 3)
      {
        return; //DB has been already seeded
      }
      await PopulateTestDataAsync(appDbContext, serviceProvider);
    }
  }
  public async static Task PopulateTestDataAsync(DbContext dbContext, IServiceProvider serviceProvider)
  {
    if(dbContext is AppDbContext appDb)
    {
      ClearAppDb(appDb);
      await PrepareExampleAppDbDataAsync(appDb, serviceProvider);
    } 
    else if(dbContext is UserDbContext userDb) 
    {
      ClearUserDb(userDb);
      await PrepareExampleUserRolesAsync(serviceProvider);
      await PrepareExampleUsersAsync(serviceProvider);
    }
  }

  private static void ClearUserDb(UserDbContext userDb)
  {
    userDb.RemoveRange(userDb.Users);
    userDb.RemoveRange(userDb.UserRoles);
    userDb.RemoveRange(userDb.Roles);
    userDb.SaveChanges();
  }

  private async static Task PrepareExampleUserRolesAsync(IServiceProvider serviceProvider)
  {
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    foreach (var role in roles)
    {
      if (!await roleManager.RoleExistsAsync(role))
      {
        await roleManager.CreateAsync(new IdentityRole(role));
      }
    }
  }

  private async static Task PrepareExampleUsersAsync(IServiceProvider serviceProvider)
  {
    var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
    
      for (int i = 0; i < 3; i++)
      {
        if (await userManager.FindByEmailAsync(emails[i]) == null)
        {
          var user = new User()
          {
            FirstName = names[i],
            LastName = names[i],
            UserName = names[i],
            Email = emails[i],
            EmailConfirmed = true
          };
          await userManager.CreateAsync(user, passwords[i]);
          await userManager.AddToRoleAsync(user, names[i]);
        }
      }
  }

  private static void ClearAppDb(AppDbContext appDb)
  {
    appDb.RemoveRange(appDb.PlayersMatchStats);
    appDb.RemoveRange(appDb.Players);
    appDb.RemoveRange(appDb.TeamsMatchesStats);
    appDb.RemoveRange(appDb.Teams);
    //appDb.RemoveRange(appDb.Stats);
    appDb.RemoveRange(appDb.Stadiums);
    appDb.RemoveRange(appDb.Matches);
    appDb.RemoveRange(appDb.Events);

    appDb.SaveChanges();
  }

  private async static Task PrepareExampleAppDbDataAsync(AppDbContext appDb, IServiceProvider serviceProvider)
  {
    var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
    var teamManagerUser = await userManager.FindByNameAsync("TeamManager");
    var eventManagerUser = await userManager.FindByNameAsync("EventManager");

    for (int i = 1; i <= 9; i++)
    {
      var stadium = new Stadium($"Stadion {i}", $"Miasto {i}");
      appDb.Stadiums.Add(stadium);
    }

    appDb.SaveChanges();

    List<Team> teams = new List<Team>(48);
    List<string> existingPeselNumbers = new List<string>(11 * 48);
    List<string> existingTeamsTags = new List<string>(48);
    int uniquePeselModifier = 0;
    for (int i = 1; i <= 48; i++)
    {
      var team = new Team($"{teamManagerUser?.Id}", $"Drużyna {i}", $"D{i}", $"Miasto {i}", 11, existingTeamsTags);
      await appDb.Teams.AddAsync(team);
      existingTeamsTags.Add(team.Tag);
      for (int j = 1; j <= 11; j++)
      {
        var player = new Player($"Imię {j} - {team.Name}", $"Nazwisko {j} - {team.Name}", $"{90030501900 + uniquePeselModifier}");
        appDb.Players.Add(player);
        team.AddPlayer(player, existingPeselNumbers);
        uniquePeselModifier++;
        existingPeselNumbers.Add(player.Pesel);
        
      }
      teams.Add(team);
    }

    appDb.SaveChanges();  //saving players and empty teams

    foreach (Team team in teams)
    {
      int n = 1;
      for (int k = 0; k < team.TeamPlayers.Count; k++)
      {
        team.UpdateTeamPlayer(k, n++);
      }
      appDb.Update(team);
    }
    appDb.SaveChanges(); //saving updated teams with players

    for (int i = 1; i <= 3; i++)
    {
      var eventName = $"Event {i}";
      var startTime = DateTime.Now.AddDays(i - 1);
      var endTime = startTime.AddDays(2).AddHours(2);
      var @event = new Event(eventManagerUser?.Id, eventName, startTime, endTime, 9, 15);

      var teamsForEvent = appDb.Teams.Skip((i - 1) * 16).Take(16).ToList();
      foreach (var team in teamsForEvent)
      {
        @event.AddTeam(team);
      }

      var stadiumsForEvent = appDb.Stadiums.Skip((i - 1) * 3).Take(3).ToList();
      foreach (var stadium in stadiumsForEvent)
      {
        @event.AddStadium(stadium);
      }

      appDb.Events.Add(@event);
    }
    appDb.SaveChanges(); //saving events
  }
}

