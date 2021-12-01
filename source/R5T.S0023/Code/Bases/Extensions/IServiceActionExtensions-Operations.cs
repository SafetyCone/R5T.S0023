using System;

using R5T.D0084.D001;
using R5T.D0101;
using R5T.D0101.I001;
using R5T.T0062;
using R5T.T0063;


namespace R5T.S0023
{
    public static partial class IServiceActionExtensions
    {
        /// <summary>
        /// Adds the <see cref="O002_UpdateFileBasedProjectRepository"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O002_UpdateFileBasedProjectRepository> AddO002_UpdateFileBasedProjectRepositoryAction(this IServiceAction _,
            IServiceAction<IAllProjectFilePathsProvider> allProjectFilePathsProviderAction,
            IServiceAction<IAllProjectNamesListingFilePathProvider> allProjectNamesListingFilePathProviderAction,
            IServiceAction<IFileBasedProjectRepository> fileBasedProjectRepositoryAction,
            IServiceAction<IProjectRepository> projectRepositoryAction)
        {
            var serviceAction = _.New<O002_UpdateFileBasedProjectRepository>(services => services.AddO002_UpdateFileBasedProjectRepository(
                allProjectFilePathsProviderAction,
                allProjectNamesListingFilePathProviderAction,
                fileBasedProjectRepositoryAction,
                projectRepositoryAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O001_AnalyzeAllCurrentProjects"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O001_AnalyzeAllCurrentProjects> AddO001_AnalyzeAllCurrentProjectsAction(this IServiceAction _,
            IServiceAction<IAllProjectFilePathsProvider> allProjectFilePathsProviderAction,
            IServiceAction<IFileBasedProjectRepository> fileBasedProjectRepositoryAction,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<IOutputFilePathProvider> outputFilePathProviderAction,
            IServiceAction<IProjectRepository> projectRepositoryAction)
        {
            var serviceAction = _.New<O001_AnalyzeAllCurrentProjects>(services => services.AddO001_AnalyzeAllCurrentProjects(
                allProjectFilePathsProviderAction,
                fileBasedProjectRepositoryAction,
                notepadPlusPlusOperatorAction,
                outputFilePathProviderAction,
                projectRepositoryAction));

            return serviceAction;
        }
    }
}
