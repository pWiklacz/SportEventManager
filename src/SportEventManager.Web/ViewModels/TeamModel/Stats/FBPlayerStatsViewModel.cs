using SportEventManager.Core.StatisticsAggregate;

namespace SportEventManager.Web.ViewModels.TeamModel.Stats;
public class FbPlayerStatsViewModel : FootballStatsViewModel
{
  public int PlayerId { get; private set; }
  
  //NOTE: I am not sure what will now happen with this method 
  //and whether it should have a base or a derived class or not
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
