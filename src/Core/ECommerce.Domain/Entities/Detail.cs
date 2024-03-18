using ECommerce.Domain.Common;

namespace ECommerce.Domain.Entities;

public class Detail : BaseEntity
{
    public Detail(string title, string description, Guid categoryId)
    {
        Title = title;
        Description = description;
        CategoryId = categoryId;
    }


    public Detail()
    {
    }
    
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required Guid CategoryId { get; set; }
    public Category Category { get; set; }
    
}