using System;
using System.Threading.Tasks;

using R5T.D0084.D001;
using R5T.D0101;


namespace R5T.S0023
{
    /// <summary>
    /// Update the project repository to using the list of all current projects.
    /// Then output a useful list of all projects the organization shared data directory.
    /// 
    /// Note: Be sure to run <see cref="O001_AnalyzeAllCurrentProjects"/> first to get an analysis of changes.
    /// </summary>
    public class O002_UpdateFileBasedProjectRepository : T0020.IActionOperation
    {
        private IAllProjectFilePathsProvider AllProjectFilePathsProvider { get; }
        private IProjectRepository ProjectRepository { get; }
        private O009_UpdateAllProjectNamesListingFile O009_UpdateAllProjectNamesListingFile { get; }


        public O002_UpdateFileBasedProjectRepository(
            IAllProjectFilePathsProvider allProjectFilePathsProvider,
            IProjectRepository projectRepository,
            O009_UpdateAllProjectNamesListingFile o009_UpdateAllProjectNamesListingFile)
        {
            this.AllProjectFilePathsProvider = allProjectFilePathsProvider;
            this.ProjectRepository = projectRepository;
            this.O009_UpdateAllProjectNamesListingFile = o009_UpdateAllProjectNamesListingFile;
        }

        public async Task Run()
        {
            // Get all repository projects.
            var repositoryProjects = await this.ProjectRepository.GetAllProjects();

            var (_, newProjects, departedProjects) = await Instances.Operation.GetProjectChanges(
                this.AllProjectFilePathsProvider,
                repositoryProjects);

            // Modify the project repository to match the current set of projects.
            await this.ProjectRepository.DeleteProjects(departedProjects);

            await this.ProjectRepository.AddProjects(newProjects);

            // With all current projects now in the repository:
            // Update the name selections using the project list, ignored names list, and duplicate name selections.
            await this.ProjectRepository.UpdateProjectNameSelections();

            await this.O009_UpdateAllProjectNamesListingFile.Run();
        }
    }
}
