using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    interface ITransactionRepository
    {
        public IEnumerable<ITransaction> getAllTransactions();
        public ITransaction getTransaction(Guid guid);
        public Task<int> SaveTransactionAsync(ITransaction transaction);
    }
}
