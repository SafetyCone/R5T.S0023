using System;

using R5T.T0040;
using R5T.T0061;
using R5T.T0062;
using R5T.T0070;
using R5T.T0088;
using R5T.T0090;
using R5T.T0098;


namespace R5T.S0023
{
    public static class Instances
    {
        public static IConsole Console { get; } = T0088.Console.Instance;
        public static IHost Host { get; } = T0070.Host.Instance;
        public static IJsonKey JsonKey { get; } = T0090.JsonKey.Instance;
        public static IJsonOperator JsonOperator { get; } = T0090.JsonOperator.Instance;
        public static IOperation Operation { get; } = T0098.Operation.Instance;
        public static IProjectPathsOperator ProjectPathsOperator { get; } = T0040.ProjectPathsOperator.Instance;
        public static IServiceAction ServiceAction { get; } = T0062.ServiceAction.Instance;
        public static IServiceCollectionOperator ServiceCollectionOperator { get; } = T0061.ServiceCollectionOperator.Instance;
    }
}
