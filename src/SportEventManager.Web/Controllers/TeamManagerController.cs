using System.Security.Claims;
using Ardalis.Specification;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SportEventManager.Core.StatisticsAggregate;
using SportEventManager.Core.TeamAggregate;
using SportEventManager.Core.TeamAggregate.Specifications;
using SportEventManager.Core.UserAggregate;
using SportEventManager.SharedKernel.Interfaces;
using SportEventManager.Web.ViewModels.TeamModel;
using SportEventManager.Web.ViewModels.TeamModel.Stats;

namespace SportEventManager.Web.Controllers;

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
    TeamsByOwnerIdSpec spec;

    if (currentUserId != null)
    {
      spec = new TeamsByOwnerIdSpec(currentUserId);

      var teams = await _teamRepository.ListAsync(spec);
      var teamsPesel = await _teamRepository.ListAsync(new TeamsWithPlayersSpec());
      var existingPeselNumbers = teamsPesel
      .SelectMany(t => t.TeamPlayers)
      .Where(tp => tp.LeaveOn == null)
      .Join(teams.SelectMany(t => t.Players),
          tp => tp.PlayerId,
          p => p.Id,
          (tp, p) => p.Pesel)
      .ToList();

      _existingPeselsNumbers = string.Join(",", existingPeselNumbers);

      if (teams == null)
      {
        return View();
      }

      var dto = new List<TeamViewModel>();

      foreach (Team team in teams)
      {
        dto.Add(
          new TeamViewModel
          {
            Id = team.Id,
            Name = team.Name,
            City = team.City,
            IsDeleted = team.IsArchived,
            NumberOfPlayers = team.NumberOfPlayers,
            FbTeamStats = FbTeamStatsViewModel.FromTeamStats(fBTeamStats: (FbTeamStats?)team.FbTeamWholeStats?.FootballStats)
          });
      }
      return View(dto);
    }
    else { return View(); }
  }

  [HttpGet]
  public async Task<IActionResult> Create()
  {
    TeamViewModel team = new TeamViewModel();
    team.Players.Add(new PlayerViewModel() { Id = 1 });
    team.TeamPlayers.Add(new TeamPlayerViewModel() { });

    var teams = await _teamRepository.ListAsync(new TeamsWithPlayersSpec());
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
        team.AddPlayer(
            new Player(newPlayer.Name, newPlayer.Surname, newPlayer.Pesel)
          );
      }

      await _teamRepository.AddAsync(team);

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

    var dto = new TeamViewModel
    {
      Id = team.Id,
      Name = team.Name,
      City = team.City,
      NumberOfPlayers = team.NumberOfPlayers,
      TeamPlayers = team.TeamPlayers
                    .Select(teamPlayer => TeamPlayerViewModel.FromTeamPlayer(teamPlayer))
                    .ToList(),
      Players = team.Players
                    .Select(player => PlayerViewModel.FromPlayer(player))
                    .ToList()
      
    };

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

    team.Name = viewModel.Name;
    team.City = viewModel.City;
    team.NumberOfPlayers = viewModel.NumberOfPlayers;


    for(int i = 0; i < viewModel.Players.Count; i++)
    {
      PlayerViewModel playerViewModel = viewModel.Players[i];
      Player? player = team.Players.FirstOrDefault(p => p.Id == playerViewModel.Id);
      if (player != null)
      {
        team.UpdatePlayer(player.Id, viewModel.Players[i].Name, viewModel.Players[i].Surname, viewModel.Players[i].Pesel);
      }
      else
      {
        team.AddPlayer(new Player(viewModel.Players[i].Name, viewModel.Players[i].Surname, viewModel.Players[i].Pesel)); 
      }
    }
      
    await _teamRepository.UpdateAsync(team);

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

    var dto = new TeamViewModel
    {
      Id = team.Id,
      Name = team.Name,
      City = team.City,
      NumberOfPlayers = team.NumberOfPlayers
    };

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

    var dto = new TeamViewModel
    {
      Id = team.Id,
      Name = team.Name,
      City = team.City,
      NumberOfPlayers = team.NumberOfPlayers,
      Players = team.Players
                    .Select(player => PlayerViewModel.FromPlayer(player))
                    .ToList()
    };

    return View(dto);
  }
}
