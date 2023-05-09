using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;

namespace SportEventManager.Core.TeamAggregate.Stats;
public class FbPlayerStats : FootballStats
{
  [Required]
  [ForeignKey("Player")]
  public int PlayerId { get; private set; }

  public FbPlayerStats(int playerId) : base()
  {
    PlayerId = Guard.Against.NegativeOrZero(playerId, nameof(playerId));
  }

  public override void Archive()
  {
    base.Archive();
  }
}
