using Ardalis.Specification;

namespace SportEventManager.Core.TeamAggregate.Specifications;
public class TeamByIdWithPlayersSpec : Specification<Team>, ISingleResultSpecification
{
  public TeamByIdWithPlayersSpec(int teamId)
  {
    //Z tą specyfikacją jest problem, że Include playerów dopełnia include TeamPlayerów
    //o elementy, które nie powinny się znaleźć w tym drugim i jeśli jeden TeamPlayer ma
    //LeaveOn == null, to automatycznie ten sam zawodnik pojawi się również w tyhc drużynach
    //z których juz odszedł :(
    Query
        .Where(team => team.Id == teamId)
        .Include(t => t.TeamPlayers.Where(tp => tp.LeaveOn == default(DateTime) || tp.LeaveOn > DateTime.Now))
        .Include(team => team.Players.Where(p => !p.IsArchived))
        .Where(t => !t.IsArchived);
  }
}
