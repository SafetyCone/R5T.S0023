using System;
using System.Threading.Tasks;

using R5T.D0084.D002;


namespace R5T.S0023
{
    public class HardCodedRepositoriesDirectoryPathProvider : IRepositoriesDirectoryPathProvider
    {
        public Task<string> GetRepositoriesDirectoryPath()
        {
            var output = @"C:\Code\DEV\Git\GitHub\SafetyCone";

            return Task.FromResult(output);
        }
    }
}
