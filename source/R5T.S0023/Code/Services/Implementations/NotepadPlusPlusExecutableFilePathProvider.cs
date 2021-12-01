using System;
using System.Threading.Tasks;


namespace R5T.S0023
{
    public class HardCodedNotepadPlusPlusExecutableFilePathProvider : INotepadPlusPlusExecutableFilePathProvider
    {
        public const string NotepadPlusPlusExecutableFilePath = @"C:\Program Files (x86)\Notepad++\notepad++.exe";


        public Task<string> GetNotepadPlusPlusExecutableFilePath()
        {
            var output = StringHelper.Enquote(HardCodedNotepadPlusPlusExecutableFilePathProvider.NotepadPlusPlusExecutableFilePath);

            return Task.FromResult(output);
        }
    }
}
