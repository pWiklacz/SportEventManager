using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace SportEventManager.Core.TeamAggregate.Stats;
public class FBTeamStats : FootballStats
{
  [DefaultValue(0)]
  public int Wins { get; set; } = 0;

  [DefaultValue(0)]
  public int Losses { get; set; } = 0;

  [DefaultValue(0)]
  public int Drawes { get; set; } = 0;

  [DefaultValue(0)]
  public int Shoots { get; set; } = 0;

  [DefaultValue(0)] 
  public int ShootsOnTarget { get; set; } = 0;

  [DefaultValue(0)] 
  public int Fouls { get; set; } = 0;

  [DefaultValue(0)]
  public int Passes { get; set; } = 0;

  [ForeignKey("Team")]
  [DefaultValue(null)]
  public int? TeamId { get; private set; } = null;

  [ForeignKey("Match")]
  [DefaultValue(null)]
  public int? MatchId { get; private set; } = null;

  public FBTeamStats(int id, bool isFromOneMatch = false) : base()
  {
    if(!isFromOneMatch) 
    { 
      TeamId = id;
    } else
    {
      MatchId = id;
    }
  }

  public FBTeamStats() : base() { }
}
