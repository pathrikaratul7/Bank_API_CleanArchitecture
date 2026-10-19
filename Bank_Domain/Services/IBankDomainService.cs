using Bank_Domain.Entities;

namespace Bank_Domain.Services
{
    public interface IBankDomainService
    {
        /// <summary>
        /// Creates a bank entity with domain-level validation and rules
        /// </summary>
        Bank CreateBank(string bankName, string bankType, bool isActive);
    }
}