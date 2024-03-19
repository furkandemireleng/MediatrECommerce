using ECommerce.Domain.Common;

namespace ECommerce.Domain.Entities;

public class Product : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid BrandId { get; set; }
    public Brand Brand { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }

    public ICollection<ProductCategory> ProductCategoryCategories { get; set; }
}