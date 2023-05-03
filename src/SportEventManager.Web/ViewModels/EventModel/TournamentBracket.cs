using SportEventManager.Core.TeamAggregate;

namespace SportEventManager.Web.ViewModels.EventModel;

public class TournamentBracket
{
  private List<Team> _teams;
  public TournamentBracket(List<Team> teams) => _teams = teams;

  public static Dictionary<Team, Team> GenerateBracket_1stRound(List<Team> teams)
  {
    Dictionary<Team, Team> bracket = new Dictionary<Team, Team>();
    List<int> rng = TournamentBracket.getRandomList(teams.Count);
    int i;
    for (i = 0; i < teams.Count; i += 2)
    {
      bracket.Add(teams[rng[i]], teams[rng[i + 1]]);
    }
    return bracket;
  }

  public static Dictionary<Team, Team> GenerateBracket_nextRound(List<Team> teams)
  {
    Dictionary<Team, Team> bracket = new Dictionary<Team, Team>();
    int i;
    for (i = 0; i < teams.Count; i += 2)
    {
      bracket.Add(teams[i], teams[i+1]);
    }
    return bracket;
  }

  private static List<int> getRandomList(int count)
  {
    var list = new List<int>();
    int i;
    for (i = 0; i <= count; i++)
    {
      list.Add(i);
    }
    Random random = new Random();
    int n = list.Count;
    while (n > 1)
    {
      n--;
      int k = random.Next(n + 1);
      (list[n], list[k]) = (list[k], list[n]);
    }
    return list;
  }
}
