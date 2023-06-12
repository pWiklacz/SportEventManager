using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ardalis.GuardClauses;
using SportEventManager.Core.EventAggregate;
using SportEventManager.Core.TeamAggregate;

namespace SportEventManager.Core.StatisticsAggregate;

public class FbPlayerMatchStats : FootballStatsBase
{
  [Required]
  [ForeignKey("Player")]
  public int PlayerId { get; set; }

  [Required] 
  public Player Player { get; set; } = null!;

  public FbPlayerMatchStats(int playerId) : base()
  {
    PlayerId = Guard.Against.NegativeOrZero(playerId, nameof(playerId));
  }
  public FbPlayerMatchStats() : base() { }
}
