using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SportEventManager.SharedKernel;

namespace SportEventManager.Core.TeamAggregate;
public class Team2User : EntityBase
{
  [Required]
  [ForeignKey("User")]
  [MaxLength(450)]
  public string OwnerId { get; private set; } = string.Empty;

  [Required]
  [ForeignKey("Team")]
  public int TeamId { get; private set; }

  public Team2User(string userId, int teamId)
  {
    OwnerId = userId;
    TeamId = teamId;
  }

  public Team2User() { }
}
