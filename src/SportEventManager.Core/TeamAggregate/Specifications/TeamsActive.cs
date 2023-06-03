using Ardalis.Specification;

namespace SportEventManager.Core.TeamAggregate.Specifications;
public class TeamsActive : Specification<Team>
{
  public TeamsActive() 
  {
    Query
      .Where(team => !team.IsArchived);
  }
}
