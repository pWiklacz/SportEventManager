using Ardalis.Specification;

namespace SportEventManager.Core.EventAggregate.Specifications;
public class EventByIdWithTeamSpec : Specification<Event>, ISingleResultSpecification
{
  public EventByIdWithTeamSpec(int eventId)
  {
    Query
        .Where(selectEvent => selectEvent.Id == eventId)
        .Include(selectEven => selectEven.Teams)
        .Include(selectEven => selectEven.Stadiums);
  }
}

