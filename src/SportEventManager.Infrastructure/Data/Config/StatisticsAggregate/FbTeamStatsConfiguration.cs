using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportEventManager.Core.StatisticsAggregate;

namespace SportEventManager.Infrastructure.Data.Config.StatisticsAggregate;
public class FbTeamStatsConfiguration : IEntityTypeConfiguration<FbTeamStats>
{
  public void Configure(EntityTypeBuilder<FbTeamStats> builder)
  {
    builder.Property(ts => ts.Id)
      .UseIdentityColumn()
      .IsRequired();

    builder.Property(ts => ts.Goals)
      .HasDefaultValue(0);

    builder.Property(ts => ts.Assists)
      .HasDefaultValue(0);

    builder.Property(ts => ts.RedCards)
      .HasDefaultValue(0);

    builder.Property(ts => ts.YellowCards)
      .HasDefaultValue(0);

    builder.Property(ts => ts.IsArchived)
      .IsRequired()
      .HasDefaultValue(false);

    builder.Property(ts => ts.Wins)
      .HasDefaultValue(0);

    builder.Property(ts => ts.Losses)
      .HasDefaultValue(0);

    builder.Property(ts => ts.Draws)
      .HasDefaultValue(0);

    builder.Property(ts => ts.Shoots)
      .HasDefaultValue(0);

    builder.Property(ts => ts.ShootsOnTarget)
      .HasDefaultValue(0);

    builder.Property(ts => ts.Fouls)
      .HasDefaultValue(0);

    builder.Property(ts => ts.Passes)
      .HasDefaultValue(0);

    builder.Property(ts => ts.TeamId)
      .IsRequired()
      .HasAnnotation("ForeignKey", "Team");
  }
}
