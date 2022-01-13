using System;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

using R5T.D0101;
using R5T.T0020;


namespace R5T.S0023
{
    [OperationMarker]
    public class O009_UpdateAllProjectNamesListingFile : IActionOperation
    {
        private IAllProjectNamesListingFilePathProvider AllProjectNamesListingFilePathProvider { get; }
        private IProjectRepository ProjectRepository { get; }


        public O009_UpdateAllProjectNamesListingFile(
            IAllProjectNamesListingFilePathProvider allProjectNamesListingFilePathProvider,
            IProjectRepository projectRepository)
        {
            this.AllProjectNamesListingFilePathProvider = allProjectNamesListingFilePathProvider;
            this.ProjectRepository = projectRepository;
        }

        public async Task Run()
        {
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
