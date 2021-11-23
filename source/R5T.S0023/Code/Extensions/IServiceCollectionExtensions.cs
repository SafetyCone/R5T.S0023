using System;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using R5T.D0081;
using R5T.D0096;
using R5T.D0098;
using R5T.L0017.D001;
using R5T.T0063;

using R5T.S0023.Startup;


namespace R5T.S0023
{
    public static class IServiceCollectionExtensions
    {
        ///// <summary>
        ///// Adds the <see cref="MachineMessageJsonReserializer"/> implementation of <see cref="IMachineMessageJsonReserializer"/> as a <see cref="ServiceLifetime.Singleton"/>.
        ///// </summary>
        //public static IServiceCollection AddMachineMessageJsonReserializer(this IServiceCollection services,
        //    IServiceAction<ILoggerUnbound> loggerUnboundAction,
        //    IServiceAction<IHumanOutput> humanOutputAction,
        //    IEnumerable<IServiceAction<IMachineMessageTypeJsonSerializationHandler>> machineMessageTypeJsonSerializationHandlerActions)
        //{
        //    services
        //        .Run(loggerUnboundAction)
        //        .Run(humanOutputAction)
        //        .Run(machineMessageTypeJsonSerializationHandlerActions)
        //        .AddSingleton<IMachineMessageJsonReserializer, MachineMessageJsonReserializer>();

        //    return services;
        //}

        /// <summary>
        /// Adds the <see cref="SimpleTextJsonSerializationHandler"/> implementation of <see cref="IMachineMessageTypeJsonSerializationHandler"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddSimpleTextJsonSerializationHandler(this IServiceCollection services)
        {
            services.AddSingleton<IMachineMessageTypeJsonSerializationHandler, SimpleTextJsonSerializationHandler>();

            return services;
        }

        ///// <summary>
        ///// Adds the <see cref="FileMachineMessageOutputSinkProvider"/> implementation of <see cref="IMachineMessageOutputSinkProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        ///// </summary>
        //public static IServiceCollection AddFileMachineMessageOutputSinkProvider(this IServiceCollection services,
        //    IServiceAction<IHumanOutput> humanOutputAction,
        //    IServiceAction<ILoggerFactory> loggerFactoryAction,
        //    IServiceAction<IMachineMessageJsonReserializer> machineMessageJsonReserializerAction,
        //    IServiceAction<IMachineOutputFilePathProvider> machineOutputFilePathProviderAction,
        //    IServiceAction<IMachineOutputSynchronicityProvider> machineOutputSynchronicityProviderAction)
        //{
        //    services
        //        .Run(humanOutputAction)
        //        .Run(loggerFactoryAction)
        //        .Run(machineMessageJsonReserializerAction)
        //        .Run(machineOutputFilePathProviderAction)
        //        .Run(machineOutputSynchronicityProviderAction)
        //        .AddSingleton<IMachineMessageOutputSinkProvider, FileMachineMessageOutputSinkProvider>();

        //    return services;
        //}

        ///// <summary>
        ///// Adds the <see cref="ConstructorBasedMachineOutputFilePathProvider"/> implementation of <see cref="IMachineOutputFilePathProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        ///// </summary>
        //public static IServiceCollection AddConstructorBasedMachineOutputFilePathProvider(this IServiceCollection services,
        //    string machineOutputFilePath)
        //{
        //    services.AddSingleton<IMachineOutputFilePathProvider>(sp => new ConstructorBasedMachineOutputFilePathProvider(
        //        machineOutputFilePath));

        //    return services;
        //}

        ///// <summary>
        ///// Adds the <see cref="MachineOutputSynchronicityProvider"/> implementation of <see cref="IMachineOutputSynchronicityProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        ///// </summary>
        //public static IServiceCollection AddMachineOutputSynchronicityProvider(this IServiceCollection services,
        //    IServiceAction<IExecutionSynchronicityProvider> executionSynchronicityProviderAction)
        //{
        //    services
        //        .Run(executionSynchronicityProviderAction)
        //        .AddSingleton<IMachineOutputSynchronicityProvider, MachineOutputSynchronicityProvider>();

        //    return services;
        //}

        ///// <summary>
        ///// Adds the <see cref="MachineOutput"/> implementation of <see cref="IMachineOutput"/> as a <see cref="ServiceLifetime.Singleton"/>.
        ///// </summary>
        //public static IServiceCollection AddMachineOutput(this IServiceCollection services,
        //    IEnumerable<IServiceAction<IMachineMessageOutputSinkProvider>> machineMessageOutputSinkProviderActions)
        //{
        //    services
        //        .Run(machineMessageOutputSinkProviderActions)
        //        .AddSingleton<IMachineOutput, MachineOutput>();

        //    return services;
        //}

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
