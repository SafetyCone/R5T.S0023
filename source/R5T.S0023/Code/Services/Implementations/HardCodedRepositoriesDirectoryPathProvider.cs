using System;
using System.Threading.Tasks;

using R5T.D0084.D002;
using R5T.T0064;


namespace R5T.S0023
{
    [ServiceImplementationMarker]
    public class HardCodedRepositoriesDirectoryPathProvider : IRepositoriesDirectoryPathProvider, IServiceImplementation
    {
        public Task<string> GetRepositoriesDirectoryPath()
        {
            var output = @"C:\Code\DEV\Git\GitHub\SafetyCone";

            return Task.FromResult(output);
        }
    }
}
