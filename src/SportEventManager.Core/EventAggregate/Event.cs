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

  private List<Stadium> _stadiums = new List<Stadium>();
  private List<Event2Stadium> _stadiums2Events = new List<Event2Stadium>();
  private List<Team> _teams = new List<Team>();
  private List<Match> _matches = new List<Match>();
  //private List<Event2User> _events2Users = new List<Event2User>();
  private List<Event2Team> _events2Teams = new List<Event2Team>();

  public IEnumerable<Event2Stadium> Events2Stadiums => _stadiums2Events.AsReadOnly();
  //public IEnumerable<Event2User> Events2Users => _events2Users.AsReadOnly();
  public IEnumerable<Match> Matches => _matches.AsReadOnly();
  public IEnumerable<Event2Team> Events2Teams => _events2Teams.AsReadOnly();
  [NotMapped]
  public IEnumerable<Team> Teams => _teams.AsReadOnly();

  [NotMapped]
  public IEnumerable<Stadium> Stadiums => _stadiums.AsReadOnly();

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

    Event2Stadium event2Stadium = new Event2Stadium(this.Id, newStadium.Id, this, newStadium);
    Guard.Against.Null(event2Stadium, nameof(event2Stadium));
    _stadiums2Events.Add(event2Stadium);
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
  //public void AddOwner(User newUser)
  //{
  //  //NOTE: Not sure if we should actually have these Owners table since User which become the owner already exists
  //  //So maybe we only need to create a team2user and pass the id as an argument not the whole user object
  //  Guard.Against.Null(newUser, nameof(newUser));
  //  _owners.Add(newUser);

  //  Event2User event2User = new Event2User(newUser.Id, this.Id);
  //  Guard.Against.Null(event2User, nameof(event2User));
  //  _events2Users.Add(event2User);
  //}
  public void Archive()
  {
    this.IsArchived = true;
  }
}
