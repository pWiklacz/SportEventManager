using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SportEventManager.Core.TeamAggregate.Specifications;
public class TeamsWithPlayersSpec : Specification<Team>, ISingleResultSpecification
{
  public TeamsWithPlayersSpec()
  {
    Query
      .Include(t => t.TeamPlayers)
      .Include(p => p.Players);

  }
}
