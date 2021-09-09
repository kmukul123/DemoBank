using DomainModel;
using DomainModel.Validators;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using System;

namespace TransactionService
{
    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            BankRepository.Startup.ConfigureServices(services);
            services.AddScoped<IValidator<ICustomer>, CustomerValidator>();
            services.AddScoped<IValidator<ITransaction>, TransactionValidator>();
            services.AddLogging();
        }
    }
}
