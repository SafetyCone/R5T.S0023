using System;
using System.Threading.Tasks;using R5T.T0064;


namespace R5T.S0023
{[ServiceDefinitionMarker]
    /// <summary>
    /// Provides the project file path containing the IProjectPath extension method base type definition.
    /// This path is required for code that wants to add a reference to the IProjectPath project, perhaps as part of automatically write IProjectPath extension methods.
    /// </summary>
    public interface IProjectPathExtensionMethodBaseProjectPathProvider:IServiceDefinition
    {
        Task<string> GetProjectPathExtensionMethodBaseProjectPath();
    }
}
