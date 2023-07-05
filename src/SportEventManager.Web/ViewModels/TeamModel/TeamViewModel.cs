using System.ComponentModel.DataAnnotations;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SportEventManager.Core.EventAggregate;
using SportEventManager.Core.TeamAggregate;
using SportEventManager.Web.ViewModels.EventModel;

namespace SportEventManager.Web.ViewModels.TeamModel;

public class TeamViewModel
{
    public int Id { get; set; }
    public string OwnerId { get; private set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public bool IsArchived { get; set; } = false;
    [Range(1, 50)]
    public int NumberOfPlayers { get; set; }
    public List<PlayerViewModel> Players { get; set; } = new();
    public List<TeamPlayerViewModel> TeamPlayers { get; set; } = new();
    public List<string>? ExistingPeselNumbers { get; set; }

    public static TeamViewModel FromTeam(Team team) => new()
    {
        Id = team.Id,
        Name = team.Name,
        City = team.City,
        NumberOfPlayers = team.NumberOfPlayers,
        OwnerId = team.OwnerId,
        IsArchived = team.IsArchived,
        Players = team.Players.Select(p => PlayerViewModel.FromPlayer(p)).ToList(),
        TeamPlayers = team.TeamPlayers.Select(tp => TeamPlayerViewModel.FromTeamPlayer(tp)).ToList()
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
}
