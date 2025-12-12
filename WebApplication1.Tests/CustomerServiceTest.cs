using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Service;
using Xunit;

namespace WebApplication1.Tests
{
    public class CustomerServiceTests
    {
        private CustomerService CreateServiceWithData(List<CustomerDto>? seedData = null)
        {
            return new CustomerService(new CustomerMemory
            {
                Customers = seedData ?? new List<CustomerDto>()
            });
        }

        [Fact]
        public async Task InsertCustomer_ShouldAddCustomer()
        {
            // Arrange
            var service = CreateServiceWithData();
            var submission = new CustomerSubmission
            {
                Firstname = "Jane",
                LastName = "Doe"
            };

            // Act
            var result = await service.InsertCustomerResult(submission);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.False(string.IsNullOrEmpty(result.Id));
        }

        [Fact]
        public async Task GetCustomer_ShouldReturnCustomer_WhenExists()
        {
            // Arrange
            var existingCustomer = new CustomerDto
            {
                Id = "123",
                Firstname = "Alice",
                LastName = "Tan",
                IsActive = true
            };

            var service = CreateServiceWithData(new List<CustomerDto> { existingCustomer });

            // Act
            var result = await service.GetCustomerRecords("123");

            // Assert
            Assert.NotNull(result.Customer);
            Assert.Equal("123", result.Customer.Id);
            Assert.Equal("", result.Message);
        }

        [Fact]
        public async Task GetCustomer_ShouldReturnNull_WhenNotFound()
        {
            // Arrange
            var service = CreateServiceWithData();

            // Act
            var result = await service.GetCustomerRecords("999");

            // Assert
            Assert.Null(result.Customer);
            Assert.Equal("Unable to find customer 999", result.Message);
        }

        [Fact]
        public async Task DeleteCustomer_ShouldReturnError_WhenNotFound()
        {
            // Arrange
            var service = CreateServiceWithData();

            // Act
            var result = await service.DeleteCustomerResult("NOPE");

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Unable to find customer NOPE for deletion", result.Message);
        }
    }
}
