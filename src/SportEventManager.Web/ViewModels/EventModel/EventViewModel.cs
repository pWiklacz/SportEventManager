using System.ComponentModel;
using SportEventManager.Core.EventAggregate;
using SportEventManager.Web.ViewModels.MatchModel;
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

  [DisplayName("Is in progress")]
  public bool IsInProgress { get; set; } = false;

  public string BackendError { get; set; } = "";

  [DisplayName("Has ended")]
  public bool IsEnded { get; private set; } = false;

  [DisplayName("Minimal quantity of players per team")]
  public int MinPlayersQuantityPerTeam { get; set; } = 0;

  [DisplayName("Base match duration [min]")]
  public int MatchDurationMinutes { get; set; } = 0;

  public static EventViewModel FromEvent(Event @event)
  {
    return new EventViewModel
    {
      Id = @event.Id,
      OwnerId = @event.OwnerId,
      Name = @event.Name,
      IsArchived = @event.IsArchived,
      IsInProgress = @event.IsInprogress,
      StartTime = @event.StartTime,
      EndTime = @event.EndTime,
      IsEnded = @event.IsEnded,
      MinPlayersQuantityPerTeam = @event.MinPlayersQuantityPerTeam,
      MatchDurationMinutes = @event.MatchDurationMinutes,
      Matches = @event.Matches.Select(m => MatchViewModel.FromMatch(m)).ToList(),
      Stadiums = @event.Stadiums.Select(s => StadiumViewModel.FromStadium(s)).ToList(),
      Teams = @event.Teams.Select(t => TeamViewModel.FromTeam(t)).ToList(),
      BackendError = ""
    };
  }

  public EventViewModel(string error = "")
  {
    Stadiums.Add(new StadiumViewModel() { Id = "" });
    Matches.Add(new MatchViewModel() { Id = 1 });
    ChosenTeamsNames.Add("default");
    BackendError = error;
  }

  public EventViewModel() { }
} 
