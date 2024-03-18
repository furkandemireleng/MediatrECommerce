using ECommerce.Domain.Common;

namespace ECommerce.Application.Interfaces.Repositories.UnitOfWorks;

public interface IUnitOfWork : IAsyncDisposable
{
    IReadRepository<T> GetReadRepository<T>() where T : class, IBaseEntity, new();
    IWriteRepository<T> GetWriteRepository<T>() where T : class, IBaseEntity, new();

    Task<int> SaveAsync();

    int Save();
}