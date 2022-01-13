using System;
using System.Collections.Generic;
using System.Linq;

using R5T.T0097;

using R5T.S0023;


namespace System
{
    public static class ProjectRepositoryStateExtensions
    {
        /// <summary>
        /// Makes a copy of each <see cref="Project"/> instance.
        /// </summary>
        public static void AddProjectCopies(this ProjectRepositoryState state,
            IEnumerable<Project> projects)
        {
            var copies = projects.Select(xProject => xProject.Copy());

            state.Projects.AddRange(copies);
        }

        public static void AddProjects(this ProjectRepositoryState state,
            IEnumerable<Project> projects)
        {
            state.Projects.AddRange(projects);
        }

        public static void AddIgnoredNames(this ProjectRepositoryState state,
            IEnumerable<string> ignoredNames)
        {
            state.IgnoredNames.AddRange(ignoredNames);
        }

        /// <summary>
        /// Makes a copy of each <see cref="ProjectNameSelection"/> instance.
        /// </summary>
        public static void AddDuplicateNameSelectionCopies(this ProjectRepositoryState state,
            IEnumerable<ProjectNameSelection> duplicateNameSelections)
        {
            var copies = duplicateNameSelections.Select(xNameSelection => xNameSelection.Copy());

            state.DuplicateNameSelections.AddRange(copies);
        }

        /// <summary>
        /// Makes a copy of each <see cref="ProjectNameSelection"/> instance.
        /// </summary>
        public static void AddNameSelectionsCopies(this ProjectRepositoryState state,
            IEnumerable<ProjectNameSelection> nameSelections)
        {
            var copies = nameSelections.Select(xNameSelection => xNameSelection.Copy());

            state.NameSelections.AddRange(copies);
        }

        public static ProjectRepositoryState Copy(this ProjectRepositoryState other)
        {
            var output = new ProjectRepositoryState()
                .FillFromCopies(
                    other.DuplicateNameSelections,
                    other.IgnoredNames,
                    other.NameSelections,
                    other.Projects);

            return output;
        }

        /// <summary>
        /// Fluent (output is the same instance as input).
        /// </summary>
        public static ProjectRepositoryState FillFromCopies(this ProjectRepositoryState state,
            IEnumerable<ProjectNameSelection> duplicateNameSelections,
            IEnumerable<string> ignoredNames,
            IEnumerable<ProjectNameSelection> nameSelections,
            IEnumerable<Project> projects)
        {
            state.AddDuplicateNameSelectionCopies(duplicateNameSelections);
            state.AddIgnoredNames(ignoredNames);
            state.AddNameSelectionsCopies(nameSelections);
            state.AddProjectCopies(projects);

            return state;
        }

        /// <summary>
        /// Fluent (output is the same instance as input).
        /// </summary>
        public static ProjectRepositoryState FillFrom(this ProjectRepositoryState state,
            IEnumerable<ProjectNameSelection> duplicateNameSelections,
            IEnumerable<string> ignoredNames,
            IEnumerable<ProjectNameSelection> nameSelections,
            IEnumerable<Project> projects)
        {
            state.DuplicateNameSelections.AddRange(duplicateNameSelections);
            state.IgnoredNames.AddRange(ignoredNames);
            state.NameSelections.AddRange(nameSelections);
            state.Projects.AddRange(projects);

            return state;
        }

        public static ProjectRepositoryState ReplaceProjects(this ProjectRepositoryState state,
            IEnumerable<Project> newProjects)
        {
            state.Projects.Clear();

            state.AddProjects(newProjects);

            return state;
        }

        public static void VerifySelectedNames(this ProjectRepositoryState state)
        {
            // Selected names should not contain any ignored names.
            var nameSelectionNames = state.NameSelections.Select(xNameSelection => xNameSelection.ProjectName);

            var ignoredNamesInNameSelections = nameSelectionNames.Intersect(state.IgnoredNames);
        }
    }
}
