using Bank_Infra.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using Bank_Application.Interface;
using Bank_Infra.Repository;
using Bank_Domain.Services;


namespace Bank_Infra
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddInfraDI(this IServiceCollection services, IConfiguration iconfiguration)
        {
            var SqlCon = iconfiguration.GetConnectionString("Con");
            
            services.AddDbContext<BankAppDbContext>(option => option.UseSqlServer(SqlCon));
            services.AddScoped<IBankRepository, BankRepository>();
            
            return services;
        }
    }
}
