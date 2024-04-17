using CustomerManagementTool.Models;

namespace CustomerManagementTool.Services.Interface
{
    public interface ICustomerService
    {
        Task<Customer> GetCustomerByIdAsync(int id);
        Task<(List<CustomerViewModel>, int)> GetCustomersAsync(int page, int pageSize);
        Task<(List<CustomerViewModel>, int)> SearchCustomersAsync(string searchKey, int page, int pageSize);
        Task<Customer> AddCustomerAsync(Customer customer);
        Task AddBulkCustomersAsync();
        Task<Customer> UpdateCustomerAsync(Customer customer);
        Task<Customer> Details(int id);
        Task DeleteCustomerAsync(int id);
    }
}
