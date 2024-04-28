using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class AddEmployeeForm
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int CivilId { get; set; }
       
        [Required]
        public string Position { get; set; }

    }
}
