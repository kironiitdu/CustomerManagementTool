using CustomerManagementTool.Data;
using CustomerManagementTool.Models;
using CustomerManagementTool.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagementTool.Services.Implements
{
    public class CustomerTaskService : ICustomerTaskService
    {
        private readonly AppDbContext _context;

        public CustomerTaskService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddTask(int customerId, TaskModel task)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null)
                return false;

            task.CustomerId = customerId;
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTask(int customerId, int taskId)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task == null || task.CustomerId != customerId)
                return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<List<TaskModel>> GetTasksByCustomerId(int customerId)
        {
            return await _context.Tasks.Where(t => t.CustomerId == customerId).ToListAsync();
        }

        public async Task<bool> UpdateTask(int customerId, int taskId, TaskModel task)
        {
            var existingTask = await _context.Tasks.FindAsync(taskId);
            if (existingTask == null || existingTask.CustomerId != customerId)
                return false;

            existingTask.Description = task.Description;
            existingTask.CreationDate = task.CreationDate;
            existingTask.Solved = task.Solved;

            await _context.SaveChangesAsync();
            return true;
        }
    }

}
