using SportEventManager.Core.MatchAggregate;
using SportEventManager.Core.TeamAggregate;
using SportEventManager.Web.ViewModels.TeamModel;

namespace SportEventManager.Web.ViewModels.MatchModel.Stats;

public class FbPlayerMatchStatsViewModel : FootballStatsViewModel
{
  public int PlayerId { get; set; }

  public PlayerViewModel Player { get; set; } = null!;

  public static FbPlayerMatchStatsViewModel FromPlayerMatchStats(FbPlayerMatchStats? playerMatchStats)
  {
    if (playerMatchStats != null)
    {
      return new FbPlayerMatchStatsViewModel()
      {
        Id = playerMatchStats.Id,
        PlayerId = playerMatchStats.PlayerId,
        Assists = playerMatchStats.Assists,
        Goals = playerMatchStats.Goals,
        RedCards = playerMatchStats.RedCards,
        YellowCards = playerMatchStats.YellowCards,
        Player = PlayerViewModel.FromPlayer(playerMatchStats.Player)
      };
    }
    return new FbPlayerMatchStatsViewModel();
  }
}
