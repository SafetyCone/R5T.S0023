using System;
using System.Threading.Tasks;

using R5T.T0064;


namespace R5T.S0023
{
    /// <summary>
    /// Performs no operations, just providing the input name as the output directory name.
    /// This assumes that the input name is a valid directory name.
    /// </summary>
    [ServiceImplementationMarker]
    public class DirectDirectoryNameProvider : IDirectoryNameProvider, IServiceImplementation
    {
        public Task<string> GetDirectoryName(string name)
        {
            return Task.FromResult(name);
        }
    }
}
