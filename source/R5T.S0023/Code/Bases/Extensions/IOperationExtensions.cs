using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using R5T.D0084.D001;
using R5T.T0092;
using R5T.T0094;
using R5T.T0097;
using R5T.T0098;

using Instances = R5T.S0023.Instances;


namespace System
{
    public static class IOperationExtensions
    {
        //public static void WriteSelectedNameChangesSummaryFile(this IOperation _,
        //    StreamWriter output,
        //    IList<(ProjectNameSelection, string)> newNameSelectionsWithReasons,
        //    IList<(ProjectNameSelection, string)> departedNameSelectionsWithReasons,
        //    IEnumerable<Project> projects)
        //{
        //    var projectsByIdentity = projects.ToDictionaryByIdentity();

        //    var newNameSelectionsCount = newNameSelectionsWithReasons.Count;

        //    output.WriteLine($"New selected names ({newNameSelectionsCount}):");
        //    output.WriteLine();

        //    if (newNameSelectionsWithReasons.None())
        //    {
        //        output.WriteLine("<none> (ok)");
        //    }
        //    else
        //    {
        //        var newNameSelectionsByReason = newNameSelectionsWithReasons
        //            .GroupBy(x => x.Item2)
        //            .ToDictionary(
        //                x => x.Key,
        //                x => x.Select(y => y.Item1).ToArray());

        //        foreach (var pair in newNameSelectionsByReason)
        //        {
        //            // Reason
        //            output.WriteLine($"# {pair.Key}");

        //            foreach (var projectNameSelection in pair.Value)
        //            {
        //                var project = projectsByIdentity[projectNameSelection.ProjectIdentity];

        //                output.WriteLine($"{project.Name}: {project.FilePath}");
        //            }
        //            output.WriteLine();
        //        }
        //    }
        //    output.WriteLine("\n***\n");

        //    var departedNameSelectionsCount = departedNameSelectionsWithReasons.Count;

        //    output.WriteLine($"Departed selected names ({departedNameSelectionsCount}):");
        //    output.WriteLine();

        //    if (departedNameSelectionsWithReasons.None())
        //    {
        //        output.WriteLine("<none> (ok)");
        //    }
        //    else
        //    {
        //        var departedNameSelectionsByReason = departedNameSelectionsWithReasons
        //            .GroupBy(x => x.Item2)
        //            .ToDictionary(
        //                x => x.Key,
        //                x => x.Select(y => y.Item1).ToArray());

        //        foreach (var pair in departedNameSelectionsByReason)
        //        {
        //            // Reason
        //            output.WriteLine($"# {pair.Key}");

        //            foreach (var projectNameSelection in pair.Value)
        //            {
        //                var projectForIdentityExists = projectsByIdentity.ContainsKey(projectNameSelection.ProjectIdentity);
        //                if(projectForIdentityExists)
        //                {
        //                    var project = projectsByIdentity[projectNameSelection.ProjectIdentity];

        //                    output.WriteLine($"{project.Name}: {project.FilePath}");
        //                }
        //                else
        //                {
        //                    output.WriteLine($"{projectNameSelection.ProjectName}: No file path exists (departed).");
        //                }
        //            }
        //            output.WriteLine();
        //        }
        //    }
        //    output.WriteLine("\n***\n");
        //}

        public static void WriteUnspecifiedDuplicateProjectNamesSummaryFile(this IOperation _,
            StreamWriter output,
            Dictionary<string, Project[]> projectsByName)
        {
            var duplicateProjectNamesCount = projectsByName.Count;

            output.WriteLine($"Duplicate project names ({duplicateProjectNamesCount}):");
            output.WriteLine();

            if (projectsByName.None())
            {
                output.WriteLine("<none> (ok)");
            }
            else
            {
                foreach (var pair in projectsByName)
                {
                    output.WriteLine($"{pair.Key}:");

                    foreach (var project in pair.Value)
                    {
                        output.WriteLine($"{project.Name}| {project.FilePath}");
                    }
                    output.WriteLine();
                }
            }
            output.WriteLine("\n***\n");
        }

        public static void WriteNewAndDepartedSummaryFile(this IOperation _,
            StreamWriter output,
            Project[] newProjects,
            Project[] departedProjects)
        {
            var newProjectsCount = newProjects.Length;

            output.WriteLine($"New projects ({newProjectsCount}):");
            output.WriteLine();

            if (newProjects.None())
            {
                output.WriteLine("<none> (ok)");
            }
            else
            {
                foreach (var project in newProjects)
                {
                    output.WriteLine($"{project.Name}: {project.FilePath}");
                }
            }
            output.WriteLine("\n***\n");

            var departedProjectsCount = departedProjects.Length;

            output.WriteLine();
            output.WriteLine($"Departed projects ({departedProjectsCount}):");
            output.WriteLine();

            if (departedProjects.None())
            {
                output.WriteLine("<none> (ok)");
            }
            else
            {
                foreach (var project in departedProjects)
                {
                    output.WriteLine($"{project.Name}: {project.FilePath}");
                }
            }
            output.WriteLine("\n***\n");
        }

        public static (
            bool isValid,
            string[] ignoredNamesPresentInExpectedNames,
            string[] unspecifiedDuplicateProjectNames,
            ProjectNameSelection[] wrongDuplicateNameSelections,
            ProjectNameSelection[] newNameSelections,
            ProjectNameSelection[] departedNameSelections)
        IsValidNameSelections(this IOperation _,
            IList<ProjectNameSelection> expectedNameSelections,
            IList<Project> projects,
            IList<string> ignoredNames,
            IList<ProjectNameSelection> duplicateNameSelections)
        {
            var (ignoredNamesPresentInExpectedNames,
                unspecifiedDuplicateProjectNames,
                wrongDuplicateNameSelections,
                newNameSelections,
                departedNameSelections)
                = _.CompareNameSelections(
                expectedNameSelections,
                projects,
                ignoredNames,
                duplicateNameSelections);

            var isValid = _.IsValidNameSelections(
                ignoredNamesPresentInExpectedNames,
                unspecifiedDuplicateProjectNames,
                wrongDuplicateNameSelections,
                newNameSelections,
                departedNameSelections);

            return (
                isValid,
                ignoredNamesPresentInExpectedNames,
                unspecifiedDuplicateProjectNames,
                wrongDuplicateNameSelections,
                newNameSelections,
                departedNameSelections);
        }

        public static bool IsValidNameSelections(this IOperation _,
            string[] ignoredNamesPresentInExpectedNames,
            string[] unspecifiedDuplicateProjectNames,
            ProjectNameSelection[] wrongDuplicateNameSelections,
            ProjectNameSelection[] newNameSelections,
            ProjectNameSelection[] departedNameSelections)
        {
            var output = true
                && ignoredNamesPresentInExpectedNames.Length == 0
                && unspecifiedDuplicateProjectNames.Length == 0
                && wrongDuplicateNameSelections.Length == 0
                && newNameSelections.Length == 0
                && departedNameSelections.Length == 0
                ;

            return output;
        }

        public static (
            string[] ignoredNamesPresentInExpectedNames,
            string[] unspecifiedDuplicateProjectNames,
            ProjectNameSelection[] wrongDuplicateNameSelections,
            ProjectNameSelection[] newNameSelections,
            ProjectNameSelection[] departedNameSelections)
        CompareNameSelections(this IOperation _,
            IList<ProjectNameSelection> expectedNameSelections,
            IList<Project> projects,
            IList<string> ignoredNames,
            IList<ProjectNameSelection> duplicateNameSelections)
        {
            // Verify inputs.
            expectedNameSelections.VerifyDistinctNamesAndDistinctIdentities();
            duplicateNameSelections.VerifyDistinctNamesAndDistinctIdentities();

            var ignoredNamesHash = new HashSet<string>(ignoredNames);

            // None of the ignored names should appear in the expected name selection.
            var expectedNameSelectionNames = expectedNameSelections
                .Select(xNameSelection => xNameSelection.ProjectName)
                ;

            // Should be empty.
            var ignoredNamesPresentInExpectedNames = expectedNameSelectionNames.Intersect(ignoredNamesHash).Now();

            // Only work with projects with unignored names.
            var unignoredProjects = projects
                .Where(xProject => !ignoredNamesHash.Contains(xProject.Name))
                .Now();

            // Ensure that all duplicate names are accounted for in duplicate name selections.
            var projectGroupsByName = unignoredProjects
                .GroupBy(xProject => xProject.Name)
                ;

            var duplicateProjectNames = projectGroupsByName
                .Where(xGroup => xGroup.Count() > 1)
                .Select(xGroup => xGroup.Key)
                .OrderAlphabetically()
                .Now();

            var unspecifiedDuplicateProjectNames = duplicateProjectNames.Except(duplicateProjectNames).Now();

            // Of those projects with duplicate names that are specified, are the correct project identities present in the expected name selections.
            // This is the complement to the unspecified duplicate project names.
            var expectedNameSelectionsByName = expectedNameSelections.ToDictionaryByName();

            var wrongDuplicateNameSelections = new List<ProjectNameSelection>();

            foreach (var duplicateNameSelection in duplicateNameSelections)
            {
                // The duplicate name might not exist in the expected names, and that's ok.
                var duplicateNameExistsInExpected = expectedNameSelectionsByName.ContainsKey(duplicateNameSelection.ProjectName);
                if(duplicateNameExistsInExpected)
                {
                    var expectedNameSelection = expectedNameSelectionsByName[duplicateNameSelection.ProjectName];

                    var sameIdentity = duplicateNameSelection.ProjectIdentity == expectedNameSelection.ProjectIdentity;
                    if(!sameIdentity)
                    {
                        // Chose the expected name selection as the instance to be "wrong".
                        wrongDuplicateNameSelections.Add(expectedNameSelection);
                    }
                }
                // Else, does not matter.
            }

            // Now get actual name selections.
            // Ok to throw away unspecified duplicate project name sets output, should be the same as unspecified duplicate project names above.
            var (actualNameSelections, _) = _.GetSelectedNames(
                projects,
                ignoredNames,
                duplicateNameSelections);

            // Get the new and departed name selections.
            var newNameSelections = actualNameSelections.Except(expectedNameSelections, NamedIdentifiedEqualityComparer<ProjectNameSelection>.Instance).Now();
            var departedNameSelections = expectedNameSelections.Except(actualNameSelections, NamedIdentifiedEqualityComparer<ProjectNameSelection>.Instance).Now();

            return (
                ignoredNamesPresentInExpectedNames,
                unspecifiedDuplicateProjectNames,
                wrongDuplicateNameSelections.Now(),
                newNameSelections,
                departedNameSelections);
        }

        //public static (
        //    ProjectNameSelection[] nameSelections,
        //    Dictionary<string, Project[]> unspecifiedDuplicateProjectNameSets)
        //GetSelectedNames(this IOperation _,
        //    IList<Project> projects,
        //    IList<string> ignoredNames,
        //    IList<ProjectNameSelection> duplicateNameSelections)
        //{
        //    // Verify inputs.
        //    projects.VerifyDistinctByIdentity();
        //    projects.VerifyDistinctByFilePath();
        //    duplicateNameSelections.VerifyDistinctNamesAndDistinctIdentities();

        //    var ignoredNamesHash = new HashSet<string>(ignoredNames);

        //    // Only work with projects with unignored names.
        //    var unignoredProjects = projects
        //        .Where(xProject => !ignoredNamesHash.Contains(xProject.Name))
        //        .Now();

        //    var projectGroupsByName = unignoredProjects
        //        .GroupBy(xProject => xProject.Name)
        //        ;

        //    // Handle non-duplicate projects.
        //    var nonDuplicateProjectNameSelections = projectGroupsByName
        //        .Where(xGroup => xGroup.Count() == 1)
        //        .SelectMany(xGroup => xGroup
        //            .Select(xProject =>
        //            {
        //                var nameSelection = new ProjectNameSelection
        //                {
        //                    ProjectIdentity = xProject.Identity,
        //                    ProjectName = xProject.Name,
        //                };

        //                return nameSelection;
        //            }))
        //        ;

        //    // Handle duplicates.
        //    var duplicateProjecGroupsByName = projectGroupsByName
        //        .Where(xGroup => xGroup.Count() > 1);

        //    var duplicateNameSelectionsNamesHash = new HashSet<string>(duplicateNameSelections.GetAllNames());

        //    // Get specified and unspecified duplicate name selections.
        //    var specifiedDuplicateProjectNameGroups = duplicateProjecGroupsByName
        //        .Where(xGroup => duplicateNameSelectionsNamesHash.Contains(xGroup.Key));

        //    var duplicateNameSelectionsByName = duplicateNameSelections.ToDictionaryByName();

        //    var duplicateProjectNameSelections = specifiedDuplicateProjectNameGroups
        //        .Select(xGroup =>
        //        {
        //            var duplicateNameSelection = duplicateNameSelectionsByName[xGroup.Key];

        //            // Make a copy of the instance.
        //            var duplicateNameSelectionCopy = duplicateNameSelection.Copy();
        //            return duplicateNameSelectionCopy;
        //        });

        //    var unspecifiedDuplicateProjectNameSets = duplicateProjecGroupsByName
        //        .Where(xGroup => !duplicateNameSelectionsNamesHash.Contains(xGroup.Key))
        //        .ToDictionary(
        //            x => x.Key,
        //            x => x.ToArray());

        //    var nameSelections = nonDuplicateProjectNameSelections.Concat(duplicateProjectNameSelections).Now();

        //    return (nameSelections, unspecifiedDuplicateProjectNameSets);
        //}

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
            IList<Project> repositoryProjects,
            IList<Project> currentProjects)
        {
            // No need to check data values, because we are using a data value-based equality comparer, and the only other field is identity, which will not have been set for the local extension method base objects.
            var newProjects = currentProjects.Except<Project>(repositoryProjects, NamedFilePathedEqualityComparer.Instance)
                .ToArray();

            var departedProjects = repositoryProjects.Except<Project>(currentProjects, NamedFilePathedEqualityComparer.Instance)
                .ToArray();

            return (newProjects, departedProjects);
        }

        public static string[] GetNewIgnoredProjectNames(this IOperation _,
            ProjectNameSelection[] priorSelectedProjectNames,
            string[] currentIgnoredProjectNames)
        {
            var repositoryIgnoredProjectNamesHash = new HashSet<string>(currentIgnoredProjectNames);

            var output = priorSelectedProjectNames
                .Where(xProjectName => repositoryIgnoredProjectNamesHash.Contains(xProjectName.ProjectName))
                .Select(xProjectName => xProjectName.ProjectName)
                .OrderAlphabetically() // To aide debugging.
                .ToArray();

            return output;
        }

        public static async Task<(
            Project[] currentProjects,
            Project[] newProjects,
            Project[] departedProjects)>
        GetProjectChanges(this IOperation _,
            IAllProjectFilePathsProvider allProjectFilePathsProvider,
            IList<Project> repositoryProjects)
        {
            var currentProjects = await _.GetCurrentProjects(allProjectFilePathsProvider);

            var (newProjects, departedProjects) = _.GetProjectChanges(
                currentProjects,
                repositoryProjects);

            return (currentProjects, newProjects, departedProjects);
        }

        public static (
            Project[] newProjects,
            Project[] departedProjects)
        GetProjectChanges(this IOperation _,
            IList<Project> currentProjects,
            IList<Project> repositoryProjects)
        {
            // Just to check, verify that current projects are distinct by (project name, project file path) pair (data-values).
            currentProjects.VerifyDistinctByNamedFilePathedData();

            // Just to check, verify that the repository's projects are distinct by (namespaced type name, code file path) pair.
            repositoryProjects.VerifyDistinctByNamedFilePathedData();

            // Determine which projcts to add, and which to remove from the repository.
            var (newProjects, departedProjects) = _.GetNewAndDepartedProjects(repositoryProjects, currentProjects);

            return (newProjects, departedProjects);
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
