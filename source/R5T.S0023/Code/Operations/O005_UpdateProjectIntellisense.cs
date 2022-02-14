using System;
using System.Linq;
using System.Threading.Tasks;

using R5T.Magyar;

using R5T.D0078;
using R5T.D0079;
using R5T.D0084.D002;
using R5T.D0101;
using R5T.T0020;

using R5T.S0023.Library;


namespace R5T.S0023
{
    /// <summary>
    /// 
    /// </summary>
    [OperationMarker]
    public class O005_UpdateProjectIntellisense : IActionOperation
    {
        private IProjectPathExtensionMethodBaseProjectPathProvider ProjectPathExtensionMethodBaseProjectPathProvider { get; }
        private IProjectRepository ProjectRepository { get; }
        private IRepositoriesDirectoryPathProvider RepositoriesDirectoryPathProvider { get; }
        private IVisualStudioProjectFileOperator VisualStudioProjectFileOperator { get; }
        private IVisualStudioSolutionFileOperator VisualStudioSolutionFileOperator { get; }


        public O005_UpdateProjectIntellisense(
            IProjectPathExtensionMethodBaseProjectPathProvider projectPathExtensionMethodBaseProjectPathProvider,
            IProjectRepository projectRepository,
            IRepositoriesDirectoryPathProvider repositoriesDirectoryPathProvider,
            IVisualStudioProjectFileOperator visualStudioProjectFileOperator,
            IVisualStudioSolutionFileOperator visualStudioSolutionFileOperator)
        {
            this.ProjectPathExtensionMethodBaseProjectPathProvider = projectPathExtensionMethodBaseProjectPathProvider;
            this.ProjectRepository = projectRepository;
            this.RepositoriesDirectoryPathProvider = repositoriesDirectoryPathProvider;
            this.VisualStudioProjectFileOperator = visualStudioProjectFileOperator;
            this.VisualStudioSolutionFileOperator = visualStudioSolutionFileOperator;
        }

        public async Task Run()
        {
            // Inputs.
            var localDataLibraryName = Instances.LibraryName.LocalData();
            var localDataLibraryDescription = Instances.LibraryDescription.LocalData();

            // Run.
            // Projects.
            var projects = await this.ProjectRepository.GetSelectedProjects();

            // For debugging.
            var project = projects
                .Where(xProject => xProject.Name == "R5T.T0123.X001")
                .Single();

            // Repository.
            var repositoryName = Instances.LibraryNameOperator.GetRepositoryName(localDataLibraryName);

            var respositoriesDirectoryPath = await this.RepositoriesDirectoryPathProvider.GetRepositoriesDirectoryPath();
            var projectPathExtensionMethodBaseProjectPath = await this.ProjectPathExtensionMethodBaseProjectPathProvider.GetProjectPathExtensionMethodBaseProjectPath();

            await Instances.RepositoryGenerator.CreateLocalRepositoryDirectoryOkIfExists(
                repositoryName,
                respositoriesDirectoryPath,
                async localRepositoryContext =>
                {
                    // Solution.
                    var solutionName = Instances.LibraryNameOperator.GetSolutionName(localDataLibraryName);

                    var solutionDirectoryPath = Instances.SolutionPathsOperator.GetSourceSolutionDirectoryPath(localRepositoryContext.DirectoryPath);

                    await Instances.SolutionGenerator.CreateSolutionOnlyIfNotExistsButAlwaysModify(
                        solutionDirectoryPath,
                        solutionName,
                        this.VisualStudioSolutionFileOperator,
                        async solutionFileContext =>
                        {
                            // Project.
                            var projectName = Instances.LibraryNameOperator.GetProjectName(localDataLibraryName);
                            var projectDescription = Instances.ProjectDescriptionGenerator.GetProjectDescription(localDataLibraryDescription);

                            var projectSpecification = Instances.ProjectOperator.GetProjectFileSpecification(
                                projectName,
                                projectDescription,
                                solutionFileContext.DirectoryPath,
                                dependencyProjectReferenceFilePaths: ArrayHelper.From(projectPathExtensionMethodBaseProjectPath));

                            await Instances.ProjectGenerator.CreateLibraryProjectOnlyIfNotExistsButAlwaysModify(
                                solutionFileContext,
                                projectSpecification,
                                this.VisualStudioProjectFileOperator,
                                this.VisualStudioSolutionFileOperator,
                                async projectFileContext =>
                                {
                                    // Create /Code/Bases/Extensions/IProjectPathExtensions.cs.
                                    // Code file.
                                    await projectFileContext.InProjectSubDirectoryPathContext(
                                        Instances.ProjectPathsOperator.GetBasesExtensionsDirectoryRelativePath(),
                                        async basesExtensionsDirectoryPathContext =>
                                        {
                                            await basesExtensionsDirectoryPathContext.InProjectSubFilePathContext(
                                                Instances.CodeFileName.IProjectPathExtensions(),
                                                async filePathContext =>
                                                {
                                                    // Code file generator, compilation unit generator, class generator, and method generator.
                                                    await Instances.CodeFileGenerator.CreateIProjectPathExtensions(
                                                        projects,
                                                        projectSpecification.DefaultNamespaceName,
                                                        filePathContext.FilePath);
                                                });
                                        });
                                });
                        });
                });
        }
    }
}
