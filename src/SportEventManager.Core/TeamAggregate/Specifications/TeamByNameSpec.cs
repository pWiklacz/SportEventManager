using Ardalis.Specification;

namespace SportEventManager.Core.TeamAggregate.Specifications;
public class TeamByNameSpec : Specification<Team>, ISingleResultSpecification
{
  public TeamByNameSpec(string teamName)
  {
    Query
     .Where(team => team.Name == teamName);

  }
}
