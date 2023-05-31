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
  
  public TeamManagerController(IRepository<Team> teamRepository)
  {
    _teamRepository = teamRepository;
    
  }

  public async Task<IActionResult> Index()
  {
    
    string? currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    TeamsByOwnerIdSpec spec;

    if (currentUserId != null)
    {
      spec = new TeamsByOwnerIdSpec(currentUserId);

      var teams = await _teamRepository.ListAsync(spec);

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

    var activeTeamPlayers = teams
        .SelectMany(t => t.TeamPlayers)
        .Where(tp => tp.LeaveOn == null)
        .ToList();

    var activePlayerIds = activeTeamPlayers.Select(tp => tp.PlayerId).ToList();

    var existingPeselNumbers = teams
    .SelectMany(t => t.Players)
    .Where(p => activePlayerIds.Contains(p.Id))
    .Select(p => p.Pesel)
    .ToList();

    string peselNumbersString = string.Join(",", existingPeselNumbers);
    team.ExistingPeselNumbers = peselNumbersString;

    return View(team);
  }

  [HttpPost]
  public async Task<IActionResult> Create(TeamViewModel viewModel)
  {
    //TODO: add owner which is use also User repository and use AddOwner method similar to AddPlayer
    //TODO: Or if it doesn't work because the User already exists then refactor it to have only id of an existing user
    //TODO: refactor below code and use existing userId

    string? currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    if (currentUserId != null)
    {
      Team team = new Team(currentUserId, viewModel.Name, viewModel.City, viewModel.NumberOfPlayers);
      foreach (PlayerViewModel newPlayer in viewModel.Players)
      {
        //TODO: make sure the player instantiates ok with player2Team also
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

    return View(dto);
  }

  [HttpPost]
  public async Task<IActionResult> Edit(TeamViewModel viewModel)
  {
    //TODO: add owner which is use also User repository and use AddOwner method similar to AddPlayer
    //Or if it doesn't work because the User already exists then refactor it to have only id of an existing user
    //Make deleting old players from a team work
    TeamByIdWithPlayersSpec spec = new TeamByIdWithPlayersSpec(viewModel.Id);
    Team? team = await _teamRepository.FirstOrDefaultAsync(spec);
    if (team == null || team.Players.IsNullOrEmpty())
    {
      return NotFound();
    }

    //Adding updated team with new players
    team.Name = viewModel.Name;
    team.City = viewModel.City;
    team.NumberOfPlayers = viewModel.NumberOfPlayers;

    foreach (PlayerViewModel newPlayer in viewModel.Players)
    {
      //TODO: make sure the player instantiates ok with player2Team also
      team.AddPlayer(
          new Player(newPlayer.Name, newPlayer.Surname, newPlayer.Pesel)
        
        ); 
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
