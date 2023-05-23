using Ardalis.Specification;

namespace SportEventManager.Core.EventAggregate.Specifications;
public class EventByIdWithTeamsAndStadiumsSpec : Specification<Event>, ISingleResultSpecification
{
  public EventByIdWithTeamsAndStadiumsSpec(int eventId)
  {
    Query
        .Where(selectEvent => selectEvent.Id == eventId)
        .Include(selectEven => selectEven.Teams)
        .Include(selectEven => selectEven.Stadiums);
  }
}

