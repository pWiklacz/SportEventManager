using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace SportEventManager.Core.TeamAggregate.Specifications;
public class TeamByNameSpec : Specification<Team>, ISingleResultSpecification
{
  public TeamByNameSpec(string teamNames)
  {
    Query
     .Where(team => team.Name == teamNames);

  }
}
