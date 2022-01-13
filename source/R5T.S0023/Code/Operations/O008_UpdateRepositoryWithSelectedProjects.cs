using System;
using System.Threading.Tasks;

using R5T.T0020;


namespace R5T.S0023
{
    /// <summary>
    /// Updates the project repository with selected (not ignored, duplicate specified) projects.
    /// Only works on projects in the repository.
    /// Demands that all duplicate project names be specified.
    /// </summary>
    [OperationMarker]
    public class O008_UpdateRepositoryWithSelectedProjects : IActionOperation
    {
        private O004_BackupFileBasedProjectRepositoryFiles O004_BackupFileBasedProjectRepositoryFiles { get; }
        private O008a_UpdateRepositoryWithSelectedProjects O008A_UpdateRepositoryWithSelectedProjects { get; }


        public O008_UpdateRepositoryWithSelectedProjects(
            O004_BackupFileBasedProjectRepositoryFiles o004_BackupFileBasedProjectRepositoryFiles,
            O008a_UpdateRepositoryWithSelectedProjects o008A_UpdateRepositoryWithSelectedProjects)
        {
            this.O004_BackupFileBasedProjectRepositoryFiles = o004_BackupFileBasedProjectRepositoryFiles;
            this.O008A_UpdateRepositoryWithSelectedProjects = o008A_UpdateRepositoryWithSelectedProjects;
        }

        public async Task Run()
        {
            // Backup.
            await this.O004_BackupFileBasedProjectRepositoryFiles.Run();

            await this.O008A_UpdateRepositoryWithSelectedProjects.Run();   
        }
    }
}
