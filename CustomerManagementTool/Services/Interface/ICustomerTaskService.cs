using CustomerManagementTool.Models;

namespace CustomerManagementTool.Services.Interface
{
    public interface ICustomerTaskService
    {
        Task<bool> AddTask(int customerId, TaskModel task);

        Task<bool> UpdateTask(int customerId, int taskId, TaskModel task);

        Task<bool> DeleteTask(int customerId, int taskId);
        Task<List<TaskModel>> GetTasksByCustomerId(int customerId);
    }
}
