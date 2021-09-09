using DomainModel;
using Microsoft.EntityFrameworkCore;
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

        /// <summary>
        /// todo ideally should return all as it could cause many issues
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ITransaction>> GetAllTransactionsAsync()
        {
            return await  BankDBContext.Transactions.ToListAsync();
        }

        public async Task<ITransaction> GetTransactionAsync(Guid guid)
        {
            return await BankDBContext.Transactions.SingleOrDefaultAsync(x => x.ExternalId == guid);
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

        public async Task<int> SaveTransactionAndCustomerAsync(ITransaction transaction)
        {
            BankDBContext.Transactions.Add(new Transaction(transaction));
            using var dbtransaction = BankDBContext.Database.BeginTransaction();
            try
            {
                var customerIspresent = await BankDBContext.Customers.AnyAsync(x => x.Id == transaction.OwnerId);
                if (!customerIspresent)
                    BankDBContext.Customers.Add(new Customer(transaction.Owner));

                var ret = await BankDBContext.SaveChangesAsync();
                dbtransaction.Commit();
                return ret;
            }
            catch (Exception)
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
