﻿using Ardalis.Specification;

namespace SportEventManager.Core.EventAggregate.Specification;
public class EventByIdWithItemsSpec : Specification<Event>, ISingleResultSpecification
{
  public EventByIdWithItemsSpec(int eventId)
  {
    Query
       .Where(selectEvent=> selectEvent.Id == eventId)
       .Where(selectEvent => selectEvent.IsArchived == false)
       .Include(selectEven => selectEven.Teams)
       .Include(selectEven => selectEven.Stadiums) 
       .Include(selectEven => selectEven.Matches);
  }
}
