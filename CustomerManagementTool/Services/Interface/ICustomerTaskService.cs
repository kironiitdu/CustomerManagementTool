using CustomerManagementTool.Models;

namespace CustomerManagementTool.Services.Interface
{
    public interface ICustomerTaskService
    {
        void AddTask(int customerId, TaskModel task);

        void UpdateTask(int customerId, int taskId, TaskModel task);

        void DeleteTask(int customerId, int taskId);
    }
}
