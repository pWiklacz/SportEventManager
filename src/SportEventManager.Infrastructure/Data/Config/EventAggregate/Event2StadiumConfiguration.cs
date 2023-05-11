using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportEventManager.Core.EventAggregate;

namespace SportEventManager.Infrastructure.Data.Config.EventAggregate;
public class Event2StadiumConfiguration : IEntityTypeConfiguration<Event2Stadium>
{
  public void Configure(EntityTypeBuilder<Event2Stadium> builder)
  {
    builder.Property(e2S => e2S.Id)
      .UseIdentityColumn()
      .IsRequired();

    builder.Property(e2S => e2S.StadiumId)
      .IsRequired()
      .HasAnnotation("ForeignKey", "Stadium");

    builder.Property(e2S => e2S.EventId)
      .IsRequired()
      .HasAnnotation("ForeignKey", "Event");
  }
}
