using Bank_Application.Dtos;
using Bank_Application.Interface;
using Bank_Application.Services;
using Bank_Application.Validation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Bank_Application
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {

            services.AddScoped<IBankService, BankService>();
            services.AddScoped<IValidator<CreateBankDto>, BankValidator>();
            services.AddValidatorsFromAssemblyContaining<BankValidator>();


            return services;


        }
    }
}
