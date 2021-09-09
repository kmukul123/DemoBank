using Microsoft.Extensions.DependencyInjection;
using Repository;

namespace BankRepository
{
    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            BankEntity.Startup.ConfigureServices(services);
            services.AddScoped<ICustomerRepository, Repository.BankRepository>();
            services.AddScoped<ITransactionRepository, Repository.BankRepository>();
            services.AddLogging();
        }
    }
}
