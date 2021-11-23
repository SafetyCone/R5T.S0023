using System;

using R5T.T0063;


namespace R5T.S0023.Startup
{
    /// <summary>
    /// Service action aggregation for service dependencies of <see cref="HostStartupBase"/>.
    /// Note: Service dependencies are services used by <see cref="HostStartupBase"/> to choose how to configure configuration or configure services.
    /// They must be present in the same service collection used to provide the startup class itself.
    /// They are usually properties of the class.
    /// </summary>
    [ServiceActionAggregationMarker]
    public interface IDependencyServiceActionAggregation : IServiceActionAggregation
    {
    }
}
