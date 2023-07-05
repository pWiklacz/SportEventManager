using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using SportEventManager.Core.EventAggregate;
using SportEventManager.Core.MatchAggregate;
using SportEventManager.SharedKernel;

namespace SportEventManager.Core.EventAggregate;

public class Stadium : EntityBase
{
  [Key]
  [Required]
  [DatabaseGenerated(DatabaseGeneratedOption.None)]
  public new string Id { get; set; } = string.Empty;

  [Required]
  [MaxLength(50)]
  public String Name { get; set; } = string.Empty;

  [Required]
  [MaxLength(50)]
  public String City { get; set; } = string.Empty;

  [Required]
  [DefaultValue(false)]
  public bool IsArchived { get; private set; } = false;

  //navigation properties

  private List<Event> _events = new();
  private List<Match> _matches = new();
  public ICollection<Event> Events => _events.AsReadOnly();

  public ICollection<Match> Matches => _matches.AsReadOnly();

  public Stadium()
  {
  }

  public Stadium(string name, string city)
  {
    City = Guard.Against.NullOrEmpty(city, nameof(city));
    Name = Guard.Against.NullOrEmpty(name, nameof(name));
    Id = City + Name;
    IsArchived = false;
  }

  public void Archive()
  {
    this.IsArchived = true;
  }
}
