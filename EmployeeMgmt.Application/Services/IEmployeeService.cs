using EmployeeMgmt.Domain.Entities;

namespace EmployeeMgmt.Application.Services {
  public interface IEmployeeService { 

    Task<IEnumerable<Employee>> GetAllEmployeesAsync();
    Task<Employee> GetEmployeeByIdAsync(int employeeId);
    Task AddEmployeeAsync(Employee employee);
    Task UpdateEmployeeAsync(Employee employee);
    Task DeleteEmployeeAsync(int employeeId);
    Task<IEnumerable<Employee>> GetEmployeesByDepartmentIdAsync(int departmentId);
  }
}