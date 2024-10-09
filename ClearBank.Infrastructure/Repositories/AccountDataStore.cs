using ClearBank.Application.Interfaces;
using ClearBank.Domain.Entities;

namespace ClearBank.Infrastructure.Repositories
{
    public class AccountDataStore : IDataStore
    {
        public Account GetAccount(string accountNumber)
        {
            // Access database to retrieve account, code removed for brevity 
            return new Account();
        }

        public void UpdateAccount(Account account)
        {
            // Update account in database, code removed for brevity
        }
    }
}
