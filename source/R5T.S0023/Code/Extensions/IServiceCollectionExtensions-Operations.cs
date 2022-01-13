using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.D0078;
using R5T.D0079;
using R5T.D0084.D001;
using R5T.D0084.D002;
using R5T.D0096;
using R5T.D0101;
using R5T.D0101.I001;
using R5T.D0105;
using R5T.D0110;
using R5T.T0063;


namespace R5T.S0023
{
    public static partial class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the <see cref="O000_Main"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO000_Main(this IServiceCollection services,
            IServiceAction<O101_UpdateRepository> o101_UpdateRepositoryAction)
        {
            services
                .Run(o101_UpdateRepositoryAction)
                .AddSingleton<O000_Main>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O900_OpenAllProjectRepositoryFiles"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO900_OpenAllProjectRepositoryFiles(this IServiceCollection services,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<IProjectRepositoryFilePathsProvider> projectRepositoryFilePathsProviderAction)
        {
            services
                .Run(notepadPlusPlusOperatorAction)
                .Run(projectRepositoryFilePathsProviderAction)
                .AddSingleton<O900_OpenAllProjectRepositoryFiles>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O101_UpdateRepository"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO101_UpdateRepository(this IServiceCollection services,
            IServiceAction<O004_BackupFileBasedProjectRepositoryFiles> o004_BackupFileBasedProjectRepositoryFilesAction,
            IServiceAction<O005_UpdateProjectIntellisense> o005_UpdateProjectIntellisenseAction,
            IServiceAction<O007a_UpdateRepositoryWithAllProjects> o007a_UpdateRepositoryWithAllProjectsAction,
            IServiceAction<O008a_UpdateRepositoryWithSelectedProjects> o008a_UpdateRepositoryWithSelectedProjectsAction,
            IServiceAction<O009_UpdateAllProjectNamesListingFile> o009_UpdateAllProjectNamesListingFileAction)
        {
            services
                .Run(o004_BackupFileBasedProjectRepositoryFilesAction)
                .Run(o005_UpdateProjectIntellisenseAction)
                .Run(o007a_UpdateRepositoryWithAllProjectsAction)
                .Run(o008a_UpdateRepositoryWithSelectedProjectsAction)
                .Run(o009_UpdateAllProjectNamesListingFileAction)
                .AddSingleton<O101_UpdateRepository>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O100_UpdateProjectRepositoryWithCurrentProjects"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO100_UpdateProjectRepositoryWithCurrentProjects(this IServiceCollection services,
            IServiceAction<O001_AnalyzeAllCurrentProjects> o001_AnalyzeAllCurrentProjectsAction,
            IServiceAction<O002_UpdateFileBasedProjectRepository> o002_UpdateFileBasedProjectRepositoryAction,
            IServiceAction<O003_PerformRequiredHumanActions> o003_PerformRequiredHumanActionsAction,
            IServiceAction<O004_BackupFileBasedProjectRepositoryFiles> o004_BackupFileBasedProjectRepositoryFilesAction,
            IServiceAction<O005_UpdateProjectIntellisense> o005_UpdateProjectIntellisenseAction)
        {
            services
                .Run(o001_AnalyzeAllCurrentProjectsAction)
                .Run(o002_UpdateFileBasedProjectRepositoryAction)
                .Run(o003_PerformRequiredHumanActionsAction)
                .Run(o004_BackupFileBasedProjectRepositoryFilesAction)
                .Run(o005_UpdateProjectIntellisenseAction)
                .AddSingleton<O100_UpdateProjectRepositoryWithCurrentProjects>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O009_UpdateAllProjectNamesListingFile"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO009_UpdateAllProjectNamesListingFile(this IServiceCollection services,
            IServiceAction<IAllProjectNamesListingFilePathProvider> allProjectNamesListingFilePathProviderAction,
            IServiceAction<IProjectRepository> projectRepositoryAction)
        {
            services
                .Run(allProjectNamesListingFilePathProviderAction)
                .Run(projectRepositoryAction)
                .AddSingleton<O009_UpdateAllProjectNamesListingFile>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O008a_UpdateRepositoryWithSelectedProjects"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO008a_UpdateRepositoryWithSelectedProjects(this IServiceCollection services,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<IProjectRepository> projectRepositoryAction)
        {
            services
                .Run(notepadPlusPlusOperatorAction)
                .Run(projectRepositoryAction)
                .AddSingleton<O008a_UpdateRepositoryWithSelectedProjects>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O008_UpdateRepositoryWithSelectedProjects"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO008_UpdateRepositoryWithSelectedProjects(this IServiceCollection services,
            IServiceAction<O004_BackupFileBasedProjectRepositoryFiles> o004_BackupFileBasedProjectRepositoryFilesAction,
            IServiceAction<O008a_UpdateRepositoryWithSelectedProjects> o008a_UpdateRepositoryWithSelectedProjectsAction)
        {
            services
                .Run(o004_BackupFileBasedProjectRepositoryFilesAction)
                .Run(o008a_UpdateRepositoryWithSelectedProjectsAction)
                .AddSingleton<O008_UpdateRepositoryWithSelectedProjects>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O007a_UpdateRepositoryWithAllProjects"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO007a_UpdateRepositoryWithAllProjects(this IServiceCollection services,
            IServiceAction<IAllProjectFilePathsProvider> allProjectFilePathsProviderAction,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<ISummaryFilePathProvider> summaryFilePathProviderAction,
            IServiceAction<IProjectRepository> projectRepositoryAction)
        {
            services
                .Run(allProjectFilePathsProviderAction)
                .Run(notepadPlusPlusOperatorAction)
                .Run(summaryFilePathProviderAction)
                .Run(projectRepositoryAction)
                .AddSingleton<O007a_UpdateRepositoryWithAllProjects>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O007_UpdateRepositoryWithAllProjects"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO007_UpdateRepositoryWithAllProjects(this IServiceCollection services,
            IServiceAction<O004_BackupFileBasedProjectRepositoryFiles> o004_BackupFileBasedProjectRepositoryFilesAction,
            IServiceAction<O007a_UpdateRepositoryWithAllProjects> o007a_UpdateRepositoryWithAllProjectsAction)
        {
            services
                .Run(o004_BackupFileBasedProjectRepositoryFilesAction)
                .Run(o007a_UpdateRepositoryWithAllProjectsAction)
                .AddSingleton<O007_UpdateRepositoryWithAllProjects>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O005_UpdateProjectIntellisense"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO005_UpdateProjectIntellisense(this IServiceCollection services,
            IServiceAction<IProjectPathExtensionMethodBaseProjectPathProvider> projectPathExtensionMethodBaseProjectPathProviderAction,
            IServiceAction<IProjectRepository> projectRepositoryAction,
            IServiceAction<IRepositoriesDirectoryPathProvider> repositoriesDirectoryPathProviderAction,
            IServiceAction<IVisualStudioProjectFileOperator> visualStudioProjectFileOperatorAction,
            IServiceAction<IVisualStudioSolutionFileOperator> visualStudioSolutionFileOperatorAction)
        {
            services
                .Run(projectPathExtensionMethodBaseProjectPathProviderAction)
                .Run(projectRepositoryAction)
                .Run(repositoriesDirectoryPathProviderAction)
                .Run(visualStudioProjectFileOperatorAction)
                .Run(visualStudioSolutionFileOperatorAction)
                .AddSingleton<O005_UpdateProjectIntellisense>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O004_BackupFileBasedProjectRepositoryFiles"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO004_BackupFileBasedProjectRepositoryFiles(this IServiceCollection services,
            IServiceAction<IBackupProjectRepositoryFilePathsProvider> backupProjectRepositoryFilePathsProviderAction,
            IServiceAction<IHumanOutput> humanOutputAction,
            IServiceAction<IProjectRepositoryFilePathsProvider> projectRepositoryFilePathsProviderAction)
        {
            services
                .Run(backupProjectRepositoryFilePathsProviderAction)
                .Run(humanOutputAction)
                .Run(projectRepositoryFilePathsProviderAction)
                .AddSingleton<O004_BackupFileBasedProjectRepositoryFiles>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O003b_PromptForRequiredHumanActionsCore"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO003b_PromptForRequiredHumanActionsCore(this IServiceCollection services,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<IProjectRepositoryFilePathsProvider> projectRepositoryFilePathsProviderAction,
            IServiceAction<ISummaryFilePathProvider> summaryFilePathProviderAction)
        {
            services
                .Run(notepadPlusPlusOperatorAction)
                .Run(projectRepositoryFilePathsProviderAction)
                .Run(summaryFilePathProviderAction)
                .AddSingleton<O003b_PromptForRequiredHumanActionsCore>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O003a_DetermineIfHumanActionsAreRequired"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO003a_DetermineIfHumanActionsAreRequired(this IServiceCollection services)
        {
            services.AddSingleton<O003a_DetermineIfHumanActionsAreRequired>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O003_PerformRequiredHumanActions"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO003_PerformRequiredHumanActions(this IServiceCollection services,
            IServiceAction<IAllProjectFilePathsProvider> allProjectFilePathsProviderAction,
            IServiceAction<IProjectRepository> projectRepositoryAction,
            IServiceAction<O003a_DetermineIfHumanActionsAreRequired> o003a_DetermineIfHumanActionsAreRequiredAction,
            IServiceAction<O003b_PromptForRequiredHumanActionsCore> o003b_PromptForRequiredHumanActionsCoreAction)
        {
            services
                .Run(allProjectFilePathsProviderAction)
                .Run(projectRepositoryAction)
                .Run(o003a_DetermineIfHumanActionsAreRequiredAction)
                .Run(o003b_PromptForRequiredHumanActionsCoreAction)
                .AddSingleton<O003_PerformRequiredHumanActions>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O002_UpdateFileBasedProjectRepository"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO002_UpdateFileBasedProjectRepository(this IServiceCollection services,
            IServiceAction<IAllProjectFilePathsProvider> allProjectFilePathsProviderAction,
            IServiceAction<IProjectRepository> projectRepositoryAction,
            IServiceAction<O009_UpdateAllProjectNamesListingFile> o009_UpdateAllProjectNamesListingFileAction)
        {
            services
                .Run(allProjectFilePathsProviderAction)
                .Run(projectRepositoryAction)
                .Run(o009_UpdateAllProjectNamesListingFileAction)
                .AddSingleton<O002_UpdateFileBasedProjectRepository>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="O001_AnalyzeAllCurrentProjects"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddO001_AnalyzeAllCurrentProjects(this IServiceCollection services,
            IServiceAction<IAllProjectFilePathsProvider> allProjectFilePathsProviderAction,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<ISummaryFilePathProvider> summaryFilePathProviderAction,
            IServiceAction<IProjectRepository> projectRepositoryAction)
        {
            services
                .Run(allProjectFilePathsProviderAction)
                .Run(notepadPlusPlusOperatorAction)
                .Run(summaryFilePathProviderAction)
                .Run(projectRepositoryAction)
                .AddSingleton<O001_AnalyzeAllCurrentProjects>();

            return services;
        }
    }
}
