using Ardalis.Specification;

namespace SportEventManager.Core.TeamAggregate.Specifications;
public class TeamsByOwnerIdSpec : Specification<Team>, ISingleResultSpecification
{
  public TeamsByOwnerIdSpec(string ownerId = "f8cf555e-2ad3-428c-ba1e-4057ac72677e")
  {
    Query
        //.Where(team => team.OwnerId == ownerId)
        .Include(team => team.Players);
  }
}
