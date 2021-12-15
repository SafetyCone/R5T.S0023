using System;
using System.Linq;
using System.Threading.Tasks;

using R5T.D0084.D001;
using R5T.T0094;
using R5T.T0097;
using R5T.T0098;

using Instances = R5T.S0023.Instances;


namespace System
{
    public static class IOperationExtensions
    {
        public static string[] GetNewDuplicateProjectNames(this IOperation _,
            Project[] currentProjects,
            string[] repositoryDuplicateProjectNames,
            string[] repositoryIgnoredNames)
        {
            // Determine any new duplicate project names.
            var currentDuplicateProjectNames = currentProjects.GetDuplicateNamesInAlphabeticalOrder();

            var newDuplicateProjectNames = currentDuplicateProjectNames.Except(repositoryDuplicateProjectNames)
                .Except(repositoryIgnoredNames) // Ignored names are ignored, even if duplicates.
                .ToArray();

            return newDuplicateProjectNames;
        }

        public static (
            Project[] newProjects,
            Project[] departedProjects)
        GetNewAndDepartedProjects(this IOperation _,
            Project[] repositoryProjects,
            Project[] currentProjects)
        {
            // No need to check data values, because we are using a data value-based equality comparer, and the only other field is identity, which will not have been set for the local extension method base objects.
            var newProjects = currentProjects.Except<Project>(repositoryProjects, NamedFilePathedEqualityComparer.Instance)
                .ToArray();

            var departedProjects = repositoryProjects.Except<Project>(currentProjects, NamedFilePathedEqualityComparer.Instance)
                .ToArray();

            return (newProjects, departedProjects);
        }

        public static async Task<(
            Project[] currentProjects,
            Project[] newProjects,
            Project[] departedProjects)>
        GetProjectChanges(this IOperation _,
            IAllProjectFilePathsProvider allProjectFilePathsProvider,
            Project[] repositoryProjects)
        {
            var currentProjects = await _.GetCurrentProjects(allProjectFilePathsProvider);

            // Just to check, verify that current projects are distinct by (project name, project file path) pair (data-values).
            currentProjects.VerifyDistinctByNamedFilePathedData();

            // Just to check, verify that the repository's projects are distinct by (namespaced type name, code file path) pair.
            repositoryProjects.VerifyDistinctByNamedFilePathedData();

            // Determine which projcts to add, and which to remove from the repository.
            var (newProjects, departedProjects) = _.GetNewAndDepartedProjects(repositoryProjects, currentProjects);

            return (currentProjects, newProjects, departedProjects);
        }

        public static async Task<Project[]> GetCurrentProjects(this IOperation _,
            IAllProjectFilePathsProvider allProjectFilePathsProvider)
        {
            // Operate by project file path, which is unique.

            // Get all current project file paths (available from the project file paths provider).
            var currentProjectFilePaths = await allProjectFilePathsProvider.GetAllProjectFilePaths();

            var currentProjects = currentProjectFilePaths
                .Select(projectFilePath =>
                {
                    var projectName = Instances.ProjectPathsOperator.GetProjectName(projectFilePath);

                    var project = new Project
                    {
                        // No identity, as that will be set (if needed) when added to the repository, and equality comparisons for Exist() methods will be done using a data value-based equality comparer.
                        Name = projectName,
                        FilePath = projectFilePath
                    };

                    return project;
                })
                .ToArray();

            return currentProjects;
        }
    }
}
