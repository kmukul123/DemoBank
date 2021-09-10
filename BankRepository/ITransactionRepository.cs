using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface ITransactionRepository
    {
         Task<IEnumerable<ITransaction>> GetAllTransactionsAsync();
         Task<int> AddTransactionAndCustomerAsync(ITransaction transaction);
         Task<int> UpdateTransactionAndCustomerAsync(ITransaction transaction);
    }
}
