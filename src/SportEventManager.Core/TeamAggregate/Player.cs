using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
  public int Number { get; set; }

  [Required]
  [DefaultValue(false)]
  public bool IsDeleted { get; private set; } = false;

  [Required]
  [ForeignKey("Team")]
  public int TeamId { get; private set; }

  public Player(string name, string surname, int number, int teamId)
  {
    Name = name;
    Surname = surname;
    Number = number;
    IsDeleted = false;
    TeamId = teamId;
  }

  public void MarkAsDeleted()
  {
    this.IsDeleted = true;
  }
}
