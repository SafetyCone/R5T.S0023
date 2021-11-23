using System;

using R5T.T0063;


namespace R5T.S0023.Startup
{
    /// <summary>
    /// Service action aggregation for services required by <see cref="HostStartupBase"/>.
    /// Note: required services are *not* service dependencies. They are services that must be chosen by a derived class in the <see cref="HostStartupBase.FillRequiredServiceActions"/> method, not chosen while configuring the service collection used to provide the startup class itself.
    /// </summary>
    [ServiceActionAggregationMarker]
    public interface IRequiredServiceActionAggregation : IServiceActionAggregation
    {
    }
}
