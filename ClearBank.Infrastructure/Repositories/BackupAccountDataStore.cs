using ClearBank.Application.Interfaces;
using ClearBank.Domain.Entities;

namespace ClearBank.Infrastructure.Repositories
{
    public class BackupAccountDataStore : IDataStore
    {
        public Account GetAccount(string accountNumber)
        {
            // Access backup data base to retrieve account, code removed for brevity 
            return new Account();
        }

        public void UpdateAccount(Account account)
        {
            // Update account in backup database, code removed for brevity
        }
    }
}
