using Ardalis.Specification;

namespace SportEventManager.Core.EventAggregate.Specifications;
public class EventWithTeam : Specification<Event>
{
  public EventWithTeam()
  {
    Query
        .Include(selectEven => selectEven.Teams)
        .Include(selectEven => selectEven.Stadiums);
  }
}
