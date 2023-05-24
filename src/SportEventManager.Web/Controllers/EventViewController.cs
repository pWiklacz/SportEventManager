using Microsoft.AspNetCore.Mvc;
using SportEventManager.Core.TeamAggregate.Specifications;
using SportEventManager.Core.TeamAggregate;
using SportEventManager.Web.ViewModels;
using SportEventManager.Web.ViewModels.EventModel;
using SportEventManager.SharedKernel.Interfaces;
using SportEventManager.Core.EventAggregate;
using SportEventManager.Core.EventAggregate.Specification;
using SportEventManager.Web.ViewModels.TeamModel;

namespace SportEventManager.Web.Controllers;

public class EventViewController : Controller
{

  private readonly IRepository<Event> _eventRepository;

  public EventViewController(IRepository<Event> eventRepository)
  {
    _eventRepository = eventRepository;
  }

  [HttpGet]
  public async Task<IActionResult> Matches(int id)
  {
    var spec = new EventsByIdWithItemsSpec(id);
    Event? ev = await _eventRepository.FirstOrDefaultAsync(spec);

    if (ev == null) { return NotFound(); }

    var dto = new EventViewModel
    {
      Id = ev.Id,
      Name = ev.Name,
      Stadiums = ev.Stadiums.Select(stadium => StadiumViewModel.FromStadium(stadium)).ToList(),
      Teams = ev.Teams.Select(team => TeamViewModel.FromTeam(team)).ToList(),
      Matches = ev.Matches.Select(match => MatchViewModel.FromMatch(match)).ToList(),
      StartTime = ev.StartTime
    };

    return View(dto);
  }

  [HttpPost]
  public IActionResult Matches(EventViewModel viewModel)
  {
    return RedirectToAction("Matches", viewModel.Id);
  }

  [HttpGet]
  public async Task<IActionResult> Bracket(int id)
  {
    var spec = new EventsByIdWithItemsSpec(id);
    Event? ev = await _eventRepository.FirstOrDefaultAsync(spec);

    if (ev == null) { return NotFound(); }

    var dto = new EventViewModel
    {
      Id = ev.Id,
      Name = ev.Name,
      Stadiums = ev.Stadiums.Select(stadium => StadiumViewModel.FromStadium(stadium)).ToList(),
      Teams = ev.Teams.Select(team => TeamViewModel.FromTeam(team)).ToList(),
      Matches = ev.Matches.Select(match => MatchViewModel.FromMatch(match)).ToList(),
      StartTime = ev.StartTime
    };

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

    var dto = new EventViewModel
    {
      Id = ev.Id,
      Name = ev.Name,
      Stadiums = ev.Stadiums.Select(stadium => StadiumViewModel.FromStadium(stadium)).ToList(),
      Teams = ev.Teams.Select(team => TeamViewModel.FromTeam(team)).ToList(),
      Matches = ev.Matches.Select(match => MatchViewModel.FromMatch(match)).ToList(),
      StartTime = ev.StartTime
    };

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

    var dto = new EventViewModel
    {
      Id = ev.Id,
      Name = ev.Name,
      Stadiums = ev.Stadiums.Select(stadium => StadiumViewModel.FromStadium(stadium)).ToList(),
      Teams = ev.Teams.Select(team => TeamViewModel.FromTeam(team)).ToList(),
      Matches = ev.Matches.Select(match => MatchViewModel.FromMatch(match)).ToList(),
      StartTime = ev.StartTime
    };

    return View(dto);
  }

  [HttpPost]
  public IActionResult Stats(EventViewModel viewModel)
  {
    return RedirectToAction("Stats", viewModel.Id);
  }
}

