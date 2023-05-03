using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportEventManager.Core.EventAggregate;
using SportEventManager.Core.EventAggregate.Specifications;
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
          startTime = @event.StartTime,
          IsDeleted = @event.IsDeleted,
        });
    }

    return View(dto);

  }

  [HttpGet]
  public async Task<IActionResult> Details(int id)
  {
    EventByIdWithTeamSpec spec = new EventByIdWithTeamSpec(id);
    Event? selectEvent = await _eventRepository.FirstOrDefaultAsync(spec);

    if (selectEvent == null)
    {
      return NotFound();
    }

    var dto = new EventViewModel
    {
      Id = selectEvent.Id,
      Name = selectEvent.Name,
      startTime = selectEvent.StartTime,
      Teams = selectEvent.Teams.Select(team => TeamViewModel.FromTeam(team)).ToList(),
      Stadiums = selectEvent.stadiums.Select(stadium => StadiumViewModel.FromStadium(stadium)).ToList()
    };

    return View(dto);
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

    //tutaj stadiony też będą pobierane z bazy danych aby uniknąć tworzenia duplikatów

    foreach(Team team in teams)
    {
      //if(team.EventId == 0)
      //{
        eventView.teamsName.Add(team.Name);
      //}
      
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

    //Dictionary<Team, Team> bracket = TournamentBracket.GenerateBracket_1stRound(eventNew.Teams.ToList());
    //foreach(var team in bracket)
    //{
    //  Match newMatch = new();

    //}

    await _eventRepository.AddAsync(eventNew);
    await _eventRepository.SaveChangesAsync();
      return RedirectToAction("Index");
    
  }

  [HttpGet]
  public async Task<IActionResult> Delete(int id)
  {
    EventByIdSpec spec = new EventByIdSpec(id);
    Event? actuallEvent = await _eventRepository.FirstOrDefaultAsync(spec);
    if(actuallEvent == null)
    {
      return NotFound();
    }

    var dto = new EventViewModel
    {
      Id = actuallEvent.Id,
      Name = actuallEvent.Name,
      startTime = actuallEvent.StartTime
    };

    return View(dto);
  }

  [HttpPost]
  public async Task<IActionResult> Delete(EventViewModel viewModel)
  {
    EventByIdSpec spec = new EventByIdSpec(viewModel.Id);
    Event? actuallEvent = await _eventRepository.FirstOrDefaultAsync(spec);

    if (actuallEvent == null)
    {
      return NotFound();
    }

    actuallEvent.MarkAsDeleted();
    await _eventRepository.UpdateAsync(actuallEvent);
    return RedirectToAction("Index");
  }
}
