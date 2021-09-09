using Repository;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;
using System.Threading.Tasks;
using DomainModel;

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
        }

        private Customer makeCustomer()
        {
            return new Customer()
            {
                Id = new Guid("6408d459-87bb-475e-a71c-9c57f18cf90c"),
                Name = "Test Customer",
            };
        }

        private Transaction maketestTransaction(ICustomer customer) => new Transaction()
        {
            Amount = 123456.78M,
            Date = DateTime.Parse("2016-08-29T09:12:33.001Z"),
            ExternalId = new Guid("e35f8169-e514-42cb-b69b-df101ad87908"),
            Description = "Test",
            FromAccount = "123-456",
            ToAccount = "789-123",
            OwnerId = customer.Id,
        };
    }
}
