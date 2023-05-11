using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ardalis.GuardClauses;
using SportEventManager.Core.StatisticsAggregate;
using SportEventManager.Core.TeamAggregate;
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
  [ForeignKey("Stadium")]
  public int StadiumId { get; set; }

  [Required]
  [DefaultValue(false)]
  public bool IsArchived { get; private set; } = false;

  [Required]
  [DefaultValue(false)]
  public bool IsEnded { get; set; } = false;

  [Required]
  [ForeignKey("Team")]
  public int FirstTeamId { get; private set; }

  [Required]
  [ForeignKey("Team")]
  public int SecondTeamId { get; private set; }

  [Required]
  [ForeignKey("Event")]
  public int EventId { get; set; }

  public String WinnerName { get; set; } = string.Empty;

  private List<Statistics> _fbTeamMatchStats = new List<Statistics>(2);

  [NotMapped]
  public IEnumerable<Statistics> FbTeamMatchStats => _fbTeamMatchStats.AsReadOnly();

  public Match() { }

  public Match(
    DateTime startTime, 
    DateTime endTime, 
    Stadium stadium,
    int stadiumId,
    int firstTeamId,
    int secondTeamId,
    bool isEnded = false,
    string winnerName = ""
    )
  {
    StartTime = Guard.Against.Null(startTime, nameof(startTime));
    EndTime = Guard.Against.Null(endTime, nameof(endTime));
    Stadium = Guard.Against.Null(stadium, nameof(stadium));
    StadiumId = Guard.Against.NegativeOrZero(stadiumId, nameof(stadiumId));
    FirstTeamId = Guard.Against.NegativeOrZero(firstTeamId, nameof(firstTeamId));
    SecondTeamId = Guard.Against.NegativeOrZero(secondTeamId, nameof(secondTeamId));
    IsArchived = false;
    IsEnded = isEnded;
    WinnerName = Guard.Against.NullOrEmpty(winnerName, nameof(winnerName));
  }

  public void Archive()
  {
    this.IsArchived = true;
  }

  public void AddStats(Statistics newStats)
  {
    Guard.Against.Null(newStats, nameof(newStats));
    _fbTeamMatchStats.Add(newStats);
  }
}
