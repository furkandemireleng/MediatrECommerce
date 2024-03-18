using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Interfaces.Repositories.UnitOfWorks;
using ECommerce.Domain.Common;
using ECommerce.Persistence.Context;
using ECommerce.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Persistence.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext applicationDbContext;

    public UnitOfWork(ApplicationDbContext applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
    }

    public async ValueTask DisposeAsync()
    {
        await applicationDbContext.DisposeAsync();
    }

    public IReadRepository<T> GetReadRepository<T>() where T : class, IBaseEntity, new()
    {
        return new ReadRepository<T>(applicationDbContext);
    }

    public IWriteRepository<T> GetWriteRepository<T>() where T : class, IBaseEntity, new()
    {
        return new WriteRepository<T>(applicationDbContext);
    }

    public async Task<int> SaveAsync()
    {
        return await applicationDbContext.SaveChangesAsync();
    }

    public int Save()
    {
        return applicationDbContext.SaveChanges();
    }
}