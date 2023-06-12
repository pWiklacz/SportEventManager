using SportEventManager.SharedKernel.Interfaces;
using SportEventManager.SharedKernel;
using SportEventManager.Core.TeamAggregate;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Ardalis.GuardClauses;
using SportEventManager.Core.StatisticsAggregate;

namespace SportEventManager.Core.EventAggregate;
public class Event : EntityBase, IAggregateRoot
{
  [Required]
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

  public Event(string? ownerId, string name, DateTime startTime, DateTime endTime)
  {
    OwnerId = Guard.Against.NullOrEmpty(ownerId, nameof(ownerId));
    Name = Guard.Against.NullOrEmpty(name, nameof(name));
    StartTime = Guard.Against.Null(startTime, nameof(startTime));
    EndTime = Guard.Against.Null(endTime, nameof(endTime));
    IsInprogress = (startTime <= DateTime.Now);
    IsArchived = false;
  }

  public void UpdateMatchStats(int i, FbTeamMatchStats homeStats, FbTeamMatchStats guestStats)
  {
    _matches[i].EndMatch(homeStats, guestStats);
  }

  public void AddStadium(Stadium newStadium)
  {
    Guard.Against.Null(newStadium, nameof(newStadium));
    if(_stadiums.Contains(newStadium))
    {
      throw new Exception("The stadium " + newStadium.Name + " was chosen more than once.");
    }
    _stadiums.Add(newStadium);
  }

  public void AddTeam(Team newTeam)
  {
    Guard.Against.Null(newTeam, nameof(newTeam));
    if(_teams.Contains(newTeam)) {
      throw new Exception("The team " + newTeam.Name + " was chosen more than once.");
    }
    _teams.Add(newTeam);
  }

  public void AddMatch(Match newMatch)
  {
    Guard.Against.Null(newMatch, nameof(newMatch));
    _matches.Add(newMatch);
  }
  public void Archive()
  {
    this.IsArchived = true;
    this._teams.Clear();
  }

  public Event() { }
}
