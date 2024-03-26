using System.Globalization;
using System.Reflection;
using System.Runtime.Serialization;
using ECommerce.Application.Base;
using ECommerce.Application.Beheviors;
using ECommerce.Application.Exceptions;
using ECommerce.Application.Features.Products.Rules;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Application;

public static class ServiceRegistration
{
    public static void AddApplication(this IServiceCollection serviceCollection)
    {
        var assembly = Assembly.GetExecutingAssembly();

        serviceCollection.AddTransient<ExcetionMiddleware>();

        // serviceCollection.AddTransient<ProductRules>(); bu da bir secenek ama tek tek eklerdik

        serviceCollection.AddRulesFromAssemblyContaining(assembly, typeof(BaseRules));

        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));

        serviceCollection.AddValidatorsFromAssembly(assembly);

        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("tr");

        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehevior<,>));
    }

    private static IServiceCollection AddRulesFromAssemblyContaining(
        this IServiceCollection serviceCollection,
        Assembly assembly,
        Type type)
    {
        var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
        foreach (var item in types)
        {
            serviceCollection.AddTransient(item);
        }

        return serviceCollection;
    }
}