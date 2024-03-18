using Bogus;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Persistence.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        Faker faker = new Faker("en");

        Product product1 = new Product
        {
            CreateDate = DateTime.UtcNow,
            Id = Guid.NewGuid(),
            IsDelete = false,
            Title = faker.Commerce.ProductName(),
            Description = faker.Commerce.ProductDescription(),
            BrandId = Guid.Empty,
            Price = faker.Finance.Amount(1, 10000),
            Discount = faker.Random.Decimal(0, 10)
        };
        // Product product2 = new Product
        // {
        //     CreateDate = DateTime.UtcNow,
        //     Id = Guid.NewGuid(),
        //     IsDelete = false,
        //     Title = faker.Commerce.ProductName(),
        //     Description = faker.Commerce.ProductDescription(),
        //     BrandId = Guid.Empty,
        //     Price = faker.Finance.Amount(1, 10000),
        //     Discount = faker.Random.Decimal(0, 10)
        // };
        // Product product3 = new Product
        // {
        //     CreateDate = DateTime.UtcNow,
        //     Id = Guid.NewGuid(),
        //     IsDelete = false,
        //     Title = faker.Commerce.ProductName(),
        //     Description = faker.Commerce.ProductDescription(),
        //     BrandId = Guid.Empty,
        //     Price = faker.Finance.Amount(1, 10000),
        //     Discount = faker.Random.Decimal(0, 10)
        // };

        builder.HasData(product1);
    }
}