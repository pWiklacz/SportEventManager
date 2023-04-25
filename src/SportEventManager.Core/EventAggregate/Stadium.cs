using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ardalis.GuardClauses;
using SportEventManager.SharedKernel;

namespace SportEventManager.Core.EventAggregate;
public class Stadium : EntityBase
{
  [Required]
  [MaxLength(50)]
  public String City { get; set; } = string.Empty;

  [Required]
  [DefaultValue(false)]
  public bool IsDeleted { get; private set; } = false;


  public Stadium() { }

  public Stadium(string city)
  {
    City = Guard.Against.NullOrEmpty(city, nameof(city));
    IsDeleted = false;
  }

  public void MarkAsDeleted()
  {
    this.IsDeleted = true;
  }
}
