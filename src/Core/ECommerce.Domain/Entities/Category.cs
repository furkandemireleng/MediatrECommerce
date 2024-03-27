using ECommerce.Domain.Common;

namespace ECommerce.Domain.Entities;

public class Category : BaseEntity
{
    public Guid ParentId { get; set; }
    public  string Name { get; set; }
    public  int Priority { get; set; }

    public ICollection<Detail> Details { get; set; } // each category can have multiple details
    public ICollection<ProductCategory> ProductCategories { get; set; } // each category can have multiple products


    public Category()
    {
    }

    public Category(Guid _parentId, string _name, int _priority)
    {
        ParentId = _parentId;
        Name = _name;
        Priority = _priority;
    }
}