namespace ECommerce.Domain.Common;

public class BaseEntity: IBaseEntity
{

    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    public bool IsDelete { get; set; } = false;

}