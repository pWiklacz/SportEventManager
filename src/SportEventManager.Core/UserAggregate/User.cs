﻿using System.ComponentModel.DataAnnotations;
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

  public UserRoleEnum? AccountType { get; private set; }

  [Required]
  [DefaultValue(false)]
  public bool IsArchived { get; private set; } = false;

  private List<Team2User> _teams2Users = new List<Team2User>();
  private List<Event> _events = new List<Event>(); 
  public IEnumerable<Team2User> Teams2Users => _teams2Users.AsReadOnly();
  public IEnumerable<Event> Events => _events.AsReadOnly();
  public void Archive()
  {
    this.IsArchived = true;
  }
}
