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
            var bankName = "SBI Bank";
            var bankType = "Public";
            var isActive = true;

            // Act
            var bank = new Bank(bankName, bankType, isActive);

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
            Assert.Throws<ArgumentException>(() => new Bank(bankName, "Public", true));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Constructor_WithNullOrEmptyBankType_ThrowsArgumentException(string bankType)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Bank("SBI", bankType, true));
        }

        [Fact]
        public void UpdateBank_WithValidData_UpdatesProperties()
        {
            // Arrange
            var bank = new Bank("SBI", "Public", true);
            var updatedBank = new Bank("HDFC", "Private", false);

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
            var bank = new Bank("SBI", "Public", true);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => bank.UpdateBank(null));
        }
    }
}