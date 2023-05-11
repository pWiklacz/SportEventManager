using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SportEventManager.SharedKernel;
using Ardalis.GuardClauses;
using SportEventManager.Core.TeamAggregate;
using Microsoft.Extensions.Logging;

namespace SportEventManager.Core.EventAggregate;
public class Event2Stadium : EntityBase
{
  [Required]
  [ForeignKey("Event")]
  public int EventId { get; private set; }

  [Required]
  [ForeignKey("Stadium")]
  public int StadiumId { get; private set; }

  public Event? Event { get; set; }

  public Stadium? Stadium { get; set; }

  public Event2Stadium(int eventId, int stadiumId,Event @event, Stadium stadium)
  {
    EventId = Guard.Against.NegativeOrZero(eventId, nameof(eventId));
    StadiumId = Guard.Against.NegativeOrZero(stadiumId, nameof(stadiumId));
    Event = Guard.Against.Null(@event, nameof(@event));
    Stadium = Guard.Against.Null(stadium, nameof(stadium)); ;
  }

  public Event2Stadium() { }
}
