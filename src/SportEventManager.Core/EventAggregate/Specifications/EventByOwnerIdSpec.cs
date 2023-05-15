using Ardalis.Specification;

namespace SportEventManager.Core.EventAggregate.Specifications;

public class EventByOwnerIdSpec : Specification<Event>, ISingleResultSpecification
{
  public EventByOwnerIdSpec(string ownerId)
  {
    Query
      .Where(e => e.OwnerId == ownerId);
  }
}
