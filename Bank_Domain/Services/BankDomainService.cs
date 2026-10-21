using Bank_Domain.Entities;

namespace Bank_Domain.Services
{
    public class BankDomainService : IBankDomainService
    {
        public Bank ValidateDomainBank(string bankName, string bankType, bool isActive, long Bank_ID = 0)
        {
            if (string.IsNullOrWhiteSpace(bankName))
                throw new ArgumentException("Bank name is required.", nameof(bankName));

            if (string.IsNullOrWhiteSpace(bankType))
                throw new ArgumentException("Bank type is required.", nameof(bankType));

            return new Bank(bankName, bankType, isActive,Bank_ID);
        }
        //public Bank UpdateBank(long Bank_ID,string bankName, string bankType, bool isActive)
        //{
        //    if (string.IsNullOrWhiteSpace(Bank_ID.ToString()))
        //        throw new ArgumentException("Bank name is required.", nameof(Bank_ID));

        //    if (string.IsNullOrWhiteSpace(bankName))
        //        throw new ArgumentException("Bank name is required.", nameof(bankName));

        //    if (string.IsNullOrWhiteSpace(bankType))
        //        throw new ArgumentException("Bank type is required.", nameof(bankType));

        //    return new Bank(bankName, bankType, isActive);
        //}
    }
}