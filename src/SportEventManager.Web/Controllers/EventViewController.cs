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
  public IActionResult Matches()
  {
    var dto = new EventViewModel();

    return View(dto);
  }

  public async Task<IActionResult> BracketAsync(int id)
  {
    var spec = new EventByIdWithItemsSpec(id);
    Event? ev = await _eventRepository.FirstOrDefaultAsync(spec);

    if (ev == null) { return NotFound(); }

    var dto = new EventViewModel
    {
      Id = ev.Id,
      Name= ev.Name,
      Stadiums = ev.stadiums.Select(stadium => StadiumViewModel.FromStadium(stadium)).ToList(),
      Teams = ev.Teams.Select(team => TeamViewModel.FromTeam(team)).ToList(),
      Matches = ev.Matches.Select(match => MatchViewModel.FromMatch(match)).ToList(),
      startTime = ev.StartTime
    };

    return View(dto);
  }

  public IActionResult Standings()
  {
    var dto = new EventViewModel();

    return View(dto);
  }

  public IActionResult Stats()
  {
    var dto = new EventViewModel();

    return View(dto);
  }
}

