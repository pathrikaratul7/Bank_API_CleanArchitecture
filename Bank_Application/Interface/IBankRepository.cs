using Bank_Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Bank_Application.Interface
{
    public interface IBankRepository
    {
        Task AddBankAsynch(Bank bank, CancellationToken cancellationToken);
        Task UpdateBankAsynch(Bank bank, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
