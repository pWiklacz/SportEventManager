using SportEventManager.SharedKernel.Interfaces;
using SportEventManager.SharedKernel;
using SportEventManager.Core.TeamAggregate;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Ardalis.GuardClauses;
using SportEventManager.Core.UserAggregate;

namespace SportEventManager.Core.EventAggregate;
public class Event : EntityBase, IAggregateRoot
{
  [Required]
  [MaxLength(100)]
  public String Name { get; set; } = string.Empty;
  public List<Stadium> _stadiums = new List<Stadium>();
  public List<Stadium2Event> _stadiums2Events = new List<Stadium2Event>();
  public List<Team> _teams = new List<Team>();
  public List<Match> _matches = new List<Match>();
  public List<User> _owners = new List<User>();
  public List<Event2User> _events2Users = new List<Event2User>();
  public IEnumerable<Stadium> Stadiums => _stadiums.AsReadOnly();
  public IEnumerable<Stadium2Event> Stadiums2Events => _stadiums2Events.AsReadOnly();
  public IEnumerable<Team> Teams => _teams.AsReadOnly();
  public IEnumerable<Match> Matches => _matches.AsReadOnly();
  public IEnumerable<User> Owners => _owners.AsReadOnly();
  public IEnumerable<Event2User> Events2Users => _events2Users.AsReadOnly();

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

    Stadium2Event stadium2Event = new Stadium2Event(this.Id, newStadium.Id);
    Guard.Against.Null(stadium2Event, nameof(stadium2Event));
    _stadiums2Events.Add(stadium2Event);
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

  public void AddOwner(User newUser)
  {
    //NOTE: Not sure if we should actually have these Owners table since User which become the owner already exists
    //So maybe we only need to create a team2user and pass the id as an argument not the whole user object
    Guard.Against.Null(newUser, nameof(newUser));
    _owners.Add(newUser);

    Event2User event2User = new Event2User(newUser.Id, this.Id);
    Guard.Against.Null(event2User, nameof(event2User));
    _events2Users.Add(event2User);
  }

  public void Archive()
  {
    this.IsArchived = true;
  }
}
