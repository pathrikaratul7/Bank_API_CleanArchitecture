using Bank_Domain.Entities;
using Xunit;

namespace Bank_Domain.Tests.Entities
{
    public class BankEntityTests
    {
        [Fact]
        public void Constructor_WithValidData_CreatesBank()
        {
            // Arrange
            long Bank_ID = 0;
            var bankName = "SBI Bank";
            var bankType = "Public";
            var isActive = true;

            // Act
            var bank = new Bank(bankName, bankType, isActive,Bank_ID);

            // Assert
            Assert.Equal(bankName, bank.BankName);
            Assert.Equal(bankType, bank.BankType);
            Assert.True(bank.IsActive);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_WithNullOrEmptyBankName_ThrowsArgumentException(string bankName)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Bank(bankName, "", true,0));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Constructor_WithNullOrEmptyBankType_ThrowsArgumentException(string bankType)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Bank("SBI", bankType, true,0));
        }

        [Fact]
        public void UpdateBank_WithValidData_UpdatesProperties()
        {
            // Arrange
            var bank = new Bank("SBI", "Public", true,0);
            var updatedBank = new Bank("HDFC", "Private", false,0);

            // Act
            bank.UpdateBank(updatedBank);

            // Assert
            Assert.Equal("HDFC", bank.BankName);
            Assert.Equal("Private", bank.BankType);
            Assert.False(bank.IsActive);
        }

        [Fact]
        public void UpdateBank_WithNullBank_ThrowsArgumentNullException()
        {
            // Arrange
            var bank = new Bank("SBI", "Public", true,0);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => bank.UpdateBank(null));
        }
    }
}