using System;
using System.Linq;
using System.Threading.Tasks;

using R5T.D0101;
using R5T.T0097;


namespace R5T.S0023
{
    public static class IProjectRepositoryExtensions
    {
        public static async Task<Project[]> GetExpectedUniqueProjects(this IProjectRepository projectRepository)
        {
            var projects = await projectRepository.GetAllProjects();
            var duplicateProjectNameSelections = await projectRepository.GetAllDuplicateProjectNameSelections();
            var ignoredProjectNames = await projectRepository.GetAllIgnoredProjectNames();

            var uniqueProjects = projects.GetUniqueItems(
                duplicateProjectNameSelections,
                ignoredProjectNames);

            return uniqueProjects;
        }

        public static async Task UpdateProjectNameSelections(this IProjectRepository projectRepository)
        {
            var expectedUniqueProjects = await projectRepository.GetExpectedUniqueProjects();

            var expectedProjectNameSelections = expectedUniqueProjects
                .Select(x => new ProjectNameSelection
                {
                    ProjectIdentity = x.Identity,
                    ProjectName = x.Name
                })
                ;

            // Just clear and add all.
            await projectRepository.ClearAllProjectNameSelections();

            foreach (var projectNameSelection in expectedProjectNameSelections)
            {
                await projectRepository.AddProjectNameSelection(projectNameSelection);
            }
        }
    }
}
