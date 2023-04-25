using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SportEventManager.Core.TeamAggregate.Specifications;
public class TeamByIdSpec : Specification<Team>, ISingleResultSpecification
{
  public TeamByIdSpec(int teamId)
  {
    Query
        .Where(team => team.Id == teamId);
  }
}
