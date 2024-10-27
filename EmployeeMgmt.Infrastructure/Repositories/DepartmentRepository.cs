using EmployeeMgmt.Domain.Entities;
using EmployeeMgmt.Infrastructure.Data;  // To use the DbContext
using Microsoft.EntityFrameworkCore;

namespace EmployeeMgmt.Infrastructure.Repositories
{
  public class DepartmentRepository : IDepartmentRepository
  {
    private readonly EmployeeMgmtDbContext _context;

    public DepartmentRepository(EmployeeMgmtDbContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<Department>> GetAllAsync()
    {
      return await _context.Departments.ToListAsync();
    }

    public async Task<Department> GetByIdAsync(int departmentId)
    {
      return await _context.Departments.FindAsync(departmentId);
    }

    public async Task AddAsync(Department department)
    {
      await _context.Departments.AddAsync(department);
      await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Department department)
    {
      _context.Departments.Update(department);
      await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int departmentId)
    {
      var department = await _context.Departments.FindAsync(departmentId);
      if (department != null)
      {
        _context.Departments.Remove(department);
        await _context.SaveChangesAsync();
      }
    }
  }
}


