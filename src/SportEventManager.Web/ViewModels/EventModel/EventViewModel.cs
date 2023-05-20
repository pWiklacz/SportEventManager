using System.ComponentModel;
using SportEventManager.Web.ViewModels.TeamModel;

namespace SportEventManager.Web.ViewModels.EventModel;

public class EventViewModel
{
  public int Id { get; set;}
  public string Name { get; set;} = string.Empty;
  public List<StadiumViewModel> Stadiums { get; set;} = new List<StadiumViewModel>();
  public List<TeamViewModel> Teams { get; set; } = new List<TeamViewModel>();
  public List<string> TeamsName { get; set; } = new();
  public List<string> SelectTeamsName { get; set; } = new();
  public List<MatchViewModel> Matches { get; set; } = new List<MatchViewModel>();

  [DisplayName("Start Time")]
  public DateTime StartTime { get; set; }

  [DisplayName("End Time")]
  public DateTime EndTime { get; set; }
  public bool IsArchived { get; set; } = false;
} 
