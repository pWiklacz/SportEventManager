using System.ComponentModel.DataAnnotations;
using SportEventManager.Core.EventAggregate;

namespace SportEventManager.Web.ViewModels.EventModel;

public class StadiumViewModel
{
  public int Id { get; set; }

  public string City { get; set; } = string.Empty;

  public static StadiumViewModel FromStadium(Stadium stadium)
  {
    return new StadiumViewModel()
    {
      Id = stadium.Id,
      City = stadium.City
    };
  }
}
