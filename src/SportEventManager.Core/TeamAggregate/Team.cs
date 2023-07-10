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
  [MaxLength(3)]
  public string Tag { get; set; } = string.Empty;

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

  public Team(string ownerId, string name, string tag, string city, int numberOfPlayers, List<string>? existingTags)
  {
    OwnerId = Guard.Against.NullOrEmpty(ownerId, nameof(ownerId));
    Name = Guard.Against.NullOrEmpty(name, nameof(name));
    if (PropertyExistsInDb(tag, existingTags))
    {
      throw new Exception("The team with tag: " + tag + " already exists.");
    }
    Tag = Guard.Against.NullOrEmpty(tag, nameof(tag));
    City = Guard.Against.NullOrEmpty(city, nameof(city));
    NumberOfPlayers = Guard.Against.NegativeOrZero(numberOfPlayers, nameof(numberOfPlayers));
    _players = new List<Player>(numberOfPlayers);
  }

  public Team() { }

  public void AddPlayer(Player newPlayer, List<string>? existingPeselNumbers, bool peselIsValidatedAlready = false)
  {
    Guard.Against.Null(newPlayer, nameof(newPlayer));
    if (PropertyExistsInDb(newPlayer.Pesel, existingPeselNumbers) && !peselIsValidatedAlready)
    {
      throw new Exception("The player with pesel: " + newPlayer.Pesel + " is already in use!" +
        " Please inform your player he needs to be removed from his current team first!");
    }
    newPlayer.ReaddPlayer();
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
    _players[index].ReusePlayer(this.Id);
  }

  public void Archive()
  {
    this.IsArchived = true;
    foreach(var teamPlayer in _teamPlayers)
    {
      teamPlayer.LeaveOn = DateTime.Now;
    }
    foreach(var player in _players)
    {
      player.Archive();
    }
  }

  //searching for _players in viewModel.players - if we got to the last player from viewModel
  //and it's still not the one from _players then the latter was deleted by user and is "removed" from a team
  public void DeleteOldPlayers(List<Player> players)
  {
    for (int i = 0; i < _players.Count; i++)
      for (int j = 0; j < players.Count; j++)
        if (_players[i].Pesel != players[j].Pesel && j != players.Count - 1)
          continue;
        else if (_players[i].Pesel == players[j].Pesel)
          break;
        else if (j == players.Count - 1 && _players[i].Pesel != players[j].Pesel) { 
          _teamPlayers[i].LeaveOn = DateTime.Now;
          _players[i].Archive();
        }
  }

  public void UpdateTeam(string name, string tag, string city, int numberOfPlayers, List<string>? existingTags)
  {
    Name = Guard.Against.NullOrEmpty(name, nameof(name));
    if (PropertyExistsInDb(tag, existingTags) && tag != this.Tag)
    {
      throw new Exception("The team with tag: " + tag + " already exists.");
    }
    Tag = Guard.Against.NullOrEmpty(tag, nameof(tag));
    City = Guard.Against.NullOrEmpty(city, nameof(city));
    NumberOfPlayers = Guard.Against.NegativeOrZero(numberOfPlayers, nameof(numberOfPlayers));
  }

  public void UpsertPlayer(
    Player? player,
    string newName,
    string newSurname,
    string newPesel,
    List<string>? existingPeselNumbers,
    bool peselIsValidatedAlready = false
    )
  {
    
    if (player != null)
    {
      if (
        player.Pesel != newPesel &&
        PropertyExistsInDb(newPesel, existingPeselNumbers) && 
        !peselIsValidatedAlready
        )
      {
        throw new Exception("The player with pesel: " + newPesel + " is already in use!" +
          " Please inform your player he needs to be removed from his current team first!");
      }
      if (peselIsValidatedAlready)
      {
        this.AddPlayer(player, existingPeselNumbers, peselIsValidatedAlready);
      }
      this.UpdatePlayer(player.Id, newName, newSurname, newPesel);
    }
    else
    {
      this.AddPlayer(new Player(newName, newSurname, newPesel), existingPeselNumbers, peselIsValidatedAlready);
    }
  }

  private bool PropertyExistsInDb(string propertyToCheck, List<string>? existingPropertyValues)
  {
    return (existingPropertyValues != null && existingPropertyValues.Count != 0 && existingPropertyValues.Contains(propertyToCheck));
  }
}
