using System;
using System.Threading.Tasks;


namespace R5T.S0023
{
    public interface IServiceCollectionSerializationFileNameProvider
    {
        Task<string> GetServiceCollectionSerializationFileName();
    }
}
