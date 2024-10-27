using Microsoft.AspNetCore.Mvc;
using EmployeeMgmt.Application.Services;
using EmployeeMgmt.Domain.Entities;

namespace EmployeeMgmt.Web.Controllers {
  public class DepartmentController : Controller
  {
    private readonly IDepartmentService _departmentService;
    private readonly IEmployeeService _employeeService;

    public DepartmentController(IDepartmentService departmentService, IEmployeeService employeeService)
    {
      _departmentService = departmentService;
      _employeeService = employeeService;
    }

    public async Task<IActionResult> Index()
    {
      var departments = await _departmentService.GetAllDepartmentsAsync();
      return View(departments);
    }


    // GET: Department/Create
    public IActionResult Create()
    {
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Department department)
    {
      try
      {
        if (ModelState.IsValid)
        {
          await _departmentService.AddDepartmentAsync(department);
          return RedirectToAction(nameof(Index));
        }
      }
      catch (InvalidOperationException ex)
      {
        ModelState.AddModelError("DepartmentName", ex.Message);
      }
      return View(department);
    }


    public async Task<IActionResult> ViewEmployees(int departmentId)
    {
      var department = await _departmentService.GetDepartmentByIdAsync(departmentId);
      if (department == null)
      {
        return NotFound();
      }
      var employees = await _employeeService.GetEmployeesByDepartmentIdAsync(departmentId);
      ViewBag.Department = department;
      return View(employees);
    }
  }
}