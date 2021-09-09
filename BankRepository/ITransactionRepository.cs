using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankRepository
{
    interface ITransactionRepository
    {
        public IEnumerable<ITransaction> getAllTransaction();
        public ITransaction getTransaction(Guid guid);
        public int saveTransaction(ITransaction transaction);
    }
}
