using System;
using System.Threading.Tasks;


namespace R5T.S0023
{
    public class ConstructorBasedLogFileNameProvider : ILogFileNameProvider
    {
        private string LogFileName { get; }


        public ConstructorBasedLogFileNameProvider(
            string logFileName)
        {
            this.LogFileName = logFileName;
        }

        public Task<string> GetLogFileName()
        {
            return Task.FromResult(this.LogFileName);
        }
    }
}
