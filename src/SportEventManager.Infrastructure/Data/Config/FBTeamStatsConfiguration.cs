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

    builder.Property(ps => ps.Wins)
      .HasDefaultValue(0);

    builder.Property(ps => ps.Losses)
      .HasDefaultValue(0);

    builder.Property(ps => ps.Drawes)
      .HasDefaultValue(0);

    builder.Property(ps => ps.Shoots)
      .HasDefaultValue(0);

    builder.Property(ps => ps.ShootsOnTarget)
      .HasDefaultValue(0);

    builder.Property(ps => ps.Fouls)
      .HasDefaultValue(0);

    builder.Property(ps => ps.Passes)
      .HasDefaultValue(0);

    builder.Property(ps => ps.TeamId)
      .IsRequired();
  }
}
