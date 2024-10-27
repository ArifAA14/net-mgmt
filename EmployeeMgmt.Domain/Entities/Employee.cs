using System.ComponentModel.DataAnnotations;

namespace EmployeeMgmt.Domain.Entities
{
  public class Employee
  {
    public int EmployeeId { get; set; }

    public string? EmployeeCode { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; }  

    [Required(ErrorMessage = "Department is required")]
    public int DepartmentId { get; set; }

    public Department? Department { get; set; }

    public DateTime HireDate { get; set; }
  }
}
