using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportEventManager.Core.EventAggregate;

namespace SportEventManager.Infrastructure.Data.Config.EventAggregate;
public class StadiumConfiguration : IEntityTypeConfiguration<Stadium>
{
  public void Configure(EntityTypeBuilder<Stadium> builder)
  {
    builder.Property(s => s.Id)
      .UseIdentityColumn()
      .IsRequired();

    builder.Property(s => s.City)
      .HasMaxLength(50)
      .IsRequired();

    builder.Property(s => s.Name)
      .HasMaxLength(50)
      .IsRequired();

    builder.Property(s => s.IsArchived)
      .IsRequired()
      .HasDefaultValue(false);
  }
}
