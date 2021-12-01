using System;
using System.Threading.Tasks;

using R5T.Lombardy;

using R5T.T0064;


namespace R5T.S0023
{
    [ServiceImplementationMarker]
    public class OutputFilePathProvider : IOutputFilePathProvider, IServiceImplementation
    {
        private IOutputDirectoryPathProvider OutputDirectoryPathProvider { get; }
        private IStringlyTypedPathOperator StringlyTypedPathOperator { get; }


        public OutputFilePathProvider(
            IOutputDirectoryPathProvider outputDirectoryPathProvider,
            IStringlyTypedPathOperator stringlyTypedPathOperator)
        {
            this.OutputDirectoryPathProvider = outputDirectoryPathProvider;
            this.StringlyTypedPathOperator = stringlyTypedPathOperator;
        }

        public async Task<string> GetOutputFilePath(string fileName)
        {
            var outputDirectoryPath = await this.OutputDirectoryPathProvider.GetOutputDirectoryPath();

            var output = this.StringlyTypedPathOperator.GetFilePath(
                outputDirectoryPath,
                fileName);

            return output;
        }
    }
}
