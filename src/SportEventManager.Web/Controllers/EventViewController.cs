using Microsoft.AspNetCore.Mvc;
using SportEventManager.Core.TeamAggregate.Specifications;
using SportEventManager.Core.TeamAggregate;
using SportEventManager.Web.ViewModels;
using SportEventManager.Web.ViewModels.EventModel;
using SportEventManager.SharedKernel.Interfaces;
using SportEventManager.Core.EventAggregate;
using SportEventManager.Core.EventAggregate.Specification;
using SportEventManager.Core.StatisticsAggregate;
using SportEventManager.Web.ViewModels.TeamModel;
using SportEventManager.Web.ViewModels.TeamModel.Stats;

namespace SportEventManager.Web.Controllers;

public class EventViewController : Controller
{

  private readonly IRepository<Event> _eventRepository;
  private readonly IRepository<Statistics> _statisticsRepository;

  public EventViewController(IRepository<Event> eventRepository, IRepository<Statistics> statisticsRepository)
  {
    _eventRepository = eventRepository; 
    _statisticsRepository = statisticsRepository;
  }

  [HttpGet]
  public async Task<IActionResult> ShowMatches(int id)
  {
    var spec = new EventsByIdWithItemsSpec(id);
    Event? ev = await _eventRepository.FirstOrDefaultAsync(spec);

    if (ev == null) { return NotFound(); }

    var viewModel = EventViewModel.FromEvent(ev);

    return View(viewModel);
  }

  [HttpPost]
  public async Task<IActionResult> ShowMatches(EventViewModel viewModel)
  {
    var spec = new EventsByIdWithItemsSpec(viewModel.Id);
    Event? ev = await _eventRepository.FirstOrDefaultAsync(spec);

    if (ev == null) { return NotFound(); }

    for (int i = 0; i < ev.Matches.Count; ++i)
    {
      var hTeamStats = TeamStatsFromViewModel(viewModel.Matches[i].HomeTeamStats);
      var gTeamStats = TeamStatsFromViewModel(viewModel.Matches[i].GuestTeamStats);
      var playerStats = PlayersStatsFromViewModel(viewModel.Matches[i].HomeTeamPlayersMatchStats,
        viewModel.Matches[i].GuestTeamPlayersMatchStats);
      ev.UpdateMatchStats(i, hTeamStats, gTeamStats, playerStats);
    }

    await _eventRepository.UpdateAsync(ev);
    await _eventRepository.SaveChangesAsync();
    return RedirectToAction("ShowMatches");
  }

  [HttpGet]
  public async Task<IActionResult> Bracket(int id)
  {
    var spec = new EventsByIdWithItemsSpec(id);
    Event? ev = await _eventRepository.FirstOrDefaultAsync(spec);

    if (ev == null) { return NotFound(); }

    var dto = EventViewModel.FromEvent(ev);

    return View(dto);
  }

  [HttpPost]
  public IActionResult Bracket(EventViewModel viewModel)
  {
    return RedirectToAction("Bracket", viewModel.Id);
  }

  //do something with those values
  [HttpGet]
  public async Task<IActionResult> Standings(int id)
  {
    var spec = new EventsByIdWithItemsSpec(id);
    Event? ev = await _eventRepository.FirstOrDefaultAsync(spec);

    if (ev == null) { return NotFound(); }

    var dto = EventViewModel.FromEvent(ev);
    return View(dto);
  }

  [HttpPost]
  public IActionResult Standings(EventViewModel viewModel)
  {
    return RedirectToAction("Standings", viewModel.Id);
  }

  //Do something with those values
  [HttpGet]
  public async Task<IActionResult> Stats(int id)
  {
    var spec = new EventsByIdWithItemsSpec(id);
    Event? ev = await _eventRepository.FirstOrDefaultAsync(spec);

    if (ev == null) { return NotFound(); }

    var dto = EventViewModel.FromEvent(ev);
    return View(dto);
  }

  [HttpPost]
  public IActionResult Stats(EventViewModel viewModel)
  {
    return RedirectToAction("Stats", viewModel.Id);
  }

  private FbTeamMatchStats TeamStatsFromViewModel(FbTeamMatchStatsViewModel stats)
  {
    return new()
    {
      Id = stats.Id,
      Shoots = stats.Shoots,
      ShootsOnTarget = stats.ShootsOnTarget,
      Fouls = stats.Fouls,
      Passes = stats.Passes,
      Goals = stats.Goals,
      Assists = stats.Assists,
      RedCards = stats.RedCards,
      YellowCards = stats.YellowCards,
      TeamId = stats.TeamId,
      Win = stats.Win,
      Draw = stats.Draw,
      Loss = stats.Loss
    };
  }

  private List<FbPlayerMatchStats> PlayersStatsFromViewModel(
    List<FbPlayerMatchStatsViewModel> homeStats,
    List<FbPlayerMatchStatsViewModel> guestStats)
  {
    List<FbPlayerMatchStats> list = homeStats.Select(playerStat => new FbPlayerMatchStats()
      {
        Id = playerStat.Id,
        PlayerId = playerStat.PlayerId,
        Goals = playerStat.Goals,
        Assists = playerStat.Assists,
        RedCards = playerStat.RedCards,
        YellowCards = playerStat.YellowCards
      })
      .ToList();

    list.AddRange(guestStats.Select(playerStat => new FbPlayerMatchStats()
    {
      Id = playerStat.Id,
      PlayerId = playerStat.PlayerId,
      Goals = playerStat.Goals,
      Assists = playerStat.Assists,
      RedCards = playerStat.RedCards,
      YellowCards = playerStat.YellowCards
    }));

    return list;
  }
}

