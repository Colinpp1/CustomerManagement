using CustomerManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerManagement.Repositories
{
    /// <summary>
    /// Interface for Customer repository operations
    /// </summary>
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer> GetByIdAsync(int id);
        Task<Customer> CreateAsync(Customer customer);
        Task<Customer> UpdateAsync(Customer customer);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
