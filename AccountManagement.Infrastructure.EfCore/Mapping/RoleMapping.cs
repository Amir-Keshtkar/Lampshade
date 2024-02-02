using System.Security.Cryptography.X509Certificates;
using AccountManagement.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EfCore.Mapping {
    public class RoleMapping : IEntityTypeConfiguration<Role> {
        public void Configure(EntityTypeBuilder<Role> builder) {
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
                    navigationBuilder.Ignore(x => x.Name);
                    navigationBuilder.Property(x => x.Code).HasMaxLength(50);
                    navigationBuilder.WithOwner(x => x.Role);

                    navigationBuilder.HasData(PermissionSeed());
                });

            builder.HasData(Seed());
        }

        public object[] Seed() {
            return new[] {
                new { Id=(long)1, Name ="مدیر سیستم" , CreationDate = DateTime.Now },
                new { Id=(long)2, Name ="کاربر سیستم", CreationDate = DateTime.Now}
            };
        }

        public object[] PermissionSeed() {
            return new object[] {   
                new{ Id=(long) 1, Code=10, RoleId=(long)1 },
                new{ Id=(long) 2, Code=11, RoleId=(long)1 },
                new{ Id=(long) 3, Code=12, RoleId=(long)1 },
                new{ Id=(long) 4, Code=13, RoleId=(long)1 },
                new{ Id=(long) 5, Code=14, RoleId=(long)1 },
                new{ Id=(long) 6, Code=15, RoleId=(long)1 },
                new{ Id=(long) 7, Code=16, RoleId=(long)1 },
                new{ Id=(long) 8, Code=17, RoleId=(long)1 },
                new{ Id=(long) 9, Code=18, RoleId=(long)1 },
                new{ Id=(long) 10, Code=19, RoleId=(long)1 },
                new{ Id=(long) 11, Code=20, RoleId=(long)1 },
                new{ Id=(long) 12, Code=21, RoleId=(long)1 },
                new{ Id=(long) 13, Code=22, RoleId=(long)1 },
                new{ Id=(long) 14, Code=23, RoleId=(long)1 },
                new{ Id=(long) 15, Code=24, RoleId=(long)1 },
                new{ Id=(long) 16, Code=25, RoleId=(long)1 },
                new{ Id=(long) 17, Code=26, RoleId=(long)1 },
                new{ Id=(long) 18, Code=27, RoleId=(long)1 },
                new{ Id=(long) 19, Code=28, RoleId=(long)1 },
                new{ Id=(long) 20, Code=29, RoleId=(long)1 },
                new{ Id=(long) 21, Code=30, RoleId=(long)1 },
                new{ Id=(long) 22, Code=31, RoleId=(long)1 },
                new{ Id=(long) 23, Code=32, RoleId=(long)1 },
                new{ Id=(long) 24, Code=33, RoleId=(long)1 },
                new{ Id=(long) 25, Code=34, RoleId=(long)1 },
                new{ Id=(long) 26, Code=35, RoleId=(long)1 },
                new{ Id=(long) 27, Code=36, RoleId=(long)1 },
                new{ Id=(long) 28, Code=37, RoleId=(long)1 },
                new{ Id=(long) 29, Code=38, RoleId=(long)1 },
                new{ Id=(long) 30, Code=39, RoleId=(long)1 },
                new{ Id=(long) 31, Code=40, RoleId=(long)1 },
                new{ Id=(long) 32, Code=41, RoleId=(long)1 },
                new{ Id=(long) 33, Code=42, RoleId=(long)1 },
                new{ Id=(long) 34, Code=43, RoleId=(long)1 },
                new{ Id=(long) 35, Code=44, RoleId=(long)1 },
                new{ Id=(long) 36, Code=45, RoleId=(long)1 },
                new{ Id=(long) 37, Code=46, RoleId=(long)1 },
                new{ Id=(long) 38, Code=47, RoleId=(long)1 },
                new{ Id=(long) 39, Code=48, RoleId=(long)1 },
                new{ Id=(long) 40, Code=49, RoleId=(long)1 },
                new{ Id=(long) 41, Code=50, RoleId=(long)1 },
                new{ Id=(long) 42, Code=51, RoleId=(long)1 },
                new{ Id=(long) 43, Code=52, RoleId=(long)1 },
                new{ Id=(long) 44, Code=53, RoleId=(long)1 },
                new{ Id=(long) 45, Code=54, RoleId=(long)1 },
                new{ Id=(long) 46, Code=55, RoleId=(long)1 },
                new{ Id=(long) 47, Code=56, RoleId=(long)1 },
                new{ Id=(long) 48, Code=57, RoleId=(long)1 },
                new{ Id=(long) 49, Code=58, RoleId=(long)1 },
                new{ Id=(long) 50, Code=59, RoleId=(long)1 },
                new{ Id=(long) 51, Code=60, RoleId=(long)1 },
                new{ Id=(long) 52, Code=61, RoleId=(long)1 },
                new{ Id=(long) 53, Code=62, RoleId=(long)1 },
                new{ Id=(long) 54, Code=63, RoleId=(long)1 },
                new{ Id=(long) 55, Code=64, RoleId=(long)1 },
                new{ Id=(long) 56, Code=65, RoleId=(long)1 },
                new{ Id=(long) 57, Code=66, RoleId=(long)1 },
                new{ Id=(long) 58, Code=67, RoleId=(long)1 },
                new{ Id=(long) 59, Code=68, RoleId=(long)1 },
                new{ Id=(long) 60, Code=69, RoleId=(long)1 },
                new{ Id=(long) 61, Code=70, RoleId=(long)1 },
                new{ Id=(long) 62, Code=71, RoleId=(long)1 },
                new{ Id=(long) 63, Code=72, RoleId=(long)1 },
                new{ Id=(long) 64, Code=73, RoleId=(long)1 },
                new{ Id=(long) 65, Code=74, RoleId=(long)1 },
                new{ Id=(long) 66, Code=75, RoleId=(long)1 },
                new{ Id=(long) 67, Code=76, RoleId=(long)1 },
                new{ Id=(long) 68, Code=77, RoleId=(long)1 },
                new{ Id=(long) 69, Code=78, RoleId=(long)1 },
                new{ Id=(long) 70, Code=79, RoleId=(long)1 },
                new{ Id=(long) 71, Code=80, RoleId=(long)1 },
                new{ Id=(long) 72, Code=81, RoleId=(long)1 },
                new{ Id=(long) 73, Code=82, RoleId=(long)1 },
                new{ Id=(long) 74, Code=83, RoleId=(long)1 },
                new{ Id=(long) 75, Code=84, RoleId=(long)1 },
                new{ Id=(long) 76, Code=85, RoleId=(long)1 },
                new{ Id=(long) 77, Code=86, RoleId=(long)1 },
                new{ Id=(long) 78, Code=87, RoleId=(long)1 },
                new{ Id=(long) 79, Code=88, RoleId=(long)1 },
                new{ Id=(long) 80, Code=89, RoleId=(long)1 },
                new{ Id=(long) 81, Code=90, RoleId=(long)1 },
                new{ Id=(long) 82, Code=91, RoleId=(long)1 },
                new{ Id=(long) 83, Code=92, RoleId=(long)1 },
                new{ Id=(long) 84, Code=93, RoleId=(long)1 },
                new{ Id=(long) 85, Code=94, RoleId=(long)1 },
                new{ Id=(long) 86, Code=95, RoleId=(long)1 },
                new{ Id=(long) 87, Code=96, RoleId=(long)1 },
                new{ Id=(long) 88, Code=97, RoleId=(long)1 },
                new{ Id=(long) 89, Code=98, RoleId=(long)1 },
                new{ Id=(long) 90, Code=99, RoleId=(long)1 },
                new{ Id=(long) 91, Code=100, RoleId=(long)1 },
                new{ Id=(long) 92, Code=101, RoleId=(long)1 },
                new{ Id=(long) 93, Code=102, RoleId=(long)1 },
                new{ Id=(long) 94, Code=103, RoleId=(long)1 },
                new{ Id=(long) 95, Code=104, RoleId=(long)1 },
                new{ Id=(long) 96, Code=105, RoleId=(long)1 },
                new{ Id=(long) 97, Code=106, RoleId=(long)1 },
                new{ Id=(long) 98, Code=107, RoleId=(long)1 },
                new{ Id=(long) 99, Code=108, RoleId=(long)1 },
                new{ Id=(long) 100, Code=109, RoleId=(long)1 },
                new{ Id=(long) 101, Code=110, RoleId=(long)1 },
                new{ Id=(long) 102, Code=111, RoleId=(long)1 },
                new{ Id=(long) 103, Code=112, RoleId=(long)1 },
                new{ Id=(long) 104, Code=113, RoleId=(long)1 },
                new{ Id=(long) 105, Code=114, RoleId=(long)1 },
                new{ Id=(long) 106, Code=115, RoleId=(long)1 },
                new{ Id=(long) 107, Code=116, RoleId=(long)1 },
                new{ Id=(long) 108, Code=117, RoleId=(long)1 },
                new{ Id=(long) 109, Code=118, RoleId=(long)1 },
                new{ Id=(long) 110, Code=119, RoleId=(long)1 },
                new{ Id=(long) 111, Code=120, RoleId=(long)1 },
                new{ Id=(long) 112, Code=121, RoleId=(long)1 },
                new{ Id=(long) 113, Code=122, RoleId = (long)1 },
            };
        }
    }
}
