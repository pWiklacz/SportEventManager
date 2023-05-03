using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using SportEventManager.Core.TeamAggregate;

namespace SportEventManager.Core.EventAggregate.Specification;
public class EventByIdWithItemsSpec : Specification<Event>, ISingleResultSpecification
{
  public EventByIdWithItemsSpec(int eventId)
  {
    Query
       .Where(selectEvent=> selectEvent.Id == eventId)
       .Where(selectEvent => selectEvent.IsDeleted == false)
       .Include(selectEven => selectEven.Teams)
       .Include(selectEven => selectEven.stadiums) 
       .Include(selectEven => selectEven.Matches);
  }
}
