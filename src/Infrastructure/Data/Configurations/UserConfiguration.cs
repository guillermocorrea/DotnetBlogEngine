using Domain;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Name)
                .HasMaxLength(45)
                .IsRequired();
            builder.Property(u => u.Password)
                .IsRequired();
        }

        public static void SeedData(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
               new User { Id = 1, Name = "Bob", Username = "writer", Password = "writer".ToSha256(), RoleId = 1 },
               new User { Id = 2, Name = "Joe", Username = "editor", Password = "editor".ToSha256(), RoleId = 2 }
               );
        }
    }
}
