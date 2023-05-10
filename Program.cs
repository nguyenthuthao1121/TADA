using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using TADA.Model;
using TADA.Repository;
using TADA.Repository.Implement;
using TADA.Service;
using TADA.Service.Implement;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var service = builder.Services;

service.AddDbContext<TadaContext>(option =>
{
    var connectionString = builder.Configuration.GetConnectionString("Default");
    option.UseSqlServer(connectionString);
    //option.UseSqlServer(connectionString);
});

service.AddSession();
service.AddMemoryCache();
service.AddMvc();


service.AddScoped<IBookRepository, BookRepository>();
service.AddScoped<IBookService, BookService>();

service.AddScoped<ICategoryRepository, CategoryRepository>();
service.AddScoped<ICategoryService, CategoryService>();

service.AddScoped<ICustomerRepository, CustomerRepository>();
service.AddScoped<ICustomerService, CustomerService>();

service.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
service.AddScoped<IAuthenticationService, AuthenticationService>();

service.AddScoped<IAccountRepository, AccountRepository>();
service.AddScoped<IAccountService, AccountService>();

service.AddScoped<IStaffRepository, StaffRepository>();
service.AddScoped<IStaffService, StaffService>();

service.AddScoped<IAddressRepository, AddressRepository>();
service.AddScoped<IAddressService, AddressService>();

service.AddScoped<IReviewRepository, ReviewRepository>();
service.AddScoped<IReviewService, ReviewService>();

service.AddScoped<ICartRepository, CartRepository>();
service.AddScoped<ICartService, CartService>();

service.AddScoped<IOrderRepository, OrderRepository>();
service.AddScoped<IOrderService, OrderService>();

service.AddScoped<IRoleRepository, RoleRepository>();
service.AddScoped<IRoleService, RoleService>();

service.AddScoped<IProviderRepository, ProviderRepository>();
service.AddScoped<IProviderService, ProviderService>();

service.AddScoped<IStatusRepository, StatusRepository>();
service.AddScoped<IStatusService, StatusService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.UseSession();

app.MapRazorPages();

app.Run();
