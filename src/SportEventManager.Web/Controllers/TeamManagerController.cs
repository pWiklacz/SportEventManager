using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
  private readonly IRepository<User> _userRepository;

  public TeamManagerController(IRepository<Team> teamRepository, IRepository<User> userRepository)
  {
    _teamRepository = teamRepository;
    _userRepository = userRepository;
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
  public IActionResult Create()
  {
    TeamViewModel team = new TeamViewModel();
    team.Players.Add(new PlayerViewModel() { Id = 1 });
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
            new Player(newPlayer.Name, newPlayer.Surname, "12345678900")
          //newPlayer.Number
          );
      }
      await _teamRepository.AddAsync(team);
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
      //TODO: make sure the player instantiates ok with player2Team also
      team.AddPlayer(
          new Player(newPlayer.Name, newPlayer.Surname, "12345678900")
         // newPlayer.Number
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
      player.Archive(); ///why it's not working?
    team.Archive();
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
