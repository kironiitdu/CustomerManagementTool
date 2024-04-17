using CustomerManagementTool.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagementTool.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }


    }
}
