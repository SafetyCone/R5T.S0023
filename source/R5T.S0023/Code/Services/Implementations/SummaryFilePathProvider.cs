using System;
using System.Threading.Tasks;

using R5T.D0048;


namespace R5T.S0023
{
    public class SummaryFilePathProvider : ISummaryFilePathProvider
    {
        public const string SummaryTextFileName = "Summary-Current Projects Analysis.txt";


        private IOutputFilePathProvider OutputFilePathProvider { get; }


        public SummaryFilePathProvider(
            IOutputFilePathProvider outputFilePathProvider)
        {
            this.OutputFilePathProvider = outputFilePathProvider;
        }

        public async Task<string> GetSummaryFilePath()
        {
            var output = await this.OutputFilePathProvider.GetOutputFilePath(SummaryFilePathProvider.SummaryTextFileName);
            return output;
        }
    }
}
