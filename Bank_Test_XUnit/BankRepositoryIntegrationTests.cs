using Bank_Domain.Entities;
using Bank_Infra.Data;
using Bank_Infra.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Microsoft.EntityFrameworkCore;
namespace Bank_Test_XUnit
{
    

  //  namespace Bank_Infra.Tests.Repository
    
        public class BankRepositoryIntegrationTests : IAsyncLifetime
        {
            private readonly DbContextOptions<BankAppDbContext> _options;
            private BankAppDbContext _context;
            private BankRepository _repository;

            public BankRepositoryIntegrationTests()
            {
                _options = new DbContextOptionsBuilder<BankAppDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                    .Options;
            }

            public async Task InitializeAsync()
            {
                _context = new BankAppDbContext(_options);
                await _context.Database.EnsureCreatedAsync();
                _repository = new BankRepository(_context);
            }

            public async Task DisposeAsync()
            {
                await _context.Database.EnsureDeletedAsync();
                await _context.DisposeAsync();
            }

            [Fact]
            public async Task AddBankAsynch_WithValidBank_SavesSuccessfully()
            {
                // Arrange
                var bank = new Bank("SBI", "Public", true);

                // Act
                await _repository.AddBankAsynch(bank, CancellationToken.None);

                // Assert
                var savedBank = await _context.Banks.FirstOrDefaultAsync();
                Assert.NotNull(savedBank);
                Assert.Equal("SBI", savedBank.BankName);
                Assert.Equal("Public", savedBank.BankType);
            }

            [Fact]
            public async Task AddBankAsynch_MultipleBanks_SavesAllSuccessfully()
            {
                // Arrange
                var bank1 = new Bank("SBI", "Public", true);
                var bank2 = new Bank("HDFC", "Private", true);

                // Act
                await _repository.AddBankAsynch(bank1, CancellationToken.None);
                await _repository.AddBankAsynch(bank2, CancellationToken.None);

                // Assert
                var banks = await _context.Banks.ToListAsync();
                Assert.Equal(2, banks.Count);
            }
        }
    
}
