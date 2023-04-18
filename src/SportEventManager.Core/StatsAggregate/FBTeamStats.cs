using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportEventManager.Core.StatsAggregate;
internal class FBTeamStats : FootBallStats
{
  public int Wins { get; set; }

  public int Losses { get; set; }

  public int Drawes { get; set; }
  
  public int Shoots { get; set; }

  public int ShootsOnTarger { get; set; }

  public int Fouls { get; set; }

  public int Passes { get; set; }
}
