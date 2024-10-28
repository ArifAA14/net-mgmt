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

    public async Task<Department> GetByNameAsync(string departmentName)
    {
      return await _context.Departments.FirstOrDefaultAsync(d => d.DepartmentName == departmentName);
    }

    public async Task AddAsync(Department department)
    {
      await _context.Departments.AddAsync(department);
      await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Department department)
    {
      // Detach the existing entity if it's already being tracked
      var trackedEntity = _context.ChangeTracker.Entries<Department>()
          .FirstOrDefault(e => e.Entity.DepartmentId == department.DepartmentId);

      if (trackedEntity != null)
      {
        trackedEntity.State = EntityState.Detached; // Detach the tracked entity
      }

      // Now attach and update the new entity
      _context.Departments.Attach(department);
      _context.Entry(department).State = EntityState.Modified;
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


