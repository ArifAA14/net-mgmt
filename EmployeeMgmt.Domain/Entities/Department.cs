namespace EmployeeMgmt.Domain.Entities
{
  public class Department {
    public int DepartmentId { get; set; }
    public required string DepartmentName { get; set; }

    public string? Description { get; set; }


    public ICollection<Employee> Employees { get; set; } = new List<Employee>();

  }
}