using Bank_Application.Interface;
using Bank_Domain.Entities;
using Bank_Infra.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Infra.Repository
{
    public class BankRepository : IBankRepository
    {
        private readonly BankAppDbContext _context;

        //private readonly IServiceProvider _serviceProvider;
        //, IServiceProvider serviceProvider
        public BankRepository(BankAppDbContext bankAppDbContext)
        {
                _context = bankAppDbContext;
           // _serviceProvider = serviceProvider;
        }

      

        public async Task AddBankAsynch(Bank bank, CancellationToken cancellationToken)
        {
            var result = await _context.Banks.AddAsync(bank);
            await SaveChangesAsync();
           // _serviceProvider.GetRequiredKeyedService<IBankRepository>("UIP");

            
        }

       

        public async Task SaveChangesAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
