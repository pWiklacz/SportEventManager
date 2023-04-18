using Microsoft.EntityFrameworkCore;
using SportEventManager.Core.TeamAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace SportEventManager.Infrastructure.Data.Config;
public class TeamConfiguration : IEntityTypeConfiguration<Team>
{
  public void Configure(EntityTypeBuilder<Team> builder)
  {
    builder.Property(p => p.Id)
      .IsRequired();

    builder.Property(t => t.Name)
        .HasMaxLength(100)
        .IsRequired();

    builder.Property(t => t.City)
        .HasMaxLength(100)
        .IsRequired();

    builder.Property(t => t.NumberOfPlayers)
        .IsRequired();

    builder.Property(t => t.OwnerId)
      .HasMaxLength(450)
      .IsRequired();

    builder.Property(p => p.IsDeleted)
      .IsRequired()
      .HasDefaultValue(false);
  }
}
