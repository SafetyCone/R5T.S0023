using System;
using System.Threading.Tasks;

using R5T.T0064;


namespace R5T.S0023
{
    [ServiceImplementationMarker]
    public class StaticValuedRootOutputDirectoryPathProvider : IRootOutputDirectoryPathProvider, IServiceImplementation
    {
        public static string RootOutputDirectoryPath { get; set; }


        public Task<string> GetRootOutputDirectoryPath()
        {
            return Task.FromResult(StaticValuedRootOutputDirectoryPathProvider.RootOutputDirectoryPath);
        }
    }
}
