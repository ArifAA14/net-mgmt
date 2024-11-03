using EmployeeMgmt.Domain.Entities;
using EmployeeMgmt.Infrastructure.Repositories;

namespace EmployeeMgmt.Application.Services
{
  public class EmployeeService : IEmployeeService
  {
    private readonly IEmployeeRepository _employeeRepository;
    private readonly INotificationService _notificationService;

    public EmployeeService(IEmployeeRepository employeeRepository, INotificationService notificationService)
    {
      _employeeRepository = employeeRepository;
      _notificationService = notificationService;
    }

    public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
    {
      return await _employeeRepository.GetAllAsync();
    }

    public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
    {
      return await _employeeRepository.GetByIdAsync(employeeId);
    }

    

    public async Task AddEmployeeAsync(Employee employee)
    {
      employee.EmployeeCode = GenerateEmployeeCode();
      await _employeeRepository.AddAsync(employee);
      _notificationService.SendNotification($"Employee {employee.Name} added successfully.");
    }

    private static string GenerateEmployeeCode()
    {
      Random random = new Random();
      int randomNumber = random.Next(1000, 9999);
      return $"EMP{randomNumber:D3}";
    }

    public async Task UpdateEmployeeAsync(Employee employee)
    {
      var existingEmployee = await _employeeRepository.GetByIdAsync(employee.EmployeeId);

      if (existingEmployee == null)
      {
        throw new InvalidOperationException("Employee not found.");
      }

      existingEmployee.Name = employee.Name;
      existingEmployee.Email = employee.Email;
      existingEmployee.HireDate = employee.HireDate;
      existingEmployee.DepartmentId = employee.DepartmentId;

      await _employeeRepository.UpdateAsync(existingEmployee);
    }

    public async Task DeleteEmployeeAsync(int employeeId)
    {
      await _employeeRepository.DeleteAsync(employeeId);
    }

    public async Task<IEnumerable<Employee>> GetEmployeesByDepartmentIdAsync(int departmentId)
    {
      var employees = await _employeeRepository.GetAllAsync();
      return employees.Where(e => e.DepartmentId == departmentId);
    }
  }
}
