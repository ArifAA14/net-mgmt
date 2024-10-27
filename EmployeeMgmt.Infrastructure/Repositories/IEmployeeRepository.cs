using EmployeeMgmt.Domain.Entities;

namespace EmployeeMgmt.Infrastructure.Repositories
{
  public interface IEmployeeRepository
  {
    Task<IEnumerable<Employee>> GetAllAsync();
    Task<Employee> GetByIdAsync(int employeeId);
    Task AddAsync(Employee employee);
    Task UpdateAsync(Employee employee);
    Task DeleteAsync(int employeeId);
  }
}

