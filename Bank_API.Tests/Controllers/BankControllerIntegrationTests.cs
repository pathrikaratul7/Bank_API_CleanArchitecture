using Bank_Application.Dtos;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text;
using System.Text.Json;
using Xunit;

namespace Bank_API.Tests.Controllers
{
    public class BankControllerIntegrationTests : IAsyncLifetime
    {
        private WebApplicationFactory<Program> _factory;
        private HttpClient _client;

        public async Task InitializeAsync()
        {
            _factory = new WebApplicationFactory<Program>();
            _client = _factory.CreateClient();
        }

        public async Task DisposeAsync()
        {
            _client?.Dispose();
            _factory?.Dispose();
        }

        [Fact]
        public async Task AddBank_WithValidData_ReturnsCreatedAtAction()
        {
            // Arrange
            var createDto = new CreateBankDto
            {
                Bankname = "ABCD",
                BankType = "Public",
                IsActive = true
            };

            var jsonContent = new StringContent(
                JsonSerializer.Serialize(createDto),
                Encoding.UTF8,
                "application/json");

            // Act
            var response = await _client.PostAsync("/api/bank/AddBankAsynch", jsonContent);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.StatusCode == HttpStatusCode.Created || response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task AddBank_WithInvalidData_ReturnsErrorResponse()
        {
            // Arrange
            var createDto = new CreateBankDto
            {
                Bankname = "",
                BankType = "",
                IsActive = true
            };

            var jsonContent = new StringContent(
                JsonSerializer.Serialize(createDto),
                Encoding.UTF8,
                "application/json");

            // Act
            var response = await _client.PostAsync("/api/bank/AddBankAsynch", jsonContent);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.IsSuccessStatusCode);
        }
    }
}