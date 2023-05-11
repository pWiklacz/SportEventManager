using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using SportEventManager.Core.TeamAggregate;
using SportEventManager.SharedKernel;

namespace SportEventManager.Core.EventAggregate;

public class Event2Team : EntityBase
{
  [Required]
  [ForeignKey("Event")]
  public int EventId { get; private set; }

  [Required]
  [ForeignKey("Team")]
  public int TeamId { get; private set; }

  public Event? Event { get; set; }

  public Team? Team { get; set; }

  public Event2Team(int eventId, int teamId, Event @event, Team team)
  {
    EventId = Guard.Against.NegativeOrZero(eventId, nameof(eventId));
    TeamId = Guard.Against.NegativeOrZero(teamId, nameof(teamId));
    Event = Guard.Against.Null(@event,nameof(@event));
    Team = Guard.Against.Null(team, nameof(team)); ;
  }

  public Event2Team() { }
}
