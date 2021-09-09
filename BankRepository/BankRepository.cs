using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    /// <summary>
    /// TODO: could use a generic repository so we dont have a seperate repository for each of the entities
    /// </summary>
    public class BankRepository : ICustomerRepository, ITransactionRepository
    {

        public BankRepository(BankDBContext bankDBContext)
        {
            BankDBContext = bankDBContext;
        }

        public BankDBContext BankDBContext { get; }

        public IEnumerable<ITransaction> getAllTransactions()
        {
            throw new NotImplementedException();
        }

        public ITransaction getTransaction(Guid guid)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// assumes customer is already there
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public async Task<int> SaveTransactionAsync(ITransaction transaction)
        {
            BankDBContext.Transactions.Add(new Transaction(transaction));
            using var dbtransaction = BankDBContext.Database.BeginTransaction();
            try
            {
                var ret = await BankDBContext.SaveChangesAsync();
                dbtransaction.Commit();
                return ret;
            } catch(Exception)
            {
                dbtransaction.Rollback();
                throw;
            }
        }

        /// <summary>
        /// TODO : can add database transactions for it
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public async Task<int> SaveCustomerAsync(ICustomer c)
        {
            BankDBContext.Customers.Add(new Customer(c));
            return await BankDBContext.SaveChangesAsync();
        }
    }
}
