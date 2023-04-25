using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ardalis.GuardClauses;
using SportEventManager.Core.TeamAggregate.Stats;
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
  [ForeignKey("User")]
  [MaxLength(450)]
  public string OwnerId { get; private set; } = string.Empty;

  [Required]
  [DefaultValue(false)]
  public bool IsDeleted { get; private set; } = false;


  private List<Player> _players = new List<Player>();

  public IEnumerable<Player> Players => _players.AsReadOnly();


  [DefaultValue(null)]
  public FBTeamStats? FbTeamWholeStats { get; set; }

  public Team(string name, string city, int numberOfPlayers)
  {
    Name = Guard.Against.NullOrEmpty(name, nameof(name));
    City = Guard.Against.NullOrEmpty(city, nameof(city));
    NumberOfPlayers = Guard.Against.NegativeOrZero(numberOfPlayers, nameof(numberOfPlayers));
    _players = new List<Player>(numberOfPlayers);
  }

  public Team(int id, string name, string city, int numberOfPlayers)
  {
    Id = Guard.Against.NegativeOrZero(id, nameof(id));
    Name = Guard.Against.NullOrEmpty(name, nameof(name));
    City = Guard.Against.NullOrEmpty(city, nameof(city));
    NumberOfPlayers = Guard.Against.NegativeOrZero(numberOfPlayers, nameof(numberOfPlayers));
    _players = new List<Player>(numberOfPlayers);
  }

  public Team() { }

  public void AddPlayer(Player newPlayer)
  {
    Guard.Against.Null(newPlayer, nameof(newPlayer));
    _players.Add(newPlayer);

    //var newPlayerAddedEvent = new NewPlayerAddedEvent(this, newPlayer);
    //base.RegisterDomainEvent(newPlayerAddedEvent);
  }

  public void MarkAsDeleted()
  {
    this.IsDeleted = true;
  }
}
