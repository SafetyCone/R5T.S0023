using System;
using System.Threading.Tasks;

using R5T.Lombardy;

using R5T.D0095.D001;


namespace R5T.S0023
{
    public class LogFilePathProvider : ILogFilePathProvider
    {
        private ILogFileNameProvider LogFileNameProvider { get; }
        private IOutputFilePathProvider OutputFilePathProvider { get; }


        public LogFilePathProvider(
            ILogFileNameProvider logFileNameProvider,
            IOutputFilePathProvider outputFilePathProvider)
        {
            this.LogFileNameProvider = logFileNameProvider;
            this.OutputFilePathProvider = outputFilePathProvider;
        }

        public async Task<string> GetLogFilePath()
        {
            var logFileName = await this.LogFileNameProvider.GetLogFileName();

            var output = await this.OutputFilePathProvider.GetOutputFilePath(logFileName);
            return output;
        }
    }
}
