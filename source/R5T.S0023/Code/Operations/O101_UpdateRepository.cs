using System;
using System.Threading.Tasks;

using R5T.T0020;


namespace R5T.S0023
{
    [OperationMarker]
    public class O101_UpdateRepository : IActionOperation
    {
        private O004_BackupFileBasedProjectRepositoryFiles O004_BackupFileBasedProjectRepositoryFiles { get; }
        private O005_UpdateProjectIntellisense O005_UpdateProjectIntellisense { get; }
        private O007a_UpdateRepositoryWithAllProjects O007A_UpdateRepositoryWithAllProjects { get; }
        private O008a_UpdateRepositoryWithSelectedProjects O008A_UpdateRepositoryWithSelectedProjects { get; }
        private O009_UpdateAllProjectNamesListingFile O009_UpdateAllProjectNamesListingFile { get; }


        public O101_UpdateRepository(
            O004_BackupFileBasedProjectRepositoryFiles o004_BackupFileBasedProjectRepositoryFiles,
            O005_UpdateProjectIntellisense o005_UpdateProjectIntellisense,
            O007a_UpdateRepositoryWithAllProjects o007A_UpdateRepositoryWithAllProjects,
            O008a_UpdateRepositoryWithSelectedProjects o008A_UpdateRepositoryWithSelectedProjects,
            O009_UpdateAllProjectNamesListingFile o009_UpdateAllProjectNamesListingFile)
        {
            this.O004_BackupFileBasedProjectRepositoryFiles = o004_BackupFileBasedProjectRepositoryFiles;
            this.O005_UpdateProjectIntellisense = o005_UpdateProjectIntellisense;
            this.O007A_UpdateRepositoryWithAllProjects = o007A_UpdateRepositoryWithAllProjects;
            this.O008A_UpdateRepositoryWithSelectedProjects = o008A_UpdateRepositoryWithSelectedProjects;
            this.O009_UpdateAllProjectNamesListingFile = o009_UpdateAllProjectNamesListingFile;
        }

        public async Task Run()
        {
            await this.O004_BackupFileBasedProjectRepositoryFiles.Run();
            await this.O007A_UpdateRepositoryWithAllProjects.Run();
            await this.O008A_UpdateRepositoryWithSelectedProjects.Run();
            await this.O009_UpdateAllProjectNamesListingFile.Run();
            await this.O005_UpdateProjectIntellisense.Run();
        }
    }
}
