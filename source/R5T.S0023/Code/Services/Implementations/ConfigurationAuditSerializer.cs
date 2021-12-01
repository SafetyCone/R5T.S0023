using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;


namespace R5T.S0023
{
    public class ConfigurationAuditSerializer : IConfigurationAuditSerializer
    {
        private IConfiguration Configuration { get; }
        private IConfigurationSerializationFilePathProvider ConfigurationSerializationFilePathProvider { get; }


        public ConfigurationAuditSerializer(
            IConfiguration configuration,
            IConfigurationSerializationFilePathProvider configurationSerializationFilePathProvider)
        {
            this.Configuration = configuration;
            this.ConfigurationSerializationFilePathProvider = configurationSerializationFilePathProvider;
        }

        public async Task SerializeConfiguration()
        {
            var configurationSerialiationFilePath = await this.ConfigurationSerializationFilePathProvider.GetServiceCollectionSerializationFilePath();

            await this.Configuration.DescribeToTextFile(configurationSerialiationFilePath);
        }
    }
}
