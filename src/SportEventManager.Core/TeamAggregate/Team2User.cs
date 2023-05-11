using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SportEventManager.Core.UserAggregate;
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

  public User? User { get; private set; }

  public Team? Team { get; private set; }

  public Team2User(string userId, int teamId, User user, Team team)
  {
    OwnerId = userId;
    TeamId = teamId;
    User = user;
    Team = team;
  }
  public Team2User() { }
}
