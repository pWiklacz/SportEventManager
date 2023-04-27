﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SportEventManager.Core.EventAggregate;
using SportEventManager.Core.TeamAggregate.Stats;
using SportEventManager.Web.ViewModels.TeamModel.Stats;

namespace SportEventManager.Web.ViewModels.EventModel;

public class MatchViewModel
{
  public int Id { get; set; }

  public DateTime StartTime { get; set; }

  public DateTime EndTime { get; set; }

  public bool IsEnded { get; set; }

  public int FirstTeamId { get; private set; }

  public int SecondTeamId { get; private set; }

  public List<FBTeamStatsViewModel> FbTeamStats { get; private set; } = new List<FBTeamStatsViewModel>(2);

  public StadiumViewModel? Stadium { get; set; }

  public static MatchViewModel FromMatch(Match match) => new MatchViewModel()
  {
    Id = match.Id,
    StartTime = match.StartTime,
    EndTime = match.EndTime,
    Stadium = StadiumViewModel.FromStadium(stadium : match.Stadium),
    IsEnded = match.IsEnded,
    FirstTeamId = match.FirstTeamId,
    SecondTeamId = match.SecondTeamId,
    FbTeamStats = match.FbTeamStats.Select(fbStats => FBTeamStatsViewModel.FromTeamStats(fbStats)).ToList()
  };
}

