using System;
using System.Threading.Tasks;

using R5T.T0064;


namespace R5T.S0023
{
    [ServiceImplementationMarker]
    public class ProcessStartTimeDirectoryNameProvider : IProcessStartTimeDirectoryNameProvider, IServiceImplementation
    {
        private IProcessStartTimeProvider ProcessStartTimeProvider { get; }
        private IDateTimeDirectoryNameProvider DateTimeDirectoryNameProvider { get; }


        public ProcessStartTimeDirectoryNameProvider(
            IProcessStartTimeProvider processStartTimeProvider,
            IDateTimeDirectoryNameProvider dateTimeDirectoryNameProvider)
        {
            this.ProcessStartTimeProvider = processStartTimeProvider;
            this.DateTimeDirectoryNameProvider = dateTimeDirectoryNameProvider;
        }

        public async Task<string> GetProcessStartTimeDirectoryName()
        {
            var processStartTime = await this.ProcessStartTimeProvider.GetProcessStartTime();

            var output = await this.DateTimeDirectoryNameProvider.GetDateTimeDirectoryName(processStartTime);
            return output;
        }
    }
}
