using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SportEventManager.Core.TeamAggregate.Specifications;
public class TeamWhereEventIdNull : Specification<Team>, ISingleResultSpecification
{
  public TeamWhereEventIdNull(int teamId)
  {
    Query
    .Where(team => team.Id == teamId);
  }

}
