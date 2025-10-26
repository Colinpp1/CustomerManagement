using CustomerManagement.Models;
using CustomerManagement.Repositories;
using CustomerManagement.Services;
using Moq;
using Xunit;

namespace CustomerManagement.Tests;

public class CustomerServiceTests
{
    [Fact]
    public async Task GetAllCustomersAsync_ReturnsCustomers()
    {
        // Arrange
        var mockRepo = new Mock<ICustomerRepository>();
        var customers = new List<Customer>
        {
            new Customer { CustomerID = 1, Name = "John", Email = "john@test.com", PhoneNumber = "123" }
        };
        mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(customers);
        var service = new CustomerService(mockRepo.Object);

        // Act
        var result = await service.GetAllCustomersAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task CreateCustomerAsync_ValidCustomer_ReturnsCustomer()
    {
        // Arrange
        var mockRepo = new Mock<ICustomerRepository>();
        var customer = new Customer { Name = "Test", Email = "test@test.com", PhoneNumber = "123" };
        mockRepo.Setup(r => r.CreateAsync(It.IsAny<Customer>())).ReturnsAsync(customer);
        var service = new CustomerService(mockRepo.Object);

        // Act
        var result = await service.CreateCustomerAsync(customer);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task DeleteCustomerAsync_ExistingCustomer_ReturnsTrue()
    {
        // Arrange
        var mockRepo = new Mock<ICustomerRepository>();
        mockRepo.Setup(r => r.ExistsAsync(1)).ReturnsAsync(true);
        mockRepo.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);
        var service = new CustomerService(mockRepo.Object);

        // Act
        var result = await service.DeleteCustomerAsync(1);

        // Assert
        Assert.True(result);
    }
}
