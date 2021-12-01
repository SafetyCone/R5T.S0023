using System;
using System.Collections.Generic;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using R5T.Lombardy;
using R5T.Quadia;

using R5T.D0075;
using R5T.D0081;
using R5T.D0084.D002;
using R5T.D0095.D001;
using R5T.D0096;
using R5T.D0096.D003;
using R5T.D0098;
using R5T.D0099.D003;
using R5T.D0101.I001;
using R5T.L0017.D001;
using R5T.T0063;

using R5T.S0023.Startup;


namespace R5T.S0023
{
    public static partial class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the <see cref="OrganizationSharedDataDirectoryPathProvider"/> implementation of <see cref="IOrganizationSharedDataDirectoryPathProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddOrganizationSharedDataDirectoryPathProvider(this IServiceCollection services,
            IServiceAction<IOrganizationDataDirectoryPathProvider> organizationDataDirectoryPathProviderAction)
        {
            services
                .Run(organizationDataDirectoryPathProviderAction)
                .AddSingleton<IOrganizationSharedDataDirectoryPathProvider, OrganizationSharedDataDirectoryPathProvider>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="OrganizationSharedDataDirectoryFilePathProvider"/> implementation of <see cref="IOrganizationSharedDataDirectoryFilePathProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddOrganizationSharedDataDirectoryFilePathProvider(this IServiceCollection services,
            IServiceAction<IOrganizationSharedDataDirectoryPathProvider> organizationSharedDataDirectoryPathProviderAction,
            IServiceAction<IStringlyTypedPathOperator> stringlyTypedPathOperatorAction)
        {
            services
                .Run(organizationSharedDataDirectoryPathProviderAction)
                .Run(stringlyTypedPathOperatorAction)
                .AddSingleton<IOrganizationSharedDataDirectoryFilePathProvider, OrganizationSharedDataDirectoryFilePathProvider>();

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
        /// Adds the <see cref="NotepadPlusPlusOperator"/> implementation of <see cref="INotepadPlusPlusOperator"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddNotepadPlusPlusOperator(this IServiceCollection services,
            IServiceAction<ICommandLineOperator> commandLineOperatorAction,
            IServiceAction<INotepadPlusPlusExecutableFilePathProvider> notepadPlusPlusExecutableFilePathProviderAction)
        {
            services
                .Run(commandLineOperatorAction)
                .Run(notepadPlusPlusExecutableFilePathProviderAction)
                .AddSingleton<INotepadPlusPlusOperator, NotepadPlusPlusOperator>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="HardCodedNotepadPlusPlusExecutableFilePathProvider"/> implementation of <see cref="INotepadPlusPlusExecutableFilePathProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddHardCodedNotepadPlusPlusExecutableFilePathProvider(this IServiceCollection services)
        {
            services.AddSingleton<INotepadPlusPlusExecutableFilePathProvider, HardCodedNotepadPlusPlusExecutableFilePathProvider>();

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

        /// <summary>
        /// Adds the <see cref="ConfigurationAuditSerializer"/> implementation of <see cref="IConfigurationAuditSerializer"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddConfigurationAuditSerializer(this IServiceCollection services,
            IServiceAction<IConfiguration> configurationAction,
            IServiceAction<IConfigurationSerializationFilePathProvider> configurationSerializationFilePathProviderAction)
        {
            services
                .Run(configurationAction)
                .Run(configurationSerializationFilePathProviderAction)
                .AddSingleton<IConfigurationAuditSerializer, ConfigurationAuditSerializer>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="ConfigurationSerializationFilePathProvider"/> implementation of <see cref="IConfigurationSerializationFilePathProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddConfigurationSerializationFilePathProvider(this IServiceCollection services,
            IServiceAction<IConfigurationSerializationFileNameProvider> configurationSerializationFileNameProviderAction,
            IServiceAction<IOutputFilePathProvider> outputFilePathProviderAction)
        {
            services
                .Run(configurationSerializationFileNameProviderAction)
                .Run(outputFilePathProviderAction)
                .AddSingleton<IConfigurationSerializationFilePathProvider, ConfigurationSerializationFilePathProvider>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="ConstructorBasedConfigurationSerializationFileNameProvider"/> implementation of <see cref="IConfigurationSerializationFileNameProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddConstructorBasedConfigurationSerializationFileNameProvider(this IServiceCollection services,
            string configurationSerializationFileName)
        {
            services.AddSingleton<IConfigurationSerializationFileNameProvider>(_ => new ConstructorBasedConfigurationSerializationFileNameProvider(
                configurationSerializationFileName));

            return services;
        }

        /// <summary>
        /// Adds the <see cref="ServiceCollectionAuditSerializer"/> implementation of <see cref="IServiceCollectionAuditSerializer"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddServiceCollectionAuditSerializer(this IServiceCollection services,
            IServiceAction<IServiceCollection> serviceCollectionAction,
            IServiceAction<IServiceCollectionSerializationFilePathProvider> serviceCollectionSerializationFilePathProviderAction)
        {
            services
                .Run(serviceCollectionAction)
                .Run(serviceCollectionSerializationFilePathProviderAction)
                .AddSingleton<IServiceCollectionAuditSerializer, ServiceCollectionAuditSerializer>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="ServiceCollectionSerializationFilePathProvider"/> implementation of <see cref="IServiceCollectionSerializationFilePathProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddServiceCollectionSerializationFilePathProvider(this IServiceCollection services,
            IServiceAction<IOutputFilePathProvider> outputFilePathProviderAction,
            IServiceAction<IServiceCollectionSerializationFileNameProvider> serviceCollectionSerializationFileNameProviderAction)
        {
            services
                .Run(outputFilePathProviderAction)
                .Run(serviceCollectionSerializationFileNameProviderAction)
                .AddSingleton<IServiceCollectionSerializationFilePathProvider, ServiceCollectionSerializationFilePathProvider>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="ConstructorBasedServiceCollectionSerializationFileNameProvider"/> implementation of <see cref="IServiceCollectionSerializationFileNameProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddConstructorBasedServiceCollectionSerializationFileNameProvider(this IServiceCollection services,
            string serviceCollectionSerializationFileName)
        {
            services.AddSingleton<IServiceCollectionSerializationFileNameProvider>(_ => new ConstructorBasedServiceCollectionSerializationFileNameProvider(
                serviceCollectionSerializationFileName));

            return services;
        }

        /// <summary>
        /// Adds the <see cref="ServiceCollectionForensicInvestigation"/> implementation of <see cref="IServiceCollectionForensicInvestigation"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddServiceCollectionForensicInvestigation(this IServiceCollection services,
            IServiceAction<IServiceCollection> serviceCollectionAction)
        {
            services
                .Run(serviceCollectionAction)
                .AddSingleton<IServiceCollectionForensicInvestigation, ServiceCollectionForensicInvestigation>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="IServiceCollection"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddServiceCollection(this IServiceCollection services)
        {
            services.AddSingleton<IServiceCollection>(_ => services);

            return services;
        }

        /// <summary>
        /// Adds the <see cref="OverridableProcessStartTimeProvider"/> implementation of <see cref="IProcessStartTimeProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddOverridableProcessStartTimeProvider(this IServiceCollection services,
            IServiceAction<ICurrentProcessStartTimeProvider> currentProcessStartTimeProviderAction)
        {
            services
                .Run(currentProcessStartTimeProviderAction)
                .AddSingleton<IProcessStartTimeProvider, OverridableProcessStartTimeProvider>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="MachineOutputFilePathProvider"/> implementation of <see cref="IMachineOutputFilePathProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddMachineOutputFilePathProvider(this IServiceCollection services,
            IServiceAction<IMachineOutputFileNameProvider> machineOutputFileNameProviderAction,
            IServiceAction<IOutputFilePathProvider> outputFilePathProviderAction)
        {
            services
                .Run(machineOutputFileNameProviderAction)
                .Run(outputFilePathProviderAction)
                .AddSingleton<IMachineOutputFilePathProvider, MachineOutputFilePathProvider>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="HumanOutputFilePathProvider"/> implementation of <see cref="IHumanOutputFilePathProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddHumanOutputFilePathProvider(this IServiceCollection services,
            IServiceAction<IHumanOutputFileNameProvider> humanOutputFileNameProviderAction,
            IServiceAction<IOutputFilePathProvider> outputFilePathProviderAction)
        {
            services
                .Run(humanOutputFileNameProviderAction)
                .Run(outputFilePathProviderAction)
                .AddSingleton<IHumanOutputFilePathProvider, HumanOutputFilePathProvider>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="LogFilePathProvider"/> implementation of <see cref="ILogFilePathProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddLogFilePathProvider(this IServiceCollection services,
            IServiceAction<ILogFileNameProvider> logFileNameProviderAction,
            IServiceAction<IOutputFilePathProvider> outputFilePathProviderAction)
        {
            services
                .Run(logFileNameProviderAction)
                .Run(outputFilePathProviderAction)
                .AddSingleton<ILogFilePathProvider, LogFilePathProvider>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="ConstructorBasedLogFileNameProvider"/> implementation of <see cref="ILogFileNameProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddConstructorBasedLogFileNameProvider(this IServiceCollection services,
            string logFileName)
        {
            services.AddSingleton<ILogFileNameProvider>(sp => new ConstructorBasedLogFileNameProvider(logFileName));

            return services;
        }

        /// <summary>
        /// Adds the <see cref="ConstructorBasedProcessNameProvider"/> implementation of <see cref="IProcessNameProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddConstructorBasedProcessNameProvider(this IServiceCollection services,
            string processName)
        {
            services.AddSingleton<IProcessNameProvider>(_ => new ConstructorBasedProcessNameProvider(
                processName));

            return services;
        }

        /// <summary>
        /// Adds the <see cref="ConstructorBasedProcessStartTimeProvider"/> implementation of <see cref="IProcessStartTimeProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddConstructorBasedProcessStartTimeProvider(this IServiceCollection services,
            DateTime processStartTime)
        {
            services.AddSingleton<IProcessStartTimeProvider>(_ => new ConstructorBasedProcessStartTimeProvider(
                processStartTime));

            return services;
        }

        /// <summary>
        /// Adds the <see cref="ConstructorBasedRootOutputDirectoryPathProvider"/> implementation of <see cref="IRootOutputDirectoryPathProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddConstructorBasedRootOutputDirectoryPathProvider(this IServiceCollection services,
            string rootOutputDirectoryPath)
        {
            services.AddSingleton<IRootOutputDirectoryPathProvider>(_ => new ConstructorBasedRootOutputDirectoryPathProvider(
                rootOutputDirectoryPath));

            return services;
        }

        /// <summary>
        /// Adds the <see cref="CurrentProcessStartTimeProvider"/> implementation of <see cref="ICurrentProcessStartTimeProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddCurrentProcessStartTimeProvider(this IServiceCollection services)
        {
            services.AddSingleton<ICurrentProcessStartTimeProvider, CurrentProcessStartTimeProvider>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="DirectDirectoryNameProvider"/> implementation of <see cref="IDirectoryNameProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddDirectDirectoryNameProvider(this IServiceCollection services)
        {
            services.AddSingleton<IDirectoryNameProvider, DirectDirectoryNameProvider>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="EntryPointAssemblyProcessNameProvider"/> implementation of <see cref="IProcessNameProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddEntryPointAssemblyProcessNameProvider(this IServiceCollection services)
        {
            services.AddSingleton<IProcessNameProvider, EntryPointAssemblyProcessNameProvider>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="OutputDirectoryPathProvider"/> implementation of <see cref="IOutputDirectoryPathProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddOutputDirectoryPathProvider(this IServiceCollection services,
            IServiceAction<IProcessStartTimeSpecificOutputDirectoryPathProvider> processStartTimeSpecificOutputDirectoryPathProviderAction)
        {
            services
                .Run(processStartTimeSpecificOutputDirectoryPathProviderAction)
                .AddSingleton<IOutputDirectoryPathProvider, OutputDirectoryPathProvider>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="OutputFilePathProvider"/> implementation of <see cref="IOutputFilePathProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddOutputFilePathProvider(this IServiceCollection services,
            IServiceAction<IOutputDirectoryPathProvider> outputDirectoryPathProviderAction,
            IServiceAction<IStringlyTypedPathOperator> stringlyTypedPathOperatorAction)
        {
            services
                .Run(outputDirectoryPathProviderAction)
                .Run(stringlyTypedPathOperatorAction)
                .AddSingleton<IOutputFilePathProvider, OutputFilePathProvider>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="ProcessDirectoryNameProvider"/> implementation of <see cref="IProcessDirectoryNameProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddProcessDirectoryNameProvider(this IServiceCollection services,
            IServiceAction<IProcessNameProvider> processNameProviderAction,
            IServiceAction<IDirectoryNameProvider> directoryNameProviderAction)
        {
            services
                .Run(processNameProviderAction)
                .Run(directoryNameProviderAction)
                .AddSingleton<IProcessDirectoryNameProvider, ProcessDirectoryNameProvider>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="ProcessSpecificOutputDirectoryPathProvider"/> implementation of <see cref="IProcessSpecificOutputDirectoryPathProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddProcessSpecificOutputDirectoryPathProvider(this IServiceCollection services,
            IServiceAction<IProcessDirectoryNameProvider> processDirectoryNameProviderAction,
            IServiceAction<IRootOutputDirectoryPathProvider> rootOutputDirectoryPathProviderAction,
            IServiceAction<IStringlyTypedPathOperator> stringlyTypedPathOperatorAction)
        {
            services
                .Run(processDirectoryNameProviderAction)
                .Run(rootOutputDirectoryPathProviderAction)
                .Run(stringlyTypedPathOperatorAction)
                .AddSingleton<IProcessSpecificOutputDirectoryPathProvider, ProcessSpecificOutputDirectoryPathProvider>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="ProcessStartTimeDirectoryNameProvider"/> implementation of <see cref="IProcessStartTimeDirectoryNameProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddProcessStartTimeDirectoryNameProvider(this IServiceCollection services,
            IServiceAction<IProcessStartTimeProvider> processStartTimeProviderAction,
            IServiceAction<IDateTimeDirectoryNameProvider> dateTimeDirectoryNameProviderAction)
        {
            services
                .Run(processStartTimeProviderAction)
                .Run(dateTimeDirectoryNameProviderAction)
                .AddSingleton<IProcessStartTimeDirectoryNameProvider, ProcessStartTimeDirectoryNameProvider>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="ProcessStartTimeSpecificOutputDirectoryPathProvider"/> implementation of <see cref="IProcessStartTimeSpecificOutputDirectoryPathProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddProcessStartTimeSpecificOutputDirectoryPathProvider(this IServiceCollection services,
            IServiceAction<IProcessSpecificOutputDirectoryPathProvider> processSpecificOutputDirectoryPathProviderAction,
            IServiceAction<IProcessStartTimeDirectoryNameProvider> processStartTimeDirectoryNameProviderAction,
            IServiceAction<IStringlyTypedPathOperator> stringlyTypedPathOperatorAction)
        {
            services
                .Run(processSpecificOutputDirectoryPathProviderAction)
                .Run(processStartTimeDirectoryNameProviderAction)
                .Run(stringlyTypedPathOperatorAction)
                .AddSingleton<IProcessStartTimeSpecificOutputDirectoryPathProvider, ProcessStartTimeSpecificOutputDirectoryPathProvider>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="StaticValuedProcessNameProvider"/> implementation of <see cref="IProcessNameProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddStaticValuedProcessNameProvider(this IServiceCollection services)
        {
            services.AddSingleton<IProcessNameProvider, StaticValuedProcessNameProvider>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="StaticValuedProcessStartTimeProvider"/> implementation of <see cref="IProcessStartTimeProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddStaticValuedProcessStartTimeProvider(this IServiceCollection services)
        {
            services.AddSingleton<IProcessStartTimeProvider, StaticValuedProcessStartTimeProvider>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="StaticValuedRootOutputDirectoryPathProvider"/> implementation of <see cref="IRootOutputDirectoryPathProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddStaticValuedRootOutputDirectoryPathProvider(this IServiceCollection services)
        {
            services.AddSingleton<IRootOutputDirectoryPathProvider, StaticValuedRootOutputDirectoryPathProvider>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="YYYYMMDD_HHMMSS_DateTimeDirectoryNameProvider"/> implementation of <see cref="IDateTimeDirectoryNameProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddYYYYMMDD_HHMMSS_DateTimeDirectoryNameProvider(this IServiceCollection services)
        {
            services.AddSingleton<IDateTimeDirectoryNameProvider, YYYYMMDD_HHMMSS_DateTimeDirectoryNameProvider>();

            return services;
        }

        /// <summary>
        /// Adds the <see cref="SimpleTextJsonSerializationHandler"/> implementation of <see cref="IMachineMessageTypeJsonSerializationHandler"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddSimpleTextJsonSerializationHandler(this IServiceCollection services)
        {
            services.AddSingleton<IMachineMessageTypeJsonSerializationHandler, SimpleTextJsonSerializationHandler>();

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
