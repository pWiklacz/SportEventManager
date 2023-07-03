using System.Security.Claims;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SportEventManager.Core.TeamAggregate;
using SportEventManager.Core.TeamAggregate.Specifications;
using SportEventManager.SharedKernel.Interfaces;
using SportEventManager.Web.ViewModels.TeamModel;

namespace SportEventManager.Web.Controllers;

[Authorize(Roles = "Admin,TeamManager")]
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

    if (currentUserId != null)
    {
      var teamsWithPlayers = await _teamRepository.ListAsync(new TeamsWithPlayersByOwnerIdSpec(currentUserId));

      if (teamsWithPlayers == null)
      {
        return View();
      }

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
  public IActionResult Create(string error = "")
  {
    TeamViewModel team = new TeamViewModel(error);
    team.Players.Add(new PlayerViewModel() { Id = 1 });
    team.TeamPlayers.Add(new TeamPlayerViewModel() { });

    return View(team);
  }

  [HttpPost]
  public async Task<IActionResult> Create(TeamViewModel viewModel)
  {
    string? currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    List<string>? existingPeselNumbers = null;
    List<string>? existingTeamsTags = null;

    if (currentUserId != null)
    {
      var teamsWithPlayers = await _teamRepository.ListAsync(new TeamsWithPlayersByOwnerIdSpec(currentUserId));
      if (teamsWithPlayers != null)
      {
        existingPeselNumbers = GetPeselNumbersFromExistingTeams(teamsWithPlayers);
        existingTeamsTags = GetTagsFromExistingTeams(teamsWithPlayers);
      }

      Team team;

      try {
        team = new Team(
          currentUserId,
          viewModel.Name,
          viewModel.Tag,
          viewModel.City,
          viewModel.NumberOfPlayers,
          existingTeamsTags
          );
      } catch (Exception ex) {
        return RedirectToAction("Create", new { error = ex.Message });
      }

      foreach (PlayerViewModel newPlayer in viewModel.Players)
      {
        try
        {
          team.AddPlayer(
            new Player(newPlayer.Name, newPlayer.Surname, newPlayer.Pesel), existingPeselNumbers
          );
        }
        catch (Exception ex)
        {
          //extract FreePeselToAnExternal list
          if(teamsWithPlayers != null && GetFreePeselList(teamsWithPlayers)!.Contains(newPlayer.Pesel)) {
            team.AddPlayer(
              newPlayer: GetExistingPlayerByPesel(teamsWithPlayers!, newPlayer.Pesel)!,
              existingPeselNumbers: existingPeselNumbers
            );
          } else {
            return RedirectToAction("Create", new { error = ex.Message });
          }
        }
      }

      //check if ok
      await _teamRepository.AddAsync(team);//wywala się, że nie ma id playera, a wcześniej samo brało do TeamPlayer :(

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
  public async Task<IActionResult> Edit(int id, string error = "")
  {
    TeamByIdWithPlayersSpec spec = new TeamByIdWithPlayersSpec(id);
    Team? team = await _teamRepository.FirstOrDefaultAsync(spec);

    if (team == null)
    {
      return NotFound();
    }
    var dto = TeamViewModel.FromTeam(team);
    dto.BackendError = error;

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

    string? currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    List<string>? existingPeselNumbers = null;
    List<string>? existingTeamsTags = null;

    if (currentUserId != null)
    {
      var teamsWithPlayers = await _teamRepository.ListAsync(new TeamsWithPlayersByOwnerIdSpec(currentUserId));
      if (teamsWithPlayers != null)
      {
        existingPeselNumbers = GetPeselNumbersFromExistingTeams(teamsWithPlayers);
        existingTeamsTags = GetTagsFromExistingTeams(teamsWithPlayers);
      }

      try
      {
        team.UpdateTeam(
          viewModel.Name,
          viewModel.Tag,
          viewModel.City,
          viewModel.NumberOfPlayers,
          existingTeamsTags
        );
      }
      catch (Exception ex)
      {
        return RedirectToAction("Edit", new { id = viewModel.Id, error = ex.Message });
      }

      foreach (PlayerViewModel playerViewModel in viewModel.Players)
      {
        try
        {
          Player? player = team.Players.FirstOrDefault(p => p.Id == playerViewModel.Id);
          team.UpsertPlayer(
            player,
            playerViewModel.Name,
            playerViewModel.Surname,
            playerViewModel.Pesel,
            existingPeselNumbers
          );
        }
        catch (Exception ex)
        {
          if (teamsWithPlayers != null && GetFreePeselList(teamsWithPlayers)!.Contains(playerViewModel.Pesel))
          {
            team.AddPlayer(
              newPlayer: GetExistingPlayerByPesel(teamsWithPlayers!, playerViewModel.Pesel)!,
              existingPeselNumbers: existingPeselNumbers
            );
          } else {
            return RedirectToAction("Edit", new { id = viewModel.Id, error = ex.Message });
          }
        }
      }

      team.DeleteOldPlayers(viewModel.getPlayersList());

      await _teamRepository.UpdateAsync(team);  //check if ok

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

    if (team == null)
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

  private List<string>? GetPeselNumbersFromExistingTeams(List<Team> teamsWithPlayers)
  {
    return teamsWithPlayers
        .SelectMany(t => t.TeamPlayers)
        .Join(teamsWithPlayers.SelectMany(t => t.Players),
          tp => tp.PlayerId,
          p => p.Id,
          (tp, p) => p.Pesel)
        .ToList();
  }

  private List<string>? GetTagsFromExistingTeams(List<Team> existingTeams)
  {
    return (List<string>?)existingTeams
      .SelectMany(t => t.Tag)
      .Cast<string>();  //don't know why the .ToList() method suggest the type is <char> and compiler say it's an error
  }

  private Player? GetExistingPlayerByPesel(List<Team> existingTeams, string pesel)
  {
      return (Player?)existingTeams
        .Select(
          t => t.Players.FirstOrDefault(
            p => p.Pesel == pesel
            )
          );
  }

  private List<string>? GetFreePeselList(List<Team> existingTeams)
  {
    //get the list of pesel numbers which has the leavOn date earlier than today
    return new List<string>();
  }
}
