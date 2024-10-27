using EmployeeMgmt.Domain.Entities;
using EmployeeMgmt.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMgmt.Infrastructure.Data
{
  public class EmployeeMgmtDbContext : DbContext
  {
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }

    public EmployeeMgmtDbContext(DbContextOptions<EmployeeMgmtDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)

    {

      // One-to-Many relationship between Department and Employees
      modelBuilder.Entity<Employee>()
          .HasOne(e => e.Department)  // Each Employee has one Department
          .WithMany(d => d.Employees)  // Each Department has many Employees
          .HasForeignKey(e => e.DepartmentId);  

    }
  }
}