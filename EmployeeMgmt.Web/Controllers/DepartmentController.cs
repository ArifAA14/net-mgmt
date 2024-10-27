using Microsoft.AspNetCore.Mvc;
using EmployeeMgmt.Application.Services;
using EmployeeMgmt.Domain.Entities;

namespace EmployeeMgmt.Web.Controllers {
  public class DepartmentController : Controller
  {
    private readonly IDepartmentService _departmentService;

    public DepartmentController(IDepartmentService departmentService)
    {
      _departmentService = departmentService;
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
  }
}