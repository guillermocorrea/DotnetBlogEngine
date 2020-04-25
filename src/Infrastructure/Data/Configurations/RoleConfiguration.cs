using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(r => r.Name)
                .HasMaxLength(45)
                .IsRequired();

            builder.HasData(
                new Role { Id = 1, Name = "Writer" },
                new Role { Id = 2, Name = "Editor" }
            );
        }
    }
}
