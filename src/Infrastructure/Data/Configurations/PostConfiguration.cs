using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(p => p.Status)
                .HasConversion<string>()
                .HasMaxLength(45)
                .IsRequired();

            builder.Property(p => p.Title)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(p => p.Body)
                .HasMaxLength(450)
                .IsRequired();

        }
    }
}
