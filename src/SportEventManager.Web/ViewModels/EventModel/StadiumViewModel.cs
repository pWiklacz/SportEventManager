using System.ComponentModel.DataAnnotations;

namespace SportEventManager.Web.ViewModels.EventModel;

public class StadiumViewModel
{
  public int Id { get; set; }

  public string City { get; set; } = string.Empty;

  public List<MatchViewModel> Matches { get; set; } = new List<MatchViewModel>();
}
