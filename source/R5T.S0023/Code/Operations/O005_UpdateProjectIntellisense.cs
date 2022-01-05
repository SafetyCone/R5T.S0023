using System;
using System.Threading.Tasks;

using R5T.Magyar;

using R5T.D0078;
using R5T.D0079;
using R5T.D0084.D002;
using R5T.D0101;

using R5T.S0023.Library;


namespace R5T.S0023
{
    /// <summary>
    /// 
    /// </summary>
    public class O005_UpdateProjectIntellisense : T0020.IActionOperation
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
            var localDataLibraryName = "LocalData"; // No R5T prefix.
            var localDataLibraryDescription = "Library for extension methods providing intellisense entries for all local data.";

            // Run.
            // Projects.
            var projects = await this.ProjectRepository.GetSelectedProjects();

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
                                projectFileContext =>
                                {
                                    // Create /Code/Bases/Extensions/IProjectPathExtensions.cs.
                                    // Code file.
                                    projectFileContext.InProjectSubDirectoryPathContextSynchronous(
                                        Instances.ProjectPathsOperator.GetBasesExtensionsDirectoryRelativePath(),
                                        basesExtensionsDirectoryPathContext =>
                                        {
                                            basesExtensionsDirectoryPathContext.InProjectSubFilePathContextSynchronous(
                                                Instances.CodeFileName.IProjectPathExtensions(),
                                                filePathContext =>
                                                {
                                                    // Code file generator, compilation unit generator, class generator, and method generator.
                                                    Instances.CodeFileGenerator.CreateIProjectPathExtensions(
                                                        projects,
                                                        projectSpecification.DefaultNamespaceName,
                                                        filePathContext.FilePath);
                                                });
                                        });

                                    return Task.CompletedTask;
                                });
                        });
                });
        }
    }
}
