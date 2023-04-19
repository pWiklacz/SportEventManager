using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using SportEventManager.Core.TeamAggregate.Stats;

namespace SportEventManager.Web.ViewModels.TeamModel.Stats;
public class FBTeamStatsViewModel : FootballStatsViewModel
{
  public int Wins { get; set; } = 0;

  public int Losses { get; set; } = 0;

  public int Drawes { get; set; } = 0;

  public int Shoots { get; set; } = 0;

  public int ShootsOnTarget { get; set; } = 0;

  public int Fouls { get; set; } = 0;

  public int Passes { get; set; } = 0;

  public static FBTeamStatsViewModel FromTeamStats(FBTeamStats? fBTeamStats)
  {
    if (fBTeamStats != null)
    {
      return new FBTeamStatsViewModel()
      {
        Wins = fBTeamStats.Wins,
        Losses = fBTeamStats.Losses,
        Drawes = fBTeamStats.Drawes,
        Shoots = fBTeamStats.Shoots,
        ShootsOnTarget = fBTeamStats.ShootsOnTarget,
        Fouls = fBTeamStats.Fouls,
        Passes = fBTeamStats.Passes,
        Goals = fBTeamStats.Goals,
        Assists = fBTeamStats.Assists,
        RedCards = fBTeamStats.RedCards,
        YellowCards = fBTeamStats.YellowCards
      };
    }
    else
    {
      return new FBTeamStatsViewModel();
    }
  }
}
