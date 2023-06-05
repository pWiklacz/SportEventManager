﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SportEventManager.Core.EventAggregate;
using SportEventManager.Core.EventAggregate.Specifications;
using SportEventManager.Core.TeamAggregate;
using SportEventManager.Core.TeamAggregate.Specifications;
using SportEventManager.Core.UserAggregate;
using SportEventManager.SharedKernel.Interfaces;
using SportEventManager.Web.ViewModels.EventModel;

namespace SportEventManager.Web.Controllers;
public class EventManagerController : Controller
{
  private readonly IRepository<Event> _eventRepository;
  private readonly IRepository<Team> _teamRepository;
  private readonly UserManager<User> _userManager;

  public EventManagerController(
    IRepository<Event> eventRepository,
    IRepository<Team> teamRepository,
    UserManager<User> userManager
    )
  {
    _eventRepository = eventRepository;
    _teamRepository = teamRepository;
    _userManager = userManager;
  }

  // GET: Event
  public async Task<IActionResult> Index()
  {
    var user = await _userManager.GetUserAsync(User);
    var spec = new EventsByOwnerIdSpec(user?.Id);
    var sportEvents = await _eventRepository.ListAsync(spec);
    if(sportEvents.IsNullOrEmpty())
    {
      return View();
    }

    var dto = new List<EventViewModel>();
    foreach(Event @event in sportEvents)
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
    var user = await _userManager.GetUserAsync(User);
    Event eventNew = new Event(user?.Id, viewModel.Name, viewModel.StartTime, viewModel.EndTime);

    foreach(StadiumViewModel newStadium in viewModel.Stadiums)
    {
      try
      {
        eventNew.AddStadium(
          new Stadium(newStadium.Name, newStadium.City)
        );
      }
      catch (Exception ex)
      {
        return RedirectToAction("Create", new { error = ex.Message });
      }
    };

    foreach(string teamName in viewModel.ChosenTeamsNames)
    {
      var spec = new TeamByNameSpec(teamName);
      Team? team = await _teamRepository.FirstOrDefaultAsync(spec);

      if(team == null) { return NotFound(); }

      try { 
        eventNew.AddTeam(team);
      } 
      catch(Exception ex) {
        return RedirectToAction("Create", new { error = ex.Message });
      }
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

    if(eventToDelete == null)
    {
      return NotFound();
    }

    var dto = EventViewModel.FromEvent(eventToDelete);
    return View(dto);
  }

  [HttpPost]
  public async Task<IActionResult> Delete(EventViewModel viewModel)
  {
    EventByIdWithTeamsAndStadiumsSpec spec = new EventByIdWithTeamsAndStadiumsSpec(viewModel.Id);
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
    if(ev == null)
    {
      return NotFound();
    }

    Dictionary<Team, Team> bracket = TournamentBracket.GenerateBracket_1stRound(ev.Teams.ToList());
    foreach(var pair in bracket)
    {
      Match match = new Match(ev.StartTime, DateTime.MaxValue, ev.Stadiums.ElementAt<Stadium>(0),
        ev.Stadiums.ElementAt<Stadium>(0).Id,
        pair.Key.Id, pair.Value.Id
      );
      ev.AddMatch(match);
    }

    await _eventRepository.UpdateAsync(ev);
    await _eventRepository.SaveChangesAsync();

    return RedirectToAction("Index");
  }
}
