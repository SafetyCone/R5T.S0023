using System;
using System.Threading.Tasks;

using R5T.D0075;


namespace R5T.S0023
{
    public class NotepadPlusPlusOperator : INotepadPlusPlusOperator
    {
        private ICommandLineOperator CommandLineOperator { get; }
        private INotepadPlusPlusExecutableFilePathProvider NotepadPlusPlusExecutableFilePathProvider { get; }


        public NotepadPlusPlusOperator(
            ICommandLineOperator commandLineOperator,
            INotepadPlusPlusExecutableFilePathProvider notepadPlusPlusExecutableFilePathProvider)
        {
            this.CommandLineOperator = commandLineOperator;
            this.NotepadPlusPlusExecutableFilePathProvider = notepadPlusPlusExecutableFilePathProvider;
        }

        public async Task OpenFilePath(string filePath)
        {
            var notepadPlusPlusExecutableFilePath = await this.NotepadPlusPlusExecutableFilePathProvider.GetNotepadPlusPlusExecutableFilePath();

            var enquotedFilePath = StringHelper.Enquote(filePath);

            await this.CommandLineOperator.Run(notepadPlusPlusExecutableFilePath, enquotedFilePath);
        }
    }
}
