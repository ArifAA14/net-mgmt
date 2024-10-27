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


    // GET: Department/Edit
    public async Task<IActionResult> Edit(int departmentId)
    {
      var department = await _departmentService.GetDepartmentByIdAsync(departmentId);
      if (department == null)
      {
        return NotFound();
      }
      return View(department);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int departmentId, Department department)
    {
      if (departmentId != department.DepartmentId)
      {
        return BadRequest();
      }

      if (ModelState.IsValid)
      {
        try
        {
          await _departmentService.UpdateDepartmentAsync(department);
          return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
          ModelState.AddModelError("DepartmentName", ex.Message);
        }
      }
      return View(department);
    }


    // POST: Department/Delete
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int departmentId)
    {
      var department = await _departmentService.GetDepartmentByIdAsync(departmentId);
      if (department == null)
      {
        return NotFound();
      }
      await _departmentService.DeleteDepartmentAsync(departmentId);
      return RedirectToAction(nameof(Index));
    }
  }
}