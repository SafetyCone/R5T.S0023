using System;
using System.Threading.Tasks;

using R5T.T0064;


namespace R5T.S0023
{
    [ServiceDefinitionMarker]
    public interface IAllProjectNamesListingFileNameProvider : IServiceDefinition
    {
        Task<string> GetAllProjectNamesListingFileName();
    }
}
