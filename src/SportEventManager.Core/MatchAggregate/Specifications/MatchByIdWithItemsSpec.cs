using Ardalis.Specification;

namespace SportEventManager.Core.MatchAggregate.Specifications;

public class MatchByIdWithItemsSpec : Specification<Match>, ISingleResultSpecification
{
  public MatchByIdWithItemsSpec(int matchId)
  {
    Query
      .Where(selectMatch => selectMatch.Id == matchId)
      .Where(selectMatch => selectMatch.IsArchived == false)
      .Include(selectMatch => selectMatch.Stadium)
      .Include(selectMatch => selectMatch.HomeTeam)
      .Include(selectMatch => selectMatch.GuestTeam)
      .Include(selectMatch => selectMatch.GuestTeamStats)
      .Include(selectMatch => selectMatch.HomeTeamStats)
      .Include(selectMatch => selectMatch.PlayersStats)
      .ThenInclude(ps => ps.Player);
  }
}
