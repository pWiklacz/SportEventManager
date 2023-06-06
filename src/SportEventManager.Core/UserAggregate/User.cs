using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using SportEventManager.Core.EventAggregate;
using SportEventManager.SharedKernel.Interfaces;
using SportEventManager.Core.TeamAggregate;

namespace SportEventManager.Core.UserAggregate;
public class User : IdentityUser, IAggregateRoot
{
  [PersonalData]
  public string? FirstName { get; set; }

  [PersonalData]
  public string? LastName { get; set; }

  [Required]
  [DefaultValue(false)]
  public bool IsArchived { get; private set; } = false;

  public void Archive()
  {
    this.IsArchived = true;
  }
}
