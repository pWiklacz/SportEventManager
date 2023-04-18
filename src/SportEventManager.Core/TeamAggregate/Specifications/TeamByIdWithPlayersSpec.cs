using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SportEventManager.Core.TeamAggregate.Specifications;
public class TeamByIdWithPlayersSpec : Specification<Team>, ISingleResultSpecification
{
  public TeamByIdWithPlayersSpec(int teamId)
  {
    Query
        .Where(team => team.Id == teamId)
        .Include(team => team.Players);
  }
}
