using System;
using System.Threading.Tasks;

using R5T.D0048;
using R5T.T0064;


namespace R5T.S0023
{
    [ServiceImplementationMarker]
    public class BackupProjectRepositoryFilePathsProvider : IBackupProjectRepositoryFilePathsProvider, IServiceImplementation
    {
        private IOutputFilePathProvider OutputFilePathProvider { get; }


        public BackupProjectRepositoryFilePathsProvider(
            IOutputFilePathProvider outputFilePathProvider)
        {
            this.OutputFilePathProvider = outputFilePathProvider;
        }

        public async Task<string> GetDuplicateProjectNamesTextFilePath()
        {
            var output = await this.OutputFilePathProvider.GetOutputFilePath("Projects-Duplicate Name Selections-Backup.txt");
            return output;
        }

        public async Task<string> GetIgnoredProjectNamesTextFilePath()
        {
            var output = await this.OutputFilePathProvider.GetOutputFilePath("Projects-Ignored Names-Backup.txt");
            return output;
        }

        public async Task<string> GetProjectNameSelectionsTextFilePath()
        {
            var output = await this.OutputFilePathProvider.GetOutputFilePath("Projects-Selected-Backup.txt");
            return output;
        }

        public async Task<string> GetProjectsListingJsonFilePath()
        {
            var output = await this.OutputFilePathProvider.GetOutputFilePath("Projects-All-Backup.json");
            return output;
        }
    }
}
