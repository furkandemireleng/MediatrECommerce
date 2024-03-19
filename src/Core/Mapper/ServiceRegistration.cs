using ECommerce.Application.Interfaces.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Mapper;

public static class ServiceRegistration
{
    public static void AddCustomMapper(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IMapper, AutoMapper.Mapper>();
    }
}