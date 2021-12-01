using System;
using System.Threading.Tasks;


namespace R5T.S0023
{
    public class ServiceCollectionSerializationFilePathProvider : IServiceCollectionSerializationFilePathProvider
    {
        private IOutputFilePathProvider OutputFilePathProvider { get; }
        private IServiceCollectionSerializationFileNameProvider ServiceCollectionSerializationFileNameProvider { get; }


        public ServiceCollectionSerializationFilePathProvider(
            IOutputFilePathProvider outputFilePathProvider,
            IServiceCollectionSerializationFileNameProvider serviceCollectionSerializationFileNameProvider)
        {
            this.OutputFilePathProvider = outputFilePathProvider;
            this.ServiceCollectionSerializationFileNameProvider = serviceCollectionSerializationFileNameProvider;
        }

        public async Task<string> GetServiceCollectionSerializationFilePath()
        {
            var serviceCollectionSerializationFileName = await this.ServiceCollectionSerializationFileNameProvider.GetServiceCollectionSerializationFileName();

            var output = await this.OutputFilePathProvider.GetOutputFilePath(serviceCollectionSerializationFileName);
            return output;
        }
    }
}
