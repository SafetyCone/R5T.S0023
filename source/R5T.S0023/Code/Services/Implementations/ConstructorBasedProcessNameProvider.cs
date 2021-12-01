using System;
using System.Threading.Tasks;

using R5T.T0064;


namespace R5T.S0023
{
    [ServiceImplementationMarker]
    public class ConstructorBasedProcessNameProvider : IProcessNameProvider, IServiceImplementation
    {
        private string ProcessName { get; }


        public ConstructorBasedProcessNameProvider(string processName)
        {
            this.ProcessName = processName;
        }

        public Task<string> GetProcessName()
        {
            return Task.FromResult(this.ProcessName);
        }
    }
}
