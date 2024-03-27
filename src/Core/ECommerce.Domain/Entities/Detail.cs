using System;
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
    
    public  string Title { get; set; }
    public  string Description { get; set; }
    public  Guid CategoryId { get; set; }
    public Category Category { get; set; }
    
}