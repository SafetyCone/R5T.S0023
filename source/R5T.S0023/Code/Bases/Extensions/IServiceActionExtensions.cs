using System;
using System.Collections.Generic;

using Microsoft.Extensions.Logging;

using R5T.D0081;
using R5T.D0096;
using R5T.D0098;
using R5T.L0017.D001;
using R5T.T0062;
using R5T.T0063;


namespace R5T.S0023
{
    public static class IServiceActionExtensions
    {
        ///// <summary>
        ///// Adds the <see cref="MachineMessageJsonReserializer"/> implementation of <see cref="IMachineMessageJsonReserializer"/> as a <see cref="ServiceLifetime.Singleton"/>.
        ///// </summary>
        //public static IServiceAction<IMachineMessageJsonReserializer> AddMachineMessageJsonReserializerAction(this IServiceAction _,
        //    IServiceAction<ILoggerUnbound> loggerUnboundAction,
        //    IServiceAction<IHumanOutput> humanOutputAction,
        //    IEnumerable<IServiceAction<IMachineMessageTypeJsonSerializationHandler>> machineMessageTypeJsonSerializationHandlerActions)
        //{
        //    var serviceAction = _.New<IMachineMessageJsonReserializer>(services => services.AddMachineMessageJsonReserializer(
        //        loggerUnboundAction,
        //        humanOutputAction,
        //        machineMessageTypeJsonSerializationHandlerActions));

        //    return serviceAction;
        //}

        /// <summary>
        /// Adds the <see cref="SimpleTextJsonSerializationHandler"/> implementation of <see cref="IMachineMessageTypeJsonSerializationHandler"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IMachineMessageTypeJsonSerializationHandler> AddSimpleTextJsonSerializationHandlerAction(this IServiceAction _)
        {
            var serviceAction = _.New<IMachineMessageTypeJsonSerializationHandler>(services => services.AddSimpleTextJsonSerializationHandler());
            return serviceAction;
        }

        ///// <summary>
        ///// Adds the <see cref="FileMachineMessageOutputSinkProvider"/> implementation of <see cref="IMachineMessageOutputSinkProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        ///// </summary>
        //public static IServiceAction<IMachineMessageOutputSinkProvider> AddFileMachineMessageOutputSinkProviderAction(this IServiceAction _,
        //    IServiceAction<IHumanOutput> humanOutputAction,
        //    IServiceAction<ILoggerFactory> loggerFactoryAction,
        //    IServiceAction<IMachineMessageJsonReserializer> machineMessageJsonReserializerAction,
        //    IServiceAction<IMachineOutputFilePathProvider> machineOutputFilePathProviderAction,
        //    IServiceAction<IMachineOutputSynchronicityProvider> machineOutputSynchronicityProviderAction)
        //{
        //    var serviceAction = _.New<IMachineMessageOutputSinkProvider>(services => services.AddFileMachineMessageOutputSinkProvider(
        //        humanOutputAction,
        //        loggerFactoryAction,
        //        machineMessageJsonReserializerAction,
        //        machineOutputFilePathProviderAction,
        //        machineOutputSynchronicityProviderAction));

        //    return serviceAction;
        //}

        ///// <summary>
        ///// Adds the <see cref="ConstructorBasedMachineOutputFilePathProvider"/> implementation of <see cref="IMachineOutputFilePathProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        ///// </summary>
        //public static IServiceAction<IMachineOutputFilePathProvider> AddConstructorBasedMachineOutputFilePathProviderAction(this IServiceAction _,
        //    string machineOutputFilePath)
        //{
        //    var serviceAction = _.New<IMachineOutputFilePathProvider>(services => services.AddConstructorBasedMachineOutputFilePathProvider(
        //        machineOutputFilePath));

        //    return serviceAction;
        //}

        ///// <summary>
        ///// Adds the <see cref="MachineOutputSynchronicityProvider"/> implementation of <see cref="IMachineOutputSynchronicityProvider"/> as a <see cref="ServiceLifetime.Singleton"/>.
        ///// </summary>
        //public static IServiceAction<IMachineOutputSynchronicityProvider> AddMachineOutputSynchronicityProviderAction(this IServiceAction _,
        //    IServiceAction<IExecutionSynchronicityProvider> executionSynchronicityProviderAction)
        //{
        //    var serviceAction = _.New<IMachineOutputSynchronicityProvider>(services => services.AddMachineOutputSynchronicityProvider(
        //        executionSynchronicityProviderAction));

        //    return serviceAction;
        //}

        ///// <summary>
        ///// Adds the <see cref="MachineOutput"/> implementation of <see cref="IMachineOutput"/> as a <see cref="ServiceLifetime.Singleton"/>.
        ///// </summary>
        //public static IServiceAction<IMachineOutput> AddMachineOutputAction(this IServiceAction _,
        //    IEnumerable<IServiceAction<IMachineMessageOutputSinkProvider>> machineMessageOutputSinkProviderAction)
        //{
        //    var serviceAction = _.New<IMachineOutput>(services => services.AddMachineOutput(
        //        machineMessageOutputSinkProviderAction));

        //    return serviceAction;
        //}

        public static IServiceAction<HostStartup> AddStartupAction(this IServiceAction _)
        {
            var output = _.New<HostStartup>(services => services.AddHostStartup());

            return output;
        }
    }
}
