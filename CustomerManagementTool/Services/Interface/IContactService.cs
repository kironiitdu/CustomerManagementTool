using CustomerManagementTool.Models;

namespace CustomerManagementTool.Services.Interface
{
    public interface IContactService
    {
        void AddContact(int customerId, Contact contact);

        void UpdateContact(int customerId, int contactId, Contact contact);

        void DeleteContact(int customerId, int contactId);
        List<Contact> GetContactsByCustomerId(int customerId);
    }
}
