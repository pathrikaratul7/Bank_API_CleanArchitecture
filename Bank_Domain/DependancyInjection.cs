using Bank_Domain.Entities;
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
            
            return services;
        }
    }
}
