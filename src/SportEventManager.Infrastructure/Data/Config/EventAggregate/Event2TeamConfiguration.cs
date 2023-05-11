using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportEventManager.Core.EventAggregate;

namespace SportEventManager.Infrastructure.Data.Config.EventAggregate;

public class Event2TeamConfiguration : IEntityTypeConfiguration<Event2Team>
{
  public void Configure(EntityTypeBuilder<Event2Team> builder)
  {
    builder.Property(e2T => e2T.Id)
      .UseIdentityColumn()
      .IsRequired();

    builder.Property(e2S => e2S.TeamId)
      .IsRequired()
      .HasAnnotation("ForeignKey", "Team");

    builder.Property(e2S => e2S.EventId)
      .IsRequired()
      .HasAnnotation("ForeignKey", "Event");
  }
}
