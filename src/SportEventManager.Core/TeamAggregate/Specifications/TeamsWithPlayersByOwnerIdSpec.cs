using Ardalis.Specification;

namespace SportEventManager.Core.TeamAggregate.Specifications;
public class TeamsWithPlayersByOwnerIdSpec : Specification<Team>
{
  public TeamsWithPlayersByOwnerIdSpec(string ownerId)
  {
    Query
      .Include(t => t.TeamPlayers.Where(tp => tp.LeaveOn == null))
      .Include(p => p.Players)
      .Where(t => !t.IsArchived)
      .Where(team => team.OwnerId == ownerId);
  }
}
