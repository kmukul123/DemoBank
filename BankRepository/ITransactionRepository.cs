using System;
using System.Collections.Generic;
using System.Text;

namespace BankRepository
{
    interface ITransactionRepository
    {
        public getAllTransaction();
        public getTransaction(Guid guid);
        public saveTransaction(guid);
    }
}
