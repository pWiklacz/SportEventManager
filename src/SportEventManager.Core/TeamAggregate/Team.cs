using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ardalis.GuardClauses;
using SportEventManager.Core.EventAggregate;
using SportEventManager.Core.TeamAggregate.Stats;
using SportEventManager.Core.UserAggregate;
using SportEventManager.SharedKernel;
using SportEventManager.SharedKernel.Interfaces;

namespace SportEventManager.Core.TeamAggregate;

public class Team : EntityBase, IAggregateRoot
{
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

  private List<Player> _players = new List<Player>();

  public IEnumerable<Player> Players => _players.AsReadOnly();

  private List<Player2Team> _playersToTeams = new List<Player2Team>();

  public IEnumerable<Player2Team> PlayersToTeams => _playersToTeams.AsReadOnly();

  public List<User> _owners = new List<User>();

  public IEnumerable<User> Owners => _owners.AsReadOnly();

  public List<Team2User> _teamsToUsers = new List<Team2User>();

  public IEnumerable<Team2User> TeamsToUsers => _teamsToUsers.AsReadOnly();

  [DefaultValue(null)]
  public FBTeamStats? FbTeamWholeStats { get; set; }

  public Team(string name, string city, int numberOfPlayers)
  {
    Name = Guard.Against.NullOrEmpty(name, nameof(name));
    City = Guard.Against.NullOrEmpty(city, nameof(city));
    NumberOfPlayers = Guard.Against.NegativeOrZero(numberOfPlayers, nameof(numberOfPlayers));
    _players = new List<Player>(numberOfPlayers);
    FbTeamWholeStats = null;
  }

  public Team() { }

  public void AddPlayer(Player newPlayer, int number)
  {
    Guard.Against.Null(newPlayer, nameof(newPlayer));
    _players.Add(newPlayer);

    Player2Team player2Team = new Player2Team(this.Id, newPlayer.Id, number);
    Guard.Against.Null(player2Team, nameof(player2Team));
    _playersToTeams.Add(player2Team);
  }

  public void AddOwner(User newUser)
  {
    //NOTE: Not sure if we should actually have these Owners table since User which become the owner already exists
    //So maybe we only need to create a team2user and pass the id as an argument not the whole user object
    Guard.Against.Null(newUser, nameof(newUser));
    _owners.Add(newUser);

    Team2User team2User = new Team2User(newUser.Id, this.Id);
    Guard.Against.Null(team2User, nameof(team2User));
    _teamsToUsers.Add(team2User);
  }

  public void Archive()
  {
    this.IsArchived = true;
  }
}
