using Microsoft.Extensions.DependencyInjection;

namespace BankRepository
{
    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            BankEntity.Startup.ConfigureServices(services);
            services.AddScoped<Repository.ICustomerRepository, Repository.BankRepository>();
        }
    }
}
