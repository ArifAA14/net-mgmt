namespace EmployeeMgmt.Application.DTOs
{
  public class EmployeePatchDTO
  {
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Department { get; set; }
    public DateTime? HireDate { get; set; }
  }
}
