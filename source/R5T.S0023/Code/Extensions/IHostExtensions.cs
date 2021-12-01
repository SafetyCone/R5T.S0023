using System;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace R5T.S0023
{
    public static class IHostExtensions
    {
        public static async Task RunAsync(this Task<IHost> gettingHost)
        {
            var host = await gettingHost;

            await host.RunAsync();
        }

        public static async Task<IHost> SerializeServiceCollectionAudit(this Task<IHost> gettingHost)
        {
            var host = await gettingHost;

            await host.SerializeServiceCollectionAudit();

            return host;
        }

        public static async Task<IHost> SerializeServiceCollectionAudit(this IHost host)
        {
            var serviceCollectionAuditSerializer = host.Services.GetRequiredService<IServiceCollectionAuditSerializer>();

            await serviceCollectionAuditSerializer.SerializeServiceCollection();

            return host;
        }

        public static async Task<IHost> SerializeConfigurationAudit(this Task<IHost> gettingHost)
        {
            var host = await gettingHost;

            await host.SerializeConfigurationAudit();

            return host;
        }

        public static async Task<IHost> SerializeConfigurationAudit(this IHost host)
        {
            var configurationAuditSerializer = host.Services.GetRequiredService<IConfigurationAuditSerializer>();

            await configurationAuditSerializer.SerializeConfiguration();

            return host;
        }
    }
}
