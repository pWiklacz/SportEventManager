using System.ComponentModel;
using SportEventManager.SharedKernel;

namespace SportEventManager.Web.ViewModels.TeamModel.Stats;
public abstract class FootballStatsViewModel : EntityBase
{
  public int Goals { get; set; } = 0;

  public int Assists { get; set; } = 0;

  public int RedCards { get; set; } = 0;

  public int YellowCards { get; set; } = 0;
}
