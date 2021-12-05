using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.Quadia.D002;

using R5T.D0048;
using R5T.D0084.D002;
using R5T.D0101.I001;
using R5T.T0063;

using R5T.S0023.Startup;


namespace R5T.S0023
{
    public static partial class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the <see cref="BackupProjectRepositoryFilePathsProvider"/> implementation of <see cref="IBackupProjectRepositoryFilePathsProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddBackupProjectRepositoryFilePathsProvider(this IServiceCollection services,
            IServiceAction<IOutputFilePathProvider> outputFilePathProviderAction)
        {
            services
                .Run(outputFilePathProviderAction)
                .AddSingleton<IBackupProjectRepositoryFilePathsProvider, BackupProjectRepositoryFilePathsProvider>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="SummaryFilePathProvider"/> implementation of <see cref="ISummaryFilePathProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddSummaryFilePathProvider(this IServiceCollection services,
            IServiceAction<IOutputFilePathProvider> outputFilePathProviderAction)
        {
            services
                .Run(outputFilePathProviderAction)
                .AddSingleton<ISummaryFilePathProvider, SummaryFilePathProvider>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="ConstructorBasedAllProjectNamesListingFileNameProvider"/> implementation of <see cref="IAllProjectNamesListingFileNameProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddConstructorBasedAllProjectNamesListingFileNameProvider(this IServiceCollection services,
            string allProjectNamesListingFileName)
        {
            services.AddSingleton<IAllProjectNamesListingFileNameProvider>(_ => new ConstructorBasedAllProjectNamesListingFileNameProvider(
                allProjectNamesListingFileName));

            return services;
        }

        /// <summary>
        /// Adds the <see cref="AllProjectNamesListingFilePathProvider"/> implementation of <see cref="IAllProjectNamesListingFilePathProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddAllProjectNamesListingFilePathProvider(this IServiceCollection services,
            IServiceAction<IAllProjectNamesListingFileNameProvider> allProjectNamesListingFileNameProviderAction,
            IServiceAction<IOrganizationSharedDataDirectoryFilePathProvider> organizationSharedDataDirectoryFilePathProviderAction)
        {
            services
                .Run(allProjectNamesListingFileNameProviderAction)
                .Run(organizationSharedDataDirectoryFilePathProviderAction)
                .AddSingleton<IAllProjectNamesListingFilePathProvider, AllProjectNamesListingFilePathProvider>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="HardCodedRepositoriesDirectoryPathProvider"/> implementation of <see cref="IRepositoriesDirectoryPathProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddHardCodedRepositoriesDirectoryPathProvider(this IServiceCollection services)
        {
            services.AddSingleton<IRepositoriesDirectoryPathProvider, HardCodedRepositoriesDirectoryPathProvider>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="HardCodedProjectRepositoryFilePathsProvider"/> implementation of <see cref="IProjectRepositoryFilePathsProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddHardCodedProjectRepositoryFilePathsProvider(this IServiceCollection services)
        {
            services.AddSingleton<IProjectRepositoryFilePathsProvider, HardCodedProjectRepositoryFilePathsProvider>();

            return services;
        }

        public static IServiceCollection AddHostStartup(this IServiceCollection services)
        {
            var dependencyServiceActions = new DependencyServiceActionAggregation();

            services.AddHostStartup<HostStartup>(dependencyServiceActions)
                // Add services required by HostStartup, but not by HostStartupBase.
                ;

            return services;
        }
    }
}
