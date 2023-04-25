using System.ComponentModel;
using SportEventManager.SharedKernel;

namespace SportEventManager.Core.TeamAggregate.Stats;
public abstract class FootballStats : EntityBase
{
  [DefaultValue(0)]
  public int Goals { get; set; }

  [DefaultValue(0)]
  public int Assists { get; set; }

  [DefaultValue(0)]
  public int RedCards { get; set; }

  [DefaultValue(0)]
  public int YellowCards { get; set; }

  public FootballStats() : base()
  {
    Goals = 0;
    Assists = 0;
    RedCards = 0;
    YellowCards = 0;
  }

}
