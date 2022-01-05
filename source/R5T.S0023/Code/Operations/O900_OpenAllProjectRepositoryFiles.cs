using System;
using System.Linq;
using System.Threading.Tasks;

using R5T.D0101.I001;
using R5T.D0105;


namespace R5T.S0023
{
    public class O900_OpenAllProjectRepositoryFiles : T0020.IActionOperation
    {
        private INotepadPlusPlusOperator NotepadPlusPlusOperator { get; }
        private IProjectRepositoryFilePathsProvider ProjectRepositoryFilePathsProvider { get; }

        
        public O900_OpenAllProjectRepositoryFiles(
            INotepadPlusPlusOperator notepadPlusPlusOperator,
            IProjectRepositoryFilePathsProvider projectRepositoryFilePathsProvider)
        {
            this.NotepadPlusPlusOperator = notepadPlusPlusOperator;
            this.ProjectRepositoryFilePathsProvider = projectRepositoryFilePathsProvider;
        }

        public async Task Run()
        {
            var (Task1Result, Task2Result, Task3Result, Task4Result) = await TaskHelper.WhenAll(
                this.ProjectRepositoryFilePathsProvider.GetDuplicateProjectNamesTextFilePath(),
                this.ProjectRepositoryFilePathsProvider.GetIgnoredProjectNamesTextFilePath(),
                this.ProjectRepositoryFilePathsProvider.GetProjectNameSelectionsTextFilePath(),
                this.ProjectRepositoryFilePathsProvider.GetProjectsListingJsonFilePath());

            var openingAllFilePaths = new[]
            {
                Task1Result,
                Task2Result,
                Task3Result,
                Task4Result
            }
            .Select(filePath => this.NotepadPlusPlusOperator.OpenFilePath(filePath))
            ;

            await Task.WhenAll(openingAllFilePaths);
        }
    }
}
