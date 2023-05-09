using Ardalis.GuardClauses;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SportEventManager.Core.TeamAggregate.Stats;

public class FbTeamMatchStats : FootballStats
{
  [DefaultValue(0)]
  public int Shoots { get; set; } = 0;

  [DefaultValue(0)]
  public int ShootsOnTarget { get; set; } = 0;

  [DefaultValue(0)]
  public int Fouls { get; set; } = 0;

  [DefaultValue(0)]
  public int Passes { get; set; } = 0;

  [Required]
  [ForeignKey("Team")]
  public int TeamId { get; private set; }

  public FbTeamMatchStats(int teamId) : base()
  {
    TeamId = Guard.Against.NegativeOrZero(teamId, nameof(teamId));
  }

  public FbTeamMatchStats() : base() { }
}
