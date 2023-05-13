using SportEventManager.SharedKernel.Interfaces;
using SportEventManager.SharedKernel;
using SportEventManager.Core.TeamAggregate;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Ardalis.GuardClauses;
using SportEventManager.Core.UserAggregate;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportEventManager.Core.EventAggregate;
public class Event : EntityBase, IAggregateRoot
{
  [Required]
  [ForeignKey("User")]
  [MaxLength(450)]
  public string OwnerId { get; private set; } = string.Empty;

  [Required]
  [MaxLength(100)]
  public String Name { get; set; } = string.Empty;

  [Required]
  public DateTime StartTime { get; set; }

  [Required]
  public DateTime EndTime { get; set; }

  [Required]
  [DefaultValue(false)]
  public bool IsArchived { get; private set; } = false;

  [Required]
  [DefaultValue(false)]
  public bool IsInprogress { get; private set; } = false;

  //navigation properties

  private List<Stadium> _stadiums  = new();
  private List<Team> _teams = new();
  private List<Match> _matches = new();
  public ICollection<Match> Matches => _matches.AsReadOnly();
  public ICollection<Team> Teams => _teams.AsReadOnly();
  public ICollection<Stadium> Stadiums => _stadiums.AsReadOnly();

  public Event(string name, DateTime startTime)
  {
    Name = Guard.Against.NullOrEmpty(name, nameof(name));
    StartTime = Guard.Against.Null(startTime, nameof(startTime));
    if (startTime <= DateTime.Now)
    {
      IsInprogress = true;
    }
  }

  public void AddStadium(Stadium newStadium)
  {
    Guard.Against.Null(newStadium, nameof(newStadium));
    _stadiums.Add(newStadium);
  }

  public void AddTeam(Team newTeam)
  {
    Guard.Against.Null(newTeam, nameof(newTeam));
    _teams.Add(newTeam);
  }

  public void AddMatch(Match newMatch)
  {
    Guard.Against.Null(newMatch, nameof(newMatch));
    Matches.Add(newMatch);
  }
  public void Archive()
  {
    this.IsArchived = true;
  }
}
