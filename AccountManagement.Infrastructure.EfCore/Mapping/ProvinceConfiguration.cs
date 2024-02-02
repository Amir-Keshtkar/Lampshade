using AccountManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace AccountManagement.Infrastructure.EfCore.Mapping {
    public class ProvinceConfiguration : IEntityTypeConfiguration<Province> {
        public void Configure(EntityTypeBuilder<Province> builder) {
            builder.ToTable(nameof(Province));

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasMaxLength(50);

            builder.HasData(Seed());
        }

        private static object[] Seed() {
            List<ProvinceDto> list;
            using var r = new StreamReader(@"./wwwroot/provinces.json");
            string json = r.ReadToEnd();
            list = JsonConvert.DeserializeObject<List<ProvinceDto>>(json);
            return list.ToArray();
        }

        public class ProvinceDto {
            public int Id { get;  set; }
            public string Name { get;  set; }
        }
    }
}
