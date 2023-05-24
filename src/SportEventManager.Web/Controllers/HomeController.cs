using System.Diagnostics;
using SportEventManager.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using SportEventManager.Core.EventAggregate;
using SportEventManager.SharedKernel.Interfaces;
using SportEventManager.Core.TeamAggregate;
using SportEventManager.Web.ViewModels.EventModel;
using SportEventManager.Web.ViewModels.TeamModel;
using Microsoft.Extensions.Logging;
using SportEventManager.Core.UserAggregate;
using Microsoft.AspNetCore.Identity;
using JetBrains.Annotations;
using SportEventManager.Core.EventAggregate.Specifications;
using Ardalis.Specification;
using System.Linq;

namespace SportEventManager.Web.Controllers;

/// <summary>
/// A sample MVC controller that uses views.
/// Razor Pages provides a better way to manage view-based content, since the behavior, viewmodel, and view are all in one place,
/// rather than spread between 3 different folders in your Web project. Look in /Pages to see examples.
/// See: https://ardalis.com/aspnet-core-razor-pages-%E2%80%93-worth-checking-out/
/// </summary>
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

    EventWithItemsSpec eventWithItemsSpec = new EventWithItemsSpec();
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
