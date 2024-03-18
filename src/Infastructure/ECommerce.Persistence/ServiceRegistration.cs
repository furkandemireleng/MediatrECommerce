using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Persistence.Context;
using ECommerce.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Connection string 'DefaultConnection' is missing or empty.");
        }

        services.AddDbContext<ApplicationDbContext>(opt => opt.UseNpgsql(connectionString));

        services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
    }
}