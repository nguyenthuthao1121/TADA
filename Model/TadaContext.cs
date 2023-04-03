using Microsoft.EntityFrameworkCore;
using TADA.Model.Entity;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace TADA.Model
{
    public class TadaContext: DbContext
    {
        public TadaContext(DbContextOptions<TadaContext> option) :base(option) 
        { 
            
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
