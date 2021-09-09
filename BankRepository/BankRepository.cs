using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankRepository
{
    /// <summary>
    /// TODO: could use a generic repository so we dont have a seperate repository for each of the entities
    /// </summary>
    public class BankRepository : ICustomerRepository, ITransactionRepository
    {
        public BankRepository(BankDBContext bankDBContext)
        {

        }
        public IEnumerable<ITransaction> getAllTransactions()
        {
            throw new NotImplementedException();
        }

        public ITransaction getTransaction(Guid guid)
        {
            throw new NotImplementedException();
        }

        public int saveTransaction(ITransaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
