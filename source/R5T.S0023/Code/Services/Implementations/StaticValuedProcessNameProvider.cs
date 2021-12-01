using System;
using System.Threading.Tasks;

using R5T.T0064;


namespace R5T.S0023
{
    [ServiceImplementationMarker]
    public class StaticValuedProcessNameProvider : IProcessNameProvider, IServiceImplementation
    {
        public static string ProcessName { get; set; }


        public Task<string> GetProcessName()
        {
            return Task.FromResult(StaticValuedProcessNameProvider.ProcessName);
        }
    }
}
