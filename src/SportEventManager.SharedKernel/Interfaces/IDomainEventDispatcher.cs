
namespace SportEventManager.SharedKernel.Interfaces;

public interface IDomainEventDispatcher
{
  Task DispatchAndClearEvents(IEnumerable<EntityBase> entitiesWithEvents);
}
