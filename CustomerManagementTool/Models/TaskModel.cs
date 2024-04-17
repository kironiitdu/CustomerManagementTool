namespace CustomerManagementTool.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Solved { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
    }
}
