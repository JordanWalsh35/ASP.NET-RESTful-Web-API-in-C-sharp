using CarAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarAPI.Data
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder) 
        {
            builder.ToTable("Car");

            builder.HasKey(v => v.Id);
            builder.Property(v => v.Brand).IsRequired();
            builder.Property(v => v.Model).IsRequired();
            builder.Property(v => v.Year).IsRequired();
            builder.Property(v => v.EngineSize).IsRequired();
            builder.Property(v => v.BHP).IsRequired();
        }
    }
}
