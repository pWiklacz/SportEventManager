using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ardalis.GuardClauses;
using SportEventManager.Core.EventAggregate;
using SportEventManager.Core.TeamAggregate;
using SportEventManager.SharedKernel;
using SportEventManager.SharedKernel.Interfaces;

namespace SportEventManager.Core.MatchAggregate;

public class Match : EntityBase, IAggregateRoot
{
  [Required]
  public DateTime StartTime { get; set; }

  [Required]
  public DateTime EndTime { get; set; }

  public String? WinnerName { get; set; } = string.Empty;

  [Required]
  [DefaultValue(false)]
  public bool IsArchived { get; private set; } = false;

  [Required]
  [DefaultValue(false)]
  public bool IsEnded { get; set; } = false;

  [Required]
  [ForeignKey("Stadium")]
  public string StadiumId { get; set; } = string.Empty;

  [Required]
  [ForeignKey(nameof(HomeTeam))]
  public int HomeTeamId { get; private set; }

  [Required]
  [ForeignKey(nameof(GuestTeam))]
  public int GuestTeamId { get; private set; }

  [Required]
  [ForeignKey(nameof(HomeTeamStats))]
  public int HomeTeamStatsId { get; private set; }

  [Required]
  [ForeignKey(nameof(GuestTeamStats))]
  public int GuestTeamStatsId { get; private set; }

  [Required]
  [ForeignKey("Event")]
  public int EventId { get; set; }

  //navigation properties

  [Required]
  public Event Event { get; set; } = null!;

  [Required]
  public Stadium Stadium { get; set; } = null!;

  [Required]
  public Team HomeTeam { get; set; } = null!;

  [Required]
  public Team GuestTeam { get; set; } = null!;

  [Required]
  public FbTeamMatchStats HomeTeamStats { get; set; } = null!;

  [Required]
  public FbTeamMatchStats GuestTeamStats { get; set; } = null!;


  private List<FbPlayerMatchStats> _playersStats = new();

  public ICollection<FbPlayerMatchStats> PlayersStats => _playersStats;


  public Match() { }

  public Match(
    DateTime startTime,
    DateTime endTime,
    string stadiumId,
    Team homeTeam,
    Team guestTeam,
    bool isEnded = false,
    string winnerName = ""
    )
  {
    StartTime = Guard.Against.Null(startTime, nameof(startTime));
    EndTime = Guard.Against.Null(endTime, nameof(endTime));
    StadiumId = Guard.Against.NullOrEmpty(stadiumId, nameof(stadiumId));
    HomeTeam = Guard.Against.Null(homeTeam, nameof(homeTeam));
    GuestTeam = Guard.Against.Null(guestTeam, nameof(guestTeam));
    IsArchived = false;
    IsEnded = isEnded;
    WinnerName = winnerName;
    HomeTeamStats = new FbTeamMatchStats(HomeTeam.Id);
    GuestTeamStats = new FbTeamMatchStats(GuestTeam.Id);

    foreach (var player in HomeTeam.Players)
    {
      _playersStats.Add(new FbPlayerMatchStats(player.Id));
    }
    foreach (var player in GuestTeam.Players)
    {
      _playersStats.Add(new FbPlayerMatchStats(player.Id));
    }
  }

  public void EndMatch(FbTeamMatchStats homeStats, FbTeamMatchStats guestStats, List<FbPlayerMatchStats> playerStats)
  {
    Guard.Against.Null(homeStats, nameof(homeStats));
    Guard.Against.Null(guestStats, nameof(guestStats));
    Guard.Against.Null(playerStats, nameof(playerStats));

    for (int i = 0; i < _playersStats.Count; i++)
    {
      _playersStats[i].Update(playerStats[i]);
    }

    HomeTeamStats.Update(homeStats, guestStats);
    GuestTeamStats.Update(guestStats, homeStats);

    if (HomeTeamStats.Win && GuestTeamStats.Loss)
      WinnerName = HomeTeam.Name;
    else if (HomeTeamStats.Loss && GuestTeamStats.Win)
      WinnerName = GuestTeam.Name;
    else WinnerName = "DRAW";
  }

  public void Archive()
  {
    this.IsArchived = true;
  }
}
