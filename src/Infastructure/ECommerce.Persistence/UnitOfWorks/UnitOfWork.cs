using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Common;

namespace ECommerce.Persistence.UnitOfWorks;

public interface IUnitOfWork : IAsyncDisposable
{
    IReadRepository<T> GetReadRepository<T>() where T : class, IBaseEntity, new();
    IWriteRepository<T> GetWriteRepository<T>() where T : class, IBaseEntity, new();

    Task<int> SaveAsync();

    int Save();

}