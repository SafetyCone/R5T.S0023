using System;
using System.Threading.Tasks;


namespace R5T.S0023
{
    public class O100_UpdateProjectRepositoryWithCurrentProjects : T0020.IActionOperation
    {
        private O001_AnalyzeAllCurrentProjects O001_AnalyzeAllCurrentProjects { get; }
        private O002_UpdateFileBasedProjectRepository O002_UpdateFileBasedProjectRepository { get; }
        private O003_PerformRequiredHumanActions O003_PerformRequiredHumanActions { get; }
        private O004_BackupFileBasedProjectRepositoryFiles O004_BackupFileBasedProjectRepositoryFiles { get; }
        private O005_UpdateProjectIntellisense O005_UpdateProjectIntellisense { get; }


        public O100_UpdateProjectRepositoryWithCurrentProjects(
            O001_AnalyzeAllCurrentProjects o001_AnalyzeAllCurrentProjects,
            O002_UpdateFileBasedProjectRepository o002_UpdateFileBasedProjectRepository,
            O003_PerformRequiredHumanActions o003_PerformRequiredHumanActions,
            O004_BackupFileBasedProjectRepositoryFiles o004_BackupFileBasedProjectRepositoryFiles,
            O005_UpdateProjectIntellisense o005_UpdateProjectIntellisense)
        {
            this.O001_AnalyzeAllCurrentProjects = o001_AnalyzeAllCurrentProjects;
            this.O002_UpdateFileBasedProjectRepository = o002_UpdateFileBasedProjectRepository;
            this.O003_PerformRequiredHumanActions = o003_PerformRequiredHumanActions;
            this.O004_BackupFileBasedProjectRepositoryFiles = o004_BackupFileBasedProjectRepositoryFiles;
            this.O005_UpdateProjectIntellisense = o005_UpdateProjectIntellisense;
        }

        public async Task Run()
        {
            await this.O001_AnalyzeAllCurrentProjects.Run();
            await this.O004_BackupFileBasedProjectRepositoryFiles.Run(); // Run backup before human might make changes.
            await this.O003_PerformRequiredHumanActions.Run();
            await this.O002_UpdateFileBasedProjectRepository.Run();
            await this.O005_UpdateProjectIntellisense.Run();
        }
    }
}
