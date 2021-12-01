using System;
using System.Threading.Tasks;

using R5T.D0099.D003;


namespace R5T.S0023
{
    public class MachineOutputFilePathProvider : IMachineOutputFilePathProvider
    {
        private IMachineOutputFileNameProvider MachineOutputFileNameProvider { get; }
        private IOutputFilePathProvider OutputFilePathProvider { get; }


        public MachineOutputFilePathProvider(
            IMachineOutputFileNameProvider machineOutputFileNameProvider,
            IOutputFilePathProvider outputFilePathProvider)
        {
            this.MachineOutputFileNameProvider = machineOutputFileNameProvider;
            this.OutputFilePathProvider = outputFilePathProvider;
        }

        public async Task<string> GetMachineOutputFilePath()
        {
            var machineOutputFileName = await this.MachineOutputFileNameProvider.GetMachineOutputFileName();

            var output = await this.OutputFilePathProvider.GetOutputFilePath(machineOutputFileName);
            return output;
        }
    }
}
