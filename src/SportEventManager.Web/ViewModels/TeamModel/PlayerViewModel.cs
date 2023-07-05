using System.ComponentModel.DataAnnotations;
using SportEventManager.Core.TeamAggregate;

namespace SportEventManager.Web.ViewModels.TeamModel;

public class PlayerViewModel
{
  public int Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public string Surname { get; set; } = string.Empty;
  public bool IsArchived { get; private set; }
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
