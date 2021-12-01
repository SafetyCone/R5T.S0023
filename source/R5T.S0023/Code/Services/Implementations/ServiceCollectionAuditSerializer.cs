using System;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using R5T.T0061.X0002;


namespace R5T.S0023
{
    public class ServiceCollectionAuditSerializer : IServiceCollectionAuditSerializer
    {
        private IServiceCollection Services { get; }
        private IServiceCollectionSerializationFilePathProvider ServiceCollectionSerializationFilePathProvider { get; }


        public ServiceCollectionAuditSerializer(
            IServiceCollection services,
            IServiceCollectionSerializationFilePathProvider serviceCollectionSerializationFilePathProvider)
        {
            this.Services = services;
            this.ServiceCollectionSerializationFilePathProvider = serviceCollectionSerializationFilePathProvider;
        }

        public async Task SerializeServiceCollection()
        {
            var serviceCollectionSerializationFilePath = await this.ServiceCollectionSerializationFilePathProvider.GetServiceCollectionSerializationFilePath();

            await Instances.ServiceCollectionOperator.DescribeToTextFile(
                serviceCollectionSerializationFilePath,
                this.Services);
        }
    }
}
