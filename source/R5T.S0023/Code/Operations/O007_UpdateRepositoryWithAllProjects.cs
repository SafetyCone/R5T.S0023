using System;
using System.Threading.Tasks;

using R5T.T0020;


namespace R5T.S0023
{
    /// <summary>
    /// Updates the project repository with ALL current (i.e. in the local filesystem) project paths.
    /// Shows new and departed projects.
    /// </summary>
    [OperationMarker]
    public class O007_UpdateRepositoryWithAllProjects : IActionOperation
    {
        private O004_BackupFileBasedProjectRepositoryFiles O004_BackupFileBasedProjectRepositoryFiles { get; }
        private O007a_UpdateRepositoryWithAllProjects O007A_UpdateRepositoryWithAllProjects { get; set; }


        public O007_UpdateRepositoryWithAllProjects(
            O004_BackupFileBasedProjectRepositoryFiles o004_BackupFileBasedProjectRepositoryFiles,
            O007a_UpdateRepositoryWithAllProjects o007A_UpdateRepositoryWithAllProjects)
        {
            this.O004_BackupFileBasedProjectRepositoryFiles = o004_BackupFileBasedProjectRepositoryFiles;
            this.O007A_UpdateRepositoryWithAllProjects = o007A_UpdateRepositoryWithAllProjects;
        }

        public async Task Run()
        {
            // Backup.
            await this.O004_BackupFileBasedProjectRepositoryFiles.Run();

            await this.O007A_UpdateRepositoryWithAllProjects.Run();
        }
    }
}
