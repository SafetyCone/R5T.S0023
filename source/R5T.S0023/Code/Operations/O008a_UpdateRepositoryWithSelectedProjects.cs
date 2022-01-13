using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using R5T.D0101;
using R5T.D0105;
using R5T.T0020;
using R5T.T0097;
using R5T.T0097.X002;


namespace R5T.S0023
{
    /// <summary>
    /// Updates the project repository with selected (not ignored, duplicate specified) projects.
    /// Only works on projects in the repository.
    /// Demands that all duplicate project names be specified.
    /// </summary>
    [OperationMarker]
    public class O008a_UpdateRepositoryWithSelectedProjects : IActionOperation
    {
        #region Static

        private static HumanActionsRequired02 DetermineRequiredHumanActionsForDuplicateProjectNames(
            IEnumerable<string> duplicateProjectNames)
        {
            var anyDuplicateProjectNames = duplicateProjectNames.Any();

            var output = new HumanActionsRequired02
            {
                ReviewDuplicateProjectNames = anyDuplicateProjectNames,
            };

            return output;
        }

        public static HumanActionsRequired03 DetermineRequiredHumanActionsForSelectedNames(
            IEnumerable<(ProjectNameSelection, string)> newSelectionsWithReasons,
            IEnumerable<(ProjectNameSelection, string)> departedSelectionsWithReasons)
        {
            var anyNewSelections = newSelectionsWithReasons.Any();
            var anyDepartedSelections = departedSelectionsWithReasons.Any();

            var output = new HumanActionsRequired03
            {
                ReviewDepartedSelectedNames = anyDepartedSelections,
                ReviewNewSelectedNames = anyNewSelections,
            };

            return output;
        }

        private static async Task<(string[] ignoredNames, ProjectNameSelection[] duplicateNameSelections)> LoadAnalysisInputData(
            string temporaryIgnoredProjectNamesTextFilePath,
            string temporaryDuplicateProjectNamesTextFilePath,
            IEnumerable<Project> projects)
        {
            var ignoredNamesHash = await Instances.IgnoredValuesOperator.LoadIgnoredValues(
                temporaryIgnoredProjectNamesTextFilePath);

            var ignoredNames = ignoredNamesHash.OrderAlphabetically().ToArray();

            var duplicateNameSelections = await Instances.Operation.LoadProjectNameSelections(
                temporaryDuplicateProjectNamesTextFilePath,
                projects);

            return (ignoredNames, duplicateNameSelections);
        }

        #endregion

        private INotepadPlusPlusOperator NotepadPlusPlusOperator { get; }
        private IProjectRepository ProjectRepository { get; }


        public O008a_UpdateRepositoryWithSelectedProjects(
            INotepadPlusPlusOperator notepadPlusPlusOperator,
            IProjectRepository projectRepository)
        {
            this.NotepadPlusPlusOperator = notepadPlusPlusOperator;
            this.ProjectRepository = projectRepository;
        }

        public async Task Run()
        {
            // Backup should be performed by calling operation.

            // Inputs.
            var temporaryDuplicateProjectNamesTextFilePath = @"C:\Temp\Projects-Duplicate Name Selections-Temp.txt";
            var temporaryIgnoredProjectNamesTextFilePath = @"C:\Temp\Projects-Ignored Names-Temp.txt";

            var unspecifiedDuplicatesSummaryFilePath = @"C:\Temp\Summary-Unspecified Duplicate Project Names.txt";
            var selectedNameChangesSummaryFilePath = @"C:\Temp\Summary-Selected Project Changes.txt";

            // Get all projects (limiting ourselves to only projects already present in the repository).
            var projects = await this.ProjectRepository.GetAllProjects();

            // Save ignored and duplicate data to temporary file locations that will be modified during the process.
            var repositoryDuplicateNameSelections = await this.ProjectRepository.GetAllDuplicateProjectNameSelections();
            var repositoryIgnoredNames = await this.ProjectRepository.GetAllIgnoredProjectNames();

            await Instances.Operation.SaveProjectNameSelections(
                temporaryDuplicateProjectNamesTextFilePath,
                repositoryDuplicateNameSelections,
                projects);

            await Instances.IgnoredValuesOperator.SaveIgnoredValues(
                temporaryIgnoredProjectNamesTextFilePath,
                repositoryIgnoredNames);

            // Load the ignored and duplicate values from the temporary file locations (to be sure we are really using that data).
            var (ignoredNames, duplicateNameSelections) = await O008a_UpdateRepositoryWithSelectedProjects.LoadAnalysisInputData(
                temporaryIgnoredProjectNamesTextFilePath,
                temporaryDuplicateProjectNamesTextFilePath,
                projects);

            var (currentNameSelections, unspecifiedDuplicateProjectNameSets) = Instances.Operation.GetSelectedNames(
                projects,
                ignoredNames,
                duplicateNameSelections);

            // Write summary of unspecified duplicates to a file.
            // Use a scope so that file is flushed by the time it's needed.
            using (var summaryFile = FileHelper.WriteTextFile(unspecifiedDuplicatesSummaryFilePath))
            {
                Instances.Operation.WriteUnspecifiedDuplicateProjectNamesSummaryFile(
                    summaryFile,
                    unspecifiedDuplicateProjectNameSets);
            }

            // Now prompt for required human actions.
            // Determine required human actions.
            var humanActionsRequired = O008a_UpdateRepositoryWithSelectedProjects.DetermineRequiredHumanActionsForDuplicateProjectNames(
                unspecifiedDuplicateProjectNameSets.Keys);

            var anyHumanActionsRequired = humanActionsRequired.Any();
            if (anyHumanActionsRequired)
            {
                Console.WriteLine("Human actions are required before determining selected projects.\n");

                // Prompt for required human actions.
                await this.PromptForHumanActionsOnDuplicateProjectNames(
                    unspecifiedDuplicatesSummaryFilePath,
                    temporaryIgnoredProjectNamesTextFilePath,
                    temporaryDuplicateProjectNamesTextFilePath,
                    humanActionsRequired);

                // Repeatedly prompt for mandatory required human actions until they are complete.
                // Note: while no required human actions are actually mandatory for this process, this code shows the desired methodology as practice.
                // Perform analysis loop to demand that all duplicate project names are either ignored, or specified.
                while (true)
                {
                    // Reload possibly modified analysis input data.
                    (ignoredNames, duplicateNameSelections) = await O008a_UpdateRepositoryWithSelectedProjects.LoadAnalysisInputData(
                        temporaryIgnoredProjectNamesTextFilePath,
                        temporaryDuplicateProjectNamesTextFilePath,
                        projects);

                    // Recalculate analysis data (same data in this case, no recalculation necessary).
                    (currentNameSelections, unspecifiedDuplicateProjectNameSets) = Instances.Operation.GetSelectedNames(
                       projects,
                       ignoredNames,
                       duplicateNameSelections);

                    // Determine required human actions.
                    humanActionsRequired = O008a_UpdateRepositoryWithSelectedProjects.DetermineRequiredHumanActionsForDuplicateProjectNames(
                        unspecifiedDuplicateProjectNameSets.Keys);

                    // Only remaining mandatory human actions prevent progress.
                    var anyMandatoryHumanActionsRequired = humanActionsRequired.AnyMandatory();
                    if (!anyMandatoryHumanActionsRequired)
                    {
                        break;
                    }

                    // Prompt for mandatory human actions only.
                    humanActionsRequired.UnsetNonMandatory();

                    Console.WriteLine("MANDATORY human actions are required before determining selected projects.\n");

                    await this.PromptForHumanActionsOnDuplicateProjectNames(
                        unspecifiedDuplicatesSummaryFilePath,
                        temporaryIgnoredProjectNamesTextFilePath,
                        temporaryDuplicateProjectNamesTextFilePath,
                        humanActionsRequired);
                }

                Console.WriteLine("All human actions required before determining selected projects are complete.\n");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("No human actions are required before determining selected projects.\n");
                Console.WriteLine();
            }

            Console.WriteLine("Selected projects can now be determined.");
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();

            // Use the name selections from above, and load the existing (repository) name selections.
            var repositoryNameSelections = await this.ProjectRepository.GetAllProjectNameSelections();

            var (_, _, newNameSelectionsWithReasons, departedNameSelectionsWithReasons)
                = Instances.Operation.GetNewAndDepartedNameSelectionsWithReasons(
                projects,
                repositoryDuplicateNameSelections,
                repositoryNameSelections,
                currentNameSelections,
                repositoryIgnoredNames,
                ignoredNames);

            //var newNameSelections = currentNameSelections.Except(repositoryNameSelections, NamedIdentifiedEqualityComparer<ProjectNameSelection>.Instance);
            //var departedNameSelections = repositoryNameSelections.Except(currentNameSelections, NamedIdentifiedEqualityComparer<ProjectNameSelection>.Instance);

            //// Determine reasons for appearance/disappearance of name selections.
            //var projectsByIdentity = projects.ToDictionaryByIdentity();
            //var repositoryDuplicateNameSelectionsByName = repositoryDuplicateNameSelections.ToDictionaryByName();
            //var duplicateNameSelectionsByName = repositoryNameSelections.ToDictionaryByName();
            //var repositoryIgnoredNamesHash = new HashSet<string>(repositoryIgnoredNames);
            //var ignoredNamesHash = new HashSet<string>(ignoredNames);

            //var defaultReason = "<Unknown> (Should not happen.)";

            //var newNameSelectionsWithReasons = new List<(ProjectNameSelection, string)>();
            //foreach (var newNameSelection in newNameSelections)
            //{
            //    // In order of least specific to most specific.
            //    var reason = defaultReason;

            //    // Is the project new? (Project is present in projects list.)
            //    var projectNameExists = projectsByIdentity.ContainsKey(newNameSelection.ProjectIdentity);
            //    if(projectNameExists)
            //    {
            //        reason = "Project is new.";
            //    }

            //    // Is the project newly unignored? (Project name is present in initial repository ignored file, but not in modified ignored file.)
            //    var nameExistsInRepositoryIgnoredNames = repositoryIgnoredNamesHash.Contains(newNameSelection.ProjectName);
            //    var nameExistsInIgnoredNames = ignoredNamesHash.Contains(newNameSelection.ProjectName);
            //    if(nameExistsInRepositoryIgnoredNames && !nameExistsInIgnoredNames)
            //    {
            //        reason = "Name is newly unignored";
            //    }

            //    // Did the choice of duplicate project name selection change? (Project name is assigned to one identity in modified duplicates file, but a different identity in the initial duplicates file.)
            //    var nameExistsInRepositoryDuplicateNameSelection = repositoryDuplicateNameSelectionsByName.ContainsKey(newNameSelection.ProjectName);
            //    var nameExistsInDuplicateNameSelection = duplicateNameSelectionsByName.ContainsKey(newNameSelection.ProjectName);
            //    if(nameExistsInRepositoryDuplicateNameSelection || nameExistsInDuplicateNameSelection)
            //    {
            //        var repositoryDuplicateNameSelection = nameExistsInRepositoryDuplicateNameSelection
            //            ? repositoryDuplicateNameSelectionsByName[newNameSelection.ProjectName]
            //            : new ProjectNameSelection() // Just use an empty.
            //            ;

            //        var projectIdentityChanged = newNameSelection.ProjectIdentity != repositoryDuplicateNameSelection.ProjectIdentity;
            //        if(projectIdentityChanged)
            //        {
            //            reason = "Duplicate selection for project name changed.";
            //        }
            //    }

            //    newNameSelectionsWithReasons.Add((newNameSelection, reason));
            //}

            //var departedNameSelectionsWithReasons = new List<(ProjectNameSelection, string)>();
            //foreach (var departedNameSelection in departedNameSelections)
            //{
            //    // In order of least specific to most specific.
            //    var reason = defaultReason;

            //    // Did the project departed? (Project is not present in projects list.)
            //    var projectNameDoesNotExist = !projectsByIdentity.ContainsKey(departedNameSelection.ProjectIdentity);
            //    if (projectNameDoesNotExist)
            //    {
            //        reason = "Project departed.";
            //    }

            //    // Is the project newly unignored? (Project name is present in modified ignored file, but not in initial repository ignored file.)
            //    var nameExistsInIgnoredNames = ignoredNamesHash.Contains(departedNameSelection.ProjectName);
            //    if (nameExistsInIgnoredNames)
            //    {
            //        reason = "Name is ignored.";
            //    }

            //    // Did the choice of duplicate project name selection change? (Project name is assigned to one identity in modified duplicates file, but a different identity in the initial duplicates file.)
            //    var nameExistsInRepositoryDuplicateNameSelection = repositoryDuplicateNameSelectionsByName.ContainsKey(departedNameSelection.ProjectName);
            //    var nameExistsInDuplicateNameSelection = duplicateNameSelectionsByName.ContainsKey(departedNameSelection.ProjectName);
            //    if (nameExistsInRepositoryDuplicateNameSelection || nameExistsInDuplicateNameSelection)
            //    {
            //        var repositoryDuplicateNameSelection = nameExistsInRepositoryDuplicateNameSelection
            //            ? repositoryDuplicateNameSelectionsByName[departedNameSelection.ProjectName]
            //            : new ProjectNameSelection() // Just use an empty.
            //            ;

            //        var projectIdentityChanged = departedNameSelection.ProjectIdentity != repositoryDuplicateNameSelection.ProjectIdentity;
            //        if (projectIdentityChanged)
            //        {
            //            reason = "Duplicate selection for project name changed.";
            //        }
            //    }

            //    departedNameSelectionsWithReasons.Add((departedNameSelection, reason));
            //}

            // Write out the reasons for the selected project changes.
            using (var summaryFile = FileHelper.WriteTextFile(selectedNameChangesSummaryFilePath))
            {
                Instances.Operation.WriteSelectedNameChangesSummaryFile(
                    summaryFile,
                    newNameSelectionsWithReasons,
                    departedNameSelectionsWithReasons,
                    projects);
            }

            var humanActionsRequiredForSelectedNames = O008a_UpdateRepositoryWithSelectedProjects.DetermineRequiredHumanActionsForSelectedNames(
                newNameSelectionsWithReasons,
                departedNameSelectionsWithReasons);

            var anyHumanActionsRequiredForSelectedNames = humanActionsRequiredForSelectedNames.Any();
            if(anyHumanActionsRequiredForSelectedNames)
            {
                Console.WriteLine("Human actions are required before updating the list of selected projects in the project repository.\n");

                // Prompt for required human actions.
                await this.PromptForHumanActionsOnSelectedProjectNames(
                    selectedNameChangesSummaryFilePath,
                    humanActionsRequiredForSelectedNames);

                // Repeatedly prompt for mandatory required human actions until they are complete.
                // Note: while no required human actions are actually mandatory for this process, this code shows the desired methodology as practice.
                while (true)
                {
                    // Recalculate analysis data (same data in this case, no recalculation necessary).

                    // Determine required human actions.
                    humanActionsRequiredForSelectedNames = O008a_UpdateRepositoryWithSelectedProjects.DetermineRequiredHumanActionsForSelectedNames(
                        newNameSelectionsWithReasons,
                        departedNameSelectionsWithReasons);

                    // Only remaining mandatory human actions prevent progress.
                    var anyMandatoryHumanActionsRequired = humanActionsRequiredForSelectedNames.AnyMandatory();
                    if (!anyMandatoryHumanActionsRequired)
                    {
                        break;
                    }

                    // Prompt for mandatory human actions only.
                    humanActionsRequiredForSelectedNames.UnsetNonMandatory();

                    Console.WriteLine("MANDATORY human actions are required before updating the projects repository.\n");

                    await this.PromptForHumanActionsOnSelectedProjectNames(
                        selectedNameChangesSummaryFilePath,
                        humanActionsRequiredForSelectedNames);
                }

                Console.WriteLine("All human actions required before updating the project repository are complete.\n");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("No human actions are required before updating the project repository.\n");
                Console.WriteLine();
            }

            Console.WriteLine("The project repository will now be updated.");
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();

            // Update the project repository.
            // Save temporary data back to the repository, ignored and duplicates.
            await this.ProjectRepository.ClearAllIgnoredProjectNames();
            await this.ProjectRepository.AddIgnoredProjectNames(ignoredNames);

            await this.ProjectRepository.ClearAllDuplicateProjectNameSelections();
            await this.ProjectRepository.AddDuplicateProjectNameSelections(duplicateNameSelections);

            // Just clear and add all selected names.
            await this.ProjectRepository.ClearAllProjectNameSelections();
            await this.ProjectRepository.AddProjectNameSelections(currentNameSelections);
        }

        private async Task PromptForHumanActionsOnSelectedProjectNames(
            string summaryFilePath,
            HumanActionsRequired03 humanActionsRequired)
        {
            await this.NotepadPlusPlusOperator.OpenFilePath(summaryFilePath);

            Console.WriteLine($"Review the summary file (which should be open in Notepad++):\n{summaryFilePath}\n");

            // * New selected project names.
            if (humanActionsRequired.ReviewNewSelectedNames)
            {
                Console.WriteLine($"=> Review the list of new selected project names.\n");
                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("No new selected project names. (ok)\n");
            }
            Console.WriteLine();

            // * Departed selected project names.
            if (humanActionsRequired.ReviewDepartedSelectedNames)
            {
                Console.WriteLine($"=> Review the list of departed selected project names.\n");
                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("No departed selected project names. (ok)\n");
            }
            Console.WriteLine();
        }

        private async Task PromptForHumanActionsOnDuplicateProjectNames(
            string summaryFilePath,
            string ignoredProjectNamesFilePath,
            string duplicateProjectNamesFilePath,
            HumanActionsRequired02 humanActionsRequired)
        {
            await this.NotepadPlusPlusOperator.OpenFilePath(ignoredProjectNamesFilePath);
            await this.NotepadPlusPlusOperator.OpenFilePath(duplicateProjectNamesFilePath);
            await this.NotepadPlusPlusOperator.OpenFilePath(summaryFilePath);

            Console.WriteLine($"Review the summary file (which should be open in Notepad++):\n{summaryFilePath}\n");

            // * New projects.
            if (humanActionsRequired.ReviewDuplicateProjectNames)
            {
                Console.WriteLine($"=> Review the list of duplicate project names.\n\nSpecify which project file path should be assigned to each duplicate project name in the duplicate name selections file, or add names that should be ignored to the ignored project names file.\n\nDuplicates:\n{duplicateProjectNamesFilePath}\nIgnored:\n{ignoredProjectNamesFilePath}\n\n(These files should also be open in Notepad++.)\n");
                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("No duplicate project names to select from or ignore. (ok)\n");
            }
            Console.WriteLine();
        }
    }
}
