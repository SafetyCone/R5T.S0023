using System;
using System.Threading.Tasks;

using R5T.D0101.I001;
using R5T.T0064;


namespace R5T.S0023
{
    [ServiceImplementationMarker]
    public class HardCodedProjectRepositoryFilePathsProvider : IProjectRepositoryFilePathsProvider, IServiceImplementation
    {
        public Task<string> GetDuplicateProjectNamesTextFilePath()
        {
            var output = @"C:\Temp\Projects-Duplicate Name Selections.txt";

            return Task.FromResult(output);
        }

        public Task<string> GetIgnoredProjectNamesTextFilePath()
        {
            var output = @"C:\Temp\Projects-Ignored Names.txt";

            return Task.FromResult(output);
        }

        public Task<string> GetProjectNameSelectionsTextFilePath()
        {
            var output = @"C:\Temp\Projects-Selected.txt";

            return Task.FromResult(output);
        }

        public Task<string> GetProjectsListingJsonFilePath()
        {
            var output = @"C:\Temp\Projects-All.json";

            return Task.FromResult(output);
        }
    }
}
