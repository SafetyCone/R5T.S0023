using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using R5T.A0001;
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

            var hostServiceActions = Instances.ServiceAction.AddHostServiceActions();

            var providedServices = new ProvidedServiceActionAggregation()
                .FilFrom(hostServiceActions)
                ;

            await this.ConfigureServices(services,
                providedServices);
        }

        protected abstract Task FillRequiredServiceActions(IRequiredServiceActionAggregation requiredServiceActions);

        protected abstract Task ConfigureServices(IServiceCollection services,
            IProvidedServiceActionAggregation providedServicesAggregation);
    }
}
