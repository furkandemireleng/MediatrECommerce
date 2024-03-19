using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Persistence.Configuration;

public class ProductCategoriesConfiguration : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        //Create 2 primary key for procut and category
        builder.HasKey(x => new { x.ProductId, x.CategoryId });

        builder.HasOne(p => p.Product)
            .WithMany(pc => pc.ProductCategories)
            .HasForeignKey(p => p.ProductId).OnDelete(DeleteBehavior.Cascade);


        builder.HasOne(c => c.Category)
            .WithMany(pc => pc.ProductCategories)
            .HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.Cascade);
    }
} 