using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportEventManager.Core.TeamAggregate;

namespace SportEventManager.Infrastructure.Data.Config.TeamAggregate;
public class Team2UserConfiguration : IEntityTypeConfiguration<Team2User>
{
  public void Configure(EntityTypeBuilder<Team2User> builder)
  {
    builder.Property(t2u => t2u.Id)
      .UseIdentityColumn()
      .IsRequired();

    builder.Property(t2u => t2u.TeamId)
      .IsRequired()
      .HasAnnotation("ForeignKey", "Team");

    builder.Property(t2u => t2u.OwnerId)
      .IsRequired()
      .HasAnnotation("ForeignKey", "User")
      .HasMaxLength(450);
  }
}
