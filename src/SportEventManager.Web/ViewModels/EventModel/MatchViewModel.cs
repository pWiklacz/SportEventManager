using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportEventManager.Web.ViewModels.EventModel;

public class MatchViewModel
{
  public int Id { get; set; }

  public DateTime StartTime { get; set; }

  public DateTime EndTime { get; set; }

  public StadiumViewModel? Stadium { get; set; }
}
