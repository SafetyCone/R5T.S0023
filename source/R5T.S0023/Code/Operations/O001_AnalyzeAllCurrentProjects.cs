using System;
using System.Linq;
using System.Threading.Tasks;

using R5T.Magyar;
using R5T.Magyar.IO;

using R5T.D0048;
using R5T.D0084.D001;
using R5T.D0101;
using R5T.D0101.I001;
using R5T.D0105;


namespace R5T.S0023
{
    /// <summary>
    /// Determine all current project file paths, then provide a summary file of any changes:
    /// * New projects.
    /// * Departed projects.
    /// * New duplicate project names.
    /// This action does *not* modify the repository!
    /// </summary>
    public class O001_AnalyzeAllCurrentProjects : T0020.IOperation
    {
        private IAllProjectFilePathsProvider AllProjectFilePathsProvider { get; }
        private INotepadPlusPlusOperator NotepadPlusPlusOperator { get; }
        private ISummaryFilePathProvider SummaryFilePathProvider { get; }
        private IProjectRepository ProjectRepository { get; }


        public O001_AnalyzeAllCurrentProjects(
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
            // Get all repository projects.
            var repositoryProjects = await this.ProjectRepository.GetAllProjects();

            var (currentProjects, newProjects, departedProjects) = await Instances.Operation.GetProjectChanges(
                this.AllProjectFilePathsProvider,
                repositoryProjects);

            // Determine any new duplicate project names.
            var repositoryDuplicateProjectNames = await this.ProjectRepository.GetDuplicateProjectNames();
            var repositoryIgnoredProjectNames = await this.ProjectRepository.GetAllIgnoredProjectNames();

            var newDuplicateProjectNames = Instances.Operation.GetNewDuplicateProjectNames(
                currentProjects,
                repositoryDuplicateProjectNames,
                repositoryIgnoredProjectNames);

            // Provide a summary of changes.
            var summaryFilePath = await this.SummaryFilePathProvider.GetSummaryFilePath();

            using (var textFile = FileHelper.WriteTextFile(summaryFilePath))
            {
                textFile.WriteLine("New projects:");
                textFile.WriteLine();

                if(newProjects.None())
                {
                    textFile.WriteLine("<none>");
                }
                else
                {
                    foreach (var project in newProjects)
                    {
                        textFile.WriteLine(project.Name);
                    }
                }

                textFile.WriteLine();
                textFile.WriteLine("Departed projects:");
                textFile.WriteLine();

                if(departedProjects.None())
                {
                    textFile.WriteLine("<none>");
                }
                else
                {
                    foreach (var project in departedProjects)
                    {
                        textFile.WriteLine(project.Name);
                    }
                }

                textFile.WriteLine();
                textFile.WriteLine("New duplicate project names:");
                textFile.WriteLine();

                if(newDuplicateProjectNames.None())
                {
                    textFile.WriteLine("<none>");
                }
                else
                {
                    foreach (var projectName in newDuplicateProjectNames)
                    {
                        textFile.WriteLine(projectName);
                    }
                }
            }

            // Show the summary in Notepad++ to be immediately helpful.
            await this.NotepadPlusPlusOperator.OpenFilePath(summaryFilePath);

            // Make no modifications to the project repository.
        }
    }
}
