using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportEventManager.Core.StatisticsAggregate;

namespace SportEventManager.Infrastructure.Data.Config.StatisticsAggregate;
public class StatisticsConfiguration : IEntityTypeConfiguration<Statistics>
{
  public void Configure(EntityTypeBuilder<Statistics> builder)
  {
    builder.Property(s => s.Id)
      .UseIdentityColumn()
      .IsRequired();
  }
}
