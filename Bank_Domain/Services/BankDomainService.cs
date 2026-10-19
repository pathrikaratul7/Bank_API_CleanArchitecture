using Bank_Domain.Entities;

namespace Bank_Domain.Services
{
    public class BankDomainService : IBankDomainService
    {
        public Bank CreateBank(string bankName, string bankType, bool isActive)
        {
            if (string.IsNullOrWhiteSpace(bankName))
                throw new ArgumentException("Bank name is required.", nameof(bankName));

            if (string.IsNullOrWhiteSpace(bankType))
                throw new ArgumentException("Bank type is required.", nameof(bankType));

            return new Bank(bankName, bankType, isActive);
        }
    }
}