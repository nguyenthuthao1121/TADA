using Microsoft.EntityFrameworkCore;
using TADA.Model.Entity;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace TADA.Model
{
    public class TadaContext: DbContext
    {
        public TadaContext(DbContextOptions<TadaContext> option) :base(option) { 
            
        }
        DbSet<Book> books { get; set; }
        DbSet<Provider> providers { get; set; }
        DbSet<Contract> contracts { get; set; }
        DbSet<Category> categories { get; set; }
        DbSet<Order> orders { get; set; }
        DbSet<Status> statuses { get; set; }
        DbSet<Customer> customers { get; set; }
        DbSet<Staff> staffs { get; set; }
        DbSet<Role> roles { get; set; }
        DbSet<Account> accounts { get; set; }
        DbSet<Cart> carts { get; set; }
        DbSet<Review> reviews { get; set; }
        DbSet<Province> provinces { get; set; }
        DbSet<District> districts { get; set; }
        DbSet<Ward> wards { get; set; }
        DbSet<Address> addresses { get; set; }

    }
}
