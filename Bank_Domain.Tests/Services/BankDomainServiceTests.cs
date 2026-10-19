using Bank_Domain.Entities;
using Bank_Domain.Services;
using Xunit;

namespace Bank_Domain.Tests.Services
{
    public class BankDomainServiceTests
    {
        private readonly BankDomainService _service;

        public BankDomainServiceTests()
        {
            _service = new BankDomainService();
        }

        [Fact]
        public void CreateBank_WithValidData_ReturnsBankEntity()
        {
            // Arrange
            var bankName = "SBI";
            var bankType = "Public";
            var isActive = true;

            // Act
            var result = _service.CreateBank(bankName, bankType, isActive);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(bankName, result.BankName);
            Assert.Equal(bankType, result.BankType);
            Assert.True(result.IsActive);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CreateBank_WithNullOrEmptyBankName_ThrowsArgumentException(string bankName)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _service.CreateBank(bankName, "Public", true));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void CreateBank_WithNullOrEmptyBankType_ThrowsArgumentException(string bankType)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _service.CreateBank("SBI", bankType, true));
        }

        [Fact]
        public void CreateBank_WithInactiveBank_CreatesInactiveBank()
        {
            // Act
            var result = _service.CreateBank("SBI", "Public", false);

            // Assert
            Assert.False(result.IsActive);
        }
    }
}