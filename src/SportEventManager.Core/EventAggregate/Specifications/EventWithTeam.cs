using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SportEventManager.Core.EventAggregate.Specifications;
public class EventWithTeam : Specification<Event>
{
  public EventWithTeam()
  {
    Query
        .Include(selectEven => selectEven.Teams)
        .Include(selectEven => selectEven.stadiums);
  }
}
