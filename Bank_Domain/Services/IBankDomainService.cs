using Bank_Domain.Entities;

namespace Bank_Domain.Services
{
    public interface IBankDomainService
    {
        /// <summary>
        /// Creates a bank entity with domain-level validation and rules
        /// </summary>
        Bank ValidateDomainBank(string bankName, string bankType, bool isActive, long Bank_ID=0);
        //Bank UpdateBank(long Bank_ID, string bankName, string bankType, bool isActive);
    }
}