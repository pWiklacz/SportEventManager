using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using SportEventManager.SharedKernel;

namespace SportEventManager.Core.EventAggregate;
public class Stadium : EntityBase
{
  [Required]
  [MaxLength(50)]
  public String Name { get; set; } = string.Empty;

  [Required]
  [MaxLength(50)]
  public String City { get; set; } = string.Empty;

  [Required]
  [DefaultValue(false)]
  public bool IsArchived { get; private set; } = false;

  public Stadium() { }

  public Stadium(string name, string city)
  {
    City = Guard.Against.NullOrEmpty(city, nameof(city));
    Name = Guard.Against.NullOrEmpty(name, nameof(name));
    IsArchived = false;
  }

  public void Archive()
  {
    this.IsArchived = true;
  }
}
