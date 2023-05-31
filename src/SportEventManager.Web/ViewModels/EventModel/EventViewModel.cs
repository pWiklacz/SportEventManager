﻿using System.ComponentModel;
using SportEventManager.Core.EventAggregate;
using SportEventManager.Web.ViewModels.TeamModel;

namespace SportEventManager.Web.ViewModels.EventModel;

public class EventViewModel
{
  public int Id { get; set;}
  public string OwnerId { get; private set; } = string.Empty;
  public string Name { get; set;} = string.Empty;

  public List<MatchViewModel> Matches { get; set; } = new List<MatchViewModel>();
  public List<StadiumViewModel> Stadiums { get; set;} = new List<StadiumViewModel>();
  public List<TeamViewModel> Teams { get; set; } = new List<TeamViewModel>();
  public List<string> AvailableTeamsNames { get; set; } = new List<string>();
  public List<string> ChosenTeamsNames { get; set; } = new List<string>();

  [DisplayName("Start Time")]
  public DateTime StartTime { get; set; } = DateTime.Now;

  [DisplayName("End Time")]
  public DateTime EndTime { get; set; } = DateTime.Now;
  public bool IsArchived { get; set; } = false;

  public bool IsInprogress { get; set; } = false;

  public string BackendError { get; set; } = "";

  public static EventViewModel FromEvent(Event @event)
  {
    return new EventViewModel
    {
      Id = @event.Id,
      OwnerId = @event.OwnerId,
      Name = @event.Name,
      IsArchived = @event.IsArchived,
      IsInprogress = @event.IsInprogress,
      StartTime = @event.StartTime,
      EndTime = @event.EndTime,
      Matches = @event.Matches.Select(m => MatchViewModel.FromMatch(m)).ToList(),
      Stadiums = @event.Stadiums.Select(s => StadiumViewModel.FromStadium(s)).ToList(),
      Teams = @event.Teams.Select(t => TeamViewModel.FromTeam(t)).ToList(),
      BackendError = ""
    };
  }

  public EventViewModel(string error = "")
  {
    Stadiums.Add(new StadiumViewModel() { Id = 1 });
    Matches.Add(new MatchViewModel() { Id = 1 });
    ChosenTeamsNames.Add("default");
    BackendError = error;
  }

  public EventViewModel() { }
} 
