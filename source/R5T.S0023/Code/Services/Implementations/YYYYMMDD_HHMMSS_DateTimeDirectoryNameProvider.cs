using System;
using System.Threading.Tasks;

using R5T.Magyar.Extensions;

using R5T.T0064;


namespace R5T.S0023
{
    [ServiceImplementationMarker]
    public class YYYYMMDD_HHMMSS_DateTimeDirectoryNameProvider : IDateTimeDirectoryNameProvider, IServiceImplementation
    {
        public Task<string> GetDateTimeDirectoryName(DateTime dateTime)
        {
            var output = dateTime.ToYYYYMMDD_HHMMSS();

            return Task.FromResult(output);
        }
    }
}
