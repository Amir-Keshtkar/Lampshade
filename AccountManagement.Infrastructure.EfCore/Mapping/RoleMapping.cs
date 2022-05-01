using System.Security.Cryptography.X509Certificates;
using AccountManagement.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EfCore.Mapping {
    public class RoleMapping: IEntityTypeConfiguration<Role> {
        public void Configure (EntityTypeBuilder<Role> builder) {
            builder.ToTable("Roles");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();

            builder.HasMany(x => x.Accounts)
                .WithOne(x => x.Role)
                .HasForeignKey(x => x.RoleId);

            builder.OwnsMany<Permission>(x => x.Permissions,
                navigationBuilder => {
                    navigationBuilder.ToTable("RolePermissions");
                    navigationBuilder.HasKey(x => x.Id);
                    navigationBuilder.Ignore(x=>x.Name);
                    navigationBuilder.Property(x => x.Code).HasMaxLength(50);
                    navigationBuilder.WithOwner(x => x.Role);
                });
        }
    }
}
