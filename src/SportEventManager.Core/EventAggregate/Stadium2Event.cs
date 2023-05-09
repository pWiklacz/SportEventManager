using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportEventManager.SharedKernel;

namespace SportEventManager.Core.EventAggregate;
public class Stadium2Event : EntityBase
{
  [Required]
  [ForeignKey("Event")]
  public int EventId { get; private set; }

  [Required]
  [ForeignKey("Stadium")]
  public int StadiumId { get; private set; }

  public Stadium2Event(int eventId, int stadiumId)
  {
    EventId = eventId;
    StadiumId = stadiumId;
  }

  public Stadium2Event() { }
}
