using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SportEventManager.Core.TeamAggregate;

namespace SportEventManager.Web.ViewModels.TeamModel;

public class PlayerViewModel
{
  public int Id { get; set; }

  [Required]
  [MaxLength(100)]
  public string Name { get; set; } = string.Empty;

  [Required]
  [MaxLength(100)]
  public string Surname { get; set; } = string.Empty;

  [Required]
  [DefaultValue(false)]
  public bool IsArchived { get; private set; }

  [Required]
  [MinLength(11)]
  [MaxLength(11)]
  public string Pesel { get; set; } = string.Empty;
  public static PlayerViewModel FromPlayer(Player player)
  {
    return new PlayerViewModel()
    {
      Id = player.Id,
      Name = player.Name,
      Surname = player.Surname,
      IsArchived = player.IsArchived,
      Pesel = player.Pesel
    };
  }
}
