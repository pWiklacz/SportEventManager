using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SportEventManager.SharedKernel;
using Ardalis.GuardClauses;
using SportEventManager.SharedKernel.Interfaces;

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
  public int Number { get; set; }

  [Required]
  public DateTime JoinOn { get; set; }

  public DateTime? LeaveOn { get; set; } = null;
  //public Team Team { get; set; } = null!;
  //public Player Player { get; set; } = null!;

  public TeamPlayer(int number)
  {
    //TeamId = teamId;
    //PlayerId = playerId;
    Guard.Against.NegativeOrZero(number, nameof(number));
    Number = number;
  }

  public TeamPlayer() { }
}
