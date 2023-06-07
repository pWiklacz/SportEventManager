using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using SportEventManager.Core.EventAggregate;

namespace SportEventManager.Core.StatisticsAggregate;

public class FbPlayerMatchStats : FootballStatsBase
{
  [Required]
  public int PlayerId { get; set; }

  public FbPlayerMatchStats(int playerId) : base()
  {
    PlayerId = Guard.Against.NegativeOrZero(playerId, nameof(playerId));
  }
  public FbPlayerMatchStats() : base() { }
}
