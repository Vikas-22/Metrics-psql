using System.ComponentModel.DataAnnotations;

namespace Metrices_psql.Models
{
    public class Employes
    {
        [Key] public int EmployeeId { get; set; }

        [Required] 
        public string EmployeeName{ get; set; }

        [Required]
        public string Email { get; set; }
    }
}
