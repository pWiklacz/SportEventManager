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

  [DefaultValue(null)]
  [NotMapped]
  public Statistics? FbPlayerStats { get; set; }

  private List<Team2Player> _teams2Players = new List<Team2Player>();
  public IEnumerable<Team2Player> Teams2Players => _teams2Players.AsReadOnly();

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
