using System;
using System.Linq;
using System.Threading.Tasks;

using R5T.D0084.D001;
using R5T.D0101;
using R5T.D0101.I001;
using R5T.T0020;


namespace R5T.S0023
{
    [OperationMarker]
    public class O006_AnalyzeProjectsVsRespository : IActionOperation
    {
        private IAllProjectFilePathsProvider AllProjectFilePathsProvider { get; }
        private IProjectRepository ProjectRepository { get; }

        private IProjectRepositoryFilePathsProvider ProjectRepositoryFilePathsProvider { get; }


        public O006_AnalyzeProjectsVsRespository(
            IAllProjectFilePathsProvider allProjectFilePathsProvider,
            IProjectRepository projectRepository,
            //
            IProjectRepositoryFilePathsProvider projectRepositoryFilePathsProvider)
        {
            this.AllProjectFilePathsProvider = allProjectFilePathsProvider;
            this.ProjectRepository = projectRepository;

            this.ProjectRepositoryFilePathsProvider = projectRepositoryFilePathsProvider;
        }

        public async Task Run()
        {
            // Inputs.
            var temporaryIgnoredProjectNamesTextFilePath = @"C:\Temp\Projects-Ignored Names-Temp.txt";
            var temporaryDuplicateProjectNamesTextFilePath = @"C:\Temp\Projects-Duplicate Name Selections-Temp.txt";

            // Copy ignored and duplicate files to temporary locations.
            var (ignoredProjectNamesTextFilePath, duplicateProjectNamesTextFilePath) = await TaskHelper.WhenAll(
                this.ProjectRepositoryFilePathsProvider.GetIgnoredProjectNamesTextFilePath(),
                this.ProjectRepositoryFilePathsProvider.GetDuplicateProjectNamesTextFilePath());

            Instances.FileSystemOperator.CopyFile(ignoredProjectNamesTextFilePath, temporaryIgnoredProjectNamesTextFilePath);
            Instances.FileSystemOperator.CopyFile(duplicateProjectNamesTextFilePath, temporaryDuplicateProjectNamesTextFilePath);

            // Get the current state of the repository.
            var repositoryInitialState = await this.ProjectRepository.GetState();

            var repositoryProjects = repositoryInitialState.Projects;

            // Get the current state of the file system.
            var fileSystemProjects = await Instances.Operation.GetCurrentProjects(
                this.AllProjectFilePathsProvider);

            // Set identities of all file system projects, using identities from repository projects matched by data values for those that exist, or a new identity for those that don't.
            // This way project identities in duplicate selection names and selections names will match with file system projects.
            fileSystemProjects.FillIdentitiesFromSourceOrSetNew(repositoryProjects);

            var fileSystemState = repositoryInitialState
                .Copy()
                .ReplaceProjects(fileSystemProjects)
                ;

            // Determine new and departed projects.
            var (newProjects, departedProjects) = Instances.Operation.GetProjectChanges(
                fileSystemProjects,
                repositoryProjects);

            // Determine new duplicate project names.
            var fileSystemProjectGroupsByName = fileSystemProjects.ToDictionaryOfArraysByName();

            var fileSystemDuplicateNames = fileSystemProjectGroupsByName
                .Where(xPair => xPair.Value.Length > 2)
                .Select(xPair => xPair.Key)
                .OrderAlphabetically()
                .ToArray();

            // 
        }
    }
}
