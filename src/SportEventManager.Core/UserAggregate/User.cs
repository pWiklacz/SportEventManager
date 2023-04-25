using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using SportEventManager.SharedKernel.Interfaces;

namespace SportEventManager.Core.UserAggregate;
public class User : IdentityUser, IAggregateRoot
{
  [PersonalData]
  public string? FirstName { get; set; }

  [PersonalData]
  public string? LastName { get; set; }
}
