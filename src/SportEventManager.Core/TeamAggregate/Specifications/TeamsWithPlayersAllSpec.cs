using Ardalis.Specification;

namespace SportEventManager.Core.TeamAggregate.Specifications;
public class TeamsWithPlayersAllSpec : Specification<Team>
{
  public TeamsWithPlayersAllSpec(string ownerId)
  {
    Query
      .Include(t => t.TeamPlayers)
      .Include(p => p.Players)
      .Where(team => team.OwnerId == ownerId);
  }
}
