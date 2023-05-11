using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SportEventManager.SharedKernel;
using Ardalis.GuardClauses;

namespace SportEventManager.Core.TeamAggregate;
public class Team2Player : EntityBase
{
  [Required]
  [ForeignKey("Team")]
  public int TeamId { get; private set; }

  [Required]
  [ForeignKey("Player")]
  public int PlayerId { get; private set; }

  [Required]
  public int Number { get; private set; }

  public Team? Team { get; private set; }

  public Player? Player { get; private set; }

  public Team2Player(int teamId, int playerId, int number, Team team, Player player)
  {
    TeamId = teamId;
    PlayerId = playerId;
    Guard.Against.NegativeOrZero(number, nameof(number));
    Number = number;
    Team = team;
    Player = player;
  }

  public Team2Player() { }
}
