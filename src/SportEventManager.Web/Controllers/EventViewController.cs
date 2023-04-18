using Microsoft.AspNetCore.Mvc;
using SportEventManager.Core.TeamAggregate.Specifications;
using SportEventManager.Core.TeamAggregate;
using SportEventManager.Web.ViewModels;

namespace SportEventManager.Web.Controllers;

public class EventViewController : Controller
{
  public IActionResult Matches()
  {
    var dto = new EventViewModel();

    return View(dto);
  }

  public IActionResult Bracket()
  {
    var dto = new EventViewModel();

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

