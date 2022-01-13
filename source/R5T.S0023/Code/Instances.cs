using System;

using R5T.T0034;
using R5T.T0037;
using R5T.T0040;
using R5T.T0044;
using R5T.T0045;
using R5T.T0057;
using R5T.T0061;
using R5T.T0062;
using R5T.T0070;
using R5T.T0088;
using R5T.T0090;
using R5T.T0098;
using R5T.T0103;
using R5T.T0110;
using R5T.T0113;
using R5T.T0115;


namespace R5T.S0023
{
    public static class Instances
    {
        public static ICodeFileGenerator CodeFileGenerator { get; } = T0045.CodeFileGenerator.Instance;
        public static ICodeFileName CodeFileName { get; } = T0037.CodeFileName.Instance;
        public static IConsole Console { get; } = T0088.Console.Instance;
        public static IFileSystemOperator FileSystemOperator { get; } = T0044.FileSystemOperator.Instance;
        public static IHost Host { get; } = T0070.Host.Instance;
        public static IIgnoredValuesOperator IgnoredValuesOperator { get; } = T0057.IgnoredValuesOperator.Instance;
        public static IJsonKey JsonKey { get; } = T0090.JsonKey.Instance;
        public static IJsonOperator JsonOperator { get; } = T0090.JsonOperator.Instance;
        public static ILibraryDescription LibraryDescription { get; } = T0110.LibraryDescription.Instance;
        public static ILibraryName LibraryName { get; } = T0110.LibraryName.Instance;
        public static ILibraryNameOperator LibraryNameOperator { get; } = T0110.LibraryNameOperator.Instance;
        public static IOperation Operation { get; } = T0098.Operation.Instance;
        public static IProjectDescriptionGenerator ProjectDescriptionGenerator { get; } = T0115.ProjectDescriptionGenerator.Instance;
        public static IProjectGenerator ProjectGenerator { get; } = T0113.ProjectGenerator.Instance;
        public static IProjectOperator ProjectOperator { get; } = T0113.ProjectOperator.Instance;
        public static IProjectPathsOperator ProjectPathsOperator { get; } = T0040.ProjectPathsOperator.Instance;
        public static IRepositoryGenerator RepositoryGenerator { get; } = T0103.RepositoryGenerator.Instance;
        public static IServiceAction ServiceAction { get; } = T0062.ServiceAction.Instance;
        public static IServiceCollectionOperator ServiceCollectionOperator { get; } = T0061.ServiceCollectionOperator.Instance;
        public static ISolutionGenerator SolutionGenerator { get; } = T0113.SolutionGenerator.Instance;
        public static ISolutionPathsOperator SolutionPathsOperator { get; } = T0040.SolutionPathsOperator.Instance;
        public static ITypeName TypeName { get; } = T0034.TypeName.Instance;
    }
}
