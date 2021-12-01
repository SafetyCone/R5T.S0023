using System;
using System.Threading.Tasks;


namespace R5T.S0023
{
    public class ConstructorBasedServiceCollectionSerializationFileNameProvider : IServiceCollectionSerializationFileNameProvider
    {
        private string ServiceCollectionSerializationFileName { get; }


        public ConstructorBasedServiceCollectionSerializationFileNameProvider(
            string serviceCollectionSerializationFileName)
        {
            this.ServiceCollectionSerializationFileName = serviceCollectionSerializationFileName;
        }

        public Task<string> GetServiceCollectionSerializationFileName()
        {
            return Task.FromResult(this.ServiceCollectionSerializationFileName);
        }
    }
}
