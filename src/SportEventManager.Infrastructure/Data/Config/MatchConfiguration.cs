using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportEventManager.Core.EventAggregate;

namespace SportEventManager.Infrastructure.Data.Config;
public class MatchConfiguration : IEntityTypeConfiguration<Match>
{
  public void Configure(EntityTypeBuilder<Match> builder)
  {
    builder.Property(m => m.Id)
      .UseIdentityColumn()
      .IsRequired();

    builder.Property(m => m.IsDeleted)
      .IsRequired()
      .HasDefaultValue(false);

    builder.Property(m => m.StartTime)
      .IsRequired()
      .IsRowVersion();

    builder.Property(m => m.EndTime)
      .IsRequired()
      .IsRowVersion();
  }
}
