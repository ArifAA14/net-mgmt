using EmployeeMgmt.Domain.Entities;

namespace EmployeeMgmt.Infrastructure.Repositories
{
  public interface IDepartmentRepository
  {
    Task<IEnumerable<Department>> GetAllAsync();
    Task<Department> GetByIdAsync(int departmentId);
    Task AddAsync(Department department);
    Task UpdateAsync(Department department);
    Task DeleteAsync(int departmentId);
  }
}