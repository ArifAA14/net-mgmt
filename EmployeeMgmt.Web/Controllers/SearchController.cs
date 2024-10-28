using Microsoft.AspNetCore.Mvc;
using EmployeeMgmt.Application.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using EmployeeMgmt.Domain.Entities;
using EmployeeMgmt.Web.Models;


namespace EmployeeMgmt.Web.Controllers
{
  public class SearchController : Controller
  {
    private readonly IEmployeeService _employeeService;
    private readonly IDepartmentService _departmentService;

    public SearchController(IEmployeeService employeeService, IDepartmentService departmentService)
    {
      _employeeService = employeeService;
      _departmentService = departmentService;
    }

    public async Task<IActionResult> Index(string searchTerm, int? departmentId, DateTime? HireDate)
    {
      var employees = await _employeeService.GetAllEmployeesAsync();
      var departments = await _departmentService.GetAllDepartmentsAsync();

      // Filter employees based on the search term
      if (!string.IsNullOrEmpty(searchTerm))
      {
        employees = employees.Where(e => e.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
      }

      // Filter employees by department if a department is selected
      if (departmentId.HasValue && departmentId.Value > 0)
      {
        employees = employees.Where(e => e.DepartmentId == departmentId);
      }

      if (HireDate.HasValue)
      {
        var hireDateOnly = HireDate.Value.Date; // Extract just the date part (local date from the form)
        Console.WriteLine($"Searching for employees hired on: {hireDateOnly}");

        foreach (var employee in employees)
        {
          Console.WriteLine($"Employee: {employee.Name}, Hire Date in DB: {employee.HireDate}, Form Date: {hireDateOnly}");

          // Compare only the year, month, and day, ignoring time zones and time components
          if (employee.HireDate.Year == hireDateOnly.Year &&
              employee.HireDate.Month == hireDateOnly.Month &&
              employee.HireDate.Day == hireDateOnly.Day)
          {
            Console.WriteLine($"Match found for employee: {employee.Name}");
          }
        }

        employees = employees.Where(e =>
            e.HireDate.Year == hireDateOnly.Year &&
            e.HireDate.Month == hireDateOnly.Month &&
            e.HireDate.Day == hireDateOnly.Day);
      }

      var viewModel = new SearchViewModel
      {
        SearchTerm = searchTerm,
        DepartmentId = departmentId,
        Employees = employees,
        HireDate = HireDate
      };

      ViewBag.Departments = new SelectList(departments, "DepartmentId", "DepartmentName", null);
      return View(viewModel);
    }




  }
}
