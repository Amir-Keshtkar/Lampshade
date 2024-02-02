using AccountManagement.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EfCore.Mapping {
    public class AccountMapping : IEntityTypeConfiguration<Account> {
        public void Configure(EntityTypeBuilder<Account> builder) {
            builder.ToTable("Accounts");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FullName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.UserName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.Mobile).HasMaxLength(20).IsRequired();
            builder.Property(x => x.ProfilePhoto).HasMaxLength(500).IsRequired();

            builder.HasOne(x => x.Role)
                .WithMany(x => x.Accounts)
                .HasForeignKey(x => x.RoleId);

            builder.HasData(Seed());
        }

        public object[] Seed() {
            return new[] {
                new{ Id= (long)1, FullName="بهداد بهرام آبادیان", UserName="admin", 
                    //"admin1234"
                    Password="10000.DoLZEtw/g/OOPTMgKr08Yw==.Po/Jt+K22MbD9jRJfcSZw44N2UVeI2Cp8KoA4YYsAJ0="
                    , Mobile="09396387926", RoleId=(long)1, ProfilePhoto="", CreationDate = DateTime.Now },
                new{ Id= (long)2, FullName="کاربر تست", UserName="user"
                    //"user1234"
                    , Password="10000.Hpa1e3Zxvwhs7+8BnEms3Q==.vVHpopaMrhSNE4TTigs7rmR9/dxJ+MXC9kkY/yaJj3U="
                    , Mobile="09962999643", RoleId=(long) 2, ProfilePhoto="", CreationDate = DateTime.Now}

            };
        }
    }
}
    