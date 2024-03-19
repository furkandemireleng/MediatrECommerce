using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Application;

public static class ServiceRegistration
{
    public static void AddApplication(this IServiceCollection serviceCollection)
    {
        var assembly = Assembly.GetExecutingAssembly();

        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
    }
}