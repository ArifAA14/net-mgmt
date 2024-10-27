using EmployeeMgmt.Domain.Entities;
using EmployeeMgmt.Infrastructure.Data;  // To use the DbContext
using Microsoft.EntityFrameworkCore;

namespace EmployeeMgmt.Infrastructure.Repositories
{
  public class EmployeeRepository : IEmployeeRepository
  {
    private readonly EmployeeMgmtDbContext _context;

    public EmployeeRepository(EmployeeMgmtDbContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
      return await _context.Employees.Include(e => e.Department).ToListAsync();
    }

    public async Task<Employee> GetByIdAsync(int employeeId)
    {
      return await _context.Employees.FindAsync(employeeId);
    }

    public async Task AddAsync(Employee employee)
    {
      await _context.Employees.AddAsync(employee);
      await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Employee employee)
    {
      _context.Employees.Update(employee);
      await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int employeeId)
    {
      var employee = await _context.Employees.FindAsync(employeeId);
      if (employee != null)
      {
        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
      }
    }
  }
}
