using System;
using System.Threading.Tasks;


namespace R5T.S0023
{
    public class ConstructorBasedConfigurationSerializationFileNameProvider : IConfigurationSerializationFileNameProvider
    {
        private string ConfigurationSerializationFileName { get; }


        public ConstructorBasedConfigurationSerializationFileNameProvider(
            string configurationSerializationFileName)
        {
            this.ConfigurationSerializationFileName = configurationSerializationFileName;
        }

        public Task<string> GetConfigurationSerializationFileName()
        {
            return Task.FromResult(this.ConfigurationSerializationFileName);
        }
    }
}
