using Ardalis.Specification;

namespace SportEventManager.Core.EventAggregate.Specifications;

public class EventsByOwnerIdSpec : Specification<Event>
{
  public EventsByOwnerIdSpec(string? ownerId)
  {
    Query
      .Where(e => e.OwnerId == ownerId);
  }
}
