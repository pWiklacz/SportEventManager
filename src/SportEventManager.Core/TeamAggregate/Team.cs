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

  //navigation properties

  [DefaultValue(null)]
  [NotMapped]
  public Statistics? FbTeamWholeStats { get; set; }

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
    FbTeamWholeStats = null;
  }

  public Team() { }

  public void AddPlayer(Player newPlayer)
  {
    Guard.Against.Null(newPlayer, nameof(newPlayer));
    _players.Add(newPlayer);
  }

  public void UpdateTeamPlayer(int index, int num)
  {
    _teamPlayers[index].Number = num;
  }

  public void UpdatePlayer(int i, string name, string surname, string pesel)
  {
    _players[i].Name = name;
    _players[i].Surname = surname;
    _players[i].Pesel = pesel;
  }

  public void Archive()
  {
    this.IsArchived = true;
    foreach(var teamPlayer in _teamPlayers)
    {
      teamPlayer.LeaveOn = DateTime.Now;
    }
  }
}
