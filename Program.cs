using Microsoft.EntityFrameworkCore;
using TADA.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
var service = builder.Services;
var connectionString = builder.Configuration.GetConnectionString("Default");
service.AddDbContext<TadaContext>(option=>option.UseSqlServer(@"Server=DESKTOP-5AUB9TQ\NGUYENTHITHUTHAO; Database=TADA;TrustServerCertificate=True;User Id=sa;Password=08334311210;"));
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
