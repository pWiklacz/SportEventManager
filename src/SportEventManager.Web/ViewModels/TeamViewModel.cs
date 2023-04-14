using MessagePack;
using System.ComponentModel.DataAnnotations;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace SportEventManager.Web.ViewModels;

  public class TeamViewModel
  {
      [Key]
      public int Id { get; set; }
      [Required]
      [MaxLength(100)]
      public String? Name { get; set; }
      [Required]
      [MaxLength(100)]
      public String? City { get; set; }
      [Required]
      [Range(0, 50)]
      public int NumberOfPlayers { get; set; }

      public virtual List<PlayerViewModel> Players { get; set; } = new List<PlayerViewModel>();
  }
