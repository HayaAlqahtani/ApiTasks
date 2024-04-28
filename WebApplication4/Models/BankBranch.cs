
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class BankBranch
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string BranchManager { get; set; }
        public int EmployeeCount { get; set; }
        public int Id { get; internal set; }
        public List<Employee> Employees { get; set; } = new List<Employee>();

    }
}
