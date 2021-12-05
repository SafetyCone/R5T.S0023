using System;
using System.Linq;
using System.Threading.Tasks;

using R5T.T0097;


namespace R5T.S0023
{
    /// <summary>
    /// Determines if any human actions are required to update the project repository.
    /// </summary>
    public class O003a_DetermineIfHumanActionsAreRequired
    {
#pragma warning disable CA1822 // Mark members as static
        public Task<HumanActionsRequired> Run(
#pragma warning restore CA1822 // Mark members as static
            Project[] currentProjects,
            Project[] repositoryProjects,
            string[] repositoryDuplicateProjectNames,
            string[] repositoryIgnoredNames)
        {
            var (newProjects, departedProjects) = Instances.Operation.GetNewAndDepartedProjects(
                currentProjects,
                repositoryProjects);

            // Are there any new projects?
            var anyNewProjects = newProjects.Any();

            // Are there any departed projects?
            var anyDepartedProjects = departedProjects.Any();

            // Are there any new duplicate project names?
            var newDuplicateProjectNames = Instances.Operation.GetNewDuplicateProjectNames(
                currentProjects,
                repositoryDuplicateProjectNames,
                repositoryIgnoredNames);

            var anyNewDuplicateProjectNames = newDuplicateProjectNames.Any();

            var output = new HumanActionsRequired
            {
                ReviewNewProjects = anyNewProjects,
                ReviewDepartedProjects = anyDepartedProjects,
                ReviewNewDuplicateProjectNames = anyNewDuplicateProjectNames,
            };

            return Task.FromResult(output);
        }
    }
}
