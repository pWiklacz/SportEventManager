using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportEventManager.Core.StatsAggregate;
abstract class FootBallStats
{
  public int Goals { get; set; }

  public int Assist { get; set; }

  public int RedCards { get; set; }

  public int YellowCards { get; set; }

}
