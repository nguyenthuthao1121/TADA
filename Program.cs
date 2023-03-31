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

service.AddScoped<IBookRepository, BookRepository>();
service.AddScoped<IBookService, BookService>();
service.AddScoped<ICategoryRepository, CategoryRepository>();
service.AddScoped<ICategoryService, CategoryService>();
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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
