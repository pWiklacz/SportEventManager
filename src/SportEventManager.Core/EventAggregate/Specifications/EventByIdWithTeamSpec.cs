using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using SportEventManager.Core.TeamAggregate;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SportEventManager.Core.EventAggregate.Specifications;
public class EventByIdWithTeamSpec : Specification<Event>, ISingleResultSpecification
{
  public EventByIdWithTeamSpec(int eventId)
  {
    Query
        .Where(selectEvent => selectEvent.Id == eventId)
        .Include(selectEven => selectEven.Teams)
        .Include(selectEven => selectEven.stadiums);
  }
}

