using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using R5T.Lombardy;
using R5T.Magyar;
using R5T.Ostrogothia.Rivet;
using R5T.Quadia.A001;

using R5T.D0075.Default;
using R5T.D0081.I001;
using R5T.D0084.A001;
//using R5T.D0093.I001;
using R5T.D0093.I002;
using R5T.D0094.I001;
using R5T.D0095.D001.I001;
using R5T.D0095.I001;
using R5T.D0096.A001;
using R5T.D0096.D003.I001;
using R5T.D0099.A001;
using R5T.D0099.D002.I001;
using R5T.D0099.D003.I001;
using R5T.D0101.I001;
//using R5T.L0017.T001;
using R5T.L0017.D001;

using R5T.S0023.Startup;


namespace R5T.S0023
{
    public class HostStartup : HostStartupBase
    {
        public override Task ConfigureConfiguration(IConfigurationBuilder configurationBuilder)
        {
            // Do nothing.
            configurationBuilder.AddJsonFile("appsettings.json");

            return Task.CompletedTask;
        }

        protected override Task ConfigureServices(IServiceCollection services, IProvidedServiceActionAggregation providedServicesAggregation)
        {
            var serviceCollectionAction = Instances.ServiceAction.AddServiceCollectionAction();

            var commandLineOperatorAction = Instances.ServiceAction.AddCommandLineOperatorAction();
            var stringlyTypedPathOperatorActions = Instances.ServiceAction.AddStringlyTypedPathOperatorActions();

#pragma warning disable CS0618 // Type or member is obsolete
            var loggerFactoryAction = Instances.ServiceAction.AddedElsewhere<ILoggerFactory>();
            var loggerUnboundAction = Instances.ServiceAction.AddedElsewhere<ILoggerUnbound>();
#pragma warning restore CS0618 // Type or member is obsolete

            // Output file path.
            var processNameProviderAction = Instances.ServiceAction.AddEntryPointAssemblyProcessNameProviderAction();

            var directoryNameProviderAction = Instances.ServiceAction.AddDirectDirectoryNameProviderAction();

            var processDirectoryNameProviderAction = Instances.ServiceAction.AddProcessDirectoryNameProviderAction(
                processNameProviderAction,
                directoryNameProviderAction);

            var rootOutputDirectoryPathProviderAction = Instances.ServiceAction.AddConstructorBasedRootOutputDirectoryPathProviderAction(@"C:\Temp\Output");

            var processSpecificOutputDirectoryPathProviderAction = Instances.ServiceAction.AddProcessSpecificOutputDirectoryPathProviderAction(
                processDirectoryNameProviderAction,
                rootOutputDirectoryPathProviderAction,
                stringlyTypedPathOperatorActions.StringlyTypedPathOperatorAction);

            var currentProcessStartTimeProviderAction = Instances.ServiceAction.AddCurrentProcessStartTimeProviderAction();

            var processStartTimeProviderAction = Instances.ServiceAction.AddOverridableProcessStartTimeProviderAction(
                currentProcessStartTimeProviderAction);

            var dateTimeDirectoryNameProviderAction = Instances.ServiceAction.AddYYYYMMDD_HHMMSS_DateTimeDirectoryNameProviderAction();

            var processStartTimeDirectoryNameProviderAction = Instances.ServiceAction.AddProcessStartTimeDirectoryNameProviderAction(
                processStartTimeProviderAction,
                dateTimeDirectoryNameProviderAction);

            var processStartTimeSpecificOutputDirectoryPathProviderAction = Instances.ServiceAction.AddProcessStartTimeSpecificOutputDirectoryPathProviderAction(
                processSpecificOutputDirectoryPathProviderAction,
                processStartTimeDirectoryNameProviderAction,
                stringlyTypedPathOperatorActions.StringlyTypedPathOperatorAction);

            var outputDirectoryPathProviderAction = Instances.ServiceAction.AddOutputDirectoryPathProviderAction(
                processStartTimeSpecificOutputDirectoryPathProviderAction);

            var outputFilePathProviderAction = Instances.ServiceAction.AddOutputFilePathProviderAction(
                outputDirectoryPathProviderAction,
                stringlyTypedPathOperatorActions.StringlyTypedPathOperatorAction);
            
            // Files.
            var executionSynchronicityProviderAction = Instances.ServiceAction.AddConstructorBasedExecutionSynchronicityProviderAction(Synchronicity.Synchronous);

            var serviceCollectionSerializationFileNameProviderAction = Instances.ServiceAction.AddConstructorBasedServiceCollectionSerializationFileNameProviderAction("Services.txt");

            var serviceCollectionSerializationFilePathProviderAction = Instances.ServiceAction.AddServiceCollectionSerializationFilePathProviderAction(
                outputFilePathProviderAction,
                serviceCollectionSerializationFileNameProviderAction);

            var serviceCollectionAuditSerializerAction = Instances.ServiceAction.AddServiceCollectionAuditSerializerAction(
                serviceCollectionAction,
                serviceCollectionSerializationFilePathProviderAction);

            var configurationSerializationFileNameProviderAction = Instances.ServiceAction.AddConstructorBasedConfigurationSerializationFileNameProviderAction("Configuration.txt");

            var configurationSerializationFilePathProviderAction = Instances.ServiceAction.AddConfigurationSerializationFilePathProviderAction(
                configurationSerializationFileNameProviderAction,
                outputFilePathProviderAction);

            var configurationAuditSerializerAction = Instances.ServiceAction.AddConfigurationAuditSerializerAction(
                providedServicesAggregation.ConfigurationAction,
                configurationSerializationFilePathProviderAction);

            //var loggerSynchronicityProviderAction = Instances.ServiceAction.AddConstructorBasedLoggerSynchronicityProviderAction(Synchronicity.Asynchronous);
            var loggerSynchronicityProviderAction = Instances.ServiceAction.AddLoggerSynchronicityProviderAction(
                executionSynchronicityProviderAction);
            //var logFilePathProviderAction = Instances.ServiceAction.AddConstructorBasedLogFilePathProvider(@"C:\Temp\log.txt");
            var logFileNameProviderAction = Instances.ServiceAction.AddConstructorBasedLogFileNameProviderAction("log.txt");

            var logFilePathProviderAction = Instances.ServiceAction.AddLogFilePathProviderAction(
                logFileNameProviderAction,
                outputFilePathProviderAction);

            var humanOutputFileNameProviderAction = Instances.ServiceAction.AddConstructorBasedHumanOutputFileNameProviderAction("Human Output.txt");

            var humanOutputFilePathProviderAction = Instances.ServiceAction.AddHumanOutputFilePathProviderAction(
                humanOutputFileNameProviderAction,
                outputFilePathProviderAction);

            //var humanOutputFilePathProviderAction = Instances.ServiceAction.AddConstructorBasedHumanOutputFilePathProviderAction(@"C:\Temp\Human Output.txt");

            var humanOutputActions = Instances.ServiceAction.AddHumanOutputActions(
                executionSynchronicityProviderAction,
                humanOutputFilePathProviderAction);

            var simpleTextJsonSerializationHandlerAction = Instances.ServiceAction.AddSimpleTextJsonSerializationHandlerAction();

            //var machineOutputFilePathProviderAction = Instances.ServiceAction.AddConstructorBasedMachineOutputFilePathProviderAction(@"C:\Temp\Machine Output.json");
            var machineOutputFileNameProviderAction = Instances.ServiceAction.AddConstructorBasedMachineOutputFileNameProviderAction("Machine Output.json");

            var machineOutputFilePathProviderAction = Instances.ServiceAction.AddMachineOutputFilePathProviderAction(
                machineOutputFileNameProviderAction,
                outputFilePathProviderAction);

            var inMemoryMachineMessageOutputSinkProviderAction = Instances.ServiceAction.AddInMemoryMachineMessageOutputSinkProviderAction();

            var machineOutputActions = Instances.ServiceAction.AddMachineOutputActions(
                executionSynchronicityProviderAction,
                humanOutputActions.HumanOutputAction,
                loggerFactoryAction,
                loggerUnboundAction,
                EnumerableHelper.From(inMemoryMachineMessageOutputSinkProviderAction),
                EnumerableHelper.From(simpleTextJsonSerializationHandlerAction),
                machineOutputFilePathProviderAction);

            // Project repository.
            var projectRepositoryFilePathsProviderAction = Instances.ServiceAction.AddHardCodedProjectRepositoryFilePathsProviderAction();

            var fileBasedProjectRepositoryAction = Instances.ServiceAction.AddFileBasedProjectRepositoryAction(
                projectRepositoryFilePathsProviderAction);

            var projectRepositoryAction = Instances.ServiceAction.ForwardFileBasedProjectRepositoryToProjectRepositoryAction(
                fileBasedProjectRepositoryAction);

            // Project file paths provider.
            var repositoriesDirectoryPathProviderAction = Instances.ServiceAction.AddHardCodedRepositoriesDirectoryPathProviderAction();

            var allProjectFilePathsProviderServiceActions = Instances.ServiceAction.AddAllProjectFilePathsProviderServiceActions(
                repositoriesDirectoryPathProviderAction);

            //var machineOutputAction = Instances.ServiceAction.AddMachineOutputAction(EnumerableHelper.From(
            //    fileMachineMessageOutputSinkProviderAction,
            //    inMemoryMachineMessageOutputSinkProviderAction));

            // Notepad++
            var notepadPlusPlusExecutableFilePathProviderAction = Instances.ServiceAction.AddHardCodedNotepadPlusPlusExecutableFilePathProviderAction();

            var notepadPlusPlusOperatorAction = Instances.ServiceAction.AddNotepadPlusPlusOperatorAction(
                commandLineOperatorAction,
                notepadPlusPlusExecutableFilePathProviderAction);

            // All project names listing file in organization shared data directory.
            var organizationProviderAction = Instances.ServiceAction.AddOrganizationProviderAction();

            var organizationDataDirectoryPathProviderActions = Instances.ServiceAction.AddOrganizationDataDirectoryPathProviderActions(
                organizationProviderAction,
                stringlyTypedPathOperatorActions.StringlyTypedPathOperatorAction);

            var organizationSharedDataDirectoryPathProviderAction = Instances.ServiceAction.AddOrganizationSharedDataDirectoryPathProviderAction(
                organizationDataDirectoryPathProviderActions.OrganizationDataDirectoryPathProviderAction);

            var organizationSharedDataDirectoryFilePathProviderAction = Instances.ServiceAction.AddOrganizationSharedDataDirectoryFilePathProviderAction(
                organizationSharedDataDirectoryPathProviderAction,
                stringlyTypedPathOperatorActions.StringlyTypedPathOperatorAction);

            var allProjectNamesListingFileNameProviderAction = Instances.ServiceAction.AddConstructorBasedAllProjectNamesListingFileNameProviderAction("Project Names-All.txt");

            var allProjectNamesListingFilePathProviderAction = Instances.ServiceAction.AddAllProjectNamesListingFilePathProviderAction(
                allProjectNamesListingFileNameProviderAction,
                organizationSharedDataDirectoryFilePathProviderAction);

            // Operations
            var o001_AnalyzeAllCurrentProjects = Instances.ServiceAction.AddO001_AnalyzeAllCurrentProjectsAction(
                allProjectFilePathsProviderServiceActions.AllProjectFilePathsProviderAction,
                fileBasedProjectRepositoryAction,
                notepadPlusPlusOperatorAction,
                outputFilePathProviderAction,
                projectRepositoryAction);
            var o002_UpdateFileBasedProjectRepositoryAction = Instances.ServiceAction.AddO002_UpdateFileBasedProjectRepositoryAction(
                allProjectFilePathsProviderServiceActions.AllProjectFilePathsProviderAction,
                allProjectNamesListingFilePathProviderAction,
                fileBasedProjectRepositoryAction,
                projectRepositoryAction);

            services
                .AddLogging(loggingBuilder =>
                {
                    loggingBuilder
                        //.AddFilter("", LogLevel.Trace) // This *does* work. (And overrides a call to SetMinimumLevel(), even if later, which I guess it should due to specificity.)
                        .SetMinimumLevel(LogLevel.Information)
                        //.AddFilter("Default", LogLevel.Trace) // This does not work.
                        .AddFilter("R5T.S0023", LogLevel.Error) // Try setting a filter here to see what happens.
                        //.AddConsole(consoleLoggerOptions =>
                        //{
                        //    // Keep defaults.
                        //})
                        //.AddSimpleConsole()
                        //.AddConsoleSynchronous()
                        .AddConsole(
                            loggerSynchronicityProviderAction)
                        .AddFile(
                            logFilePathProviderAction,
                            loggerSynchronicityProviderAction)
                        //.AddSimpleConsole
                        ;
                })
                .AddSingleton<Yabbo.TestService>()
                .Run(configurationAuditSerializerAction)
                .Run(humanOutputActions.HumanOutputAction)
                .Run(machineOutputActions.MachineOutputAction)
                .Run(serviceCollectionAuditSerializerAction)
                // Operations.
                .Run(o001_AnalyzeAllCurrentProjects)
                .Run(o002_UpdateFileBasedProjectRepositoryAction)
                ;

            return Task.CompletedTask;
        }

        protected override Task FillRequiredServiceActions(IRequiredServiceActionAggregation requiredServiceActions)
        {
            // Do nothing since none are required.

            return Task.CompletedTask;
        }
    }
}
