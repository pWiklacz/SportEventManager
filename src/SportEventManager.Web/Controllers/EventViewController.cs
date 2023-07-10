using Microsoft.AspNetCore.Mvc;
using SportEventManager.Web.ViewModels.EventModel;
using SportEventManager.SharedKernel.Interfaces;
using SportEventManager.Core.EventAggregate;
using SportEventManager.Core.EventAggregate.Specification;
using SportEventManager.Web.ViewModels.MatchModel.Stats;
using System.Text;

namespace SportEventManager.Web.Controllers;

public class EventViewController : Controller
{
  private readonly IRepository<Event> _eventRepository;
  
  public EventViewController(IRepository<Event> eventRepository)
  {
    _eventRepository = eventRepository;
  }

  [HttpGet]
  public async Task<IActionResult> ShowMatches(int id)
  {
    var spec = new EventByIdWithItemsSpec(id);
    Event? ev = await _eventRepository.FirstOrDefaultAsync(spec);

    if (ev == null) { return NotFound(); }

    var viewModel = EventViewModel.FromEvent(ev);

    return View(viewModel);
  }

  [HttpGet]
  public async Task<IActionResult> Bracket(int id)
  {
    var spec = new EventByIdWithItemsSpec(id);
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

  [HttpGet]
  public async Task<IActionResult> Standings(int id)
  {
    var spec = new EventByIdWithItemsSpec(id);
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

  //To add playerStats view you just need to add an object FbPlayerFullStatsViewModel
  //Then a property of this object to FbTeamFullStatsVM and rewrite also its values
  //in FromEvent(...).FromTeamAndMatches
  [HttpGet]
  public async Task<IActionResult> Stats(int id)
  {
    var spec = new EventByIdWithItemsSpec(id);
    Event? ev = await _eventRepository.FirstOrDefaultAsync(spec);

    if (ev == null) { return NotFound(); }

    var dto = StatsViewModelFull.FromEvent(ev);
    dto.Stats = dto.Stats.OrderByDescending(s => s.Wins)
                  .ThenBy(s => s.Losses)
                  .ThenByDescending(s => s.Goals).ToList();

    return View(dto);
  }

  [HttpPost]
  public IActionResult Stats(EventViewModel viewModel)
  {
    return RedirectToAction("Stats", viewModel.Id);
  }

  public async Task<IActionResult> ExportToExcel(int id)
  {
    var spec = new EventByIdWithItemsSpec(id);
    Event? ev = await _eventRepository.FirstOrDefaultAsync(spec);

    if (ev == null) { return NotFound(); }

    var dto = StatsViewModelFull.FromEvent(ev);
    dto.Stats = dto.Stats.OrderByDescending(s => s.Wins)
                  .ThenBy(s => s.Losses)
                  .ThenByDescending(s => s.Goals).ToList();

    var csv = new StringBuilder();
    csv.AppendLine("TeamName; TeamTag; Wins; Draws; Losses; Goals");

    
    foreach (var stat in dto.Stats)
    {
      csv.AppendLine($"{stat.TeamName}; {stat.TeamTag}; {stat.Wins}; {stat.Draws}; {stat.Losses}; {stat.Goals}");
    }

    
    var preamble = Encoding.UTF8.GetPreamble();
    var body = Encoding.UTF8.GetBytes(csv.ToString());

    var bytes = preamble.Concat(body).ToArray();

    
    return File(bytes, "text/csv", "Stats.csv");
  }

}



