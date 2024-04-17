using CustomerManagementTool.Models;
using CustomerManagementTool.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagementTool.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        public async Task<IActionResult> Index()
        {
           
            return View();
        }

        public async Task<IActionResult> AddBulkCustomers()
        {
            await _customerService.AddBulkCustomersAsync();
            return Ok("Customer Added");
        }
        public async Task<IActionResult> GetCustomer(int page = 1, int pageSize = 10, string searchKey = "")
        {
            (List<CustomerViewModel> customers, int totalCount) result;
            if (!string.IsNullOrEmpty(searchKey))
            {
                result = await _customerService.SearchCustomersAsync(searchKey, page, pageSize);
            }
            else
            {
                result = await _customerService.GetCustomersAsync(page, pageSize);
            }

            return Ok(new { Customers = result.customers, TotalCount = result.totalCount });
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _customerService.AddCustomerAsync(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }
        public async Task<IActionResult> Edit(int id)
        {
            Customer customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _customerService.UpdateCustomerAsync(customer);
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Handle concurrency exception
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }
        public async Task<IActionResult> Delete(int id)
        {
            Customer customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _customerService.DeleteCustomerAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id) 
        {
            var customer = await _customerService.Details(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }
    }


}
