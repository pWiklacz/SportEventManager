using SportEventManager.Core.TeamAggregate;

namespace SportEventManager.Web.ViewModels.TeamModel;

public class TeamPlayerViewModel
{
  public int Id { get; set; }

  public int Number { get; set; } = 1;

  public int PlayerId { get; set; }

  public static TeamPlayerViewModel FromTeamPlayer(TeamPlayer teamPlayer)
  {
    return new TeamPlayerViewModel()
    {
      Id = teamPlayer.Id,
      Number = teamPlayer.Number,
      PlayerId = teamPlayer.PlayerId
    };
  }
  
}
