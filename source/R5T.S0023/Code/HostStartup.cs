using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using R5T.Magyar;

using R5T.D0081.I001;
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

            return Task.CompletedTask;
        }

        protected override Task ConfigureServices(IServiceCollection services, IProvidedServiceActionAggregation providedServicesAggregation)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            var loggerFactoryAction = Instances.ServiceAction.AddedElsewhere<ILoggerFactory>();
            var loggerUnboundAction = Instances.ServiceAction.AddedElsewhere<ILoggerUnbound>();
#pragma warning restore CS0618 // Type or member is obsolete

            var executionSynchronicityProviderAction = Instances.ServiceAction.AddConstructorBasedExecutionSynchronicityProviderAction(Synchronicity.Synchronous);

            //var loggerSynchronicityProviderAction = Instances.ServiceAction.AddConstructorBasedLoggerSynchronicityProviderAction(Synchronicity.Asynchronous);
            var loggerSynchronicityProviderAction = Instances.ServiceAction.AddLoggerSynchronicityProviderAction(
                executionSynchronicityProviderAction);
            var logFilePathProviderAction = Instances.ServiceAction.AddConstructorBasedLogFilePathProvider(@"C:\Temp\log.txt");

            var humanOutputFilePathProviderAction = Instances.ServiceAction.AddConstructorBasedHumanOutputFilePathProviderAction(@"C:\Temp\Human Output.txt");

            var humanOutputActions = Instances.ServiceAction.AddHumanOutputActions(
                executionSynchronicityProviderAction,
                humanOutputFilePathProviderAction);

            var simpleTextJsonSerializationHandlerAction = Instances.ServiceAction.AddSimpleTextJsonSerializationHandlerAction();

            var machineOutputFilePathProviderAction = Instances.ServiceAction.AddConstructorBasedMachineOutputFilePathProviderAction(@"C:\Temp\Machine Output.json");

            var inMemoryMachineMessageOutputSinkProviderAction = Instances.ServiceAction.AddInMemoryMachineMessageOutputSinkProviderAction();

            var machineOutputActions = Instances.ServiceAction.AddMachineOutputActions(
                executionSynchronicityProviderAction,
                humanOutputActions.HumanOutputAction,
                loggerFactoryAction,
                loggerUnboundAction,
                EnumerableHelper.From(inMemoryMachineMessageOutputSinkProviderAction),
                EnumerableHelper.From(simpleTextJsonSerializationHandlerAction),
                machineOutputFilePathProviderAction);


            //var machineOutputAction = Instances.ServiceAction.AddMachineOutputAction(EnumerableHelper.From(
            //    fileMachineMessageOutputSinkProviderAction,
            //    inMemoryMachineMessageOutputSinkProviderAction));

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
                .Run(humanOutputActions.HumanOutputAction)
                .Run(machineOutputActions.MachineOutputAction)
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
