using System;
using System.Threading.Tasks;


namespace R5T.S0023
{
    public class ConstructorBasedProjectPathExtensionMethodBaseProjectPathProvider : IProjectPathExtensionMethodBaseProjectPathProvider
    {
        private string ProjectPathExtensionMethodBaseProjectPath { get; }


        public ConstructorBasedProjectPathExtensionMethodBaseProjectPathProvider(
            string projectPathExtensionMethodBaseProjectPath)
        {
            this.ProjectPathExtensionMethodBaseProjectPath = projectPathExtensionMethodBaseProjectPath;
        }

        public Task<string> GetProjectPathExtensionMethodBaseProjectPath()
        {
            return Task.FromResult(this.ProjectPathExtensionMethodBaseProjectPath);
        }
    }
}
