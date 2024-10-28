using Microsoft.AspNetCore.Mvc;
using EmployeeMgmt.Application.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
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

      if (!string.IsNullOrEmpty(searchTerm))
      {
        employees = employees.Where(e => e.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
      }

      if (departmentId.HasValue && departmentId.Value > 0)
      {
        employees = employees.Where(e => e.DepartmentId == departmentId);
      }

      if (HireDate.HasValue)
      {
        var hireDateOnly = HireDate.Value.Date;

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
