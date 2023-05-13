using SportEventManager.Core.StatisticsAggregate;

namespace SportEventManager.Web.ViewModels.TeamModel.Stats;

public class FbTeamMatchStatsViewModel : FootballStatsViewModel
{
  public int Shoots { get; set; } = 0;

  public int ShootsOnTarget { get; set; } = 0;

  public int Fouls { get; set; } = 0;

  public int Passes { get; set; } = 0; 

  public int TeamId { get; set; }

  public static FbTeamMatchStatsViewModel FromTeamMatchStats(FbTeamMatchStats? fBTeamStats)
  {
    if (fBTeamStats != null)
    {
      return new FbTeamMatchStatsViewModel()
      {
        Id = fBTeamStats.Id,
        Shoots = fBTeamStats.Shoots,
        ShootsOnTarget = fBTeamStats.ShootsOnTarget,
        Fouls = fBTeamStats.Fouls,
        Passes = fBTeamStats.Passes,
        Goals = fBTeamStats.Goals,
        Assists = fBTeamStats.Assists,
        RedCards = fBTeamStats.RedCards,
        YellowCards = fBTeamStats.YellowCards,
      };
    }
    else
    {
      return new FbTeamMatchStatsViewModel();
    }
  }
}
