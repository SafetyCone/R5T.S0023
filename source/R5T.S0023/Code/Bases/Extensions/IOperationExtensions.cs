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
        public static async Task<(
            Project[] currentProjects,
            Project[] newProjects,
            Project[] departedProjects)>
        GetProjectChanges(this IOperation _,
            IAllProjectFilePathsProvider allProjectFilePathsProvider,
            Project[] repositoryProjects)
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

            // Just to check, verify that current projects are distinct by (project name, project file path) pair (data-values).
            currentProjects.VerifyDistinctByNamedFilePathedData();

            // Just to check, verify that the repository's projects are distinct by (namespaced type name, code file path) pair.
            repositoryProjects.VerifyDistinctByNamedFilePathedData();

            // Determine which projcts to add, and which to remove from the repository.
            // No need to check data values, because we are using a data value-based equality comparer, and the only other field is identity, which will not have been set for the local extension method base objects.
            var newProjects = currentProjects.Except<Project>(repositoryProjects, NamedFilePathedEqualityComparer.Instance)
                .ToArray();

            var departedProjects = repositoryProjects.Except<Project>(currentProjects, NamedFilePathedEqualityComparer.Instance)
                .ToArray();


            return (currentProjects, newProjects, departedProjects);
        }
    }
}
