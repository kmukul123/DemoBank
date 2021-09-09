using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface ITransactionRepository
    {
        public Task<IEnumerable<ITransaction>> GetAllTransactionsAsync();
        public Task<ITransaction> GetTransactionAsync(Guid guid);
        public Task<int> SaveTransactionAsync(ITransaction transaction);
    }
}
