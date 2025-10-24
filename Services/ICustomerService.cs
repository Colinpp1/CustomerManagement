using CustomerManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerManagement.Services
{
    /// <summary>
    /// Interface for Customer business logic
    /// </summary>
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
        Task<Customer> CreateCustomerAsync(Customer customer);
        Task<Customer> UpdateCustomerAsync(int id, Customer customer);
        Task<bool> DeleteCustomerAsync(int id);
    }
}
