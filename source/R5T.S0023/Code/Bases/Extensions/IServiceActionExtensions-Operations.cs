using System;

using R5T.D0078;
using R5T.D0079;
using R5T.D0084.D001;
using R5T.D0084.D002;
using R5T.D0096;
using R5T.D0101;
using R5T.D0101.I001;
using R5T.D0110;
using R5T.D0105;
using R5T.T0062;
using R5T.T0063;


namespace R5T.S0023
{
    public static partial class IServiceActionExtensions
    {
        /// <summary>
        /// Adds the <see cref="O000_Main"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O000_Main> AddO000_MainAction(this IServiceAction _,
            IServiceAction<O101_UpdateRepository> o101_UpdateRepositoryAction)
        {
            var serviceAction = _.New<O000_Main>(services => services.AddO000_Main(
                o101_UpdateRepositoryAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O900_OpenAllProjectRepositoryFiles"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O900_OpenAllProjectRepositoryFiles> AddO900_OpenAllProjectRepositoryFilesAction(this IServiceAction _,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<IProjectRepositoryFilePathsProvider> projectRepositoryFilePathsProviderAction)
        {
            var serviceAction = _.New<O900_OpenAllProjectRepositoryFiles>(services => services.AddO900_OpenAllProjectRepositoryFiles(
                notepadPlusPlusOperatorAction,
                projectRepositoryFilePathsProviderAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O101_UpdateRepository"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O101_UpdateRepository> AddO101_UpdateRepositoryAction(this IServiceAction _,
            IServiceAction<O004_BackupFileBasedProjectRepositoryFiles> o004_BackupFileBasedProjectRepositoryFilesAction,
            IServiceAction<O005_UpdateProjectIntellisense> o005_UpdateProjectIntellisenseAction,
            IServiceAction<O007a_UpdateRepositoryWithAllProjects> o007a_UpdateRepositoryWithAllProjectsAction,
            IServiceAction<O008a_UpdateRepositoryWithSelectedProjects> o008a_UpdateRepositoryWithSelectedProjectsAction,
            IServiceAction<O009_UpdateAllProjectNamesListingFile> o009_UpdateAllProjectNamesListingFileAction)
        {
            var serviceAction = _.New<O101_UpdateRepository>(services => services.AddO101_UpdateRepository(
                o004_BackupFileBasedProjectRepositoryFilesAction,
                o005_UpdateProjectIntellisenseAction,
                o007a_UpdateRepositoryWithAllProjectsAction,
                o008a_UpdateRepositoryWithSelectedProjectsAction,
                o009_UpdateAllProjectNamesListingFileAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O100_UpdateProjectRepositoryWithCurrentProjects"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O100_UpdateProjectRepositoryWithCurrentProjects> AddO100_UpdateProjectRepositoryWithCurrentProjectsAction(this IServiceAction _,
            IServiceAction<O001_AnalyzeAllCurrentProjects> o001_AnalyzeAllCurrentProjectsAction,
            IServiceAction<O002_UpdateFileBasedProjectRepository> o002_UpdateFileBasedProjectRepositoryAction,
            IServiceAction<O003_PerformRequiredHumanActions> o003_PerformRequiredHumanActionsAction,
            IServiceAction<O004_BackupFileBasedProjectRepositoryFiles> o004_BackupFileBasedProjectRepositoryFilesAction,
            IServiceAction<O005_UpdateProjectIntellisense> o005_UpdateProjectIntellisenseAction)
        {
            var serviceAction = _.New<O100_UpdateProjectRepositoryWithCurrentProjects>(services => services.AddO100_UpdateProjectRepositoryWithCurrentProjects(
                o001_AnalyzeAllCurrentProjectsAction,
                o002_UpdateFileBasedProjectRepositoryAction,
                o003_PerformRequiredHumanActionsAction,
                o004_BackupFileBasedProjectRepositoryFilesAction,
                o005_UpdateProjectIntellisenseAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O009_UpdateAllProjectNamesListingFile"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O009_UpdateAllProjectNamesListingFile> AddO009_UpdateAllProjectNamesListingFileAction(this IServiceAction _,
            IServiceAction<IAllProjectNamesListingFilePathProvider> allProjectNamesListingFilePathProviderAction,
            IServiceAction<IProjectRepository> projectRepositoryAction)
        {
            var serviceAction = _.New<O009_UpdateAllProjectNamesListingFile>(services => services.AddO009_UpdateAllProjectNamesListingFile(
                allProjectNamesListingFilePathProviderAction,
                projectRepositoryAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O008a_UpdateRepositoryWithSelectedProjects"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O008a_UpdateRepositoryWithSelectedProjects> AddO008a_UpdateRepositoryWithSelectedProjectsAction(this IServiceAction _,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<IProjectRepository> projectRepositoryAction)
        {
            var serviceAction = _.New<O008a_UpdateRepositoryWithSelectedProjects>(services => services.AddO008a_UpdateRepositoryWithSelectedProjects(
                notepadPlusPlusOperatorAction,
                projectRepositoryAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O008_UpdateRepositoryWithSelectedProjects"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O008_UpdateRepositoryWithSelectedProjects> AddO008_UpdateRepositoryWithSelectedProjectsAction(this IServiceAction _,
            IServiceAction<O004_BackupFileBasedProjectRepositoryFiles> o004_BackupFileBasedProjectRepositoryFilesAction,
            IServiceAction<O008a_UpdateRepositoryWithSelectedProjects> o008a_UpdateRepositoryWithSelectedProjectsAction)
        {
            var serviceAction = _.New<O008_UpdateRepositoryWithSelectedProjects>(services => services.AddO008_UpdateRepositoryWithSelectedProjects(
                o004_BackupFileBasedProjectRepositoryFilesAction,
                o008a_UpdateRepositoryWithSelectedProjectsAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O007a_UpdateRepositoryWithAllProjects"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O007a_UpdateRepositoryWithAllProjects> AddO007a_UpdateRepositoryWithAllProjects(this IServiceAction _,
            IServiceAction<IAllProjectFilePathsProvider> allProjectFilePathsProviderAction,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<ISummaryFilePathProvider> summaryFilePathProviderAction,
            IServiceAction<IProjectRepository> projectRepositoryAction)
        {
            var serviceAction = _.New<O007a_UpdateRepositoryWithAllProjects>(services => services.AddO007a_UpdateRepositoryWithAllProjects(
                allProjectFilePathsProviderAction,
                notepadPlusPlusOperatorAction,
                summaryFilePathProviderAction,
                projectRepositoryAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O007_UpdateRepositoryWithAllProjects"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O007_UpdateRepositoryWithAllProjects> AddO007_UpdateRepositoryWithAllProjects(this IServiceAction _,
            IServiceAction<O004_BackupFileBasedProjectRepositoryFiles> o004_BackupFileBasedProjectRepositoryFilesAction,
            IServiceAction<O007a_UpdateRepositoryWithAllProjects> o007a_UpdateRepositoryWithAllProjectsAction)
        {
            var serviceAction = _.New<O007_UpdateRepositoryWithAllProjects>(services => services.AddO007_UpdateRepositoryWithAllProjects(
                o004_BackupFileBasedProjectRepositoryFilesAction,
                o007a_UpdateRepositoryWithAllProjectsAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O005_UpdateProjectIntellisense"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O005_UpdateProjectIntellisense> AddO005_UpdateProjectIntellisenseAction(this IServiceAction _,
            IServiceAction<IProjectPathExtensionMethodBaseProjectPathProvider> projectPathExtensionMethodBaseProjectPathProviderAction,
            IServiceAction<IProjectRepository> projectRepositoryAction,
            IServiceAction<IRepositoriesDirectoryPathProvider> repositoriesDirectoryPathProviderAction,
            IServiceAction<IVisualStudioProjectFileOperator> visualStudioProjectFileOperatorAction,
            IServiceAction<IVisualStudioSolutionFileOperator> visualStudioSolutionFileOperatorAction)
        {
            var serviceAction = _.New<O005_UpdateProjectIntellisense>(services => services.AddO005_UpdateProjectIntellisense(
                projectPathExtensionMethodBaseProjectPathProviderAction,
                projectRepositoryAction,
                repositoriesDirectoryPathProviderAction,
                visualStudioProjectFileOperatorAction,
                visualStudioSolutionFileOperatorAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O004_BackupFileBasedProjectRepositoryFiles"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O004_BackupFileBasedProjectRepositoryFiles> AddO004_BackupFileBasedProjectRepositoryFilesAction(this IServiceAction _,
            IServiceAction<IBackupProjectRepositoryFilePathsProvider> backupProjectRepositoryFilePathsProviderAction,
            IServiceAction<IHumanOutput> humanOutputAction,
            IServiceAction<IProjectRepositoryFilePathsProvider> projectRepositoryFilePathsProviderAction)
        {
            var serviceAction = _.New<O004_BackupFileBasedProjectRepositoryFiles>(services => services.AddO004_BackupFileBasedProjectRepositoryFiles(
                backupProjectRepositoryFilePathsProviderAction, 
                humanOutputAction,
                projectRepositoryFilePathsProviderAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O003b_PromptForRequiredHumanActionsCore"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O003b_PromptForRequiredHumanActionsCore> AddO003b_PromptForRequiredHumanActionsCoreAction(this IServiceAction _,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<IProjectRepositoryFilePathsProvider> projectRepositoryFilePathsProviderAction,
            IServiceAction<ISummaryFilePathProvider> summaryFilePathProviderAction)
        {
            var serviceAction = _.New<O003b_PromptForRequiredHumanActionsCore>(services => services.AddO003b_PromptForRequiredHumanActionsCore(
                notepadPlusPlusOperatorAction,
                projectRepositoryFilePathsProviderAction,
                summaryFilePathProviderAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O003a_DetermineIfHumanActionsAreRequired"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O003a_DetermineIfHumanActionsAreRequired> AddO003a_DetermineIfHumanActionsAreRequiredAction(this IServiceAction _)
        {
            var serviceAction = _.New<O003a_DetermineIfHumanActionsAreRequired>(services => services.AddO003a_DetermineIfHumanActionsAreRequired());
            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O003_PerformRequiredHumanActions"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O003_PerformRequiredHumanActions> AddO003_PerformRequiredHumanActionsAction(this IServiceAction _,
            IServiceAction<IAllProjectFilePathsProvider> allProjectFilePathsProviderAction,
            IServiceAction<IProjectRepository> projectRepositoryAction,
            IServiceAction<O003a_DetermineIfHumanActionsAreRequired> o003a_DetermineIfHumanActionsAreRequiredAction,
            IServiceAction<O003b_PromptForRequiredHumanActionsCore> o003b_PromptForRequiredHumanActionsCoreAction)
        {
            var serviceAction = _.New<O003_PerformRequiredHumanActions>(services => services.AddO003_PerformRequiredHumanActions(
                allProjectFilePathsProviderAction,
                projectRepositoryAction,
                o003a_DetermineIfHumanActionsAreRequiredAction,
                o003b_PromptForRequiredHumanActionsCoreAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O002_UpdateFileBasedProjectRepository"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O002_UpdateFileBasedProjectRepository> AddO002_UpdateFileBasedProjectRepositoryAction(this IServiceAction _,
            IServiceAction<IAllProjectFilePathsProvider> allProjectFilePathsProviderAction,
            IServiceAction<IProjectRepository> projectRepositoryAction,
            IServiceAction<O009_UpdateAllProjectNamesListingFile> o009_UpdateAllProjectNamesListingFileAction)
        {
            var serviceAction = _.New<O002_UpdateFileBasedProjectRepository>(services => services.AddO002_UpdateFileBasedProjectRepository(
                allProjectFilePathsProviderAction,
                projectRepositoryAction,
                o009_UpdateAllProjectNamesListingFileAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="O001_AnalyzeAllCurrentProjects"/> operation as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<O001_AnalyzeAllCurrentProjects> AddO001_AnalyzeAllCurrentProjectsAction(this IServiceAction _,
            IServiceAction<IAllProjectFilePathsProvider> allProjectFilePathsProviderAction,
            IServiceAction<INotepadPlusPlusOperator> notepadPlusPlusOperatorAction,
            IServiceAction<ISummaryFilePathProvider> summaryFilePathProviderAction,
            IServiceAction<IProjectRepository> projectRepositoryAction)
        {
            var serviceAction = _.New<O001_AnalyzeAllCurrentProjects>(services => services.AddO001_AnalyzeAllCurrentProjects(
                allProjectFilePathsProviderAction,
                notepadPlusPlusOperatorAction,
                summaryFilePathProviderAction,
                projectRepositoryAction));

            return serviceAction;
        }
    }
}
