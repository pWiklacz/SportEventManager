﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportEventManager.Core.EventAggregate;

namespace SportEventManager.Infrastructure.Data.Config;
public class Event2UserConfiguration : IEntityTypeConfiguration<Event2User>
{
  public void Configure(EntityTypeBuilder<Event2User> builder)
  {
    builder.Property(e2u => e2u.Id)
      .UseIdentityColumn()
      .IsRequired();

    builder.Property(e2u => e2u.EventId)
      .IsRequired()
      .HasAnnotation("ForeignKey", "Event");

    builder.Property(e2u => e2u.OwnerId)
      .IsRequired()
      .HasAnnotation("ForeignKey", "User")
      .HasMaxLength(450);
  }
}
