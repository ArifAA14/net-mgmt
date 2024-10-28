using Microsoft.AspNetCore.Mvc;
using EmployeeMgmt.Application.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using EmployeeMgmt.Domain.Entities;



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
      var employees = await _employeeService.GetAllEmployeesAsync();
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
        try
        {
          employee.HireDate = DateTime.SpecifyKind(employee.HireDate, DateTimeKind.Utc);
          await _employeeService.AddEmployeeAsync(employee);
          return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
          // Handle any other unexpected exceptions
          ModelState.AddModelError("", "This Email already exists.");
          Console.WriteLine(ex.Message); // Log the error
        }
      }

      Console.WriteLine("Errors during validation -");
      foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
      {
        Console.WriteLine(error.ErrorMessage);
      }

      // Reload the departments in case of an error
      ViewBag.Departments = new SelectList(await _departmentService.GetAllDepartmentsAsync(), "DepartmentId", "DepartmentName");
      return View(employee);  // Re-render the form with validation errors
    }



    // GET: Employee/Edit
    public async Task<IActionResult> Edit(int employeeId)
    {
      var employee = await _employeeService.GetEmployeeByIdAsync(employeeId);
      var departments = await _departmentService.GetAllDepartmentsAsync();
      ViewBag.Departments = new SelectList(departments, "DepartmentId", "DepartmentName");
      ViewBag.Employee = employee;

      if (employee == null)
      {
        return NotFound();
      }
      return View(employee);
    }


    // POST: Employee/Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int employeeId, Employee employee)
    {
      if (employeeId != employee.EmployeeId)
      {
        return BadRequest();
      }

      var existingEmployee = await _employeeService.GetEmployeeByIdAsync(employeeId);
      if (existingEmployee == null)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          employee.EmployeeCode = existingEmployee.EmployeeCode;
          employee.EmployeeId = existingEmployee.EmployeeId;
          employee.HireDate = DateTime.SpecifyKind(employee.HireDate, DateTimeKind.Utc);

          await _employeeService.UpdateEmployeeAsync(employee);
          return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
          ModelState.AddModelError("", ex.Message);
        }
      }

      foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
      {
        Console.WriteLine(error.ErrorMessage);
      }

      var departments = await _departmentService.GetAllDepartmentsAsync();
      ViewBag.Departments = new SelectList(departments, "DepartmentId", "DepartmentName");
      return View(employee);
    }

    // POST: Department/Delete
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int employeeId)
    {
      var employee = await _employeeService.GetEmployeeByIdAsync(employeeId);
      if (employee == null)
      {
        return NotFound();
      }
      await _employeeService.DeleteEmployeeAsync(employeeId);
      return RedirectToAction(nameof(Index));
    }

  }
}