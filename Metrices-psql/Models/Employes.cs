//using System.ComponentModel.DataAnnotations;

//namespace Metrices_psql.Models
//{
//    public class Employes
//    {


//            [Key]
//            public int EmployeeId { get; set; }

//            [Required]
//            public string EmployeeName { get; set; }

//            [Required]
//            public string Email { get; set; }


//            [Required]
//            public string Department { get; set; }



//            [Required]
//            [Range(0, int.MaxValue, ErrorMessage = "Salary must be a positive number.")]
//            public decimal Salary { get; set; } = 25000;
//        }

//    }

using System.ComponentModel.DataAnnotations;

namespace Metrices_psql.Models
{
    public class Employes
    {
        [Key]
        [Required]
        [RegularExpression(@"^\d+$", ErrorMessage = "EmployeeId should contain only digits.")]
        public int EmployeeId { get; set; }
 
        [Required]
        public string EmployeeName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required]
        [EnumDataType(typeof(Department), ErrorMessage = "Invalid department value.")]
        public string Department { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive number.")]
        public decimal Salary { get; set; }
    }

    public enum Department
    {
        HR,
        DEV,
        TESTING,
        MANAGEMENT
        // Add other department values as needed
    }
}

