using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore.Query;

namespace SportEventManager.Core.EventAggregate.Specifications;
public class EventByIdSpec : Specification<Event>, ISingleResultSpecification
{
  public EventByIdSpec(int eventId)
  {
    Query
      .Where(sportEvent => sportEvent.Id == eventId);
  }
}
