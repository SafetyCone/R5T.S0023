using System;

using Microsoft.Extensions.DependencyInjection;


namespace R5T.S0023
{
    public class ServiceCollectionForensicInvestigation : IServiceCollectionForensicInvestigation
    {
        public IServiceCollection Services { get; }


        public ServiceCollectionForensicInvestigation(
            IServiceCollection services)
        {
            this.Services = services;
        }
    }
}
