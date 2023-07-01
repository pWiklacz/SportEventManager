using System.ComponentModel.DataAnnotations;
using SportEventManager.Core.TeamAggregate;

namespace SportEventManager.Web.ViewModels.TeamModel;

public class TeamViewModel
{
  public int Id { get; set; }
  public string OwnerId { get; private set; } = string.Empty;
  public string Name { get; set; } = string.Empty;

  [MaxLength(3)]
  public string Tag { get; set; } = string.Empty;
  public string City { get; set; } = string.Empty;
  public bool IsArchived { get; set; } = false;
  [Range(1, 50)]
  public int NumberOfPlayers { get; set; } = 1;
  public List<PlayerViewModel> Players { get; set; } = new();
  public List<TeamPlayerViewModel> TeamPlayers { get; set; } = new();
  public List<string>? ExistingPeselNumbers { get; set; }
  public string BackendError { get; set; } = "";

  public static TeamViewModel FromTeam(Team team) => new()
  {
    Id = team.Id,
    Name = team.Name,
    Tag = team.Tag,
    City = team.City,
    NumberOfPlayers = team.NumberOfPlayers,
    OwnerId = team.OwnerId,
    IsArchived = team.IsArchived,
    Players = team.Players.Select(p => PlayerViewModel.FromPlayer(p)).ToList(),
    TeamPlayers = team.TeamPlayers.Select(tp => TeamPlayerViewModel.FromTeamPlayer(tp)).ToList(),
    BackendError = ""
  };

  public List<Player> getPlayersList()
  {
    var list = new List<Player>();
    foreach (var player in Players)
    {
      list.Add(new Player(player.Name, player.Surname, player.Pesel));
    }
    return list;
  }

  public TeamViewModel(string error)
  {
    BackendError = error;
  }

  public TeamViewModel() { }
}
