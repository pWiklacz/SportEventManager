using Ardalis.Specification;

namespace SportEventManager.Core.EventAggregate.Specifications;
public class EventsWithTeamsSpec : Specification<Event>
{
  public EventsWithTeamsSpec()
  {
    Query
        .Include(selectEvent => selectEvent.Teams);
  }
}
