using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportEventManager.Core.StatisticsAggregate;

namespace SportEventManager.Infrastructure.Data.Config.StatisticsAggregate;
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

    builder.Property(ps => ps.IsArchived)
      .IsRequired()
      .HasDefaultValue(false);

    builder.Property(ps => ps.PlayerId)
      .IsRequired()
      .HasAnnotation("ForeignKey", "Player");
  }
}
