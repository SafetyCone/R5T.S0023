using System;
using System.Threading.Tasks;

using R5T.Lombardy;

using R5T.T0064;


namespace R5T.S0023
{
    [ServiceImplementationMarker]
    public class OrganizationSharedDataDirectoryFilePathProvider : IOrganizationSharedDataDirectoryFilePathProvider, IServiceImplementation
    {
        private IOrganizationSharedDataDirectoryPathProvider OrganizationSharedDataDirectoryPathProvider { get; }
        private IStringlyTypedPathOperator StringlyTypedPathOperator { get; }


        public OrganizationSharedDataDirectoryFilePathProvider(
            IOrganizationSharedDataDirectoryPathProvider organizationSharedDataDirectoryPathProvider,
            IStringlyTypedPathOperator stringlyTypedPathOperator)
        {
            this.OrganizationSharedDataDirectoryPathProvider = organizationSharedDataDirectoryPathProvider;
            this.StringlyTypedPathOperator = stringlyTypedPathOperator;
        }

        public async Task<string> GetFilePath(string fileName)
        {
            var organizationSharedDataDirectoryPath = await this.OrganizationSharedDataDirectoryPathProvider.GetOrganizationSharedDataDirectoryPath();

            var output = this.StringlyTypedPathOperator.GetFilePath(organizationSharedDataDirectoryPath, fileName);
            return output;
        }
    }
}
