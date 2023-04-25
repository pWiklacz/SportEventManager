using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportEventManager.Core.TeamAggregate;

namespace SportEventManager.Infrastructure.Data.Config;
public class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
  public void Configure(EntityTypeBuilder<Player> builder)
  {
    builder.Property(p => p.Id)
      .UseIdentityColumn()
      .IsRequired();

    builder.Property(p => p.Name)
        .HasMaxLength(100)
        .IsRequired();

    builder.Property(p => p.Surname)
        .HasMaxLength(100)
        .IsRequired();

    builder.Property(p => p.Number)
        .IsRequired();

    builder.Property(p => p.IsDeleted)
      .IsRequired()
      .HasDefaultValue(false);

    builder.Property(p => p.TeamId)
      .IsRequired()
      .HasAnnotation("ForeignKey", "Team");
  }
}
