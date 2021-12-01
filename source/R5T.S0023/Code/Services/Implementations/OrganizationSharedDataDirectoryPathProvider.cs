using System;
using System.Threading.Tasks;

using R5T.Quadia;

using R5T.T0064;


namespace R5T.S0023
{
    [ServiceImplementationMarker]
    public class OrganizationSharedDataDirectoryPathProvider : IOrganizationSharedDataDirectoryPathProvider, IServiceImplementation
    {
        private IOrganizationDataDirectoryPathProvider OrganizationDataDirectoryPathProvider { get; }


        public OrganizationSharedDataDirectoryPathProvider(
            IOrganizationDataDirectoryPathProvider organizationDataDirectoryPathProvider)
        {
            this.OrganizationDataDirectoryPathProvider = organizationDataDirectoryPathProvider;
        }

        public async Task<string> GetOrganizationSharedDataDirectoryPath()
        {
            var organizationDataDirectoryPath = await this.OrganizationDataDirectoryPathProvider.GetOrganizationDataDirectoryPath();

            // Just use the organization data director path.
            return organizationDataDirectoryPath;
        }
    }
}
