using Ardalis.GuardClauses;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using SportEventManager.Core.EventAggregate;

namespace SportEventManager.Core.MatchAggregate;

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

  public void Update(FbTeamMatchStats stats, FbTeamMatchStats enemyStats)
  {
    Guard.Against.Null(stats, nameof(stats));

    if (Id != stats.Id && TeamId != stats.TeamId)
    {
      throw new Exception("Try to update wrong entity of statistics.");
    }

    Shoots = stats.Shoots;
    ShootsOnTarget = stats.ShootsOnTarget;
    Fouls = stats.Fouls;
    Passes = stats.Passes;
    Goals = stats.Goals;
    Assists = stats.Assists;
    RedCards = stats.RedCards;
    YellowCards = stats.YellowCards;

    if (Goals > enemyStats.Goals)
      Win = true;
    else if (Goals == enemyStats.Goals)
      Draw = true;
    else Loss = true;
  }
}
