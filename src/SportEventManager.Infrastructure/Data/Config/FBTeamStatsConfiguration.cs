using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportEventManager.Core.TeamAggregate.Stats;

namespace SportEventManager.Infrastructure.Data.Config;
public class FBTeamStatsConfiguration : IEntityTypeConfiguration<FBTeamStats>
{
  public void Configure(EntityTypeBuilder<FBTeamStats> builder)
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

    builder.Property(ts => ts.Wins)
      .HasDefaultValue(0);

    builder.Property(ts => ts.Losses)
      .HasDefaultValue(0);

    builder.Property(ts => ts.Drawes)
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
      .HasDefaultValue(null);

    builder.Property(ts => ts.MatchId)
      .HasDefaultValue(null);
  }
}
