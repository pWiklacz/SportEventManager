using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SportEventManager.SharedKernel;
using Ardalis.GuardClauses;

namespace SportEventManager.Core.TeamAggregate;
public class TeamPlayer : EntityBase
{
  [Required]
  [ForeignKey("Team")]
  public int TeamId { get; private set; }

  [Required]
  [ForeignKey("Player")]
  public int PlayerId { get; private set; }

  [Required]
  public int Number { get; private set; }

  public TeamPlayer(int teamId, int playerId, int number)
  {
    TeamId = teamId;
    PlayerId = playerId;
    Guard.Against.NegativeOrZero(number, nameof(number));
    Number = number;
  }

  public TeamPlayer() { }
}
