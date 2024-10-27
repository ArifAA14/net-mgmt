using Microsoft.AspNetCore.Mvc;
using EmployeeMgmt.Application.Services;  // Assuming you have EmployeeService in Application layer

namespace EmployeeMgmt.Web.Controllers {
  public class DepartmentController : Controller
  {
    private readonly IDepartmentService _departmentService;

    public DepartmentController(IDepartmentService departmentService) {
      _departmentService = departmentService;
    }

    public async Task<IActionResult> Index() {
      var departments = await _departmentService.GetAllDepartmentsAsync(); // Fetch all departments from app layer
      return View(departments);  
    }
  }
}