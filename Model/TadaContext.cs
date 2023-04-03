using Microsoft.EntityFrameworkCore;
using TADA.Model.Entity;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

<<<<<<< HEAD
namespace TADA.Model;

public class TadaContext: DbContext
{
    public TadaContext(DbContextOptions<TadaContext> option) :base(option) 
    { 
        
=======
namespace TADA.Model
{
    public class TadaContext: DbContext
    {
        public TadaContext(DbContextOptions<TadaContext> option) :base(option) 
        { 
            
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Ward> Wards { get; set; }
        public DbSet<Address> Addresses { get; set; }

>>>>>>> 8704d3caa62f5f089e211017247bb897177b811d
    }
    public DbSet<Book> Books { get; set; }
    public DbSet<Provider> Providers { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Staff> Staff { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Province> Provinces { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<Ward> Wards { get; set; }
    public DbSet<Address> Addresses { get; set; }

}