using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using Microsoft.Extensions.Logging;

namespace SportEventManager.Core.EventAggregate.Specifications;
public class EventsByOwnerIdWithItemsSpec : Specification<Event>
{
  public EventsByOwnerIdWithItemsSpec(string? ownerId) {
    Query
        .Where(selectEvent => selectEvent.OwnerId == ownerId)
        .Include(selectEvent => selectEvent.Teams)
        .Include(selectEvent => selectEvent.Stadiums)
        .Include(selectEvent => selectEvent.Matches);
  }
}
