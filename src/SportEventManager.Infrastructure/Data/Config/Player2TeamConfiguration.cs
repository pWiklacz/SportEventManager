using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportEventManager.Core.TeamAggregate;

namespace SportEventManager.Infrastructure.Data.Config;
public class Player2TeamConfiguration : IEntityTypeConfiguration<Player2Team>
{
  public void Configure(EntityTypeBuilder<Player2Team> builder)
  {
    builder.Property(p2t => p2t.Id)
      .UseIdentityColumn()
      .IsRequired();

    builder.Property(p2t => p2t.TeamId)
      .IsRequired()
      .HasAnnotation("ForeignKey", "Team");

    builder.Property(p2t => p2t.PlayerId)
      .IsRequired()
      .HasAnnotation("ForeignKey", "Player");

    builder.Property(p => p.Number)
      .IsRequired();
  }
}
