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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetail>()
            .HasKey(bc => new { bc.OrderId, bc.BookId });
            modelBuilder.Entity<OrderDetail>()
                .HasOne(bc => bc.Book)
                .WithMany(b => b.OrderDetails)
                .HasForeignKey(bc => bc.BookId);
            modelBuilder.Entity<OrderDetail>()
                .HasOne(bc => bc.Order)
                .WithMany(c => c.OrderDetails)
                .HasForeignKey(bc => bc.OrderId);

            modelBuilder.Entity<CartDetail>()
            .HasKey(bc => new { bc.CartId, bc.BookId });
            modelBuilder.Entity<CartDetail>()
                .HasOne(bc => bc.Book)
                .WithMany(b => b.CartDetails)
                .HasForeignKey(bc => bc.BookId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<CartDetail>()
                .HasOne(bc => bc.Cart)
                .WithMany(c => c.CartDetails)
                .HasForeignKey(bc => bc.CartId)
                .OnDelete(DeleteBehavior.Restrict);
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Provider> Providers { get; set; }
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
        public DbSet<CartDetail> CartDetail { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }

    }
}
