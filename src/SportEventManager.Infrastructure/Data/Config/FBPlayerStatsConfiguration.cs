using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportEventManager.Core.TeamAggregate;
using SportEventManager.Core.TeamAggregate.Stats;

namespace SportEventManager.Infrastructure.Data.Config;
public class FBPlayerStatsConfiguration : IEntityTypeConfiguration<FBPlayerStats>
{
  public void Configure(EntityTypeBuilder<FBPlayerStats> builder)
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
      .IsRequired();
  }
}
