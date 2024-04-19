using CustomerManagementTool.Models;
using CustomerManagementTool.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CustomerManagementTool.Controllers
{
    public class TaskController : Controller
    {
        private readonly ICustomerTaskService _customerTaskService;

        public TaskController(ICustomerTaskService customerTaskService)
        {
            _customerTaskService = customerTaskService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TaskModel>>> GetTasksByCustomerId(int customerId)
        {
            var tasks = await _customerTaskService.GetTasksByCustomerId(customerId);
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<ActionResult<TaskModel>> AddTask(int customerId, TaskModel task)
        {
            var success = await _customerTaskService.AddTask(customerId, task);
            if (!success)
                return BadRequest("Failed to add task.");

            return Ok();
        }

        [HttpPut("{taskId}")]
        public async Task<ActionResult> UpdateTask(int customerId, int taskId, TaskModel task)
        {
            var success = await _customerTaskService.UpdateTask(customerId, taskId, task);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteTask(int customerId, int taskId)
        {
            var success = await _customerTaskService.DeleteTask(customerId, taskId);
            if (!success)
                return NotFound();

            return Ok();
        }
    }
}
