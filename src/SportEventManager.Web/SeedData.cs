using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SportEventManager.Infrastructure.Data;
using SportEventManager.Core.UserAggregate;

namespace SportEventManager.Web;

public static class SeedData
{
  public static readonly string[] roles = new[] { "Admin", "EventManager", "TeamManager" };
  public static readonly string[] emails = new[] { "admin@admin.com", "event@event.com", "team@team.com" };
  public static readonly string[] names = new[] { "Admin", "EventManager", "TeamManager" };
  public static readonly string[] passwords = new[] { "Admin1!", "Event1!", "Team1!" };

  public async static Task InitializeAsync(IServiceProvider serviceProvider)
  {
    using (var appDbContext = new AppDbContext(
        serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>(), null))
    {
      await PopulateTestDataAsync(appDbContext, serviceProvider);
    }

    using (var userDbContext = new UserDbContext(
        serviceProvider.GetRequiredService<DbContextOptions<UserDbContext>>()))
    {
      await PopulateTestDataAsync(userDbContext, serviceProvider);
    }
  }
  public async static Task PopulateTestDataAsync(DbContext dbContext, IServiceProvider serviceProvider)
  {
    if(dbContext is AppDbContext appDb)
    {
      //seed the appDb with some data once you'll need them for testing purposes
      appDb.SaveChanges();
    } 
    else if(dbContext is UserDbContext userDb) 
    {
      ClearUserDb(userDb);
      await PrepareExampleUserRolesAsync(serviceProvider);
      await PrepareExampleUsersAsync(serviceProvider);
      await userDb.SaveChangesAsync();
    }
  }

  private static void ClearUserDb(UserDbContext userDb)
  {
    foreach (var user in userDb.Users)
    {
      userDb.Remove(user);
    }
    foreach (var userRole in userDb.UserRoles)
    {
      userDb.Remove(userRole);
    }
    foreach (var role in userDb.Roles)
    {
      userDb.Remove(role);
    }
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
}

