using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportEventManager.Core.EventAggregate;
using SportEventManager.Core.MatchAggregate;

namespace SportEventManager.Infrastructure.Data.Config.EventAggregate;
public class MatchConfiguration : IEntityTypeConfiguration<Match>
{
  public void Configure(EntityTypeBuilder<Match> builder)
  {
    builder.Property(m => m.Id)
      .UseIdentityColumn()
      .IsRequired();

    builder.Property(m => m.IsArchived)
      .IsRequired()
      .HasDefaultValue(false);

    builder.Property(m => m.StartTime)
      .IsRequired();

    builder.Property(m => m.EndTime)
      .IsRequired();

    builder.Property(m => m.StadiumId)
      .IsRequired()
      .HasAnnotation("ForeignKey", "Stadium");

    builder.Property(m => m.IsEnded)
      .IsRequired()
      .HasDefaultValue(false);

    builder.Property(m => m.WinnerName)
      .HasMaxLength(100);

    builder.Property(m => m.EventId)
      .IsRequired()
      .HasAnnotation("ForeignKey", "Event");
  }
}
