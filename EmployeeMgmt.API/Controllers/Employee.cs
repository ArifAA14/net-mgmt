using EmployeeMgmt.Application.Services;
using EmployeeMgmt.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using EmployeeMgmt.Domain.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeMgmt.API.Controllers
{
  [ApiController]
  [Route("employee")]
  public class EmployeeController : ControllerBase
  {
    private readonly IEmployeeService _employeeService;
    private readonly IDepartmentService _departmentService;
    private readonly ILogger<EmployeeController> _logger;


    public EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService, ILogger<EmployeeController> logger)
    {
      _employeeService = employeeService;
      _departmentService = departmentService;
      _logger = logger;

    }

    // GET: /employee
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetAllEmployees(int page = 1, int pageSize = 10)
    {
      var employees = await _employeeService.GetAllEmployeesAsync();

      var paginatedEmployees = employees
          .Skip((page - 1) * pageSize)
          .Take(pageSize)
          .ToList();

      var employeeDTOs = paginatedEmployees.Select(e => new EmployeeDTO
      {
        EmployeeId = e.EmployeeId,
        EmployeeCode = e.EmployeeCode,
        Name = e.Name,
        Email = e.Email,
        Department = e.Department?.DepartmentName,
        HireDate = e.HireDate
      }).ToList();


      var totalEmployees = employees.Count();
      var metadata = new
      {
        TotalItems = totalEmployees,
        PageSize = pageSize,
        CurrentPage = page,
        TotalPages = (int)Math.Ceiling(totalEmployees / (double)pageSize)
      };

      Response.Headers.Append("X-Pagination", System.Text.Json.JsonSerializer.Serialize(metadata));

      return Ok(employeeDTOs);
    }


    // POST /employee for creating a new employee
    [HttpPost]
    public async Task<ActionResult<EmployeeDTO>> CreateEmployee([FromBody] EmployeeDTO employeeDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var department = await _departmentService.GetDepartmentByNameAsync(employeeDto?.Department);
      if (department == null)
      {
        return BadRequest("Invalid department specified.");
      }

      var employee = new Employee
      {
        Name = employeeDto.Name,
        Email = employeeDto.Email,
        DepartmentId = department.DepartmentId,
        HireDate = (DateTime)employeeDto.HireDate
      };

      await _employeeService.AddEmployeeAsync(employee);

      employeeDto.EmployeeId = employee.EmployeeId;
      employeeDto.EmployeeCode = employee.EmployeeCode;
      return CreatedAtAction(nameof(GetAllEmployees), new { id = employee.EmployeeId }, employeeDto);
    }


    // PATCH: /employee/{id}
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeePatchDTO employeeDto)
    {
      var existingEmployee = await _employeeService.GetEmployeeByIdAsync(id);
      if (existingEmployee == null)
      {
        return NotFound("Employee not found.");
      }

      // Only update fields provided in EmployeePatchDTO
      if (!string.IsNullOrEmpty(employeeDto.Name))
      {
        existingEmployee.Name = employeeDto.Name;
      }

      if (!string.IsNullOrEmpty(employeeDto.Email))
      {
        existingEmployee.Email = employeeDto.Email;
      }

      if (!string.IsNullOrEmpty(employeeDto.Department))
      {
        var department = await _departmentService.GetDepartmentByNameAsync(employeeDto.Department);
        if (department != null)
        {
          existingEmployee.DepartmentId = department.DepartmentId;
        }
        else
        {
          return BadRequest("Invalid department specified.");
        }
      }

      if (employeeDto.HireDate.HasValue)
      {
        existingEmployee.HireDate = employeeDto.HireDate.Value;
      }

      await _employeeService.UpdateEmployeeAsync(existingEmployee);

      return NoContent();
    }


    // DELETE /employee/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
      await _employeeService.DeleteEmployeeAsync(id);
      return NoContent();
    }
  }
}
