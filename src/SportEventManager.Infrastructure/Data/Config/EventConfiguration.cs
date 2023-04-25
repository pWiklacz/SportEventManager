using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportEventManager.Core.EventAggregate;
using SportEventManager.Core.TeamAggregate.Stats;

namespace SportEventManager.Infrastructure.Data.Config;
public class EventConfiguration : IEntityTypeConfiguration<Event>
{
  public void Configure(EntityTypeBuilder<Event> builder)
  {
    builder.Property(s => s.Id)
      .UseIdentityColumn()
      .IsRequired();

    builder.Property(s => s.Name)
      .HasMaxLength(100)
      .IsRequired();

    builder.Property(m => m.StartTime)
      .IsRequired();

    builder.Property(p => p.IsDeleted)
      .IsRequired()
      .HasDefaultValue(false);
  }
}
