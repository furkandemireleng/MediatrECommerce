using ECommerce.Domain.Common;

namespace ECommerce.Application.Interfaces.Repositories;

public interface IWriteRepository<T> where T : class, IBaseEntity, new()
{
    Task<int> AddAsync(T entity);
    Task<int> AddRangeAsync(IList<T> entity);

    Task<T> UpdateAsync(T entity);

    Task HardDeleteAsync(T entity);
    Task SoftDeleteAsync(T entity);
}