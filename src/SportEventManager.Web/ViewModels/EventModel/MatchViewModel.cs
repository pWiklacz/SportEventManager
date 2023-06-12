using System.ComponentModel.DataAnnotations.Schema;
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

  public FbTeamMatchStatsViewModel HomeTeamStats { get; set; } = null!;

  public FbTeamMatchStatsViewModel GuestTeamStats { get; set; } = null!;

  public int HomeTeamId { get; set; }

  public int GuestTeamId { get; set; }

  public TeamViewModel HomeTeam { get; set; } = null!;

  public TeamViewModel GuestTeam { get; set; } = null!;

  public List<FbPlayerMatchStatsViewModel> HomeTeamPlayersMatchStats { get; set; } = new();

  public List<FbPlayerMatchStatsViewModel> GuestTeamPlayersMatchStats { get; set; } = new();

  public bool IsLive
  {
    get
    { 
      return DateTime.Now >= StartTime && DateTime.Now <= EndTime;
    }
  }

  public static MatchViewModel FromMatch(Match match)
  {
    //var HomeTeamPlayersStats = match.PlayersStats.Select(
    //    ps => FbPlayerMatchStatsViewModel.FromPlayerMatchStats(ps)).Where(ps => ps.Player.Id)
    //  .ToList();

    return new()
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
      HomeTeamId = match.HomeTeamId,
      GuestTeamId = match.GuestTeamId,
      HomeTeam = TeamViewModel.FromTeam(team: match.HomeTeam),
      GuestTeam = TeamViewModel.FromTeam(team: match.GuestTeam),
    };
  }

  //private List<FbPlayerMatchStatsViewModel> GetTeamPlayersStats(TeamViewModel team, Match match)
  //{
  //  //var list = new List<FbPlayerMatchStatsViewModel>();
  //  //foreach (var player in team.Players)
  //  //{
  //  //      list.Add(match.PlayersStats.Select(
  //  //          ps => FbPlayerMatchStatsViewModel.FromPlayerMatchStats(ps)).Where(ps => ps.PlayerId == player.Id))
  //  //}
  //}

  public int CalculateMinutesElapsed()
  {
    TimeSpan elapsedTime = DateTime.Now - StartTime;
    int minutesElapsed = (int)elapsedTime.TotalMinutes;

    
    return minutesElapsed;
  }
}

