using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using SportEventManager.Core.TeamAggregate;

namespace SportEventManager.Core.StatisticsAggregate;
public class FbPlayerStats : FootballStatsBase
{
  [Required]
  [ForeignKey("Player")]
  public int PlayerId { get; private set; }

  [Required] 
  public Player Player { get; private set; } = null!;

  public FbPlayerStats(int playerId) : base()
  {
    PlayerId = Guard.Against.NegativeOrZero(playerId, nameof(playerId));
  }

  public FbPlayerStats() { }
}
