using Bank_Application.Dtos;
using Bank_Application.Validation;
using Xunit;

namespace Bank_Application.Tests.Validation
{
    public class BankValidatorTests
    {
        private readonly BankValidator _validator;

        public BankValidatorTests()
        {
            _validator = new BankValidator();
        }

        [Fact]
        public async Task Validate_WithValidData_ReturnsSuccess()
        {
            // Arrange
            var dto = new CreateBankDto
            {
                Bankname = "SBI",
                BankType = "Public",
                IsActive = true
            };

            // Act
            var result = await _validator.ValidateAsync(dto);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public async Task Validate_WithEmptyBankName_ReturnsFalse()
        {
            // Arrange
            var dto = new CreateBankDto
            {
                Bankname = "",
                BankType = "Public",
                IsActive = true
            };

            // Act
            var result = await _validator.ValidateAsync(dto);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName == "Bankname");
        }

        [Fact]
        public async Task Validate_WithEmptyBankType_ReturnsFalse()
        {
            // Arrange
            var dto = new CreateBankDto
            {
                Bankname = "SBI",
                BankType = "",
                IsActive = true
            };

            // Act
            var result = await _validator.ValidateAsync(dto);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, x => x.PropertyName == "BankType");
        }
    }
}