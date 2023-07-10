using SportEventManager.Core.MatchAggregate;
using SportEventManager.Core.TeamAggregate;

namespace SportEventManager.Web.ViewModels.MatchModel.Stats;

public class FbTeamFullStatsViewModel : FootballStatsViewModel
{
  public int Shoots { get; set; } = 0;

  public int ShootsOnTarget { get; set; } = 0;

  public int Fouls { get; set; } = 0;

  public int Passes { get; set; } = 0;

  public int Wins { get; set; } = 0;

  public int Losses { get; set; } = 0;

  public int Draws { get; set; } = 0;

  public int TeamId { get; set; }

  public string TeamName { get; set; } = string.Empty;

  public string TeamTag { get; set; } = string.Empty;

  public static FbTeamFullStatsViewModel FromTeamAndMatches(Team? team, List<Match>? matches)
  {
    if (team != null && matches != null && matches.Count > 0)
    {
      var stats = new FbTeamFullStatsViewModel(team.Id, team.Name, team.Tag);
      foreach(var match in matches)
      {
        if(match.HomeTeamId == team.Id)
        {
          stats.Goals += match.HomeTeamStats.Goals;
          stats.Assists += match.HomeTeamStats.Assists;
          stats.RedCards += match.HomeTeamStats.RedCards;
          stats.YellowCards += match.HomeTeamStats.YellowCards;
          stats.Shoots += match.HomeTeamStats.Shoots;
          stats.ShootsOnTarget += match.HomeTeamStats.ShootsOnTarget;
          stats.Fouls += match.HomeTeamStats.Fouls;
          stats.Passes += match.HomeTeamStats.Passes;
          stats.Wins += match.HomeTeamStats.Win ? 1 : 0;
          stats.Draws += match.HomeTeamStats.Draw ? 1 : 0;
          stats.Losses += match.HomeTeamStats.Loss ? 1 : 0;
        } else if(match.GuestTeamId == team.Id)
        {
          stats.Goals += match.GuestTeamStats.Goals;
          stats.Assists += match.GuestTeamStats.Assists;
          stats.RedCards += match.GuestTeamStats.RedCards;
          stats.YellowCards += match.GuestTeamStats.YellowCards;
          stats.Shoots += match.GuestTeamStats.Shoots;
          stats.ShootsOnTarget += match.GuestTeamStats.ShootsOnTarget;
          stats.Fouls += match.GuestTeamStats.Fouls;
          stats.Passes += match.GuestTeamStats.Passes;
          stats.Wins += match.GuestTeamStats.Win ? 1 : 0;
          stats.Draws += match.GuestTeamStats.Draw ? 1 : 0;
          stats.Losses += match.GuestTeamStats.Loss ? 1 : 0;
        }
        if(stats.Losses > 0)
        {
          break;  //no need to iterate more if the team lost until we'll have league also
        }
      }
      return stats;
    }
    return new FbTeamFullStatsViewModel();
  }

  public FbTeamFullStatsViewModel(int teamId, string teamName, string teamTag)
  {
    this.TeamId = teamId;
    this.TeamName = teamName;
    this.TeamTag = teamTag;
  }

  public FbTeamFullStatsViewModel(): base() { }
}
