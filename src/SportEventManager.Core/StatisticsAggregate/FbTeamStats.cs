using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using SportEventManager.Core.TeamAggregate;

namespace SportEventManager.Core.StatisticsAggregate;
public class FbTeamStats : FootballStatsBase
{
  [DefaultValue(0)]
  public int Wins { get; set; } = 0;

  [DefaultValue(0)]
  public int Losses { get; set; } = 0;

  [DefaultValue(0)]
  public int Draws { get; set; } = 0;

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

  [Required]
  public Team Team { get; private set; } = null!;

  public FbTeamStats(int teamId) : base()
  {
    TeamId = Guard.Against.NegativeOrZero(teamId, nameof(teamId));
  }

  public FbTeamStats() : base() { }
}
