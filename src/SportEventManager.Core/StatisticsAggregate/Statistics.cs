using System.ComponentModel.DataAnnotations.Schema;
using SportEventManager.SharedKernel;
using SportEventManager.SharedKernel.Interfaces;

namespace SportEventManager.Core.StatisticsAggregate;
public class Statistics : EntityBase, IAggregateRoot
{
  [NotMapped]
  public FootballStatsBase FootballStats { get; set; } = new FbTeamStats(); //don't know which just took one

  public Statistics() { }

  public Statistics(FootballStatsBase footballStats)
  {
    FootballStats = footballStats;
  }

  //1. Always initialize the FootballStats property with one of the classes derived from FootballStatsBase
  //2. Add any methods that you need for managing the derived classes
}
