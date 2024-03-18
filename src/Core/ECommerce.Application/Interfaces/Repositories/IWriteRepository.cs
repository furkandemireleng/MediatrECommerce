using ECommerce.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Interfaces.Repositories;

public interface IWriteRepository<T> where T : class, IBaseEntity, new()
{
    
    
    Task AddAsync(T entity);
    Task AddRangeAsync(IList<T> entities);

    Task<T> UpdateAsync(T entity);

    Task HardDeleteAsync(T entity);
}