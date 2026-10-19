using Bank_Application.Interface;
using Bank_Domain.Entities;
using Bank_Infra.Data;
using System.Threading.Tasks;

namespace Bank_Infra.Repository
{
    public class BankRepository : IBankRepository
    {
        private readonly BankAppDbContext _context;

        public BankRepository(BankAppDbContext bankAppDbContext)
        {
            _context = bankAppDbContext;
        }

        public async Task AddBankAsynch(Bank bank, CancellationToken cancellationToken)
        {
            await _context.Banks.AddAsync(bank, cancellationToken);
            await SaveChangesAsync(cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
