using Microsoft.Extensions.DependencyInjection;
using Bank_Infra;
using Bank_Application;
using Bank_Domain;

namespace Bank_API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentDI(this IServiceCollection services, IConfiguration config)
        {

            services.AddInfraDI(config).
                AddDomainDI().
                AddApplicationDI();


            return services;

        }

    }
}
