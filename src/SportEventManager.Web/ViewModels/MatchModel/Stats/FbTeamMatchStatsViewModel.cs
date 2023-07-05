using System.ComponentModel;
using SportEventManager.Core.MatchAggregate;

namespace SportEventManager.Web.ViewModels.MatchModel.Stats;

public class FbTeamMatchStatsViewModel : FootballStatsViewModel
{
  public int Shoots { get; set; } = 0;

  public int ShootsOnTarget { get; set; } = 0;

  public int Fouls { get; set; } = 0;

  public int Passes { get; set; } = 0;

  public bool Win { get; set; } = false;

  public bool Loss { get; set; } = false;

  public bool Draw { get; set; } = false;

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
        TeamId = fBTeamStats.TeamId,
        Win = fBTeamStats.Win,
        Draw = fBTeamStats.Draw,
        Loss = fBTeamStats.Loss
      };
    }
    return new FbTeamMatchStatsViewModel();
  }
}
