using CustomerManagementTool.Enum;

namespace CustomerManagementTool.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public ContactType Type { get; set; }
        public string? Value { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
    }
}
