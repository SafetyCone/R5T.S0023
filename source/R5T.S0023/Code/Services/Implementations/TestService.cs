using System;

using Microsoft.Extensions.Logging;

using R5T.L0017.X001;
using R5T.T0064;

using Instances = R5T.S0023.Instances;


namespace R5T.Yabbo
{
    [ServiceImplementationMarker]
    public class TestService : INoServiceDefinition, IServiceImplementation
    {
        private ILogger Logger { get; }


        public TestService(
            ILogger<TestService> logger)
        {
            this.Logger = logger;
        }

        public void Run()
        {
            this.Logger.TestLogLevelOutput();

            using var exclusiveUsageContext = Instances.Console.GetExclusiveUsageContext();

            this.Logger.TestLogLevelEnabled(Console.Out);
        }
    }
}
