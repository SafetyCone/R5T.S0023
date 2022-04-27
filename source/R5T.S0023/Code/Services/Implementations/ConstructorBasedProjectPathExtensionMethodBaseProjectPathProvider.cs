using System;
using System.Threading.Tasks;

using R5T.T0064;


namespace R5T.S0023
{
    /// <summary>
    /// <inheritdoc cref="IProjectPathExtensionMethodBaseProjectPathProvider"/>
    /// </summary>
    [ServiceImplementationMarker]
    public class ConstructorBasedProjectPathExtensionMethodBaseProjectPathProvider : IProjectPathExtensionMethodBaseProjectPathProvider, IServiceImplementation
    {
        private string ProjectPathExtensionMethodBaseProjectPath { get; }


        public ConstructorBasedProjectPathExtensionMethodBaseProjectPathProvider(
            [NotServiceComponent] string projectPathExtensionMethodBaseProjectPath)
        {
            this.ProjectPathExtensionMethodBaseProjectPath = projectPathExtensionMethodBaseProjectPath;
        }

        public Task<string> GetProjectPathExtensionMethodBaseProjectPath()
        {
            return Task.FromResult(this.ProjectPathExtensionMethodBaseProjectPath);
        }
    }
}
