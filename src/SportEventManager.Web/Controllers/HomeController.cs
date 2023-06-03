using System.Diagnostics;
using SportEventManager.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using SportEventManager.Core.EventAggregate;
using SportEventManager.SharedKernel.Interfaces;
using SportEventManager.Web.ViewModels.EventModel;
using SportEventManager.Core.EventAggregate.Specifications;

namespace SportEventManager.Web.Controllers;

public class HomeController : Controller
{
  private readonly IRepository<Event> _eventRepository;

  public HomeController(IRepository<Event> eventRepository)
  {
    _eventRepository = eventRepository;
  }

  // GET: EventSettings
  public async Task<IActionResult> Index()
  {
    var sportEvents = new List<Event>();

    EventsWithItemsSpec eventWithItemsSpec = new EventsWithItemsSpec();
    sportEvents = await _eventRepository.ListAsync(eventWithItemsSpec);

    var dto = new List<EventViewModel>();
    foreach(Event @event in sportEvents)
    {
      dto.Add(
        EventViewModel.FromEvent(@event)
       );
    }

    return View(dto);
  }

  public IActionResult Privacy()
  {
    return View();
  }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
