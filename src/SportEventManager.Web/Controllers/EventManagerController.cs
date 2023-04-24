using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportEventManager.Core.EventAggregate;
using SportEventManager.SharedKernel.Interfaces;
using SportEventManager.Web.ViewModels;


namespace SportEventManager.Web.Controllers;
public class EventManagerController : Controller
{
  private readonly IRepository<Event>? _eventRepository;

  public EventManagerController(IRepository<Event> eventRepository)
  {
    _eventRepository = eventRepository;
  }

  // GET: EventSettings
  public ActionResult Index()
  {
    return View();
  }

  // GET: EventSettings/Details/5
  public ActionResult Details(int id)
  {
    return View();
  }

  // GET: EventSettings/Create
  [HttpGet]
  public IActionResult Create()
  {
    EventViewModel eventView= new EventViewModel();
    return View(eventView);
  }

  // POST: EventSettings/Create
  [HttpPost]
  public ActionResult Create(EventViewModel viewModel)
  {
      
      return View();
    
  }

  // POST: EventSettings/Create
  [HttpPost]
  public ActionResult Next(IFormCollection collection)
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
