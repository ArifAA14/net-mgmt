namespace EmployeeMgmt.Web.Models
{
  public class SearchViewModel
  {
    public string SearchTerm { get; set; }
    public int? DepartmentId { get; set; }
    public DateTime? HireDate { get; set; }  // Include HireDate for filtering
    public IEnumerable<EmployeeMgmt.Domain.Entities.Employee> Employees { get; set; }
  }
}