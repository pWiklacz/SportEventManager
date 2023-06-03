using Ardalis.Specification;

namespace SportEventManager.Core.TeamAggregate.Specifications;

public class TeamByOwnerIdSpec : Specification<Team>, ISingleResultSpecification
{
  public TeamByOwnerIdSpec(string ownerId)
  {
    Query
      .Where(team => team.OwnerId == ownerId);
  }
}
