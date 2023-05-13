using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ardalis.GuardClauses;
using SportEventManager.Core.StatisticsAggregate;
using SportEventManager.SharedKernel;

namespace SportEventManager.Core.TeamAggregate;

public class Player : EntityBase
{
  [Required]
  [MaxLength(100)]
  public String Name { get; set; }

  [Required]
  [MaxLength(100)]
  public String Surname { get; set; }

  [Required]
  [DefaultValue(false)]
  public bool IsArchived { get; private set; } = false;

  //navigation properties

  public FbPlayerStats? FbPlayerStats { get; set; }

  private List<Team> _teams = new();
  private List<TeamPlayer> _teamPlayers = new();
  public ICollection<Team> Teams => _teams.AsReadOnly();
  public ICollection<TeamPlayer> TeamPlayers => _teamPlayers.AsReadOnly();

  public Player(string name, string surname)
  {
    Name = Guard.Against.NullOrEmpty(name, nameof(name));
    Surname = Guard.Against.NullOrEmpty(surname, nameof(surname));
    IsArchived = false;
  }

  public void Archive()
  {
    this.IsArchived = true;
  }
}
