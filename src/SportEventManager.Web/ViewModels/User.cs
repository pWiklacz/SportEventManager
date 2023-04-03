using Microsoft.AspNetCore.Identity;

namespace SportEventManager.Web.ViewModels;

// Add profile data for application users by adding properties to the User class
public class User : IdentityUser
{
  [PersonalData]
  public string? FirstName { get; set; }

  [PersonalData]
  public string? LastName { get; set; }
}

