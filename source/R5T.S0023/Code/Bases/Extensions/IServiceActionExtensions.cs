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
using R5T.T0062;
using R5T.T0063;


namespace R5T.S0023
{
    public static partial class IServiceActionExtensions
    {
        /// <summary>
        /// Adds the <see cref="OrganizationSharedDataDirectoryPathProvider"/> implementation of <see cref="IOrganizationSharedDataDirectoryPathProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IOrganizationSharedDataDirectoryPathProvider> AddOrganizationSharedDataDirectoryPathProviderAction(this IServiceAction _,
            IServiceAction<IOrganizationDataDirectoryPathProvider> organizationDataDirectoryPathProviderAction)
        {
            var serviceAction = _.New<IOrganizationSharedDataDirectoryPathProvider>(services => services.AddOrganizationSharedDataDirectoryPathProvider(
                organizationDataDirectoryPathProviderAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="OrganizationSharedDataDirectoryFilePathProvider"/> implementation of <see cref="IOrganizationSharedDataDirectoryFilePathProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IOrganizationSharedDataDirectoryFilePathProvider> AddOrganizationSharedDataDirectoryFilePathProviderAction(this IServiceAction _,
            IServiceAction<IOrganizationSharedDataDirectoryPathProvider> organizationSharedDataDirectoryPathProviderAction,
            IServiceAction<IStringlyTypedPathOperator> stringlyTypedPathOperatorAction)
        {
            var serviceAction = _.New<IOrganizationSharedDataDirectoryFilePathProvider>(services => services.AddOrganizationSharedDataDirectoryFilePathProvider(
                organizationSharedDataDirectoryPathProviderAction,
                stringlyTypedPathOperatorAction));

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
        /// Adds the <see cref="NotepadPlusPlusOperator"/> implementation of <see cref="INotepadPlusPlusOperator"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<INotepadPlusPlusOperator> AddNotepadPlusPlusOperatorAction(this IServiceAction _,
            IServiceAction<ICommandLineOperator> commandLineOperatorAction,
            IServiceAction<INotepadPlusPlusExecutableFilePathProvider> notepadPlusPlusExecutableFilePathProviderAction)
        {
            var serviceAction = _.New<INotepadPlusPlusOperator>(services => services.AddNotepadPlusPlusOperator(
                commandLineOperatorAction,
                notepadPlusPlusExecutableFilePathProviderAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="HardCodedNotepadPlusPlusExecutableFilePathProvider"/> implementation of <see cref="INotepadPlusPlusExecutableFilePathProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<INotepadPlusPlusExecutableFilePathProvider> AddHardCodedNotepadPlusPlusExecutableFilePathProviderAction(this IServiceAction _)
        {
            var serviceAction = _.New<INotepadPlusPlusExecutableFilePathProvider>(services => services.AddHardCodedNotepadPlusPlusExecutableFilePathProvider());
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

        /// <summary>       
        /// Adds the <see cref="ConfigurationAuditSerializer"/> implementation of <see cref="IConfigurationAuditSerializer "/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IConfigurationAuditSerializer> AddConfigurationAuditSerializerAction(this IServiceAction _,
            IServiceAction<IConfiguration> configurationAction,
            IServiceAction<IConfigurationSerializationFilePathProvider> configurationSerializationFilePathProviderAction)
        {
            var serviceAction = _.New<IConfigurationAuditSerializer>(services => services.AddConfigurationAuditSerializer(
                configurationAction,
                configurationSerializationFilePathProviderAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="ConfigurationSerializationFilePathProvider"/> implementation of <see cref="IConfigurationSerializationFilePathProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IConfigurationSerializationFilePathProvider> AddConfigurationSerializationFilePathProviderAction(this IServiceAction _,
            IServiceAction<IConfigurationSerializationFileNameProvider> configurationSerializationFileNameProviderAction,
            IServiceAction<IOutputFilePathProvider> outputFilePathProviderAction)
        {
            var serviceAction = _.New<IConfigurationSerializationFilePathProvider>(services => services.AddConfigurationSerializationFilePathProvider(
                configurationSerializationFileNameProviderAction,
                outputFilePathProviderAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="ConstructorBasedConfigurationSerializationFileNameProvider"/> implementation of <see cref="IConfigurationSerializationFileNameProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IConfigurationSerializationFileNameProvider> AddConstructorBasedConfigurationSerializationFileNameProviderAction(this IServiceAction _,
            string configurationSerializationFileName)
        {
            var serviceAction = _.New<IConfigurationSerializationFileNameProvider>(services => services.AddConstructorBasedConfigurationSerializationFileNameProvider(
                configurationSerializationFileName));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="ServiceCollectionAuditSerializer"/> implementation of <see cref="IServiceCollectionAuditSerializer"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IServiceCollectionAuditSerializer> AddServiceCollectionAuditSerializerAction(this IServiceAction _,
            IServiceAction<IServiceCollection> serviceCollectionAction,
            IServiceAction<IServiceCollectionSerializationFilePathProvider> serviceCollectionSerializationFilePathProviderAction)
        {
            var serviceAction = _.New<IServiceCollectionAuditSerializer>(services => services.AddServiceCollectionAuditSerializer(
                serviceCollectionAction,
                serviceCollectionSerializationFilePathProviderAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="ServiceCollectionSerializationFilePathProvider"/> implementation of <see cref="IServiceCollectionSerializationFilePathProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IServiceCollectionSerializationFilePathProvider> AddServiceCollectionSerializationFilePathProviderAction(this IServiceAction _,
            IServiceAction<IOutputFilePathProvider> outputFilePathProviderAction,
            IServiceAction<IServiceCollectionSerializationFileNameProvider> serviceCollectionSerializationFileNameProviderAction)
        {
            var serviceAction = _.New<IServiceCollectionSerializationFilePathProvider>(services => services.AddServiceCollectionSerializationFilePathProvider(
                outputFilePathProviderAction,
                serviceCollectionSerializationFileNameProviderAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="ConstructorBasedServiceCollectionSerializationFileNameProvider"/> implementation of <see cref="IServiceCollectionSerializationFileNameProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IServiceCollectionSerializationFileNameProvider> AddConstructorBasedServiceCollectionSerializationFileNameProviderAction(this IServiceAction _,
            string serviceCollectionSerializationFileName)
        {
            var serviceAction = _.New<IServiceCollectionSerializationFileNameProvider>(services => services.AddConstructorBasedServiceCollectionSerializationFileNameProvider(
                serviceCollectionSerializationFileName));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="ServiceCollectionForensicInvestigation"/> implementation of <see cref="IServiceCollectionForensicInvestigation"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IServiceCollectionForensicInvestigation> AddServiceCollectionForensicInvestigationAction(this IServiceAction _,
            IServiceAction<IServiceCollection> serviceCollectionAction)
        {
            var serviceAction = _.New<IServiceCollectionForensicInvestigation>(services => services.AddServiceCollectionForensicInvestigation(
                serviceCollectionAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="IServiceCollection"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IServiceCollection> AddServiceCollectionAction(this IServiceAction _)
        {
            var serviceAction = _.New<IServiceCollection>(services => services.AddServiceCollection());
            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="OverridableProcessStartTimeProvider"/> implementation of <see cref="IProcessStartTimeProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IProcessStartTimeProvider> AddOverridableProcessStartTimeProviderAction(this IServiceAction _,
            IServiceAction<ICurrentProcessStartTimeProvider> currentProcessStartTimeProviderAction)
        {
            var serviceAction = _.New<IProcessStartTimeProvider>(services => services.AddOverridableProcessStartTimeProvider(
                currentProcessStartTimeProviderAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="MachineOutputFilePathProvider"/> implementation of <see cref="IMachineOutputFilePathProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IMachineOutputFilePathProvider> AddMachineOutputFilePathProviderAction(this IServiceAction _,
            IServiceAction<IMachineOutputFileNameProvider> machineOutputFileNameProviderAction,
            IServiceAction<IOutputFilePathProvider> outputFilePathProviderAction)
        {
            var serviceAction = _.New<IMachineOutputFilePathProvider>(services => services.AddMachineOutputFilePathProvider(
                machineOutputFileNameProviderAction,
                outputFilePathProviderAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="HumanOutputFilePathProvider"/> implementation of <see cref="IHumanOutputFilePathProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IHumanOutputFilePathProvider> AddHumanOutputFilePathProviderAction(this IServiceAction _,
            IServiceAction<IHumanOutputFileNameProvider> humanOutputFileNameProviderAction,
            IServiceAction<IOutputFilePathProvider> outputFilePathProviderAction)
        {
            var serviceAction = _.New<IHumanOutputFilePathProvider>(services => services.AddHumanOutputFilePathProvider(
                humanOutputFileNameProviderAction,
                outputFilePathProviderAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="LogFilePathProvider"/> implementation of <see cref="ILogFilePathProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<ILogFilePathProvider> AddLogFilePathProviderAction(this IServiceAction _,
            IServiceAction<ILogFileNameProvider> logFileNameProviderAction,
            IServiceAction<IOutputFilePathProvider> outputFilePathProviderAction)
        {
            var serviceAction = _.New<ILogFilePathProvider>(services => services.AddLogFilePathProvider(
                logFileNameProviderAction,
                outputFilePathProviderAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="ConstructorBasedLogFileNameProvider"/> implementation of <see cref="ILogFileNameProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<ILogFileNameProvider> AddConstructorBasedLogFileNameProviderAction(this IServiceAction _,
            string logFileName)
        {
            var serviceAction = _.New<ILogFileNameProvider>(services => services.AddConstructorBasedLogFileNameProvider(
                logFileName));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="YYYYMMDD_HHMMSS_DateTimeDirectoryNameProvider"/> implementation of <see cref="IDateTimeDirectoryNameProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IDateTimeDirectoryNameProvider> AddYYYYMMDD_HHMMSS_DateTimeDirectoryNameProviderAction(this IServiceAction _)
        {
            var serviceAction = _.New<IDateTimeDirectoryNameProvider>(services => services.AddYYYYMMDD_HHMMSS_DateTimeDirectoryNameProvider());
            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="StaticValuedRootOutputDirectoryPathProvider"/> implementation of <see cref="IRootOutputDirectoryPathProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IRootOutputDirectoryPathProvider> AddStaticValuedRootOutputDirectoryPathProviderAction(this IServiceAction _)
        {
            var serviceAction = _.New<IRootOutputDirectoryPathProvider>(services => services.AddStaticValuedRootOutputDirectoryPathProvider());
            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="StaticValuedProcessStartTimeProvider"/> implementation of <see cref="IProcessStartTimeProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IProcessStartTimeProvider> AddStaticValuedProcessStartTimeProviderAction(this IServiceAction _)
        {
            var serviceAction = _.New<IProcessStartTimeProvider>(services => services.AddStaticValuedProcessStartTimeProvider());
            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="StaticValuedProcessNameProvider"/> implementation of <see cref="IProcessNameProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IProcessNameProvider> AddStaticValuedProcessNameProviderAction(this IServiceAction _)
        {
            var serviceAction = _.New<IProcessNameProvider>(services => services.AddStaticValuedProcessNameProvider());
            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="ProcessStartTimeSpecificOutputDirectoryPathProvider"/> implementation of <see cref="IProcessStartTimeSpecificOutputDirectoryPathProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IProcessStartTimeSpecificOutputDirectoryPathProvider> AddProcessStartTimeSpecificOutputDirectoryPathProviderAction(this IServiceAction _,
            IServiceAction<IProcessSpecificOutputDirectoryPathProvider> processSpecificOutputDirectoryPathProviderAction,
            IServiceAction<IProcessStartTimeDirectoryNameProvider> processStartTimeDirectoryNameProviderAction,
            IServiceAction<IStringlyTypedPathOperator> stringlyTypedPathOperatorAction)
        {
            var serviceAction = _.New<IProcessStartTimeSpecificOutputDirectoryPathProvider>(services => services.AddProcessStartTimeSpecificOutputDirectoryPathProvider(
                processSpecificOutputDirectoryPathProviderAction,
                processStartTimeDirectoryNameProviderAction,
                stringlyTypedPathOperatorAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="ProcessStartTimeDirectoryNameProvider"/> implementation of <see cref="IProcessStartTimeDirectoryNameProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IProcessStartTimeDirectoryNameProvider> AddProcessStartTimeDirectoryNameProviderAction(this IServiceAction _,
            IServiceAction<IProcessStartTimeProvider> processStartTimeProviderAction,
            IServiceAction<IDateTimeDirectoryNameProvider> dateTimeDirectoryNameProviderAction)
        {
            var serviceAction = _.New<IProcessStartTimeDirectoryNameProvider>(services => services.AddProcessStartTimeDirectoryNameProvider(
                processStartTimeProviderAction,
                dateTimeDirectoryNameProviderAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="ProcessSpecificOutputDirectoryPathProvider"/> implementation of <see cref="IProcessSpecificOutputDirectoryPathProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IProcessSpecificOutputDirectoryPathProvider> AddProcessSpecificOutputDirectoryPathProviderAction(this IServiceAction _,
            IServiceAction<IProcessDirectoryNameProvider> processDirectoryNameProviderAction,
            IServiceAction<IRootOutputDirectoryPathProvider> rootOutputDirectoryPathProviderAction,
            IServiceAction<IStringlyTypedPathOperator> stringlyTypedPathOperatorAction)
        {
            var serviceAction = _.New<IProcessSpecificOutputDirectoryPathProvider>(services => services.AddProcessSpecificOutputDirectoryPathProvider(
                processDirectoryNameProviderAction,
                rootOutputDirectoryPathProviderAction,
                stringlyTypedPathOperatorAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="ProcessDirectoryNameProvider"/> implementation of <see cref="IProcessDirectoryNameProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IProcessDirectoryNameProvider> AddProcessDirectoryNameProviderAction(this IServiceAction _,
            IServiceAction<IProcessNameProvider> processNameProviderAction,
            IServiceAction<IDirectoryNameProvider> directoryNameProviderAction)
        {
            var serviceAction = _.New<IProcessDirectoryNameProvider>(services => services.AddProcessDirectoryNameProvider(
                processNameProviderAction,
                directoryNameProviderAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="OutputFilePathProvider"/> implementation of <see cref="IOutputFilePathProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IOutputFilePathProvider> AddOutputFilePathProviderAction(this IServiceAction _,
            IServiceAction<IOutputDirectoryPathProvider> outputDirectoryPathProviderAction,
            IServiceAction<IStringlyTypedPathOperator> stringlyTypedPathOperatorAction)
        {
            var serviceAction = _.New<IOutputFilePathProvider>(services => services.AddOutputFilePathProvider(
                outputDirectoryPathProviderAction,
                stringlyTypedPathOperatorAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="OutputDirectoryPathProvider"/> implementation of <see cref="IOutputDirectoryPathProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IOutputDirectoryPathProvider> AddOutputDirectoryPathProviderAction(this IServiceAction _,
            IServiceAction<IProcessStartTimeSpecificOutputDirectoryPathProvider> processStartTimeSpecificOutputDirectoryPathProviderAction)
        {
            var serviceAction = _.New<IOutputDirectoryPathProvider>(services => services.AddOutputDirectoryPathProvider(
                processStartTimeSpecificOutputDirectoryPathProviderAction));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="EntryPointAssemblyProcessNameProvider"/> implementation of <see cref="IProcessNameProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IProcessNameProvider> AddEntryPointAssemblyProcessNameProviderAction(this IServiceAction _)
        {
            var serviceAction = _.New<IProcessNameProvider>(services => services.AddEntryPointAssemblyProcessNameProvider());
            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="DirectDirectoryNameProvider"/> implementation of <see cref="IDirectoryNameProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IDirectoryNameProvider> AddDirectDirectoryNameProviderAction(this IServiceAction _)
        {
            var serviceAction = _.New<IDirectoryNameProvider>(services => services.AddDirectDirectoryNameProvider());
            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="CurrentProcessStartTimeProvider"/> implementation of <see cref="ICurrentProcessStartTimeProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<ICurrentProcessStartTimeProvider> AddCurrentProcessStartTimeProviderAction(this IServiceAction _)
        {
            var serviceAction = _.New<ICurrentProcessStartTimeProvider>(services => services.AddCurrentProcessStartTimeProvider());
            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="ConstructorBasedRootOutputDirectoryPathProvider"/> implementation of <see cref="IRootOutputDirectoryPathProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IRootOutputDirectoryPathProvider> AddConstructorBasedRootOutputDirectoryPathProviderAction(this IServiceAction _,
            string rootOutputDirectoryPath)
        {
            var serviceAction = _.New<IRootOutputDirectoryPathProvider>(services => services.AddConstructorBasedRootOutputDirectoryPathProvider(
                rootOutputDirectoryPath));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="ConstructorBasedProcessStartTimeProvider"/> implementation of <see cref="IProcessStartTimeProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IProcessStartTimeProvider> AddConstructorBasedProcessStartTimeProviderAction(this IServiceAction _,
            DateTime processStartTime)
        {
            var serviceAction = _.New<IProcessStartTimeProvider>(services => services.AddConstructorBasedProcessStartTimeProvider(
                processStartTime));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="ConstructorBasedProcessNameProvider"/> implementation of <see cref="IProcessNameProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IProcessNameProvider> AddConstructorBasedProcessNameProviderAction(this IServiceAction _,
            string processName)
        {
            var serviceAction = _.New<IProcessNameProvider>(services => services.AddConstructorBasedProcessNameProvider(
                processName));

            return serviceAction;
        }

        /// <summary>
        /// Adds the <see cref="SimpleTextJsonSerializationHandler"/> implementation of <see cref="IMachineMessageTypeJsonSerializationHandler"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IMachineMessageTypeJsonSerializationHandler> AddSimpleTextJsonSerializationHandlerAction(this IServiceAction _)
        {
            var serviceAction = _.New<IMachineMessageTypeJsonSerializationHandler>(services => services.AddSimpleTextJsonSerializationHandler());
            return serviceAction;
        }

        public static IServiceAction<HostStartup> AddStartupAction(this IServiceAction _)
        {
            var output = _.New<HostStartup>(services => services.AddHostStartup());

            return output;
        }
    }
}
