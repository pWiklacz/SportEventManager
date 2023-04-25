using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SportEventManager.Core.EventAggregate;

namespace SportEventManager.Web.ViewModels.EventModel;

public class MatchViewModel
{
  public int Id { get; set; }

  public DateTime StartTime { get; set; }

  public DateTime EndTime { get; set; }

  public bool IsEnd { get; set; }

  public StadiumViewModel? Stadium { get; set; }

  public static MatchViewModel FromMatch(Match match) => new MatchViewModel()
  {
    Id = match.Id,
    StartTime = match.StartTime,
    EndTime = match.EndTime,
    Stadium = StadiumViewModel.FromStadium(stadium : match.Stadium)
  };
}

