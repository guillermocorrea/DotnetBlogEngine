using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Infrastructure.Data.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(c => c.Content)
                .HasMaxLength(255)
                .IsRequired();
        }

        public static void SeedData(EntityTypeBuilder<Comment> builder)
        {
            builder.HasData(
               new Comment
               {
                   Id = 1,
                   Content = "Great post!",
                   CreationDate = DateTime.Now,
                   Username = "Anonymous",
                   PostId = 4
               });
        }
    }
}
