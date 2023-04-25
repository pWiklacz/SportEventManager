using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SportEventManager.Core.TeamAggregate.Stats;
public class FBPlayerStats : FootballStats
{
  [Required]
  [ForeignKey("Player")]
  public int PlayerId { get; private set; }

  public FBPlayerStats(int playerId) : base()
  {
    PlayerId = playerId;
  }
}
