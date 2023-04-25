using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SportEventManager.Core.TeamAggregate.Stats;
public class FBTeamStats : FootballStats
{
  [DefaultValue(0)]
  public int Wins { get; set; }

  [DefaultValue(0)]
  public int Losses { get; set; }

  [DefaultValue(0)]
  public int Drawes { get; set; }

  [DefaultValue(0)]
  public int Shoots { get; set; }

  [DefaultValue(0)]
  public int ShootsOnTarget { get; set; }

  [DefaultValue(0)]
  public int Fouls { get; set; }

  [DefaultValue(0)]
  public int Passes { get; set; }

  [Required]
  [ForeignKey("Team")]
  public int TeamId { get; private set; }

  public FBTeamStats(int teamId) : base()
  {
    Wins = 0;
    Losses = 0;
    Drawes = 0;
    Shoots = 0;
    ShootsOnTarget = 0;
    Fouls = 0;
    Passes = 0;
    TeamId = teamId;
  }

  
}
