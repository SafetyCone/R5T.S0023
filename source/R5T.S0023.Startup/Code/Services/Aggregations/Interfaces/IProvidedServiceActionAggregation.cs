using System;

using R5T.T0063;


namespace R5T.S0023.Startup
{
    /// <summary>
    /// Service action aggregation for services provided by <see cref="HostStartupBase"/>.
    /// </summary>
    [ServiceActionAggregationMarker]
    public interface IProvidedServiceActionAggregation : IServiceActionAggregation,
        A0001.IHostServiceActionAggregration
    {
    }
}
