using ClearBank.Domain.Entities;

namespace ClearBank.Application.Interfaces
{
    public interface IDataStore
    {
        Account GetAccount(string accountNumber);
        void UpdateAccount(Account account);
    }
}
