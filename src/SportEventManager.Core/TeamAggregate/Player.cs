using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SportEventManager.Core.TeamAggregate.Stats;
using SportEventManager.SharedKernel;

namespace SportEventManager.Core.TeamAggregate;

public class Player : EntityBase
{
  [Required]
  [MaxLength(100)]
  public String Name { get; set; } = String.Empty;

  [Required]
  [MaxLength(100)]
  public String Surname { get; set; } = String.Empty;

  [Required]
  [DefaultValue(false)]
  public bool IsArchived { get; private set; } = false;

  public FBPlayerStats? FbPlayerStats { get; set; }

  public Player(string name, string surname)
  {
    Name = name;
    Surname = surname;
    IsArchived = false;
  }

  public void Archive()
  {
    this.IsArchived = true;
  }
}
