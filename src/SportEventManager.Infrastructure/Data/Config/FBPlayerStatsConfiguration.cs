using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportEventManager.Core.TeamAggregate;
using SportEventManager.Core.TeamAggregate.Stats;

namespace SportEventManager.Infrastructure.Data.Config;
public class FbPlayerStatsConfiguration : IEntityTypeConfiguration<FbPlayerStats>
{
  public void Configure(EntityTypeBuilder<FbPlayerStats> builder)
  {
    builder.Property(ps => ps.Id)
      .UseIdentityColumn()
      .IsRequired();

    builder.Property(ps => ps.Goals)
      .HasDefaultValue(0);

    builder.Property(ps => ps.Assists)
      .HasDefaultValue(0);

    builder.Property(ps => ps.RedCards)
      .HasDefaultValue(0);

    builder.Property(ps => ps.YellowCards)
      .HasDefaultValue(0);

    builder.Property(ps => ps.PlayerId)
      .IsRequired()
      .HasAnnotation("ForeignKey", "Player");
  }
}
