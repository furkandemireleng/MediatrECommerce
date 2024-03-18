using Bogus;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Persistence.Configuration;

public class DetailConfiguration:IEntityTypeConfiguration<Detail>
{
    public void Configure(EntityTypeBuilder<Detail> builder)
    {

        Faker faker = new Faker("en");

        Detail detail1 = new Detail
        {
            CreateDate = DateTime.UtcNow,
            Id = Guid.NewGuid(),
            IsDelete = false,
            Title = faker.Lorem.Slug(),
            Description = faker.Lorem.Sentence(5),
            CategoryId = Guid.Empty
        };
        


        builder.HasData(detail1);
    }
}