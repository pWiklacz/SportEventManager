using Microsoft.AspNetCore.Mvc;
using SportEventManager.Core.EventAggregate;
using SportEventManager.Core.MatchAggregate;
using SportEventManager.Core.MatchAggregate.Specifications;
using SportEventManager.SharedKernel.Interfaces;
using SportEventManager.Web.ViewModels.MatchModel;

namespace SportEventManager.Web.Controllers;

public class MatchViewController : Controller
{
  private readonly IRepository<Match> _matchRepository;

  public MatchViewController(IRepository<Match> matchRepository)
  {
    _matchRepository = matchRepository;
  }

  [HttpGet]
  public async Task<IActionResult> Stats(int id)
  {
    var spec = new MatchByIdWithItemsSpec(id);

    Match? match = await _matchRepository.FirstOrDefaultAsync(spec);

    if (match == null) { return NotFound(); }

    var viewModel = MatchViewModel.FromMatch(match);

    return View(viewModel);
  }



}
