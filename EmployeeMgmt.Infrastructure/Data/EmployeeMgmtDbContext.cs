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

      modelBuilder.Entity<Employee>()
        .HasOne(e => e.Department)
        .WithMany(d => d.Employees)
        .HasForeignKey(e => e.DepartmentId);


      modelBuilder.Entity<Employee>()
        .HasIndex(e => e.Email)
        .IsUnique();

      modelBuilder.Entity<Employee>()
        .HasIndex(e => e.EmployeeCode)
        .IsUnique();

      base.OnModelCreating(modelBuilder);

    }
  }
}