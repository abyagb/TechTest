using ClearBank.Common.Enums;

namespace ClearBank.Application.Interfaces
{
    public interface IDataStoreFactory
    {
        IDataStore GetDataStore();
    }
}
