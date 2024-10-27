using EmployeeMgmt.Domain.Entities;
using EmployeeMgmt.Infrastructure.Repositories;

namespace EmployeeMgmt.Application.Services
{
  public class DepartmentService : IDepartmentService
  {
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentService(IDepartmentRepository departmentRepository)
    {
      _departmentRepository = departmentRepository;
    }

    public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
    {
      return await _departmentRepository.GetAllAsync();
    }

    public async Task<Department> GetDepartmentByIdAsync(int departmentId)
    {
      return await _departmentRepository.GetByIdAsync(departmentId);
    }

    public async Task AddDepartmentAsync(Department department)
    {
      await _departmentRepository.AddAsync(department);
    }

    public async Task UpdateDepartmentAsync(Department department)
    {
      await _departmentRepository.UpdateAsync(department);
    }

    public async Task DeleteDepartmentAsync(int departmentId)
    {
      await _departmentRepository.DeleteAsync(departmentId);
    }
  }
}