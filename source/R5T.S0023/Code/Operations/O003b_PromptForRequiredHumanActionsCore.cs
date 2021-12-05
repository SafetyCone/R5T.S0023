using System;
using System.Threading.Tasks;

using R5T.D0101.I001;
using R5T.D0105;


namespace R5T.S0023
{
    /// <summary>
    /// After determining whether human actions are required, prompt the human through all required actions.
    /// </summary>
    public class O003b_PromptForRequiredHumanActionsCore
    {
        private INotepadPlusPlusOperator NotepadPlusPlusOperator { get; }
        private IProjectRepositoryFilePathsProvider ProjectRepositoryFilePathsProvider { get; }
        private ISummaryFilePathProvider SummaryFilePathProvider { get; }


        public O003b_PromptForRequiredHumanActionsCore(
            INotepadPlusPlusOperator notepadPlusPlusOperator,
            IProjectRepositoryFilePathsProvider projectRepositoryFilePathsProvider,
            ISummaryFilePathProvider summaryFilePathProvider)
        {
            this.NotepadPlusPlusOperator = notepadPlusPlusOperator;
            this.ProjectRepositoryFilePathsProvider = projectRepositoryFilePathsProvider;
            this.SummaryFilePathProvider = summaryFilePathProvider;
        }

        public async Task Run(HumanActionsRequired humanActionsRequired)
        {
            var (summaryFilePath, ignoredProjectNamesTextFilePath, duplicateProjectNamesTextFilePath) = await TaskHelper.WhenAll(
                this.SummaryFilePathProvider.GetSummaryFilePath(),
                this.ProjectRepositoryFilePathsProvider.GetIgnoredProjectNamesTextFilePath(),
                this.ProjectRepositoryFilePathsProvider.GetDuplicateProjectNamesTextFilePath());

            await this.NotepadPlusPlusOperator.OpenFilePath(summaryFilePath);

            Console.WriteLine($"Review the summary file (which should be open in Notepad++):\n{summaryFilePath}\n");

            // * Review list of new projects.
            if (humanActionsRequired.ReviewNewProjects)
            {
                Console.WriteLine("*) Review the list of new projects.\n");
                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();

                // * For any new projects that should be ignored, add to the ignored projects file: <ignored projects file path>.
                await this.NotepadPlusPlusOperator.OpenFilePath(ignoredProjectNamesTextFilePath);

                Console.WriteLine($"*) Add any project names that should be ignored to the ignored project names file:\n{ignoredProjectNamesTextFilePath}\n");
                Console.WriteLine("Press enter when finished...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("*) No new projects to review or ignore.\n");
            }

            // * Review list of departed projects.
            if (humanActionsRequired.ReviewDepartedProjects)
            {
                Console.WriteLine("*) Review the list of departed projects.\nNote: departed project names will be removed from the lists of ignored project names and selected project names.");
                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("*) No departed projects to review.\n");
            }

            // * Choose among duplicates in list of new duplicate projects by modifying the duplicate selections file: <duplicate selections file path>.
            if (humanActionsRequired.ReviewNewDuplicateProjectNames)
            {
                await this.NotepadPlusPlusOperator.OpenFilePath(duplicateProjectNamesTextFilePath);

                Console.WriteLine($"*) Review the list of new duplicate project names. Select one of the projects from the list of duplicates for each name and add to the duplicate project names file:\n{duplicateProjectNamesTextFilePath}\n");
                Console.WriteLine("Press enter when finished...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("*) No new duplicate project names to choose among.");
            }
        }
    }
}
