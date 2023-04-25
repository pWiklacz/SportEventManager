using SportEventManager.SharedKernel.Interfaces;
using SportEventManager.SharedKernel;
using SportEventManager.Core.TeamAggregate;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Ardalis.GuardClauses;

namespace SportEventManager.Core.EventAggregate;
public class Event : EntityBase, IAggregateRoot
{
  [Required]
  [MaxLength(100)]
  public String Name { get; set; } = string.Empty;

  public List<Stadium> _stadiums = new List<Stadium>();

  public List<Team> _teams = new List<Team>();

  public List<Match> _matches = new List<Match>();

  public IEnumerable<Stadium> stadiums => _stadiums.AsReadOnly();

  public IEnumerable<Team> Teams => _teams.AsReadOnly();

  public IEnumerable<Match> Matches => _matches.AsReadOnly();

  //You should make a constructor for usage maybe?? Not sure how complicated you need

  public DateTime StartTime { get; set; }

  [Required]
  [DefaultValue(false)]
  public bool IsDeleted { get; private set; } = false;

  public Event(string name, DateTime startTime) { 
    Name = Guard.Against.NullOrEmpty(name, nameof(name));
    StartTime = startTime;
    //_stadiums = new List<Stadium>(numOfStadium);
    //_teams = new List<Team>(numOfTeam);
  }
  public Event(int id, string name, DateTime startTime)
  {
    Name = Guard.Against.NullOrEmpty(name, nameof(name));
    StartTime = startTime;
    //_stadiums = new List<Stadium>(numOfStadium);
    //_teams = new List<Team>(numOfTeam);
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
    _matches.Add(newMatch);
  }

  public void MarkAsDeleted()
  {
    this.IsDeleted = true;
  }
}
