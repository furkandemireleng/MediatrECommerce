using Bogus;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Persistence.Configuration;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        Faker faker = new Faker("en");

        Category category1 = new Category
        {
            Name = "Elektorinik",
            CreateDate = DateTime.UtcNow,
            Id = Guid.NewGuid(),
            IsDelete = false,
            ParentId = Guid.Empty,
            Priority = 1
        };
        
        builder.HasData(category1);
    }
}