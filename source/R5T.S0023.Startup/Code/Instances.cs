using System;

using R5T.T0062;


namespace R5T.S0023.Startup
{
    public static class Instances
    {
        public static IServiceAction ServiceAction { get; } = T0062.ServiceAction.Instance;
    }
}
