using Ardalis.Specification;

namespace SportEventManager.Core.TeamAggregate.Specifications;
public class TeamsWithoutEventsSpec : Specification<Team>
{
  public TeamsWithoutEventsSpec() 
  {
    Query
      .Where(team => !team.Events.Any() && !team.IsArchived);
  }
}
