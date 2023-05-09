using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportEventManager.Core.EventAggregate;

namespace SportEventManager.Infrastructure.Data.Config;
public class EventConfiguration : IEntityTypeConfiguration<Event>
{
  public void Configure(EntityTypeBuilder<Event> builder)
  {
    builder.Property(e => e.Id)
      .UseIdentityColumn()
      .IsRequired();

    builder.Property(e => e.Name)
      .HasMaxLength(100)
      .IsRequired();

    builder.Property(e => e.StartTime)
      .IsRequired();

    builder.Property(e => e.EndTime)
      .IsRequired();

    builder.Property(e => e.IsArchived)
      .IsRequired()
      .HasDefaultValue(false);

    builder.Property(e => e.IsInprogress)
      .IsRequired()
      .HasDefaultValue(false);
  }
}
