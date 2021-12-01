using System;
using System.Threading.Tasks;

using R5T.T0064;


namespace R5T.S0023
{
    [ServiceImplementationMarker]
    public class ConstructorBasedProcessStartTimeProvider : IProcessStartTimeProvider, IServiceImplementation
    {
        private DateTime ProcessStartTime { get; }


        public ConstructorBasedProcessStartTimeProvider(
            DateTime processStartTime)
        {
            this.ProcessStartTime = processStartTime;
        }

        public Task<DateTime> GetProcessStartTime()
        {
            return Task.FromResult(this.ProcessStartTime);
        }
    }
}
