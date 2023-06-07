using SportEventManager.Core.EventAggregate;
using SportEventManager.Core.StatisticsAggregate;
using SportEventManager.Core.TeamAggregate;
using SportEventManager.Web.ViewModels.TeamModel;
using SportEventManager.Web.ViewModels.TeamModel.Stats;

namespace SportEventManager.Web.ViewModels.EventModel;

public class MatchViewModel
{
  public int Id { get; set; }

  public DateTime StartTime { get; set; }

  public DateTime EndTime { get; set; }

  public String? WinnerName { get; set; } = string.Empty;

  public bool IsArchived { get; set; } = false;

  public bool IsEnded { get; set; } = false;

  public int EventId { get; set; }

  public StadiumViewModel? Stadium { get; set; }

  public FbTeamMatchStatsViewModel? HomeTeamStats { get; set; }

  public FbTeamMatchStatsViewModel? GuestTeamStats { get; set; }

  public TeamViewModel HomeTeam { get; set; } = null!;

  public TeamViewModel GuestTeam { get; set; } = null!;

  public static MatchViewModel FromMatch(Match match) => new()
  {
    Id = match.Id,
    StartTime = match.StartTime,
    EndTime = match.EndTime,
    Stadium = StadiumViewModel.FromStadium(stadium: match.Stadium),
    IsEnded = match.IsEnded,
    WinnerName = match.WinnerName,
    IsArchived = match.IsArchived,
    EventId = match.EventId,
    HomeTeamStats = FbTeamMatchStatsViewModel.FromTeamMatchStats(match.HomeTeamStats),
    GuestTeamStats = FbTeamMatchStatsViewModel.FromTeamMatchStats(match.GuestTeamStats),
    HomeTeam = TeamViewModel.FromTeam(team: match.HomeTeam),
    GuestTeam = TeamViewModel.FromTeam(team: match.GuestTeam)
  };
}

