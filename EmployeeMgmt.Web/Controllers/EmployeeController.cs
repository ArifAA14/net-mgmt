using Microsoft.AspNetCore.Mvc;
using EmployeeMgmt.Application.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using EmployeeMgmt.Domain.Entities;
using EmployeeMgmt.Domain.ValueObjects;  // Assuming you have EmployeeService in Application layer


namespace EmployeeMgmt.Web.Controllers
{
  public class EmployeeController : Controller
  {
    private readonly IEmployeeService _employeeService;
    private readonly IDepartmentService _departmentService;

    public EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService)
    {
      _employeeService = employeeService;
      _departmentService = departmentService;
    }

    public async Task<IActionResult> Index()
    {
      var employees = await _employeeService.GetAllEmployeesAsync(); // Fetch all employees from app layer
      return View(employees);  
    }

    // GET: Employee/Create
    public async Task<IActionResult> Create()
    {
      var departments = await _departmentService.GetAllDepartmentsAsync();
      ViewBag.Departments = new SelectList(departments, "DepartmentId", "DepartmentName");
      return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Employee employee)
    {
      if (ModelState.IsValid)
      {
        // Convert HireDate to UTC before saving
        employee.HireDate = DateTime.SpecifyKind(employee.HireDate, DateTimeKind.Utc);
        await _employeeService.AddEmployeeAsync(employee);
        return RedirectToAction(nameof(Index));
      }

      // Log validation errors if any
      Console.WriteLine("Errors during validation -");
      foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
      {
        Console.WriteLine(error.ErrorMessage);
      }

      ViewBag.Departments = new SelectList(await _departmentService.GetAllDepartmentsAsync(), "DepartmentId", "DepartmentName");
      return View(employee);
    }
  }
}