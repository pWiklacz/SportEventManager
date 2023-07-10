using SportEventManager.Core.EventAggregate;

namespace SportEventManager.Web.ViewModels.MatchModel.Stats;

public class StatsViewModelFull
{
  public int Id { get; set; }

  public string Name { get; set; }

  public List<FbTeamFullStatsViewModel> Stats = new List<FbTeamFullStatsViewModel>();

  public static StatsViewModelFull FromEvent(Event @event)
  {
    var vm = new StatsViewModelFull(@event.Id, @event.Name);
    foreach (var team in @event.Teams)
    {
      vm.Stats.Add(FbTeamFullStatsViewModel.FromTeamAndMatches(team, @event.Matches.ToList()));
    }
    return vm;
  }

  StatsViewModelFull(int id, string name)
  {
    this.Id = id;
    this.Name = name;
  }
}
