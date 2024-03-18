using Bogus;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ECommerce.Persistence.Configuration;

public class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(256);

        Faker faker = new Faker("en");

        Brand brand1 = new Brand()
        {
            Name = faker.Commerce.Department(),
            CreateDate = DateTime.UtcNow,
            Id = Guid.NewGuid(),
            IsDelete = false
        };

        builder.HasData(brand1);
    }
}