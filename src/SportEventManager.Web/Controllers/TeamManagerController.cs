using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SportEventManager.Core.TeamAggregate;
using SportEventManager.Core.TeamAggregate.Specifications;
using SportEventManager.Core.TeamAggregate.Stats;
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
    TeamsByOwnerIdSpec spec = new TeamsByOwnerIdSpec();
    var teams = await _teamRepository.ListAsync();
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
          IsDeleted = team.IsDeleted,
          NumberOfPlayers = team.NumberOfPlayers,
          FbTeamStats = FBTeamStatsViewModel.FromTeamStats(fBTeamStats: team.FbTeamStats)
        });
    }

    return View(dto);
  }

  [HttpGet]
  public IActionResult Create()
  {
    TeamViewModel team = new TeamViewModel();
    team.Players.Add(new PlayerViewModel() { Id = 1 });
    return View(team);
  }

  [HttpPost]
  public async Task<IActionResult> Create(TeamViewModel viewModel)
  {//TODO: add owner
    Team team = new Team(viewModel.Name, viewModel.City, viewModel.NumberOfPlayers);
    foreach(PlayerViewModel newPlayer in viewModel.Players)
    {
      team.AddPlayer(
          new Player(newPlayer.Name, newPlayer.Surname, newPlayer.Number, team.Id)
        );
    }
    await _teamRepository.AddAsync(team);
    await _teamRepository.SaveChangesAsync();

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
      Players = team.Players
                    .Select(player => PlayerViewModel.FromPlayer(player))
                    .ToList()
    };

    return View(dto);
  }

  [HttpPost]
  public async Task<IActionResult> Edit(TeamViewModel viewModel)
  {//TODO: add owner and make deleting work properly
    //Deleting old players from a team
    TeamByIdWithPlayersSpec spec = new TeamByIdWithPlayersSpec(viewModel.Id);
    Team? team = await _teamRepository.FirstOrDefaultAsync(spec);
    if (team == null || team.Players.IsNullOrEmpty())
    {
      return NotFound();
    }

  //  foreach (Player player in team.Players) { 
  //    if(!viewModel.Players.Contains(PlayerViewModel.FromPlayer(player))) { 
  //      player.MarkAsDeleted();
  //    }
  //  }

    //Adding updated team with new players
    team.Name = viewModel.Name;
    team.City = viewModel.City;
    team.NumberOfPlayers = viewModel.NumberOfPlayers;

    foreach (PlayerViewModel newPlayer in viewModel.Players)
    {
      team.AddPlayer(
          new Player(newPlayer.Name, newPlayer.Surname, newPlayer.Number, team.Id)
        );
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
    TeamByIdSpec spec = new TeamByIdSpec(viewModel.Id);
    Team? team = await _teamRepository.FirstOrDefaultAsync(spec);

    if (team == null )
    {
      return NotFound();
    }

    foreach(Player player in team.Players)
      player.MarkAsDeleted(); ///why it's not working?
    team.MarkAsDeleted();
    await _teamRepository.UpdateAsync(team);
    //TODO: delete also teamstats and playerstats
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
