using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ardalis.GuardClauses;
using SportEventManager.Core.EventAggregate;
using SportEventManager.Core.MatchAggregate;
using SportEventManager.SharedKernel;
using SportEventManager.SharedKernel.Interfaces;

namespace SportEventManager.Core.TeamAggregate;

public class Team : EntityBase, IAggregateRoot
{
  [Required]
  [MaxLength(450)]
  public string OwnerId { get; private set; } = string.Empty;

  [Required]
  [MaxLength(100)]
  public String Name { get; set; } = string.Empty;

  [Required]
  [MaxLength(100)]
  public String City { get; set; } = string.Empty;

  [Required]
  public int NumberOfPlayers { get; set; } = 0;

  [Required]
  [DefaultValue(false)]
  public bool IsArchived { get; private set; } = false;

  private List<Player> _players = new();
  private List<Event> _events = new();
  private List<Match> _homeMatches = new();
  private List<Match> _awayMatches = new();
  private List<TeamPlayer> _teamPlayers = new();

  [InverseProperty(nameof(Match.HomeTeam))] 
  public ICollection<Match> HomeMatches => _homeMatches.AsReadOnly();

  [InverseProperty(nameof(Match.GuestTeam))]
  public ICollection<Match> AwayMatches => _awayMatches.AsReadOnly();
  public ICollection<Event> Events => _events.AsReadOnly();
  public ICollection<Player> Players => _players.AsReadOnly();
  public ICollection<TeamPlayer> TeamPlayers => _teamPlayers.AsReadOnly();

  public Team(string ownerId, string name, string city, int numberOfPlayers)
  {
    OwnerId = Guard.Against.NullOrEmpty(ownerId, nameof(ownerId));
    Name = Guard.Against.NullOrEmpty(name, nameof(name));
    City = Guard.Against.NullOrEmpty(city, nameof(city));
    NumberOfPlayers = Guard.Against.NegativeOrZero(numberOfPlayers, nameof(numberOfPlayers));
    _players = new List<Player>(numberOfPlayers);
  }

  public Team() { }

  public void AddPlayer(Player newPlayer, List<string>? existingPeselsNumbers)
  {
    Guard.Against.Null(newPlayer, nameof(newPlayer));
    if (existingPeselsNumbers != null) {
      if (existingPeselsNumbers.Contains(newPlayer.Pesel))
      {
        throw new Exception("The number pesel: " + newPlayer.Pesel + " is already exist.");
      }
    }
    _players.Add(newPlayer);
  }

  public void UpdateTeamPlayer(int index, int num)
  {
    _teamPlayers[index].Number = num;
  }

  private void UpdatePlayer(int id, string name, string surname, string pesel)
  {
    int index = _players.FindIndex(p => p.Id == id);
    _players[index].Name = Guard.Against.NullOrEmpty(name, nameof(name));
    _players[index].Surname = Guard.Against.NullOrEmpty(surname, nameof(surname));
    _players[index].Pesel = Guard.Against.NullOrEmpty(pesel, nameof(pesel));
  }

  public void Archive()
  {
    this.IsArchived = true;
    foreach(var teamPlayer in _teamPlayers)
    {
      teamPlayer.LeaveOn = DateTime.Now;
    }
  }

  public void DeletOldPlayers(List<Player> players)
  {
    foreach(var player in _players) {
      if(!players.Contains(player)) 
      {
        _players.Remove(player);
      }
    }
  }

  public void UpdateTeam(string name, string city, int numberOfPlayers)
  {
    Name = Guard.Against.NullOrEmpty(name, nameof(name));
    City = Guard.Against.NullOrEmpty(city, nameof(city));
    NumberOfPlayers = Guard.Against.NegativeOrZero(numberOfPlayers, nameof(numberOfPlayers));
  }

  public void UpsertPlayer(Player? player, string newName, string newSurname, string newPesel, List<string>? existingPeselNumber)
  {
    if (player != null)
    {
      this.UpdatePlayer(player.Id, newName, newSurname, newPesel);
    }
    else
    {
      this.AddPlayer(new Player(newName, newSurname, newPesel), existingPeselNumber);
    }
  }
}
