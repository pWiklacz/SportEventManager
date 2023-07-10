using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using SportEventManager.SharedKernel;

namespace SportEventManager.Core.TeamAggregate;

public class Player : EntityBase
{
  [Required]
  [MaxLength(100)]
  public String Name { get; set; }

  [Required]
  [MaxLength(100)]
  public String Surname { get; set; }

  [Required]
  [DefaultValue(false)]
  public bool IsArchived { get; private set; } = false;

  [Required]
  [MinLength(11)]
  [MaxLength(11)]
  public string Pesel { get; set; }

  private List<Team> _teams = new();
  public ICollection<Team> Teams => _teams.AsReadOnly();

  private List<TeamPlayer> _teamPlayers = new();
  public ICollection<TeamPlayer> TeamPlayers => _teamPlayers.AsReadOnly();

  public Player(string name, string surname, string pesel)
  {
    Name = Guard.Against.NullOrEmpty(name, nameof(name));
    Surname = Guard.Against.NullOrEmpty(surname, nameof(surname));
    Pesel = Guard.Against.NullOrEmpty(pesel, nameof(pesel));
    IsArchived = false;
  }

  public void Archive()
  {
    this.IsArchived = true;
  }

  public void ReusePlayer(int index)
  {
    this.IsArchived = false;
    foreach(var tp in _teamPlayers)
    {
      if(tp.TeamId == index)
      {
        tp.LeaveOn = null;
      }
    }
  }

  public void ReaddPlayer()
  {
    this.IsArchived = false;
  }
}
