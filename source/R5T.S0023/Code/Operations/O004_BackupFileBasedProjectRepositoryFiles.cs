using System;
using System.Threading.Tasks;

using R5T.D0096;
using R5T.D0101.I001;


namespace R5T.S0023
{
    public class O004_BackupFileBasedProjectRepositoryFiles : T0020.IActionOperation
    {
        private IBackupProjectRepositoryFilePathsProvider BackupProjectRepositoryFilePathsProvider { get; }
        private IHumanOutput HumanOutput { get; }
        private IProjectRepositoryFilePathsProvider ProjectRepositoryFilePathsProvider { get; }


        public O004_BackupFileBasedProjectRepositoryFiles(
            IBackupProjectRepositoryFilePathsProvider backupProjectRepositoryFilePathsProvider,
            IHumanOutput humanOutput,
            IProjectRepositoryFilePathsProvider projectRepositoryFilePathsProvider)
        {
            this.BackupProjectRepositoryFilePathsProvider = backupProjectRepositoryFilePathsProvider;
            this.HumanOutput = humanOutput;
            this.ProjectRepositoryFilePathsProvider = projectRepositoryFilePathsProvider;
        }

        public async Task Run()
        {
            var (Task1Result, Task2Result, Task3Result, Task4Result) = await TaskHelper.WhenAll(
                TaskHelper.WhenAll(
                    this.ProjectRepositoryFilePathsProvider.GetDuplicateProjectNamesTextFilePath(),
                    this.BackupProjectRepositoryFilePathsProvider.GetDuplicateProjectNamesTextFilePath()),
                TaskHelper.WhenAll(
                    this.ProjectRepositoryFilePathsProvider.GetIgnoredProjectNamesTextFilePath(),
                    this.BackupProjectRepositoryFilePathsProvider.GetIgnoredProjectNamesTextFilePath()),
                TaskHelper.WhenAll(
                    this.ProjectRepositoryFilePathsProvider.GetProjectNameSelectionsTextFilePath(),
                    this.BackupProjectRepositoryFilePathsProvider.GetProjectNameSelectionsTextFilePath()),
                TaskHelper.WhenAll(
                    this.ProjectRepositoryFilePathsProvider.GetProjectsListingJsonFilePath(),
                    this.BackupProjectRepositoryFilePathsProvider.GetProjectsListingJsonFilePath()));

            foreach (var fileBackupSourceDestinationPair in new[]
            {
                Task1Result,
                Task2Result,
                Task3Result,
                Task4Result
            })
            {
                var sourceFilePath = fileBackupSourceDestinationPair.Task1Result;
                var destinationFilePath = fileBackupSourceDestinationPair.Task2Result;

                if(Instances.FileSystemOperator.FileExists(sourceFilePath))
                {
                    Instances.FileSystemOperator.CopyFile(sourceFilePath, destinationFilePath);

                    this.HumanOutput.WriteLine($"File based project repository file back-up copy made:\nSource:\n{sourceFilePath}\nDestination:\n{destinationFilePath}\n");
                }
            }
        }
    }
}
