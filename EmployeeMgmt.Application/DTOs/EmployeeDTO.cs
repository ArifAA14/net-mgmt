using System.ComponentModel.DataAnnotations;

namespace EmployeeMgmt.Application.DTOs
{
  public class EmployeeDTO
  {
    public int EmployeeId { get; set; }
    public string? EmployeeCode { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Department is required.")]
    public string? Department { get; set; }

    [Required(ErrorMessage = "Hire Date is required.")]
    public DateTime? HireDate { get; set; }
  }
}