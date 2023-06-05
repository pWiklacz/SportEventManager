using Ardalis.GuardClauses;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SportEventManager.Core.StatisticsAggregate;

public class FbTeamMatchStats : FootballStatsBase
{
  [DefaultValue(0)] public int Shoots { get; set; } = 0;

  [DefaultValue(0)] public int ShootsOnTarget { get; set; } = 0;

  [DefaultValue(0)] public int Fouls { get; set; } = 0;

  [DefaultValue(0)] public int Passes { get; set; } = 0;

  public FbTeamMatchStats() : base() { }
}
