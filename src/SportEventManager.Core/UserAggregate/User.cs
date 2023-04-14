using Microsoft.AspNetCore.Identity;
using SportEventManager.SharedKernel.Interfaces;

namespace SportEventManager.Core.UserAggregate;
public class User : IdentityUser, IAggregateRoot
{
  [PersonalData]
  public string? FirstName { get; set; }

  [PersonalData]
  public string? LastName { get; set; }

  //public User(string name, string surname)
  //{
  //  FirstName = Guard.Against.NullOrEmpty(name, nameof(name));
  //  LastName = Guard.Against.NullOrEmpty(surname, nameof(surname));
  //}
}
