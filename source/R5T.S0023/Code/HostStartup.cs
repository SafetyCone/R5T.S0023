using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using R5T.Magyar;
using R5T.Ostrogothia.Rivet;

using R5T.A0003;
using R5T.D0048.Default;
using R5T.D0081.I001;
using R5T.D0084.A001;
using R5T.D0094.I001;
using R5T.D0095.I001;
using R5T.D0098.I002;
using R5T.D0099.D002.I001;
using R5T.D0101.I001;
using R5T.D0105.I001;

using R5T.S0023.Startup;

using IProvidedServiceActionAggregation = R5T.S0023.Startup.IProvidedServiceActionAggregation;
using IRequiredServiceActionAggregation = R5T.S0023.Startup.IRequiredServiceActionAggregation;
using ServicesPlatformRequiredServiceActionAggregation = R5T.A0003.RequiredServiceActionAggregation;


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
            // Inputs.
            var executionSynchronicityProviderAction = Instances.ServiceAction.AddConstructorBasedExecutionSynchronicityProviderAction(Synchronicity.Synchronous);
            var organizationProviderAction = Instances.ServiceAction.AddOrganizationProviderAction(); // Rivet organization.
            var rootOutputDirectoryPathProviderAction = Instances.ServiceAction.AddConstructorBasedRootOutputDirectoryPathProviderAction(@"C:\Temp\Output");

            // Services platform.
            var inMemoryMachineMessageOutputSinkProviderAction = Instances.ServiceAction.AddInMemoryMachineMessageOutputSinkProviderAction();

            var simpleTextJsonSerializationHandlerAction = Instances.ServiceAction.AddSimpleTextJsonSerializationHandlerAction();

            var servicesPlatformRequiredServiceActionAggregation = new ServicesPlatformRequiredServiceActionAggregation
            {
                ConfigurationAction = providedServicesAggregation.ConfigurationAction,
                ExecutionSynchronicityProviderAction = executionSynchronicityProviderAction,
                LoggerAction = providedServicesAggregation.LoggerAction,
                LoggerFactoryAction = providedServicesAggregation.LoggerFactoryAction,
                MachineMessageOutputSinkProviderActions = EnumerableHelper.From(inMemoryMachineMessageOutputSinkProviderAction),
                MachineMessageTypeJsonSerializationHandlerActions = EnumerableHelper.From(simpleTextJsonSerializationHandlerAction),
                OrganizationProviderAction = organizationProviderAction,
                RootOutputDirectoryPathProviderAction = rootOutputDirectoryPathProviderAction,
            };

            var servicesPlatform = Instances.ServiceAction.AddProvidedServiceActionAggregation(
                servicesPlatformRequiredServiceActionAggregation);

            // Core competencies.
            var backupProjectRepositoryFilePathsProviderAction = Instances.ServiceAction.AddBackupProjectRepositoryFilePathsProviderAction(
                servicesPlatform.OutputFilePathProviderAction);
            var summaryFilePathProviderAction = Instances.ServiceAction.AddSummaryFilePathProviderAction(
                servicesPlatform.OutputFilePathProviderAction);

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

            // All project names listing file in organization shared data directory.
            var allProjectNamesListingFileNameProviderAction = Instances.ServiceAction.AddConstructorBasedAllProjectNamesListingFileNameProviderAction("Project Names-All.txt");

            var allProjectNamesListingFilePathProviderAction = Instances.ServiceAction.AddAllProjectNamesListingFilePathProviderAction(
                allProjectNamesListingFileNameProviderAction,
                //organizationSharedDataDirectoryFilePathProviderAction);
                servicesPlatform.OrganizationSharedDataDirectoryFilePathProviderAction);

            // Misc.
            // Notepad++
            var notepadPlusPlusExecutableFilePathProviderAction = Instances.ServiceAction.AddHardCodedNotepadPlusPlusExecutableFilePathProviderAction();

            var notepadPlusPlusOperatorAction = Instances.ServiceAction.AddNotepadPlusPlusOperatorAction(
                //commandLineOperatorAction,
                servicesPlatform.CommandLineOperatorAction,
                notepadPlusPlusExecutableFilePathProviderAction);

            // Operations
            var o001_AnalyzeAllCurrentProjects = Instances.ServiceAction.AddO001_AnalyzeAllCurrentProjectsAction(
                allProjectFilePathsProviderServiceActions.AllProjectFilePathsProviderAction,
                notepadPlusPlusOperatorAction,
                summaryFilePathProviderAction,
                projectRepositoryAction);
            var o002_UpdateFileBasedProjectRepositoryAction = Instances.ServiceAction.AddO002_UpdateFileBasedProjectRepositoryAction(
                allProjectFilePathsProviderServiceActions.AllProjectFilePathsProviderAction,
                allProjectNamesListingFilePathProviderAction,
                projectRepositoryAction);
            var o003a_DetermineIfHumanActionsAreRequiredAction = Instances.ServiceAction.AddO003a_DetermineIfHumanActionsAreRequiredAction();
            var o003b_PromptForRequiredHumanActionsCoreAction = Instances.ServiceAction.AddO003b_PromptForRequiredHumanActionsCoreAction(
                notepadPlusPlusOperatorAction,
                projectRepositoryFilePathsProviderAction,
                summaryFilePathProviderAction);
            var o003_PerformRequiredHumanActionsAction = Instances.ServiceAction.AddO003_PerformRequiredHumanActionsAction(
                allProjectFilePathsProviderServiceActions.AllProjectFilePathsProviderAction,
                projectRepositoryAction,
                o003a_DetermineIfHumanActionsAreRequiredAction,
                o003b_PromptForRequiredHumanActionsCoreAction);
            var o004_BackupFileBasedProjectRepositoryFilesAction = Instances.ServiceAction.AddO004_BackupFileBasedProjectRepositoryFilesAction(
                backupProjectRepositoryFilePathsProviderAction,
                servicesPlatform.HumanOutputAction,
                projectRepositoryFilePathsProviderAction);

            var o100_UpdateProjectRepositoryWithCurrentProjectsAction = Instances.ServiceAction.AddO100_UpdateProjectRepositoryWithCurrentProjectsAction(
                o001_AnalyzeAllCurrentProjects,
                o002_UpdateFileBasedProjectRepositoryAction,
                o003_PerformRequiredHumanActionsAction,
                o004_BackupFileBasedProjectRepositoryFilesAction);

            var o900_OpenAllProjectRepositoryFilesAction = Instances.ServiceAction.AddO900_OpenAllProjectRepositoryFilesAction(
                notepadPlusPlusOperatorAction,
                projectRepositoryFilePathsProviderAction);

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
                            //loggerSynchronicityProviderAction)
                            servicesPlatform.LoggerSynchronicityProviderAction)
                        .AddFile(
                            //logFilePathProviderAction,
                            //loggerSynchronicityProviderAction)
                            servicesPlatform.LogFilePathProviderAction,
                            servicesPlatform.LoggerSynchronicityProviderAction)
                        //.AddSimpleConsole
                        ;
                })
                .AddSingleton<Yabbo.TestService>()
                //.Run(configurationAuditSerializerAction)
                .Run(servicesPlatform.ConfigurationAuditSerializerAction)
                //.Run(humanOutputActions.HumanOutputAction)
                .Run(servicesPlatform.HumanOutputAction)
                //.Run(machineOutputActions.MachineOutputAction)
                .Run(servicesPlatform.MachineOutputAction)
                //.Run(serviceCollectionAuditSerializerAction)
                .Run(servicesPlatform.ServiceCollectionAuditSerializerAction)
                // Operations.
                .Run(o001_AnalyzeAllCurrentProjects)
                .Run(o002_UpdateFileBasedProjectRepositoryAction)
                .Run(o003_PerformRequiredHumanActionsAction)
                .Run(o004_BackupFileBasedProjectRepositoryFilesAction)
                .Run(o100_UpdateProjectRepositoryWithCurrentProjectsAction)
                .Run(o900_OpenAllProjectRepositoryFilesAction)
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
