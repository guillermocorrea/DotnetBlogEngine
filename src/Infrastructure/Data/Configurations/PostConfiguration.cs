using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

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

        public static void SeedData(EntityTypeBuilder<Post> builder)
        {
            builder.HasData(
               new Post { Id = 1, Title = "Draft Post Title", Body = "Draft post body", Status = Post.PostStatus.Draft, UserId = 1 },
               new Post { Id = 2, Title = "Pending Post Title", Body = "Pending post body", Status = Post.PostStatus.Pending, UserId = 1 },
               new Post { Id = 3, Title = "Rejected Post Title", Body = "Rejected post body", Status = Post.PostStatus.Rejected, UserId = 1 },
               new Post { Id = 4, Title = "Approved Post Title", Body = "Approved post body", Status = Post.PostStatus.Approved, UserId = 1, PublishDate = DateTime.Now },
               new Post { Id = 5, Title = "Approved Post Title 2", Body = "Approved post body 2", Status = Post.PostStatus.Approved, UserId = 1, PublishDate = DateTime.Now }
               );
        }
    }
}
