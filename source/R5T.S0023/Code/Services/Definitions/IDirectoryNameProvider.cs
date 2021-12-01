using System;
using System.Threading.Tasks;

using R5T.T0064;


namespace R5T.S0023
{
    /// <summary>
    /// Processes a name to provide a corresponding directory name.
    /// This includes removing disallowed characters, obeying length constraints, or could be just a lookup.
    /// </summary>
    [ServiceDefinitionMarker]
    public interface IDirectoryNameProvider : IServiceDefinition
    {
        Task<string> GetDirectoryName(string name);
    }
}
