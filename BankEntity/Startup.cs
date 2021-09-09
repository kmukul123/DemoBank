using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using System;

namespace BankEntity
{
    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Environment.GetEnvironmentVariable("BankDBConnectionString");
            services.AddDbContext<BankDBContext>(
                options => options.UseSqlServer(connectionString));
        }
    }
}
