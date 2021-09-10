using DomainModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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
            var result = await BankDBContext.Transactions.AsNoTracking().ToListAsync();
            
            return result;
        }

        public async Task<Transaction> GetTransactionAsync(Guid guid)
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

        public async Task<int> AddTransactionAndCustomerAsync(ITransaction transaction)
        {
            BankDBContext.Transactions.Add(new Transaction(transaction));



            var customerIspresent = await BankDBContext.Customers.AnyAsync(x => x.Id == transaction.OwnerId);
            //beahviour could be modifed if customer is not already present
            if (!customerIspresent)
                BankDBContext.Customers.Add(new Customer(transaction.Owner));

            return await TryCommitChanges();


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

        public async Task<int> UpdateTransactionAndCustomerAsync(ITransaction transaction)
        {
            var existingTransaction = await this.GetTransactionAsync(transaction.ExternalId);
            if (existingTransaction == null)
                // TOOD review this from security perspective
                return -1;
            UpdateexistingTransactionValues(existingTransaction, transaction);

            using var dbtransaction = BankDBContext.Database.BeginTransaction();
            return await TryCommitChanges();
        }

        private void UpdateexistingTransactionValues(Transaction existingTransaction, ITransaction transaction)
        {
            existingTransaction.Amount = transaction.Amount;
            existingTransaction.Date = transaction.Date;
            existingTransaction.Description = transaction.Description;
            existingTransaction.ExternalId = transaction.ExternalId;
            existingTransaction.FromAccount = transaction.FromAccount;
            existingTransaction.Owner = transaction.Owner;
            existingTransaction.OwnerId = transaction.OwnerId;
            existingTransaction.Owner = transaction.Owner;
        }

        private async Task<int> TryCommitChanges()
        {
            using var dbtransaction = BankDBContext.Database.BeginTransaction();
            try
            {

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

    }
}
