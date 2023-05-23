using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace SportEventManager.Core.EventAggregate.Specifications;
public class EventWithItemsSpec : Specification<Event>
{
  public EventWithItemsSpec()
  {
    Query
        .Include(selectEvent => selectEvent.Teams)
        .Include(selectEvent => selectEvent.Stadiums)
        .Include(selectEvent => selectEvent.Matches);
  }
}
