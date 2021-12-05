using System;
using System.Threading.Tasks;

using R5T.Quadia.D002;

using R5T.T0064;


namespace R5T.S0023
{
    [ServiceImplementationMarker]
    public class AllProjectNamesListingFilePathProvider : IAllProjectNamesListingFilePathProvider, IServiceImplementation
    {
        private IAllProjectNamesListingFileNameProvider AllProjectNamesListingFileNameProvider { get; }
        private IOrganizationSharedDataDirectoryFilePathProvider OrganizationSharedDataDirectoryFilePathProvider { get; }


        public AllProjectNamesListingFilePathProvider(
            IAllProjectNamesListingFileNameProvider allProjectNamesListingFileNameProvider,
            IOrganizationSharedDataDirectoryFilePathProvider organizationSharedDataDirectoryFilePathProvider)
        {
            this.AllProjectNamesListingFileNameProvider = allProjectNamesListingFileNameProvider;
            this.OrganizationSharedDataDirectoryFilePathProvider = organizationSharedDataDirectoryFilePathProvider;
        }

        public async Task<string> GetAllProjectNamesListingFilePath()
        {
            var allProjectNamesListingFileName = await this.AllProjectNamesListingFileNameProvider.GetAllProjectNamesListingFileName();

            var output = await this.OrganizationSharedDataDirectoryFilePathProvider.GetFilePath(allProjectNamesListingFileName);
            return output;
        }
    }
}
