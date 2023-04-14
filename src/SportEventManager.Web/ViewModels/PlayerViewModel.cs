using MessagePack;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;


namespace SportEventManager.Web.ViewModels;

  public class PlayerViewModel
  {
      public PlayerViewModel(){}

      [Key]
      public int Id { get; set; }

      [ForeignKey("Team")]
      public int TeamsId { get; set; }

      [Required]
      [MaxLength(100)]
      public String? Name { get; set; }

      [Required]
      [MaxLength(100)]
      public String? Surname { get; set; }

      [Required]
      [Range(1,50)]
      public int Number { get; set; }

      [Required]
      public bool isDelete { get; set; } = false;

      public virtual TeamViewModel? Teams { get; private set; }
  }
