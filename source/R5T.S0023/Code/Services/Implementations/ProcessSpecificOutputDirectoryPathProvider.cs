using System;
using System.Threading.Tasks;

using R5T.Lombardy;

using R5T.T0064;


namespace R5T.S0023
{
    [ServiceImplementationMarker]
    public class ProcessSpecificOutputDirectoryPathProvider : IProcessSpecificOutputDirectoryPathProvider, IServiceImplementation
    {
        private IProcessDirectoryNameProvider ProcessDirectoryNameProvider { get; }
        private IRootOutputDirectoryPathProvider RootOutputDirectoryPathProvider { get; }
        private IStringlyTypedPathOperator StringlyTypedPathOperator { get; }


        public ProcessSpecificOutputDirectoryPathProvider(
            IProcessDirectoryNameProvider processDirectoryNameProvider,
            IRootOutputDirectoryPathProvider rootOutputDirectoryPathProvider,
            IStringlyTypedPathOperator stringlyTypedPathOperator)
        {
            this.RootOutputDirectoryPathProvider = rootOutputDirectoryPathProvider;
            this.ProcessDirectoryNameProvider = processDirectoryNameProvider;
            this.StringlyTypedPathOperator = stringlyTypedPathOperator;
        }

        public async Task<string> GetProcessSpecificOutputDirectoryPath()
        {
            var (rootOutputDirectoryPath, processDirectoryName) = await TaskHelper.WhenAll(
                this.RootOutputDirectoryPathProvider.GetRootOutputDirectoryPath(),
                this.ProcessDirectoryNameProvider.GetProcessDirectoryName());

            var output = this.StringlyTypedPathOperator.GetDirectoryPath(
                rootOutputDirectoryPath,
                processDirectoryName);

            return output;
        }
    }
}
