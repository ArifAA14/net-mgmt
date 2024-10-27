using Microsoft.EntityFrameworkCore;
using EmployeeMgmt.Infrastructure.Data;
using EmployeeMgmt.Application.Services;
using EmployeeMgmt.Infrastructure.Repositories;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add DbContext for PostgreSQL
builder.Services.AddDbContext<EmployeeMgmtDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
