using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ardalis.GuardClauses;
using SportEventManager.Core.EventAggregate;
using SportEventManager.Core.StatisticsAggregate;
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

  [DefaultValue(null)]
  [NotMapped]
  public Statistics? FbTeamWholeStats { get; set; }

  private List<Player> _players = new List<Player>();
  private List<Team2Player> _teams2Players = new List<Team2Player>();
  private List<Team2User> _teams2Users = new List<Team2User>();
  private List<Event2Team> _events2Teams = new List<Event2Team>();
  private List<User> _owners = new List<User>();

  public IEnumerable<Team2Player> Teams2Players => _teams2Players.AsReadOnly();

  public IEnumerable<Team2User> Teams2Users => _teams2Users.AsReadOnly();

  public IEnumerable<Event2Team> Events2Teams => _events2Teams.AsReadOnly();

  [NotMapped]
  public IEnumerable<User> Owners => _owners.AsReadOnly();

  [NotMapped]
  public IEnumerable<Player> Players => _players.AsReadOnly();


  public Team(string name, string city, int numberOfPlayers)
  {
    Name = Guard.Against.NullOrEmpty(name, nameof(name));
    City = Guard.Against.NullOrEmpty(city, nameof(city));
    NumberOfPlayers = Guard.Against.NegativeOrZero(numberOfPlayers, nameof(numberOfPlayers));
    _players = new List<Player>(numberOfPlayers);
    FbTeamWholeStats = null;
  }

  public Team() { }

  public void AddPlayer(Player newPlayer, int number = 1)
  {
    Guard.Against.Null(newPlayer, nameof(newPlayer));
    _players.Add(newPlayer);

    Team2Player player2Team = new Team2Player(this.Id, newPlayer.Id, number, this, newPlayer);
    Guard.Against.Null(player2Team, nameof(player2Team));
    _teams2Players.Add(player2Team);
  }

  public void AddOwner(User newUser)
  {
    //NOTE: Not sure if we should actually have these Owners table since User which become the owner already exists
    //So maybe we only need to create a team2user and pass the id as an argument not the whole user object
    //Guard.Against.Null(newUser, nameof(newUser));
    //_owners.Add(newUser);

    Team2User team2User = new Team2User(newUser.Id, this.Id, newUser, this);
    Guard.Against.Null(team2User, nameof(team2User));
    _teams2Users.Add(team2User);
  }

  public void Archive()
  {
    this.IsArchived = true;
  }
}
