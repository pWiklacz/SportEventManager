using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SportEventManager.Core.UserAggregate;

namespace SportEventManager.Infrastructure.Data.Config;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.Property(u => u.FirstName)
      .HasMaxLength(255);

    builder.Property(u => u.LastName)
      .HasMaxLength(255);

    builder.Property(e => e.IsArchived)
      .IsRequired()
      .HasDefaultValue(false);
  }
}
