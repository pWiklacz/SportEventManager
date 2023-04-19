using SportEventManager.Core.TeamAggregate.Stats;

namespace SportEventManager.Web.ViewModels.TeamModel.Stats;
public class FBPlayerStatsViewModel : FootballStatsViewModel
{
  public static FBPlayerStatsViewModel FromPlayerStats(FBPlayerStats? fBPlayerStats)
  {
    if (fBPlayerStats != null)
    {
      return new FBPlayerStatsViewModel()
      {
        Goals = fBPlayerStats.Goals,
        Assists = fBPlayerStats.Assists,
        RedCards = fBPlayerStats.RedCards,
        YellowCards = fBPlayerStats.YellowCards
      };
    }
    else
    {
      return new FBPlayerStatsViewModel();
    }
  }
}
