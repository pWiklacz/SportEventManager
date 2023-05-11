using SportEventManager.Core.EventAggregate;
using SportEventManager.Web.ViewModels.TeamModel.Stats;

namespace SportEventManager.Web.ViewModels.EventModel;

public class MatchViewModel
{
  public int Id { get; set; }

  public DateTime StartTime { get; set; }

  public DateTime EndTime { get; set; }

  public bool IsEnded { get; set; }

  public int FirstTeamId { get; private set; }

  public int SecondTeamId { get; private set; }

  public List<FbTeamMatchStatsViewModel> FbTeamMatchStats { get; private set; } = new List<FbTeamMatchStatsViewModel>(2);

  public StadiumViewModel? Stadium { get; set; }

  public static MatchViewModel FromMatch(Match match) => new MatchViewModel()
  {
    Id = match.Id,
    StartTime = match.StartTime,
    EndTime = match.EndTime,
    Stadium = StadiumViewModel.FromStadium(stadium : match.Stadium),
    IsEnded = match.IsEnded,
    FirstTeamId = match.FirstTeamId,
    SecondTeamId = match.SecondTeamId,
    //TODO: make this work - adding the <type, type> doesn't help
    //FbTeamMatchStats = match.FbTeamMatchStats.Select(FbTeamMatchStatsViewModel.FromTeamMatchStats).ToList()
  };
}

