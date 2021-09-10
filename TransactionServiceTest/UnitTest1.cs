using Microsoft.Extensions.DependencyInjection;
using Service;
using System;
using Xunit;

namespace TransactionServiceTest
{
    public class UnitTest1
    {
        [Fact]
        public void DIIsworking()
        {
            var services = new ServiceCollection();
            Environment.SetEnvironmentVariable("BankDBConnectionString", "Test");
            Service.Startup.ConfigureServices(services);

            var provider = services.BuildServiceProvider();
            Assert.NotNull(provider.GetService<ITransactionService>());

        }
    }
}
