using System.ComponentModel.DataAnnotations;

namespace SportEventManager.Web.ViewModels;

public class TeamViewModel
{
  public int Id { get; set; }

  public String Name { get; set; } = String.Empty;

  public String City { get; set; } = String.Empty;

  [Range(1, 50)]
  public int NumberOfPlayers { get; set; }

  public string OwnerId { get; private set; } = String.Empty;

  public bool IsDeleted { get; set; } = false;

  public List<PlayerViewModel> Players { get; set; } = new List<PlayerViewModel>();
}
