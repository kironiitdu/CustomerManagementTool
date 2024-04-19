using CustomerManagementTool.Models;
using CustomerManagementTool.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CustomerManagementTool.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpPost]
        public IActionResult Add(int customerId, Contact contact)
        {
            try
            {
                _contactService.AddContact(customerId, contact);
                return PartialView("_ContactList", _contactService.GetContactsByCustomerId(customerId));
            }
            catch (Exception ex)
            {
                // Handle exception
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Edit(int customerId, int contactId, Contact contact)
        {
            try
            {
                _contactService.UpdateContact(customerId, contactId, contact);
                return PartialView("_ContactList", _contactService.GetContactsByCustomerId(customerId));
            }
            catch (Exception ex)
            {
                // Handle exception
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult DeleteContact(int customerId, int contactId)
        {
            try
            {
                _contactService.DeleteContact(customerId, contactId);
                var contacts = _contactService.GetContactsByCustomerId(customerId);
                return Json(contacts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        public IActionResult GetContactsByCustomerId(int customerId)
        {
            var contacts = _contactService.GetContactsByCustomerId(customerId);
            return Ok(contacts);
        }
    }
}
