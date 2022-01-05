using System;
using System.Linq;
using System.Threading.Tasks;

using R5T.Magyar.IO;

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
        private IAllProjectNamesListingFilePathProvider AllProjectNamesListingFilePathProvider { get; }
        private IProjectRepository ProjectRepository { get; }


        public O002_UpdateFileBasedProjectRepository(
            IAllProjectFilePathsProvider allProjectFilePathsProvider,
            IAllProjectNamesListingFilePathProvider allProjectNamesListingFilePathProvider,
            IProjectRepository projectRepository)
        {
            this.AllProjectFilePathsProvider = allProjectFilePathsProvider;
            this.AllProjectNamesListingFilePathProvider = allProjectNamesListingFilePathProvider;
            this.ProjectRepository = projectRepository;
        }

        public async Task Run()
        {
            // Get all repository projects.
            var repositoryProjects = await this.ProjectRepository.GetAllProjects();

            var (currentProjects, newProjects, departedProjects) = await Instances.Operation.GetProjectChanges(
                this.AllProjectFilePathsProvider,
                repositoryProjects);

            // Modify the project repository to match the current set of projects.
            await this.ProjectRepository.DeleteProjects(departedProjects);

            await this.ProjectRepository.AddProjects(newProjects);

            // With all current projects now in the repository:
            // Update the name selections using the project list, ignored names list, and duplicate name selections.
            await this.ProjectRepository.UpdateProjectNameSelections();

            // Then update the list of all project names (including any duplicates).
            // Get all projects again, now with new projects added and departed projects removed.
            var allProjects = await this.ProjectRepository.GetAllProjects();

            var allProjectNamesInOrder = allProjects
                .Select(xProject => xProject.Name)
                .OrderAlphabetically()
                ;

            var allProjectNamesListingFilePath = await this.AllProjectNamesListingFilePathProvider.GetAllProjectNamesListingFilePath();

            FileHelper.WriteAllLinesSynchronous(
                allProjectNamesListingFilePath,
                allProjectNamesInOrder);
        }
    }
}
