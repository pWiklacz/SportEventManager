using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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

  [Required]
  [DefaultValue(false)]
  public bool IsArchived { get; private set; } = false;

  public virtual void Archive()
  {
    this.IsArchived = true;
  }

  public FootballStats() : base()
  {
    Goals = 0;
    Assists = 0;
    RedCards = 0;
    YellowCards = 0;
    IsArchived = false;
  }
}
