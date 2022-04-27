using System;
using System.Threading.Tasks;

using R5T.T0064;


namespace R5T.S0023
{
    [ServiceImplementationMarker]
    public class ConstructorBasedAllProjectNamesListingFileNameProvider : IAllProjectNamesListingFileNameProvider, IServiceImplementation
    {
        private string AllProjectNamesListingFileName { get; }


        public ConstructorBasedAllProjectNamesListingFileNameProvider(
            [NotServiceComponent] string allProjectNamesListingFileName)
        {
            this.AllProjectNamesListingFileName = allProjectNamesListingFileName;
        }

        public Task<string> GetAllProjectNamesListingFileName()
        {
            return Task.FromResult(this.AllProjectNamesListingFileName);
        }
    }
}
