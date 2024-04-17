namespace CustomerManagementTool.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime Birthday { get; set; }
        public List<Contact>? Contacts { get; set; }
        public List<TaskModel>? Tasks { get; set; }
    }
}
