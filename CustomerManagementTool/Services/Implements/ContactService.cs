using CustomerManagementTool.Data;
using CustomerManagementTool.Models;
using CustomerManagementTool.Services.Interface;

namespace CustomerManagementTool.Services.Implements
{
    public class ContactService : IContactService
    {
        private readonly AppDbContext _context; 

        public ContactService(AppDbContext context)
        {
            _context = context;
        }

        public void AddContact(int customerId, Contact contact)
        {
            contact.CustomerId = customerId; 
            _context.Contacts.Add(contact);
            _context.SaveChanges();
        }

        public void UpdateContact(int customerId, int contactId, Contact contact)
        {
            var existingContact = _context.Contacts.Find(customerId, contactId);

            if (existingContact != null)
            {
                existingContact.Type = contact.Type; 
                existingContact.Value = contact.Value;
                _context.SaveChanges();
            }
            else
            {
                
                throw new ArgumentException("Contact not found");
            }
        }

        public void DeleteContact(int customerId, int contactId)
        {
            var contact = _context.Contacts.FirstOrDefault(c => c.CustomerId == customerId && c.Id == contactId);

            if (contact != null)
            {
                _context.Contacts.Remove(contact);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Contact not found");
            }
        }

        public List<Contact> GetContactsByCustomerId(int customerId)
        {
            return _context.Contacts.Where(c => c.CustomerId == customerId).ToList();
        }
    }
}
