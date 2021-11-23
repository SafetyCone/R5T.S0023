using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using R5T.D0088;


namespace R5T.S0023.Startup
{
    public abstract class HostStartupBase : IHostStartup
    {
        public abstract Task ConfigureConfiguration(IConfigurationBuilder configurationBuilder);

        public async Task ConfigureServices(IServiceCollection services)
        {
            var requiredServiceActions = new RequiredServiceActionAggregation();
                
            await this.FillRequiredServiceActions(requiredServiceActions);

            var providedServices = new ProvidedServiceActionAggregation();

            await this.ConfigureServices(services,
                providedServices);
        }

        protected abstract Task FillRequiredServiceActions(IRequiredServiceActionAggregation requiredServiceActions);

        protected abstract Task ConfigureServices(IServiceCollection services,
            IProvidedServiceActionAggregation providedServicesAggregation);
    }
}
