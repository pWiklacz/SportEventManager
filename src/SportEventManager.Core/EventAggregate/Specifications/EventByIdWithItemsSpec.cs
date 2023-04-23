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
  public EventByIdWithItemsSpec(int teamId)
  {
    Query
       .Where(team => team.Id == teamId);
  }
}
