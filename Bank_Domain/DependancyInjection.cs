using Bank_Domain.Entities;
using Bank_Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Domain
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddDomainDI(this IServiceCollection services)
        {
            services.AddScoped<IBankDomainService, BankDomainService>();
            
            return services;
        }
    }
}
