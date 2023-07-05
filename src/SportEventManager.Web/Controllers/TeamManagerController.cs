using System.Data;
using System.Linq;
using System.Numerics;
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
      teamsWithPlayers = RemoveOldPlayers(teamsWithPlayers);
     
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
    List<string>? freePeselNumbersList = null;

    if (currentUserId != null)
    {
      var teamsWithPlayers = await _teamRepository.ListAsync(new TeamsWithPlayersByOwnerIdSpec(currentUserId));
      //teamsWithPlayers = RemoveOldPlayers(teamsWithPlayers);

      if (teamsWithPlayers != null)
      {
        existingPeselNumbers = GetPeselNumbersFromExistingTeams(teamsWithPlayers)!;
        existingTeamsTags = GetTagsFromExistingTeams(teamsWithPlayers)!;
        freePeselNumbersList = GetFreePeselList(teamsWithPlayers)!;
      }

      Team team;

      try {
        team = new Team(
          currentUserId,
          viewModel.Name,
          viewModel.Tag.ToUpper(),
          viewModel.City,
          viewModel.NumberOfPlayers,
          existingTeamsTags
          );
      } catch (Exception ex) {
        return RedirectToAction("Create", new { error = ex.Message });
      }

      await _teamRepository.AddAsync(team);

      foreach (PlayerViewModel newPlayer in viewModel.Players)
      {
        try
        {
          team.AddPlayer(
            new Player(newPlayer.Name, newPlayer.Surname, newPlayer.Pesel), existingPeselNumbers
          );
        }
        catch (Exception ex) //player's pesel exists
        {
          //add existing player to team if he's free
          if (freePeselNumbersList!.Contains(newPlayer.Pesel))
          {
            team.AddPlayer(
              newPlayer: GetExistingPlayerByPesel(teamsWithPlayers!, newPlayer.Pesel)!,
              existingPeselNumbers: existingPeselNumbers,
              peselIsValidatedAlready: true
            );
          }
          else
          { //show a communicate that the player is already in use
            return RedirectToAction("Create", new { error = ex.Message });
          }
        }
      }

      await _teamRepository.UpdateAsync(team);

      for (int i = 0; i < viewModel.TeamPlayers.Count; i++)
      {
        team.UpdateTeamPlayer(i, viewModel.TeamPlayers[i].Number); //check if ok
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

    team = RemoveOldPlayersFromTeam(team);

    var dto = TeamViewModel.FromTeam(team!);
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
    List<string>? freePeselNumbersList = null;

    if (currentUserId != null)
    {
      var teamsWithPlayers = await _teamRepository.ListAsync(new TeamsWithPlayersByOwnerIdSpec(currentUserId));
      if (teamsWithPlayers != null)
      {
        existingPeselNumbers = GetPeselNumbersFromExistingTeams(teamsWithPlayers)!;
        existingTeamsTags = GetTagsFromExistingTeams(teamsWithPlayers)!;
        freePeselNumbersList = GetFreePeselList(teamsWithPlayers)!;
      }

      try
      {
        team.UpdateTeam(
          viewModel.Name,
          viewModel.Tag.ToUpper(),
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
        catch (Exception ex) //player's pesel exists
        {
          //use existing player to team if he's free
          if (freePeselNumbersList!.Contains(playerViewModel.Pesel))
          {
            team.UpsertPlayer(
              GetExistingPlayerByPesel(teamsWithPlayers!, playerViewModel.Pesel)!,
              playerViewModel.Name,
              playerViewModel.Surname,
              playerViewModel.Pesel,
              existingPeselNumbers,
              peselIsValidatedAlready: true
            );
          }
          else
          { //show a communicate that the player is already in use
            return RedirectToAction("Edit", new { id = viewModel.Id, error = ex.Message });
          }
        }
      }

      team.DeleteOldPlayers(viewModel.getPlayersList());//check if ok

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

    team = RemoveOldPlayersFromTeam(team);

    team!.Archive();
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
    List<string> tags = new List<string>();
    foreach(var team in existingTeams)
    {
      if(!tags.Contains(team.Tag.ToUpper()))
      {
        tags.Add(team.Tag.ToUpper());
      }
    }
    return tags;
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

  /**
   * Get the list of Player.Pesel numbers which has the TeamPlayer.LeaveOn date earlier than today
   */
  private List<string>? GetFreePeselList(List<Team> existingTeams)
  {  
    return existingTeams
    .SelectMany(t => t.TeamPlayers)
    .Join(existingTeams.SelectMany(t => t.Players),
        tp => tp.PlayerId,
        p => p.Id,
        (tp, p) => new { TeamPlayer = tp, Player = p }) // Include both TeamPlayer and Player objects
      .Where(tpp => tpp.TeamPlayer.LeaveOn != null &&
          tpp.TeamPlayer.LeaveOn < DateTime.Now)
      .Select(tpp => tpp.Player.Pesel)
      .ToList();
  }

  //this should change the list of teams to remove the players which are 
  //no longer in the team due to TeamPlayer.LeaveOn property filled
  //i was trying to remove it from here or from migration but none of this works
  private List<Team>? RemoveOldPlayers(List<Team> existingTeams)
  {
    foreach(Team team in existingTeams)
    {
      for(int i = 0; i < team.TeamPlayers.Count; i++)
      {
        if (team.TeamPlayers.ElementAt(i).LeaveOn != null &&
          team.TeamPlayers.ElementAt(i).LeaveOn < DateTime.Now)
        {
          //some exceptions cause it's broken
         // team.Players.Remove(team.Players.ElementAt(i));
          //team.TeamPlayers.Remove(team.TeamPlayers.ElementAt(i));
        }
      }
    }
    return existingTeams;
  }

  //this should change the team to remove the players which are 
  //no longer in the team due to TeamPlayer.LeaveOn property filled
  //i was trying to remove it from here or from migration but none of this works
  private Team? RemoveOldPlayersFromTeam(Team team)
  {
    for (int i = 0; i < team.TeamPlayers.Count; i++)
    {
      if (team.TeamPlayers.ElementAt(i).LeaveOn != null &&
        team.TeamPlayers.ElementAt(i).LeaveOn < DateTime.Now)
      {
        //some exceptions cause it's broken
        //team.Players.Remove(team.Players.ElementAt(i));
        //team.TeamPlayers.Remove(team.TeamPlayers.ElementAt(i));
      }
    }
    return team;
  }
}
