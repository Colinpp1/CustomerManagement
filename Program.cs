using CustomerManagement.Data;
using CustomerManagement.Models;
using CustomerManagement.Repositories;
using CustomerManagement.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CustomerManagement
{
    class Program
    {
        private static ICustomerService _customerService;

        static async Task Main(string[] args)
        {
            // Setup database
            var options = new DbContextOptionsBuilder<CustomerDbContext>()
                .UseSqlite("Data Source=customers.db")
                .Options;

            using var context = new CustomerDbContext(options);
            
            // Ensure database is created
            await context.Database.EnsureCreatedAsync();

            // Setup dependency injection
            var repository = new CustomerRepository(context);
            _customerService = new CustomerService(repository);

            Console.WriteLine("╔════════════════════════════════════════════╗");
            Console.WriteLine("║   Customer Management System (EF Core)    ║");
            Console.WriteLine("╚════════════════════════════════════════════╝\n");

            bool exit = false;
            while (!exit)
            {
                ShowMenu();
                var choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            await ListAllCustomers();
                            break;
                        case "2":
                            await GetCustomerById();
                            break;
                        case "3":
                            await CreateCustomer();
                            break;
                        case "4":
                            await UpdateCustomer();
                            break;
                        case "5":
                            await DeleteCustomer();
                            break;
                        case "6":
                            exit = true;
                            Console.WriteLine("\nGoodbye!");
                            break;
                        default:
                            Console.WriteLine("\nInvalid option. Please try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nError: {ex.Message}");
                }

                if (!exit)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("\n┌────────────────────────────────────┐");
            Console.WriteLine("│           MAIN MENU                │");
            Console.WriteLine("├────────────────────────────────────┤");
            Console.WriteLine("│ 1. List All Customers              │");
            Console.WriteLine("│ 2. Get Customer by ID              │");
            Console.WriteLine("│ 3. Create New Customer             │");
            Console.WriteLine("│ 4. Update Customer                 │");
            Console.WriteLine("│ 5. Delete Customer                 │");
            Console.WriteLine("│ 6. Exit                            │");
            Console.WriteLine("└────────────────────────────────────┘");
            Console.Write("\nEnter your choice: ");
        }

        static async Task ListAllCustomers()
        {
            Console.WriteLine("\n═══ ALL CUSTOMERS ═══\n");
            var customers = await _customerService.GetAllCustomersAsync();
            
            foreach (var customer in customers)
            {
                Console.WriteLine($"ID: {customer.CustomerID}");
                Console.WriteLine($"Name: {customer.Name}");
                Console.WriteLine($"Email: {customer.Email}");
                Console.WriteLine($"Phone: {customer.PhoneNumber}");
                Console.WriteLine("─────────────────────────────");
            }
        }

        static async Task GetCustomerById()
        {
            Console.Write("\nEnter Customer ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var customer = await _customerService.GetCustomerByIdAsync(id);
                
                Console.WriteLine("\n═══ CUSTOMER DETAILS ═══\n");
                Console.WriteLine($"ID: {customer.CustomerID}");
                Console.WriteLine($"Name: {customer.Name}");
                Console.WriteLine($"Email: {customer.Email}");
                Console.WriteLine($"Phone: {customer.PhoneNumber}");
            }
            else
            {
                Console.WriteLine("Invalid ID format.");
            }
        }

        static async Task CreateCustomer()
        {
            Console.WriteLine("\n═══ CREATE NEW CUSTOMER ═══\n");
            
            var customer = new Customer();
            
            Console.Write("Name: ");
            customer.Name = Console.ReadLine();
            
            Console.Write("Email: ");
            customer.Email = Console.ReadLine();
            
            Console.Write("Phone Number: ");
            customer.PhoneNumber = Console.ReadLine();

            var created = await _customerService.CreateCustomerAsync(customer);
            Console.WriteLine($"\n✓ Customer created successfully with ID: {created.CustomerID}");
        }

        static async Task UpdateCustomer()
        {
            Console.Write("\nEnter Customer ID to update: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var customer = await _customerService.GetCustomerByIdAsync(id);
                
                Console.WriteLine("\n═══ UPDATE CUSTOMER ═══\n");
                Console.WriteLine($"Current Name: {customer.Name}");
                Console.Write("New Name (press Enter to keep current): ");
                var name = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(name))
                    customer.Name = name;
                
                Console.WriteLine($"Current Email: {customer.Email}");
                Console.Write("New Email (press Enter to keep current): ");
                var email = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(email))
                    customer.Email = email;
                
                Console.WriteLine($"Current Phone: {customer.PhoneNumber}");
                Console.Write("New Phone (press Enter to keep current): ");
                var phone = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(phone))
                    customer.PhoneNumber = phone;

                await _customerService.UpdateCustomerAsync(id, customer);
                Console.WriteLine("\n✓ Customer updated successfully!");
            }
            else
            {
                Console.WriteLine("Invalid ID format.");
            }
        }

        static async Task DeleteCustomer()
        {
            Console.Write("\nEnter Customer ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var customer = await _customerService.GetCustomerByIdAsync(id);
                Console.WriteLine($"\nAre you sure you want to delete '{customer.Name}'? (y/n): ");
                var confirm = Console.ReadLine();
                
                if (confirm?.ToLower() == "y")
                {
                    await _customerService.DeleteCustomerAsync(id);
                    Console.WriteLine("\n✓ Customer deleted successfully!");
                }
                else
                {
                    Console.WriteLine("\nDeletion cancelled.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID format.");
            }
        }
    }
}
