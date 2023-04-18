using System.ComponentModel.DataAnnotations;
using SportEventManager.Core.TeamAggregate;


namespace SportEventManager.Web.ViewModels;

public class PlayerViewModel
{
  public int Id { get; set; }
  public String Name { get; set; } = String.Empty;

  public String Surname { get; set; } = String.Empty;

  [Range(1, 99)]
  public int Number { get; set; }

  public bool IsDeleted { get; private set; }

  public static PlayerViewModel FromPlayer(Player player)
  {
    return new PlayerViewModel()
    {
      Id = player.Id,
      Name = player.Name,
      Surname = player.Surname,
      Number = player.Number,
      IsDeleted = player.IsDeleted
    };
  }
}
