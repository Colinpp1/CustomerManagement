using CustomerManagement.Models;
using CustomerManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerManagement.Services
{
    /// <summary>
    /// Service layer for Customer business logic
    /// </summary>
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            var customer = await _repository.GetByIdAsync(id);
            if (customer == null)
                throw new KeyNotFoundException($"Customer with ID {id} not found.");
            
            return customer;
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            // Business validation
            if (string.IsNullOrWhiteSpace(customer.Name))
                throw new ArgumentException("Customer name is required.");

            if (string.IsNullOrWhiteSpace(customer.Email))
                throw new ArgumentException("Customer email is required.");

            return await _repository.CreateAsync(customer);
        }

        public async Task<Customer> UpdateCustomerAsync(int id, Customer customer)
        {
            if (id != customer.CustomerID)
                throw new ArgumentException("Customer ID mismatch.");

            var exists = await _repository.ExistsAsync(id);
            if (!exists)
                throw new KeyNotFoundException($"Customer with ID {id} not found.");

            // Business validation
            if (string.IsNullOrWhiteSpace(customer.Name))
                throw new ArgumentException("Customer name is required.");

            if (string.IsNullOrWhiteSpace(customer.Email))
                throw new ArgumentException("Customer email is required.");

            return await _repository.UpdateAsync(customer);
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var exists = await _repository.ExistsAsync(id);
            if (!exists)
                throw new KeyNotFoundException($"Customer with ID {id} not found.");

            return await _repository.DeleteAsync(id);
        }
    }
}
