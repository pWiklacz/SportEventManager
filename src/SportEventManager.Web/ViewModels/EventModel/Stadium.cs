using System.ComponentModel.DataAnnotations;

namespace SportEventManager.Web.ViewModels.EventModel;

public class Stadium
{
  [Key]
  public int Id { get; set; }

  public string City { get; set; } = string.Empty;

  public List<Match> Matches { get; set; } = new List<Match>();
}
