using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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

  public Statistics? FbPlayerStats { get; set; }

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
