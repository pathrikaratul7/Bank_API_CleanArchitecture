using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank_Domain.Entities;

namespace Bank_Application.Interface
{
    public interface IBankRepository
    {
        Task AddBankAsynch(Bank bank, CancellationToken cancellationToken);
        Task SaveChangesAsync();
    }
}
