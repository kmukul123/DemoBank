using Repository;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;
using System.Threading.Tasks;
using DomainModel;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BankRepositoryTests
{
    public class UnitTest1
    {
        private BankDBContext _dbContext;
        private BankRepository _repository;

        public UnitTest1()
        {
            var options = new DbContextOptionsBuilder<BankDBContext>()
                .UseInMemoryDatabase(databaseName: "BankDB")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
            _dbContext = new BankDBContext(options);
            _repository = new BankRepository(_dbContext);
        }

        [Fact]
        public async Task RepositoryCanSave()
        {
            var customer = makeCustomer();
            await _repository.SaveCustomerAsync(customer);

            var transaction = maketestTransaction(customer);
            await _repository.SaveTransactionAsync(transaction);
            var count = await _dbContext.Transactions.CountAsync();
            Assert.Equal(count, 1);
        }

        [Fact]
        public async Task RepositoryCanSaveAndRead()
        {
            var customer = makeCustomer();
            await _repository.SaveCustomerAsync(customer);

            var transaction = maketestTransaction(customer);
            await _repository.SaveTransactionAsync(transaction);

           var count = await _repository.getAllTransactions();
        }

        private Customer makeCustomer()
        {
            return new Customer()
            {
                Id = new Guid("11111111-87bb-475e-a71c-9c57f18cf90c"),
                Name = "Test Customer",
            };
        }

        private Transaction maketestTransaction(ICustomer customer) => new Transaction()
        {
            Amount = 123456.78M,
            Date = DateTime.Parse("2016-08-29T09:12:33.001Z"),
            ExternalId = new Guid("22222222-e514-42cb-b69b-df101ad87908"),
            Description = "Test",
            FromAccount = "123-456",
            ToAccount = "789-123",
            OwnerId = customer.Id,
        };
    }
}
