using System;
using System.Linq;
using System.IO;
using System.Threading.Tasks;


using R5T.D0084.D001;
using R5T.D0101;
using R5T.D0105;
using R5T.D0110;
using R5T.T0020;
using R5T.T0097;


namespace R5T.S0023
{
    /// <summary>
    /// Updates the project repository with ALL current (i.e. in the local filesystem) project paths.
    /// Shows new and departed projects.
    /// </summary>
    [OperationMarker]
    public class O007a_UpdateRepositoryWithAllProjects : IActionOperation
    {
        #region Static

        private static HumanActionsRequired01 DetermineRequiredHumanActions(
            Project[] newProjects,
            Project[] departedProjects)
        {
            var anyNewProjects = newProjects.Any();
            var anyDepartedProjects = departedProjects.Any();

            var humanActionsRequired = new HumanActionsRequired01
            {
                ReviewDepartedProjects = anyDepartedProjects,
                ReviewNewProjects = anyNewProjects,
            };

            return humanActionsRequired;
        }

        #endregion


        private IAllProjectFilePathsProvider AllProjectFilePathsProvider { get; }
        private INotepadPlusPlusOperator NotepadPlusPlusOperator { get; }
        private ISummaryFilePathProvider SummaryFilePathProvider { get; }
        private IProjectRepository ProjectRepository { get; }


        public O007a_UpdateRepositoryWithAllProjects(
            IAllProjectFilePathsProvider allProjectFilePathsProvider,
            INotepadPlusPlusOperator notepadPlusPlusOperator,
            ISummaryFilePathProvider summaryFilePathProvider,
            IProjectRepository projectRepository)
        {
            this.AllProjectFilePathsProvider = allProjectFilePathsProvider;
            this.NotepadPlusPlusOperator = notepadPlusPlusOperator;
            this.SummaryFilePathProvider = summaryFilePathProvider;
            this.ProjectRepository = projectRepository;
        }

        public async Task Run()
        {
            // Repository Backup should be run by calling operation.

            // Get all current projects (in the file system).
            var currentProjects = await Instances.Operation.GetCurrentProjects(
                this.AllProjectFilePathsProvider);

            // Get all repository projects (previously existing projects).
            var repositoryProjects = await this.ProjectRepository.GetAllProjects();

            // Determine new and departed projects.
            var (newProjects, departedProjects) = Instances.Operation.GetProjectChanges(
                currentProjects,
                repositoryProjects);

            // Write summary to file.
            var summaryFilePath = await this.SummaryFilePathProvider.GetSummaryFilePath();

            // Use a scope so that file is flushed by the time it's needed.
            using (var summaryFile = FileHelper.WriteTextFile(summaryFilePath))
            {
                Instances.Operation.WriteNewAndDepartedSummaryFile(
                    summaryFile,
                    newProjects,
                    departedProjects);
            }

            // Now prompt for required human actions.
            // Determine required human actions.
            var humanActionsRequired = O007a_UpdateRepositoryWithAllProjects.DetermineRequiredHumanActions(
                newProjects,
                departedProjects);

            var anyHumanActionsRequired = humanActionsRequired.Any();
            if (anyHumanActionsRequired)
            {
                Console.WriteLine("Human actions are required before updating the list of all projects in the project repository.\n");

                // Prompt for required human actions.
                await this.PromptForHumanActions(humanActionsRequired);

                // Repeatedly prompt for mandatory required human actions until they are complete.
                // Note: while no required human actions are actually mandatory for this process, this code shows the desired methodology as practice.
                while (true)
                {
                    // Recalculate analysis data (same data in this case, no recalculation necessary).

                    // Determine required human actions.
                    humanActionsRequired = O007a_UpdateRepositoryWithAllProjects.DetermineRequiredHumanActions(
                        newProjects,
                        departedProjects);

                    // Only remaining mandatory human actions prevent progress.
                    var anyMandatoryHumanActionsRequired = humanActionsRequired.AnyMandatory();
                    if (!anyMandatoryHumanActionsRequired)
                    {
                        break;
                    }

                    // Prompt for mandatory human actions only.
                    humanActionsRequired.UnsetNonMandatory();

                    Console.WriteLine("MANDATORY human actions are required before updating the project repository.\n");

                    await this.PromptForHumanActions(humanActionsRequired);
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
            // Modify the project repository to match the current set of projects.
            // Deletions first.
            await this.ProjectRepository.DeleteProjects(departedProjects);

            await this.ProjectRepository.AddProjects(newProjects);
        }

        private async Task PromptForHumanActions(HumanActionsRequired01 humanActionsRequired)
        {
            var summaryFilePath = await this.SummaryFilePathProvider.GetSummaryFilePath();

            await this.NotepadPlusPlusOperator.OpenFilePath(summaryFilePath);

            Console.WriteLine($"Review the summary file (which should be open in Notepad++):\n{summaryFilePath}\n");

            // * New projects.
            if (humanActionsRequired.ReviewNewProjects)
            {
                Console.WriteLine("=> Review the list of new projects.\n");
                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("No new projects to review or ignore. (ok)\n");
            }
            Console.WriteLine();

            // * Review list of departed projects.
            if (humanActionsRequired.ReviewDepartedProjects)
            {
                Console.WriteLine("=> Review the list of departed projects.\n");
                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("No departed projects to review. (ok)\n");
            }
            Console.WriteLine();
        }
    }
}
