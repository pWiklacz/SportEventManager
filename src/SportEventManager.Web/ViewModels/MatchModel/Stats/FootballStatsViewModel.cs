using System.ComponentModel;
using SportEventManager.SharedKernel;

namespace SportEventManager.Web.ViewModels.MatchModel.Stats;
public abstract class FootballStatsViewModel
{
  public int Id { get; set; }
  public int Goals { get; set; } = 0;

  public int Assists { get; set; } = 0;

  public int RedCards { get; set; } = 0;

  public int YellowCards { get; set; } = 0;
}
