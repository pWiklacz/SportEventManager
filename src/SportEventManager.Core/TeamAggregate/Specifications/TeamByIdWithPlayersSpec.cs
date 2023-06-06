using Ardalis.Specification;

namespace SportEventManager.Core.TeamAggregate.Specifications;
public class TeamByIdWithPlayersSpec : Specification<Team>, ISingleResultSpecification
{
  public TeamByIdWithPlayersSpec(int teamId)
  {
    Query
        .Where(team => team.Id == teamId)
        .Include(t => t.TeamPlayers.Where(tp => tp.LeaveOn == null))
        .Include(team => team.Players)
        .Where(t => !t.IsArchived);
  }
}
