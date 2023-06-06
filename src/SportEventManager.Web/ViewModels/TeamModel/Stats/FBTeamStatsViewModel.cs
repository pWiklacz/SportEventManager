using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using SportEventManager.Core.StatisticsAggregate;

namespace SportEventManager.Web.ViewModels.TeamModel.Stats;
public class FbTeamStatsViewModel : FootballStatsViewModel
{
  public int Wins { get; set; } = 0;

  public int Losses { get; set; } = 0;

  public int Draws { get; set; } = 0;

  public int Shoots { get; set; } = 0;

  public int ShootsOnTarget { get; set; } = 0;

  public int Fouls { get; set; } = 0;

  public int Passes { get; set; } = 0;

  public int TeamId { get; set; }

  public static FbTeamStatsViewModel FromTeamStats(FbTeamStats? fBTeamStats)
  {
    if (fBTeamStats != null)
    {
      return new FbTeamStatsViewModel()
      {
        Id = fBTeamStats.Id,
        Wins = fBTeamStats.Wins,
        Losses = fBTeamStats.Losses,
        Draws = fBTeamStats.Draws,
        Shoots = fBTeamStats.Shoots,
        ShootsOnTarget = fBTeamStats.ShootsOnTarget,
        Fouls = fBTeamStats.Fouls,
        Passes = fBTeamStats.Passes,
        Goals = fBTeamStats.Goals,
        Assists = fBTeamStats.Assists,
        RedCards = fBTeamStats.RedCards,
        YellowCards = fBTeamStats.YellowCards,
        TeamId = fBTeamStats.TeamId,
      };
    }
    else
    {
      return new FbTeamStatsViewModel();
    }
  }
}
