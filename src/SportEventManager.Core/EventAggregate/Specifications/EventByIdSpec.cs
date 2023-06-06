using Ardalis.Specification;

namespace SportEventManager.Core.EventAggregate.Specifications;
public class EventByIdSpec : Specification<Event>, ISingleResultSpecification
{
  public EventByIdSpec(int eventId)
  {
    Query
      .Where(sportEvent => sportEvent.Id == eventId);
  }
}
