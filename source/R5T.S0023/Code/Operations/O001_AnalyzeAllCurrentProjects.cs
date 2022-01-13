using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using R5T.Magyar;
using R5T.Magyar.IO;

using R5T.D0048;
using R5T.D0084.D001;
using R5T.D0101;
using R5T.D0101.I001;
using R5T.D0105;
using R5T.D0110;


namespace R5T.S0023
{
    /// <summary>
    /// Determine all current project file paths, then provide a summary file of any changes:
    /// * New projects.
    /// * Departed projects.
    /// * New duplicate project names.
    /// This action does *not* modify the repository!
    /// </summary>
    public class O001_AnalyzeAllCurrentProjects : T0020.IActionOperation
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

            // Newly ignored projects.
            // Selected project names that will be ignored.
            // Load selected projects.
            var selectedProjectNames = await this.ProjectRepository.GetAllProjectNameSelections();

            var newIgnoredProjectNames = Instances.Operation.GetNewIgnoredProjectNames(
                selectedProjectNames,
                repositoryIgnoredProjectNames);

            var currentProjectGroupsByName = currentProjects.ToDictionaryOfArraysByName();

            // Provide a summary of changes.
            var summaryFilePath = await this.SummaryFilePathProvider.GetSummaryFilePath();

            using (var textFile = FileHelper.WriteTextFile(summaryFilePath))
            {
                Instances.Operation.WriteNewAndDepartedSummaryFile(
                    textFile,
                    newProjects,
                    departedProjects);

                var newDuplicateProjectNamesCount = newDuplicateProjectNames.Length;

                textFile.WriteLine();
                textFile.WriteLine($"New duplicate project names ({newDuplicateProjectNamesCount}):");
                textFile.WriteLine();

                if(newDuplicateProjectNames.None())
                {
                    textFile.WriteLine("<none>");
                }
                else
                {
                    foreach (var projectName in newDuplicateProjectNames)
                    {
                        textFile.WriteLine($"{projectName}:");

                        // Write out paths of duplicates.
                        var projects = currentProjectGroupsByName[projectName];

                        foreach (var project in projects)
                        {
                            textFile.WriteLine($"{project.FilePath}");
                        }

                        textFile.WriteLine();
                    }
                }

                var newIgnoredProjectNamesCount = newIgnoredProjectNames.Length;

                textFile.WriteLine();
                textFile.WriteLine($"New ignored project names ({newIgnoredProjectNamesCount}):");
                textFile.WriteLine();

                if (newIgnoredProjectNames.None())
                {
                    textFile.WriteLine("<none>");
                }
                else
                {
                    foreach (var projectName in newIgnoredProjectNames)
                    {
                        textFile.WriteLine($"{projectName}:");

                        // Write out paths of newly ignored.
                        var projects = currentProjectGroupsByName[projectName];

                        foreach (var project in projects)
                        {
                            textFile.WriteLine($"{project.FilePath}");
                        }

                        textFile.WriteLine();
                    }
                }
            }

            // Show the summary in Notepad++ to be immediately helpful.
            await this.NotepadPlusPlusOperator.OpenFilePath(summaryFilePath);

            // Make no modifications to the project repository.
        }
    }
}
