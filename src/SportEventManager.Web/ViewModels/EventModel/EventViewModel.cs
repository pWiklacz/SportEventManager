using SportEventManager.Core.TeamAggregate;
using SportEventManager.Web.ViewModels.TeamModel;

namespace SportEventManager.Web.ViewModels.EventModel;

public class EventViewModel
{
  public int Id { get; set;}

  public string Name { get; set;} = string.Empty;

  public List<StadiumViewModel> Stadiums { get; set;} = new List<StadiumViewModel>();

  public List<TeamViewModel> Teams { get; set; } = new List<TeamViewModel>();

  public List<MatchViewModel> Matches { get; set; } = new List<MatchViewModel>();

  public DateTime startTime { get; set; }
} 
