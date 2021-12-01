using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using R5T.L0017.D001;
using R5T.L0018.D001;
using R5T.T0063;


namespace R5T.S0023.Startup
{
    [ServiceActionAggregationImplementationMarker]
    public class ProvidedServiceActionAggregation : IProvidedServiceActionAggregation, IServiceActionAggregationImplementation
    {
        public IServiceAction<HostBuilderContext> HostBuilderContextAction { get; set; }
#pragma warning disable CS0618 // Type or member is obsolete
        public IServiceAction<IApplicationLifetime> ApplicationLifetimeAction { get; set; }
#pragma warning restore CS0618 // Type or member is obsolete
        public IServiceAction<IConfiguration> ConfigurationAction { get; set; }
        public IServiceAction<IConfigureOptions<LoggerFilterOptions>> ConfigureLoggerFilterOptionsAction { get; set; }
        public IServiceAction<IHost> HostAction { get; set; }
        public IServiceAction<IHostApplicationLifetime> HostApplicationLifetimeAction { get; set; }
        public IServiceAction<IHostEnvironment> HostEnvironmentAction { get; set; }
#pragma warning disable CS0618 // Type or member is obsolete
        public IServiceAction<IHostingEnvironment> HostingEnvironmentAction { get; set; }
#pragma warning restore CS0618 // Type or member is obsolete
        public IServiceAction<IHostLifetime> HostLifetimeAction { get; set; }
        public IServiceAction<ILoggerUnbound> LoggerAction { get; set; }
        public IServiceAction<ILoggerFactory> LoggerFactoryAction { get; set; }
        public IServiceAction<IOptionsUnbound> OptionsAction { get; set; }
        public IServiceAction<IOptionsFactoryUnbound> OptionsFactoryAction { get; set; }
        public IServiceAction<IOptionsMonitorUnbound> OptionsMonitorAction { get; set; }
        public IServiceAction<IOptionsMonitorCacheUnbound> OptionsMonitorCacheAction { get; set; }
        public IServiceAction<IOptionsSnapshotUnbound> OptionsSnapshotUnbound { get; set; }
    }
}
