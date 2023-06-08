using System.ComponentModel.DataAnnotations;
using SportEventManager.Core.EventAggregate;

namespace SportEventManager.Web.ViewModels.EventModel;

public class StadiumViewModel
{
  public string Id { get; set; } = string.Empty;

  public string City { get; set; } = string.Empty;

  public string Name { get; set; } = string.Empty;

  public bool IsArchived { get; private set; } = false;

  public static StadiumViewModel FromStadium(Stadium stadium)
  {
    return new StadiumViewModel()
    {
      Id = stadium.Id,
      City = stadium.City,
      Name = stadium.Name,
      IsArchived = stadium.IsArchived,
    };
  }
}
