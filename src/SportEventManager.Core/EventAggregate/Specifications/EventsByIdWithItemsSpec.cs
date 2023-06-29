using Ardalis.Specification;

namespace SportEventManager.Core.EventAggregate.Specification;
public class EventsByIdWithItemsSpec : Specification<Event>, ISingleResultSpecification
{
  public  EventsByIdWithItemsSpec(int eventId)
  {
    Query
      .Where(selectEvent => selectEvent.Id == eventId)
      .Where(selectEvent => selectEvent.IsArchived == false)
      .Include(selectEven => selectEven.Teams)
      .Include(selectEven => selectEven.Stadiums)
      .Include(selectEven => selectEven.Matches)
      .ThenInclude(match => match.GuestTeamStats)
      .Include(selectEven => selectEven.Matches)
      .ThenInclude(match => match.HomeTeamStats)
      .Include(selectEven => selectEven.Matches)
      .ThenInclude(match => match.PlayersStats)
    .ThenInclude(ps => ps.Player);
    //.Include(selectEven => selectEven.Matches)
    //.ThenInclude(match => match.HomeTeam.Players)
    //.Include(selectEven => selectEven.Matches)
    //.ThenInclude(match => match.GuestTeam.Players)

  }
}
