using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using SportEventManager.Core.EventAggregate;
using SportEventManager.Core.TeamAggregate;

namespace SportEventManager.Core.MatchAggregate;

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

  public void Update(FbPlayerMatchStats stats)
  {
    Guard.Against.Null(stats, nameof(stats));

    if (Id != stats.Id && PlayerId != stats.PlayerId)
    {
      throw new Exception("Try to update wrong entity of statistics.");
    }

    Goals = stats.Goals;
    Assists = stats.Assists;
    RedCards = stats.RedCards;
    YellowCards = stats.YellowCards;

    if (YellowCards == 2)
      RedCards = 1;
  }
}
