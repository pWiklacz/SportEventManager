using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportEventManager.Core.EventAggregate;
using SportEventManager.Core.TeamAggregate;
using SportEventManager.Core.TeamAggregate.Specifications;
using SportEventManager.SharedKernel.Interfaces;
using SportEventManager.Web.ViewModels.EventModel;
using SportEventManager.Web.ViewModels.TeamModel;
using SportEventManager.Web.ViewModels.TeamModel.Stats;

namespace SportEventManager.Web.Controllers;
public class EventManagerController : Controller
{
  private readonly IRepository<Event> _eventRepository;
  private readonly IRepository<Team> _teamRepository;

  public EventManagerController(IRepository<Event> eventRepository, IRepository<Team> teamRepository)
  {
    _eventRepository = eventRepository;
    _teamRepository = teamRepository;
  }

  // GET: EventSettings
  public async Task<IActionResult> Index()
  {
    var sportEvent = await _eventRepository.ListAsync();
    if(sportEvent == null)
    {
      return View();
    }

    var dto = new List<EventViewModel>();
    foreach(Event @event in sportEvent)
    {
      dto.Add(
        new EventViewModel
        {
          Id = @event.Id,
          Name = @event.Name,
          startTime = @event.StartTime
        });
    }

    return View(dto);

  }

  // GET: EventSettings/Details/5
  public ActionResult Details(int id)
  {
    return View();
  }

  // GET: EventSettings/Create
  [HttpGet]
  public async Task<IActionResult> Create()
  {
    EventViewModel eventView= new EventViewModel();
    eventView.Stadiums.Add(new StadiumViewModel() { Id = 1 });
    eventView.Matches.Add(new MatchViewModel() { Id= 1 });
    eventView.selectTeamsName.Add("defoult");


    TeamsByOwnerIdSpec spec = new TeamsByOwnerIdSpec();
    var teams = await _teamRepository.ListAsync();  
    if (teams == null)
    {
      return View(eventView);
    }

    foreach(Team team in teams)
    {
      eventView.teamsName.Add(team.Name);
    }
  

    return View(eventView);
  }

  // POST: EventSettings/Create
  [HttpPost]
  public async Task<IActionResult> Create(EventViewModel viewModel)
  {

    Event eventNew = new Event(viewModel.Name, viewModel.startTime);
    foreach(StadiumViewModel newStadium in viewModel.Stadiums)
    {
      eventNew.AddStadium(
        new Stadium(newStadium.City)
        );
    };

    foreach(string teamName in viewModel.selectTeamsName)
    {
      var spec = new TeamByNameSpec(teamName);
      Team? team = await _teamRepository.FirstOrDefaultAsync(spec);

      if(team == null) { return NotFound(); }

      eventNew.AddTeam(team);
      
    }

    Dictionary<Team, Team> bracket = TournamentBracket.GenerateBracket_1stRound(eventNew.Teams.ToList());
    foreach(var team in bracket)
    {
      Match newMatch = new();

    }

    await _eventRepository.AddAsync(eventNew);
    await _eventRepository.SaveChangesAsync();
      return RedirectToAction("Index");
    
  }


  // GET: EventSettings/Edit/5
  public ActionResult Edit(int id)
  {
    return View();
  }

  // POST: EventSettings/Edit/5
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult Edit(int id, IFormCollection collection)
  {
    try
    {
      return RedirectToAction(nameof(Index));
    }
    catch
    {
      return View();
    }
  }

  // GET: EventSettings/Delete/5
  public ActionResult Delete(int id)
  {
    return View();
  }

  // POST: EventSettings/Delete/5
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult Delete(int id, IFormCollection collection)
  {
    try
    {
      return RedirectToAction(nameof(Index));
    }
    catch
    {
      return View();
    }
  }
}
