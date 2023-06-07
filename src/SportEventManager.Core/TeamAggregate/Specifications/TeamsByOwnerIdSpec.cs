using Ardalis.Specification;

namespace SportEventManager.Core.TeamAggregate.Specifications;
public class TeamsByOwnerIdSpec : Specification<Team>, ISingleResultSpecification
{
  public TeamsByOwnerIdSpec(string ownerId)
  {
    Query
        .Where(team => team.OwnerId == ownerId)
        .Where(team => !team.IsArchived);
  }
}
