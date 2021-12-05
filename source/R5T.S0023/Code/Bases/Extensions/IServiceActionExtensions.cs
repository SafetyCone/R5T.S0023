using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.Quadia.D002;

using R5T.D0048;
using R5T.D0084.D002;
using R5T.D0101.I001;
using R5T.T0062;
using R5T.T0063;


namespace R5T.S0023
{
    public static partial class IServiceActionExtensions
    {
        /// <summary>
        /// Adds the <see cref="BackupProjectRepositoryFilePathsProvider"/> implementation of <see cref="IBackupProjectRepositoryFilePathsProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IBackupProjectRepositoryFilePathsProvider> AddBackupProjectRepositoryFilePathsProviderAction(this IServiceAction _,
            IServiceAction<IOutputFilePathProvider> outputFilePathProviderAction)
        {
            var serviceAction = _.New<IBackupProjectRepositoryFilePathsProvider>(services => services.AddBackupProjectRepositoryFilePathsProvider(
                outputFilePathProviderAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="SummaryFilePathProvider"/> implementation of <see cref="ISummaryFilePathProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<ISummaryFilePathProvider> AddSummaryFilePathProviderAction(this IServiceAction _,
            IServiceAction<IOutputFilePathProvider> outputFilePathProviderAction)
        {
            var serviceAction = _.New<ISummaryFilePathProvider>(services => services.AddSummaryFilePathProvider(
                outputFilePathProviderAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="ConstructorBasedAllProjectNamesListingFileNameProvider"/> implementation of <see cref="IAllProjectNamesListingFileNameProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IAllProjectNamesListingFileNameProvider> AddConstructorBasedAllProjectNamesListingFileNameProviderAction(this IServiceAction _,
            string allProjectNamesListingFileName)
        {
            var serviceAction = _.New<IAllProjectNamesListingFileNameProvider>(services => services.AddConstructorBasedAllProjectNamesListingFileNameProvider(
                allProjectNamesListingFileName));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="AllProjectNamesListingFilePathProvider"/> implementation of <see cref="IAllProjectNamesListingFilePathProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IAllProjectNamesListingFilePathProvider> AddAllProjectNamesListingFilePathProviderAction(this IServiceAction _,
            IServiceAction<IAllProjectNamesListingFileNameProvider> allProjectNamesListingFileNameProviderAction,
            IServiceAction<IOrganizationSharedDataDirectoryFilePathProvider> organizationSharedDataDirectoryFilePathProviderAction)
        {
            var serviceAction = _.New<IAllProjectNamesListingFilePathProvider>(services => services.AddAllProjectNamesListingFilePathProvider(
                allProjectNamesListingFileNameProviderAction,
                organizationSharedDataDirectoryFilePathProviderAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="HardCodedRepositoriesDirectoryPathProvider"/> implementation of <see cref="IRepositoriesDirectoryPathProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IRepositoriesDirectoryPathProvider> AddHardCodedRepositoriesDirectoryPathProviderAction(this IServiceAction _)
        {
            var serviceAction = _.New<IRepositoriesDirectoryPathProvider>(services => services.AddHardCodedRepositoriesDirectoryPathProvider());
            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="HardCodedProjectRepositoryFilePathsProvider"/> implementation of <see cref="IProjectRepositoryFilePathsProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IProjectRepositoryFilePathsProvider> AddHardCodedProjectRepositoryFilePathsProviderAction(this IServiceAction _)
        {
            var serviceAction = _.New<IProjectRepositoryFilePathsProvider>(services => services.AddHardCodedProjectRepositoryFilePathsProvider());
            return serviceAction;
        }

        public static IServiceAction<HostStartup> AddStartupAction(this IServiceAction _)
        {
            var output = _.New<HostStartup>(services => services.AddHostStartup());

            return output;
        }
    }
}
