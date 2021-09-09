using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Repository
{
    public static class Startup
    {
        public static void ConfigureRepositoryServices(IServiceCollection services)
        {
            var connectionString = Environment.GetEnvironmentVariable("BankDBConnectionString");
            services.AddDbContext<BankDBContext>(
                options => options.UseSqlServer(connectionString));
        }
    }
}
