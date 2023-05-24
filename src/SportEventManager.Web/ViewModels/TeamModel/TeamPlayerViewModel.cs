using SportEventManager.Core.TeamAggregate;

namespace SportEventManager.Web.ViewModels.TeamModel;

public class TeamPlayerViewModel
{
  public int Number { get; set; }

  public static TeamPlayerViewModel FromTeamPlayer(TeamPlayer teamPlayer)
  {
    return new TeamPlayerViewModel()
    {
      Number = teamPlayer.Number
    };
  }
  
}
