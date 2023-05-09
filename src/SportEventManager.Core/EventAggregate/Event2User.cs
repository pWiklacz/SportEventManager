using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SportEventManager.SharedKernel;

namespace SportEventManager.Core.EventAggregate;
public class Event2User : EntityBase
{
  [Required]
  [ForeignKey("User")]
  [MaxLength(450)]
  public string OwnerId { get; private set; } = string.Empty;

  [Required]
  [ForeignKey("Event")]
  public int EventId { get; private set; }

  public Event2User() { }

  public Event2User(string userId, int eventId)
  {
    OwnerId = userId;
    EventId = eventId;
  }
}
