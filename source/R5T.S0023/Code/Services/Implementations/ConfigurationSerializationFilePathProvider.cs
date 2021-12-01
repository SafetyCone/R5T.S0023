using System;
using System.Threading.Tasks;


namespace R5T.S0023
{
    public class ConfigurationSerializationFilePathProvider : IConfigurationSerializationFilePathProvider
    {
        private IConfigurationSerializationFileNameProvider ConfigurationSerializationFileNameProvider { get; }
        private IOutputFilePathProvider OutputFilePathProvider { get; }


        public ConfigurationSerializationFilePathProvider(
            IConfigurationSerializationFileNameProvider configurationSerializationFileNameProvider,
            IOutputFilePathProvider outputFilePathProvider)
        {
            this.ConfigurationSerializationFileNameProvider = configurationSerializationFileNameProvider;
            this.OutputFilePathProvider = outputFilePathProvider;
        }

        public async Task<string> GetServiceCollectionSerializationFilePath()
        {
            var configurationSerializationFileName = await this.ConfigurationSerializationFileNameProvider.GetConfigurationSerializationFileName();

            var output = await this.OutputFilePathProvider.GetOutputFilePath(configurationSerializationFileName);
            return output;
        }
    }
}
