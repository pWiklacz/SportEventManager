using Microsoft.AspNetCore.Mvc;
using SportEventManager.Core.EventAggregate;
using SportEventManager.Core.MatchAggregate;
using SportEventManager.Core.MatchAggregate.Specifications;
using SportEventManager.SharedKernel.Interfaces;
using SportEventManager.Web.ViewModels.MatchModel;
using SportEventManager.Web.ViewModels.MatchModel.Stats;

namespace SportEventManager.Web.Controllers;

public class MatchViewController : Controller
{
  private readonly IRepository<Match> _matchRepository;

  public MatchViewController(IRepository<Match> matchRepository)
  {
    _matchRepository = matchRepository;
  }

  [HttpGet]
  public async Task<IActionResult> Index(int id)
  {
    var spec = new MatchByIdWithItemsSpec(id);

    Match? match = await _matchRepository.FirstOrDefaultAsync(spec);

    if (match == null) { return NotFound(); }

    var viewModel = MatchViewModel.FromMatch(match);

    return View(viewModel);
  }

  [HttpGet]
  public async Task<IActionResult> Update(int id)
  {
    var spec = new MatchByIdWithItemsSpec(id);

    Match? match = await _matchRepository.FirstOrDefaultAsync(spec);

    if (match == null) { return NotFound(); }

    var viewModel = MatchViewModel.FromMatch(match);

    return View(viewModel);
  }

  [HttpPost]
  public async Task<IActionResult> Update(MatchViewModel viewModel)
  {
    var spec = new MatchByIdWithItemsSpec(viewModel.Id);
    Match? match = await _matchRepository.FirstOrDefaultAsync(spec);

    if (match == null) { return NotFound();}

    var hTeamStats = TeamStatsFromViewModel(viewModel.HomeTeamStats);
    var gTeamStats = TeamStatsFromViewModel(viewModel.GuestTeamStats);
    var playerStats = PlayersStatsFromViewModel(viewModel.HomeTeamPlayersMatchStats,
      viewModel.GuestTeamPlayersMatchStats);

    match.EndMatch(hTeamStats,gTeamStats,playerStats);

    await _matchRepository.UpdateAsync(match);
    await _matchRepository.SaveChangesAsync();

    return RedirectToAction("Index", new { id = viewModel.Id });
  }

  private static FbTeamMatchStats TeamStatsFromViewModel(FbTeamMatchStatsViewModel stats)
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

  private static List<FbPlayerMatchStats> PlayersStatsFromViewModel(
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
