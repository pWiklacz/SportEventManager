using Ardalis.GuardClauses;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using SportEventManager.Core.EventAggregate;

namespace SportEventManager.Core.StatisticsAggregate;
 
public class FbTeamMatchStats : FootballStatsBase
{
  [DefaultValue(0)] public int Shoots { get; set; } = 0;

  [DefaultValue(0)] public int ShootsOnTarget { get; set; } = 0;

  [DefaultValue(0)] public int Fouls { get; set; } = 0;

  [DefaultValue(0)] public int Passes { get; set; } = 0;

  [DefaultValue(false)]
  public bool Win { get; set; } = false;

  [DefaultValue(false)]
  public bool Loss { get; set; } = false;

  [DefaultValue(false)]
  public bool Draw { get; set; } = false;

  [Required]
  public int TeamId { get; set; }

  public FbTeamMatchStats(int teamId)
  {
    TeamId = Guard.Against.NegativeOrZero(teamId, nameof(teamId));
  }

  public FbTeamMatchStats() : base() { }
}
