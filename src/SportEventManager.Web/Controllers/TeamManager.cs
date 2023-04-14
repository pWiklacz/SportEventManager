using Microsoft.AspNetCore.Mvc;
using SportEventManager.Web.ViewModels;

namespace SportEventManager.Web.Controllers;

  public class TeamManager : Controller
  {
      public IActionResult Index()
      {
          return View();
      }

      public IActionResult Create()
      {
          return View();
      }

      public IActionResult Edit()
      {
          return View();
      }

      public IActionResult Delete()
      {
          return View();
      }
  }
