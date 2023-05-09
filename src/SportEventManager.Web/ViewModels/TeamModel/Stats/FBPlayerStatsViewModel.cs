using SportEventManager.Core.TeamAggregate.Stats;

namespace SportEventManager.Web.ViewModels.TeamModel.Stats;
public class FbPlayerStatsViewModel : FootballStatsViewModel
{
  public int PlayerId { get; private set; }

  public static FbPlayerStatsViewModel FromPlayerStats(FbPlayerStats? fBPlayerStats)
  {
    if (fBPlayerStats != null)
    {
      return new FbPlayerStatsViewModel()
      {
        Id = fBPlayerStats.Id,
        PlayerId = fBPlayerStats.PlayerId,
        Goals = fBPlayerStats.Goals,
        Assists = fBPlayerStats.Assists,
        RedCards = fBPlayerStats.RedCards,
        YellowCards = fBPlayerStats.YellowCards
      };
    }
    else
    {
      return new FbPlayerStatsViewModel();
    }
  }
}
