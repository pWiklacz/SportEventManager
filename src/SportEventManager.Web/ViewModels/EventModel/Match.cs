using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportEventManager.Web.ViewModels.EventModel;

public class Match
{
  [Key]
  public int Id { get; set; }
  [Timestamp]
  public DateTime StartTime { get; set; }
  [Timestamp]
  public DateTime EndTime { get; set; }
  public int StadiumId { get; set; }

  [ForeignKey("StadiumId")]
  public Stadium? Stadium { get; set; }
}
