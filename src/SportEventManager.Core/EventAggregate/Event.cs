using SportEventManager.SharedKernel.Interfaces;
using SportEventManager.SharedKernel;
using SportEventManager.Core.TeamAggregate;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Ardalis.GuardClauses;
using SportEventManager.Core.MatchAggregate;

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

  [Required]
  [DefaultValue(false)]
  public bool IsEnded { get; private set; } = false;

  [Required]
  public int MinPlayersQuantityPerTeam { get; set; } = 0;

  [Required]
  public int MatchDurationMinutes { get; set; } = 0;

  //navigation properties

  private List<Stadium> _stadiums = new();
  private List<Team> _teams = new();
  private List<Match> _matches = new();
  public ICollection<Match> Matches => _matches.AsReadOnly();
  public ICollection<Team> Teams => _teams.AsReadOnly();
  public ICollection<Stadium> Stadiums => _stadiums.AsReadOnly();

  public Event(
    string? ownerId,
    string name,
    DateTime startTime,
    DateTime endTime,
    int minPlayersQuantityPerTeam,
    int matchDurationMinutes
    )
  {
    OwnerId = Guard.Against.NullOrEmpty(ownerId, nameof(ownerId));
    Name = Guard.Against.NullOrEmpty(name, nameof(name));
    StartTime = Guard.Against.Null(startTime, nameof(startTime));
    EndTime = Guard.Against.Null(endTime, nameof(endTime));
    Guard.Against.NegativeOrZero(endTime - startTime, null, "End time must be later than start time!");
    IsInprogress = (startTime <= DateTime.Now && endTime > DateTime.Now);
    IsEnded = (!this.IsInprogress && endTime <= DateTime.Now);
    Guard.Against.InvalidInput(
      minPlayersQuantityPerTeam,
      nameof(minPlayersQuantityPerTeam),
      (minPlayerQuantity =>
        minPlayerQuantity is 7 or 9 or 11),
      "Players quantity per team must be 7, 9 or 11!"
      );
    MinPlayersQuantityPerTeam = Guard.Against.NegativeOrZero(minPlayersQuantityPerTeam, nameof(minPlayersQuantityPerTeam));
    Guard.Against.InvalidInput(
      matchDurationMinutes,
      nameof(matchDurationMinutes),
      mdr => mdr is <= 150 and >= 15,
      "Match duration must be between 15 and 150 minutes"
    );
    MatchDurationMinutes = Guard.Against.NegativeOrZero(matchDurationMinutes, nameof(matchDurationMinutes));
    IsArchived = false;
  }

  public void AddStadium(Stadium newStadium)
  {
    Guard.Against.Null(newStadium, nameof(newStadium));
    if (_stadiums.Any(stadium => stadium.Id == newStadium.Id))
    {
      throw new Exception("The stadium " + newStadium.Name + " was chosen more than once.");
    }
    _stadiums.Add(newStadium); //TODO: fix the problem with adding a stadium that exists in the db
  }

  public void AddTeam(Team newTeam)
  {
    Guard.Against.Null(newTeam, nameof(newTeam));
    if (_teams.Contains(newTeam))
    {
      throw new Exception("The team " + newTeam.Name + " was chosen more than once.");
    }
    Guard.Against.Negative(
      newTeam.NumberOfPlayers - MinPlayersQuantityPerTeam,
      null,
      "Number of players in team " + newTeam.Name + " is too low. Minimum is " + MinPlayersQuantityPerTeam
      );
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
    foreach (Match match in _matches)
    {
      match.Archive();
    }
  }

  public Event() { }
}
