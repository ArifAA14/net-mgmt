using EmployeeMgmt.Application.Services;
using EmployeeMgmt.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using EmployeeMgmt.Domain.Entities;
using Microsoft.Extensions.Logging;


namespace EmployeeMgmt.API.Controllers
{
  [ApiController]
  [Route("department")]
  public class DepartmentController : ControllerBase
  {
    private readonly IDepartmentService _departmentService;

    public DepartmentController(IDepartmentService departmentService)
    {
      _departmentService = departmentService;
    }


    // GET: /department
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DepartmentDTO>>> GetAllDepartments()
    {
      var departments = await _departmentService.GetAllDepartmentsAsync();
      var departmentDTOs = departments.Select(d => new DepartmentDTO
      {
        DepartmentId = d.DepartmentId,
        DepartmentName = d.DepartmentName,
        Description = d.Description
      }).ToList();
      return Ok(departmentDTOs);
    }

    // POST /department for creating a new department
    [HttpPost]
    public async Task<ActionResult<DepartmentDTO>> CreateDepartment([FromBody] DepartmentDTO departmentDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var department = new Department
      {
        DepartmentName = departmentDto.DepartmentName,
        Description = departmentDto.Description
      };

      await _departmentService.AddDepartmentAsync(department);

      departmentDto.DepartmentId = department.DepartmentId;
      return CreatedAtAction(nameof(GetAllDepartments), new { id = department.DepartmentId }, departmentDto);
    }


    // PATCH: /department/{id}
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateDepartment(int id, [FromBody] DepartmentDTO departmentDto)
    {
      var existingDepartment = await _departmentService.GetDepartmentByIdAsync(id);
      if (existingDepartment == null)
      {
        return NotFound("Department not found.");
      }

      // Only update fields provided in DepartmentPatchDTO
      if (!string.IsNullOrEmpty(departmentDto.DepartmentName))
      {
        existingDepartment.DepartmentName = departmentDto.DepartmentName;
      }

      if (!string.IsNullOrEmpty(departmentDto.Description))
      {
        existingDepartment.Description = departmentDto.Description;
      }

      await _departmentService.UpdateDepartmentAsync(existingDepartment);

      return NoContent();
    }


    // DELETE /department/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDepartment(int id)
    {
      await _departmentService.DeleteDepartmentAsync(id);
      return NoContent();
    }

  }
}