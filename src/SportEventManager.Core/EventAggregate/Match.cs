using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ardalis.GuardClauses;
using SportEventManager.Core.TeamAggregate;
using SportEventManager.Core.TeamAggregate.Stats;
using SportEventManager.SharedKernel;

namespace SportEventManager.Core.EventAggregate;

public class Match : EntityBase
{
  [Required]
  public DateTime StartTime { get; set; }

  [Required]
  public DateTime EndTime { get; set; }

  [Required]
  public Stadium Stadium { get; set; } = new Stadium();

  [Required]
  [DefaultValue(false)]
  public bool IsDeleted { get; private set; } = false;

  [Required]
  [DefaultValue(false)]
  public bool IsEnded { get; set; } = false;

  [Required]
  [ForeignKey("Team")]
  public int FirstTeamId { get; private set; }

  [Required]
  [ForeignKey("Team")]
  public int SecondTeamId { get; private set; }

  private List<FBTeamStats> _fbTeamStats = new List<FBTeamStats>(2);

  public IEnumerable<FBTeamStats> FbTeamStats => _fbTeamStats.AsReadOnly();

  [MaxLength(100)]
  public string WinnerName { get; set; } = string.Empty;

  public Match() { }

  public Match(
    DateTime startTime, 
    DateTime endTime, 
    Stadium stadium,
    int firstTeamId,
    int secondTeamId,
    bool isEnded = false,
    string winnerName = ""
    )
  {
    StartTime = startTime;
    EndTime = endTime;
    Stadium = stadium;
    FirstTeamId = firstTeamId;
    SecondTeamId = secondTeamId;
    IsDeleted = false;
    IsEnded = isEnded;
    WinnerName = winnerName;
  }

  public void MarkAsDeleted()
  {
    this.IsDeleted = true;
  }

  public void AddStats(FBTeamStats newStats)
  {
    Guard.Against.Null(newStats, nameof(newStats));
    _fbTeamStats.Add(newStats);

    //var newPlayerAddedEvent = new NewPlayerAddedEvent(this, newPlayer);
    //base.RegisterDomainEvent(newPlayerAddedEvent);
  }
}
