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

  public List<Match> _matches = new List<Match>();

  public IEnumerable<Match> Matches => _matches.AsReadOnly();

  public Stadium() { }

  public Stadium(string city)
  {
    City = Guard.Against.NullOrEmpty(city, nameof(city));
    IsDeleted = false;
    _matches = new List<Match>();
  }

  public void AddMatch(Match newMatch)
  {
    Guard.Against.Null(newMatch, nameof(newMatch));
    _matches.Add(newMatch);
  }

  public void MarkAsDeleted()
  {
    this.IsDeleted = true;
  }
}
