using DomainModel;
using DomainModel.Validators;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Service;
using System;

namespace Service
{
    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            BankRepository.Startup.ConfigureServices(services);
            services.AddSingleton<IValidator<ICustomer>, CustomerValidator>();
            services.AddSingleton<IValidator<ITransaction>, TransactionValidator>();
            services.AddLogging();
            
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddLogging();
        }
    }
}
