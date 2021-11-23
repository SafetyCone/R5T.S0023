using System;

using Microsoft.Extensions.DependencyInjection;


namespace R5T.S0023.Startup
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddHostStartup<THostStartup>(this IServiceCollection services,
            IDependencyServiceActionAggregation dependencyServiceActions)
            where THostStartup : HostStartupBase
        {
            services.AddSingleton<THostStartup>()
                // Run all service actions in dependency service actions.
                ;

            return services;
        }
    }
}
