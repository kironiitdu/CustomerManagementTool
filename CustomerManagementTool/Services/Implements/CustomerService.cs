using CustomerManagementTool.Data;
using CustomerManagementTool.Models;
using CustomerManagementTool.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagementTool.Services.Implements
{
    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext _context;

        public CustomerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> AddCustomerAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            var customer = await _context.Customers!.FindAsync(id);
            return customer!;
        }

        public async Task<(List<CustomerViewModel>, int)> GetCustomersAsync(int page, int pageSize)
        {
            var totalCount = await _context.Customers.CountAsync();

            var customers = await _context.Customers
                .Select(c => new CustomerViewModel
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Birthday = c.Birthday.ToString("yyyy-MM-dd"),
                })
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (customers, totalCount);
        }
        public async Task<(List<CustomerViewModel>, int)> SearchCustomersAsync(string searchKey, int page, int pageSize)
        {
            var totalCount = await _context.Customers.CountAsync();

            var customers = await _context.Customers
                .Where(c => c.FirstName.Contains(searchKey) || c.LastName.Contains(searchKey))
                .Select(c => new CustomerViewModel
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Birthday = c.Birthday.ToString("yyyy-MM-dd")
                })
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (customers, totalCount);
        }
        public async Task<Customer> UpdateCustomerAsync(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return customer;
        }

        //Bulk Insert
        public async Task AddBulkCustomersAsync()
        {
            int numberOfCustomers = 2000;
            List<Customer> customers = GenerateCustomers(numberOfCustomers);

            _context.Customers.AddRange(customers);
            await _context.SaveChangesAsync();
        }

        private List<Customer> GenerateCustomers(int numberOfCustomers)
        {
            List<Customer> customers = new List<Customer>();

            for (int i = 0; i < numberOfCustomers; i++)
            {
                customers.Add(new Customer
                {
                    FirstName = "FirstName" + i,
                    LastName = "LastName" + i,
                    Birthday = DateTime.Now.AddYears(-i),
                });
            }

            return customers;
        }

        public async Task<Customer> Details(int id)
        {
            var customer = await _context.Customers.Include(c => c.Contacts).Include(c => c.Tasks)
             .FirstOrDefaultAsync(c => c.Id == id);
            return customer!;
        }
    }
}
