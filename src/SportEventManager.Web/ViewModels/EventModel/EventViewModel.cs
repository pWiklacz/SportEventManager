using SportEventManager.Core.TeamAggregate;

namespace SportEventManager.Web.ViewModels.EventModel;

public class EventViewModel
{
  public int ID { get; set;}

  public string Name { get; set;} = string.Empty;

  public List<Stadium> Stadiums { get; set;} = new List<Stadium>();

  public List<Team> Teams { get; set; } = new List<Team>();

  public List<Match> Matches { get; set; } = new List<Match>();

  public DateTime startTime { get; set; }
}
