using Bank_Application.Dtos;
using Bank_Application.Interface;
using Bank_Application.Services;
using Bank_Domain.Entities;
using Bank_Domain.Services;
using FluentValidation;
using Moq;
using Xunit;

namespace Bank_Application.Tests.Services
{
    public class BankServiceTests
    {
        private readonly Mock<IBankRepository> _mockRepository;
        private readonly Mock<IValidator<CreateBankDto>> _mockValidator;
        private readonly Mock<IBankDomainService> _mockDomainService;
        private readonly BankService _service;

        public BankServiceTests()
        {
            _mockRepository = new Mock<IBankRepository>();
            _mockValidator = new Mock<IValidator<CreateBankDto>>();
            _mockDomainService = new Mock<IBankDomainService>();

            _service = new BankService(_mockRepository.Object, _mockValidator.Object, _mockDomainService.Object);
        }

        [Fact]
        public async Task AddBankAsynch_WithValidData_ReturnsBankResponseDto()
        {
            // Arrange
            var createDto = new CreateBankDto
            {
                Bankname = "SBI",
                BankType = "Public",
                IsActive = true
            };

            var bank = new Bank("SBI", "Public", true);

            // Mock validation result
            var validationResult = new FluentValidation.Results.ValidationResult();
            _mockValidator
                .Setup(x => x.ValidateAsync(It.IsAny<CreateBankDto>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(validationResult);

            // Mock domain service
            _mockDomainService
                .Setup(x => x.CreateBank(createDto.Bankname, createDto.BankType, createDto.IsActive))
                .Returns(bank);

            // Mock repository
            _mockRepository
                .Setup(x => x.AddBankAsynch(It.IsAny<Bank>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _service.AddBankAsynch(createDto, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("SBI", result.Bankname);
            Assert.Equal("Public", result.BankType);
            Assert.True(result.IsActive);

            // Verify mocks were called
            _mockValidator.Verify(x => x.ValidateAsync(createDto, It.IsAny<CancellationToken>()), Times.Once);
            _mockDomainService.Verify(x => x.CreateBank(createDto.Bankname, createDto.BankType, createDto.IsActive), Times.Once);
            _mockRepository.Verify(x => x.AddBankAsynch(It.IsAny<Bank>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task AddBankAsynch_WithInvalidData_ThrowsValidationException()
        {
            // Arrange
            var createDto = new CreateBankDto { Bankname = "", BankType = "", IsActive = true };

            var validationFailure = new FluentValidation.Results.ValidationFailure("Bankname", "Bank name is required");
            var validationResult = new FluentValidation.Results.ValidationResult(new[] { validationFailure });

            _mockValidator
                .Setup(x => x.ValidateAsync(It.IsAny<CreateBankDto>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(validationResult);

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => _service.AddBankAsynch(createDto, CancellationToken.None));
        }

        [Fact]
        public async Task AddBankAsynch_WhenRepositoryFails_ThrowsException()
        {
            // Arrange
            var createDto = new CreateBankDto
            {
                Bankname = "SBI",
                BankType = "Public",
                IsActive = true
            };

            var validationResult = new FluentValidation.Results.ValidationResult();
            var bank = new Bank("SBI", "Public", true);

            _mockValidator
                .Setup(x => x.ValidateAsync(It.IsAny<CreateBankDto>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(validationResult);

            _mockDomainService
                .Setup(x => x.CreateBank(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(bank);

            _mockRepository
                .Setup(x => x.AddBankAsynch(It.IsAny<Bank>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.AddBankAsynch(createDto, CancellationToken.None));
        }
    }
}