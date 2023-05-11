using System.ComponentModel.DataAnnotations;
using SportEventManager.Core.StatisticsAggregate;
using SportEventManager.Core.TeamAggregate;
using SportEventManager.Web.ViewModels.TeamModel.Stats;

namespace SportEventManager.Web.ViewModels.TeamModel;

public class PlayerViewModel
{
  public int Id { get; set; }
  public string Name { get; set; } = string.Empty;

  public string Surname { get; set; } = string.Empty;

  //[Range(1, 99)]
  //public int Number { get; set; }

  public bool IsDeleted { get; private set; }

  public FbPlayerStatsViewModel? FbPlayerStats { get; set; }

  public static PlayerViewModel FromPlayer(Player player)
  {
    return new PlayerViewModel()
    {
      Id = player.Id,
      Name = player.Name,
      Surname = player.Surname,
      //Number = player.Number,
      IsDeleted = player.IsArchived,
      FbPlayerStats = FbPlayerStatsViewModel.FromPlayerStats(fBPlayerStats: (FbPlayerStats?) player.FbPlayerStats?.FootballStats)
    };
  }
}
