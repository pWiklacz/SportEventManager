using System.Data;
using System.Security.Claims;
using Ardalis.Specification;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SportEventManager.Core.TeamAggregate;
using SportEventManager.Core.TeamAggregate.Specifications;
using SportEventManager.Core.UserAggregate;
using SportEventManager.SharedKernel.Interfaces;
using SportEventManager.Web.ViewModels.TeamModel;

namespace SportEventManager.Web.Controllers;

[Authorize(Roles = "Admin,TeamManager")]
public class TeamManagerController : Controller
{
  private readonly IRepository<Team> _teamRepository;
  private string _existingPeselsNumbers;
  
  public TeamManagerController(IRepository<Team> teamRepository)
  {
    _teamRepository = teamRepository;
    _existingPeselsNumbers = " ";
  }

  public async Task<IActionResult> Index()
  {
    string? currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    if (currentUserId != null)
    {
      var teamsWithPlayers = await _teamRepository.ListAsync(new TeamsWithPlayersByOwnerIdSpec(currentUserId));

      if (teamsWithPlayers == null)
      {
        _existingPeselsNumbers = "";
        return View();
      }

      var existingPeselNumbers = teamsWithPlayers
        .SelectMany(t => t.TeamPlayers)
        .Join(teamsWithPlayers.SelectMany(t => t.Players),
          tp => tp.PlayerId,
          p => p.Id,
          (tp, p) => p.Pesel)
        .ToList();

      _existingPeselsNumbers = string.Join(",", existingPeselNumbers);

      var dto = new List<TeamViewModel>();

      foreach (Team team in teamsWithPlayers)
      {
        dto.Add(
          TeamViewModel.FromTeam(team)
          );
      }

      return View(dto);
    }
    
    return View();
  }

  [HttpGet]
  public IActionResult Create()
  {
    TeamViewModel team = new TeamViewModel();
    team.Players.Add(new PlayerViewModel() { Id = 1 });
    team.TeamPlayers.Add(new TeamPlayerViewModel() { });
    team.ExistingPeselNumbers = _existingPeselsNumbers;

    return View(team);
  }

  [HttpPost]
  public async Task<IActionResult> Create(TeamViewModel viewModel)
  {
    string? currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    if (currentUserId != null)
    {
      Team team = new Team(currentUserId, viewModel.Name, viewModel.City, viewModel.NumberOfPlayers);
      foreach (PlayerViewModel newPlayer in viewModel.Players)
      {
        //add a pesel validation here and if it exists in viewModel.ExistingPeselNumbers then redirect to Create again
        //but with an error (see EventManagerController there is an example there when i catch the exception and do the redirect
        //showing exceptions message in the front)
        team.AddPlayer(
            new Player(newPlayer.Name, newPlayer.Surname, newPlayer.Pesel)
          );
      }

      await _teamRepository.AddAsync(team);

      //i'm not sure if the indexes of viewModel.TeamPlayer will always be the same as in _teamPlayers, test it
      for (int i = 0; i < viewModel.TeamPlayers.Count; i++)
      {
        team.UpdateTeamPlayer(i, viewModel.TeamPlayers[i].Number);
      }

      await _teamRepository.UpdateAsync(team);
      await _teamRepository.SaveChangesAsync();
    }

    return RedirectToAction("Index");
  }

  [HttpGet]
  public async Task<IActionResult> Edit(int id)
  {
    TeamByIdWithPlayersSpec spec = new TeamByIdWithPlayersSpec(id);
    Team? team = await _teamRepository.FirstOrDefaultAsync(spec);

    if (team == null)
    {
      return NotFound();
    }

    var dto = TeamViewModel.FromTeam(team);
    dto.ExistingPeselNumbers = _existingPeselsNumbers;

    return View(dto);
  }

  [HttpPost]
  public async Task<IActionResult> Edit(TeamViewModel viewModel)
  {
    TeamByIdWithPlayersSpec spec = new TeamByIdWithPlayersSpec(viewModel.Id);
    Team? team = await _teamRepository.FirstOrDefaultAsync(spec);
    if (team == null || team.Players.IsNullOrEmpty())
    {
      return NotFound();
    }

    team.UpdateTeam(viewModel.Name, viewModel.City, viewModel.NumberOfPlayers);

    foreach(PlayerViewModel playerViewModel in viewModel.Players)
    {
      Player? player = team.Players.FirstOrDefault(p => p.Id == playerViewModel.Id);
      team.UpsertPlayer(player, playerViewModel.Name, playerViewModel.Surname, playerViewModel.Pesel);
    }

    //tu się psuje, bo nie może rozpoznać gracza, że istnieje na podstawie danych innych niż ID, moze trzeba zmienić
    //żeby zamiast Contains używało foreacha też po liście z modelu podanej i porównywało ich po peselach.
    //team.DeletOldPlayers(viewModel.getPlayersList());
      
    await _teamRepository.UpdateAsync(team);

    //i'm not sure if the indexes of viewModel.TeamPlayer will always be the same as in _teamPlayers, test it
    for (int i = 0; i < viewModel.TeamPlayers.Count; i++)
    {
      team.UpdateTeamPlayer(i, viewModel.TeamPlayers[i].Number);
    }

    await _teamRepository.UpdateAsync(team);

    await _teamRepository.SaveChangesAsync();

    return RedirectToAction("Index");
  }

  [HttpGet]
  public async Task<IActionResult> Delete(int id)
  {
    TeamByIdSpec spec = new TeamByIdSpec(id);
    Team? team = await _teamRepository.FirstOrDefaultAsync(spec);

    if (team == null)
    {
      return NotFound();
    }

    var dto = TeamViewModel.FromTeam(team);

    return View(dto);
  }

  [HttpPost]
  public async Task<IActionResult> Delete(TeamViewModel viewModel)
  {
    TeamByIdWithPlayersSpec spec = new TeamByIdWithPlayersSpec(viewModel.Id);
    Team? team = await _teamRepository.FirstOrDefaultAsync(spec);

    if (team == null )
    {
      return NotFound();
    }

    team.Archive();
    await _teamRepository.UpdateAsync(team);
    
    return RedirectToAction("Index");
  }

  [HttpGet]
  public async Task<IActionResult> Details(int id)
  {
    TeamByIdWithPlayersSpec spec = new TeamByIdWithPlayersSpec(id);
    Team? team = await _teamRepository.FirstOrDefaultAsync(spec);

    if (team == null)
    {
      return NotFound();
    }

    var dto = TeamViewModel.FromTeam(team);

    return View(dto);
  }
}
