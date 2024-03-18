using ECommerce.Domain.Common;

namespace ECommerce.Domain.Entities;

public class Brand:BaseEntity
{

    public required string Name { get; set; }


    public Brand(string name)
    {
        Name = name;
    }

    public Brand()
    {
        
    }
    
}