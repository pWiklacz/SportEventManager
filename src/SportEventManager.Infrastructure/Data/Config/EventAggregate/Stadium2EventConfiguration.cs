using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportEventManager.Core.EventAggregate;

namespace SportEventManager.Infrastructure.Data.Config.EventAggregate;
public class Stadium2EventConfiguration : IEntityTypeConfiguration<Stadium2Event>
{
  public void Configure(EntityTypeBuilder<Stadium2Event> builder)
  {
    builder.Property(s2e => s2e.Id)
      .UseIdentityColumn()
      .IsRequired();

    builder.Property(s2e => s2e.StadiumId)
      .IsRequired()
      .HasAnnotation("ForeignKey", "Stadium");

    builder.Property(s2e => s2e.EventId)
      .IsRequired()
      .HasAnnotation("ForeignKey", "Event");
  }
}
