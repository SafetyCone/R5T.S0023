using System;
using System.Threading.Tasks;

using R5T.T0064;


namespace R5T.S0023
{
    [ServiceImplementationMarker]
    public class OutputDirectoryPathProvider : IOutputDirectoryPathProvider, IServiceImplementation
    {
        private IProcessStartTimeSpecificOutputDirectoryPathProvider ProcessStartTimeSpecificOutputDirectoryPathProvider { get; }


        public OutputDirectoryPathProvider(
            IProcessStartTimeSpecificOutputDirectoryPathProvider processStartTimeSpecificOutputDirectoryPathProvider)
        {
            this.ProcessStartTimeSpecificOutputDirectoryPathProvider = processStartTimeSpecificOutputDirectoryPathProvider;
        }

        public async Task<string> GetOutputDirectoryPath()
        {
            var output = await this.ProcessStartTimeSpecificOutputDirectoryPathProvider.GetProcessStartTimeSpecificOutputDirectoryPath();
            return output;
        }
    }
}
