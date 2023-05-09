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
          StartTime = @event.StartTime,
          IsArchived = @event.IsArchived,
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
      StartTime = selectEvent.StartTime,
      Teams = selectEvent.Teams.Select(team => TeamViewModel.FromTeam(team)).ToList(),
      Stadiums = selectEvent.Stadiums.Select(stadium => StadiumViewModel.FromStadium(stadium)).ToList()
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
    eventView.SelectTeamsName.Add("defoult");

    EventWithTeam spec = new EventWithTeam();
    List<Event> existEvents = await _eventRepository.ListAsync(spec);

    List<String> eventsTeamName = new();

    foreach(Event eventToFilter in existEvents)
    {
        foreach(Team team in eventToFilter.Teams)
      {
        eventsTeamName.Add(team.Name);
      }

    }

    var teams = await _teamRepository.ListAsync();  
    if (teams == null)
    {
      return View(eventView);
    }


    foreach(Team team in teams)
    {
      if (!eventsTeamName.Contains(team.Name) && !team.IsArchived) { 
        eventView.TeamsName.Add(team.Name);
      }
    }

    return View(eventView);
  }

  // POST: EventSettings/Create
  [HttpPost]
  public async Task<IActionResult> Create(EventViewModel viewModel)
  {

    Event eventNew = new Event(viewModel.Name, viewModel.StartTime);
    foreach(StadiumViewModel newStadium in viewModel.Stadiums)
    {
      //TODO: Make a normal name in front-end
      eventNew.AddStadium(
        new Stadium("name", newStadium.City)
      );
    };

    foreach(string teamName in viewModel.SelectTeamsName)
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
      StartTime = actuallEvent.StartTime
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

    actuallEvent.Archive();
    await _eventRepository.UpdateAsync(actuallEvent);
    return RedirectToAction("Index");
  }

  //public ActionResult Generate()
  //{
  //  return View();
  //}

  public async Task<ActionResult> Generate(int id)
  {
    EventByIdWithTeamSpec spec = new EventByIdWithTeamSpec(id);
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
