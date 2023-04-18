using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SportEventManager.Web.Controllers;
public class EventManagerController : Controller
{
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
  public ActionResult Create()
  {
    return View();
  }

  // POST: EventSettings/Create
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult Create(IFormCollection collection)
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
