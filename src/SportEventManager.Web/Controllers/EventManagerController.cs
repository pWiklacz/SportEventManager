using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SportEventManager.Core.EventAggregate;
using SportEventManager.Core.EventAggregate.Specification;
using SportEventManager.Core.EventAggregate.Specifications;
using SportEventManager.Core.MatchAggregate;
using SportEventManager.Core.TeamAggregate;
using SportEventManager.Core.TeamAggregate.Specifications;
using SportEventManager.SharedKernel.Interfaces;
using SportEventManager.Web.ViewModels.EventModel;

namespace SportEventManager.Web.Controllers;
[Authorize(Roles = "Admin,EventManager")]
public class EventManagerController : Controller
{
  private readonly IRepository<Event> _eventRepository;
  private readonly IRepository<Team> _teamRepository;

  public EventManagerController(
    IRepository<Event> eventRepository,
    IRepository<Team> teamRepository
    )
  {
    _eventRepository = eventRepository;
    _teamRepository = teamRepository;
  }

  // GET: Event
  public async Task<IActionResult> Index()
  {
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    var spec = new EventsByOwnerIdSpec(userId);
    var sportEvents = await _eventRepository.ListAsync(spec);
    if (sportEvents.IsNullOrEmpty())
    {
      return View(new List<EventViewModel>());
    }

    var dto = new List<EventViewModel>();
    foreach (Event @event in sportEvents)
    {
      dto.Add(
        EventViewModel.FromEvent(@event)
      );
    }

    return View(dto);
  }

  [HttpGet]
  public async Task<IActionResult> Details(int id)
  {
    EventByIdWithTeamsAndStadiumsSpec spec = new EventByIdWithTeamsAndStadiumsSpec(id);
    Event? selectEvent = await _eventRepository.FirstOrDefaultAsync(spec);

    if (selectEvent == null)
    {
      return NotFound();
    }

    var dto = EventViewModel.FromEvent(selectEvent);
    return View(dto);
  }

  [HttpGet]
  public async Task<IActionResult> Create(string error = "")
  {
    EventViewModel viewModel = new EventViewModel(error);

    TeamsActive teamsActive = new TeamsActive();
    var teams = await _teamRepository.ListAsync(teamsActive);

    if (teams.IsNullOrEmpty())
    {
      return View(viewModel);
    }

    foreach (Team team in teams)
    {
      viewModel.AvailableTeamsNames.Add(team.Name);
    }

    return View(viewModel);
  }

  [HttpPost]
  public async Task<IActionResult> Create(EventViewModel viewModel)
  {
    if (viewModel.StartTime > viewModel.EndTime)
    {
      return RedirectToAction("Create", new { error = "Event must end AFTER it starts." });
    }

    Event? eventNew = null;
    try
    {
      var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      eventNew = new Event(
        userId,
        viewModel.Name,
        viewModel.StartTime,
        viewModel.EndTime,
        viewModel.MinPlayersQuantityPerTeam,
        viewModel.MatchDurationMinutes
      );

      foreach (StadiumViewModel newStadium in viewModel.Stadiums)
      {
        eventNew.AddStadium(
          new Stadium(newStadium.Name, newStadium.City)
        );
      }

      foreach (string teamName in viewModel.ChosenTeamsNames)
      {
        var spec = new TeamByNameSpec(teamName);
        Team? team = await _teamRepository.FirstOrDefaultAsync(spec);

        if (team == null) { return NotFound(); }

        eventNew.AddTeam(team);
      }
    }
    catch (Exception ex)
    {
      return RedirectToAction("Create", new { error = ex.Message });
    }


    await _eventRepository.AddAsync(eventNew);
    await _eventRepository.SaveChangesAsync();
    return RedirectToAction("Index");
  }

  [HttpGet]
  public async Task<IActionResult> Delete(int id)
  {
    EventByIdSpec spec = new EventByIdSpec(id);
    Event? eventToDelete = await _eventRepository.FirstOrDefaultAsync(spec);

    if (eventToDelete == null)
    {
      return NotFound();
    }

    var dto = EventViewModel.FromEvent(eventToDelete);
    return View(dto);
  }

  [HttpPost]
  public async Task<IActionResult> Delete(EventViewModel viewModel)
  {
    EventsByIdWithItemsSpec spec = new EventsByIdWithItemsSpec(viewModel.Id);
    Event? eventToDelete = await _eventRepository.FirstOrDefaultAsync(spec);

    if (eventToDelete == null)
    {
      return NotFound();
    }

    eventToDelete.Archive();
    await _eventRepository.UpdateAsync(eventToDelete);
    return RedirectToAction("Index");
  }

  [HttpGet]
  public async Task<ActionResult> Generate(int id)
  {
    EventByIdWithTeamsAndStadiumsSpec spec = new EventByIdWithTeamsAndStadiumsSpec(id);
    Event? ev = await _eventRepository.FirstOrDefaultAsync(spec);
    if (ev == null)
    {
      return NotFound();
    }

    var minutes = 0;
    Dictionary<Team, Team> bracket = TournamentBracket.GenerateBracket_1stRound(ev.Teams.ToList());
    foreach (var pair in bracket)
    {
      Match match = new Match(ev.StartTime.AddMinutes(minutes), ev.StartTime.AddMinutes(minutes + ev.MatchDurationMinutes),
        ev.Stadiums.ElementAt<Stadium>(0).Id,
        pair.Key, pair.Value
      );
      ev.AddMatch(match);

      minutes += 15;
    }

    await _eventRepository.UpdateAsync(ev);
    await _eventRepository.SaveChangesAsync();

    return RedirectToAction("Index");
  }
}
