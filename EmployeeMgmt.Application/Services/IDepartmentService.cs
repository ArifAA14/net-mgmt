using EmployeeMgmt.Domain.Entities;

namespace EmployeeMgmt.Application.Services {
  public interface IDepartmentService {
    Task<IEnumerable<Department>> GetAllDepartmentsAsync();
    Task<Department> GetDepartmentByIdAsync(int departmentId);
    Task AddDepartmentAsync(Department department);
    Task UpdateDepartmentAsync(Department department);
    Task DeleteDepartmentAsync(int departmentId);
  }
}