using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.D0084.D001;
using R5T.D0101;
using R5T.D0101.I001;
using R5T.T0063;


namespace R5T.S0023
{
    public static partial class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the <see cref="O002_UpdateFileBasedProjectRepository"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO002_UpdateFileBasedProjectRepository(this IServiceCollection services,
            IServiceAction<IAllProjectFilePathsProvider> allProjectFilePathsProviderAction,
            IServiceAction<IAllProjectNamesListingFilePathProvider> allProjectNamesListingFilePathProviderAction,
            IServiceAction<IFileBasedProjectRepository> fileBasedProjectRepositoryAction,
            IServiceAction<IProjectRepository> projectRepositoryAction)
        {
            services
                .Run(allProjectFilePathsProviderAction)
                .Run(allProjectNamesListingFilePathProviderAction)
                .Run(fileBasedProjectRepositoryAction)
                .Run(projectRepositoryAction)
                .AddSingleton<O002_UpdateFileBasedProjectRepository>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O001_AnalyzeAllCurrentProjects"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO001_AnalyzeAllCurrentProjects(this IServiceCollection services,
            IServiceAction<IAllProjectFilePathsProvider> allProjectFilePathsProviderAction,
            IServiceAction<IFileBasedProjectRepository> fileBasedProjectRepositoryAction,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<IOutputFilePathProvider> outputFilePathProviderAction,
            IServiceAction<IProjectRepository> projectRepositoryAction)
        {
            services
                .Run(allProjectFilePathsProviderAction)
                .Run(fileBasedProjectRepositoryAction)
                .Run(notepadPlusPlusOperatorAction)
                .Run(outputFilePathProviderAction)
                .Run(projectRepositoryAction)
                .AddSingleton<O001_AnalyzeAllCurrentProjects>();

            return services;
        }
    }
}
