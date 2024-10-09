using ClearBank.Application.Config;
using ClearBank.Application.Interfaces;
using ClearBank.Infrastructure.Repositories;
using Microsoft.Extensions.Options;

namespace ClearBank.Infrastructure.Factories
{
    public class DataStoreFactory : IDataStoreFactory
    {
        private readonly IOptions<DataStoreSettings> _dataStoreConfig;

        public DataStoreFactory(IOptions<DataStoreSettings> dataStoreConfig)
        {
            _dataStoreConfig = dataStoreConfig;
        }

        public IDataStore GetDataStore()
        {
           if(_dataStoreConfig.Value.UseBackupDataStore)
            {
                return new BackupAccountDataStore();
            }
            else
            {
                return new AccountDataStore();
            }
        }
    }

}
