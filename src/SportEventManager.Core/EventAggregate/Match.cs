using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SportEventManager.SharedKernel;

namespace SportEventManager.Core.EventAggregate;

public class Match : EntityBase
{
  [Required]
  [Timestamp]
  public DateTime StartTime { get; set; }

  [Required]
  [Timestamp]
  public DateTime EndTime { get; set; }

  [Required]
  public Stadium Stadium { get; set; } = new Stadium();

  [Required]
  [ForeignKey("Stadium")]
  public int StadiumId { get; set; }

  [Required]
  [DefaultValue(false)]
  public bool IsDeleted { get; private set; } = false;

  [Required]
  [DefaultValue(false)]
  public bool IsEnd { get; set; } = false;

  public Match() { }

  public Match(DateTime startTime, DateTime endTime, Stadium stadium, int stadiumId)
  {
    StartTime = startTime;
    EndTime = endTime;
    Stadium = stadium;
    StadiumId = stadiumId;
    IsDeleted = false;
  }

  public void MarkAsDeleted()
  {
    this.IsDeleted = true;
  }
}
