using System;
using System.Threading.Tasks;

using R5T.T0020;


namespace R5T.S0023
{
    [OperationMarker]
    public class O000_Main : IActionOperation
    {
        private O101_UpdateRepository O101_UpdateRepository { get; }


        public O000_Main(
            O101_UpdateRepository o101_UpdateRepository)
        {
            this.O101_UpdateRepository = o101_UpdateRepository;
        }

        public async Task Run()
        {
            await this.O101_UpdateRepository.Run();
        }
    }
}
