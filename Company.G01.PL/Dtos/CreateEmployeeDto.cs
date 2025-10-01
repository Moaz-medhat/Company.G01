using Company.G01.DAL.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Company.G01.PL.Dtos
{
    public class CreateEmployeeDto
    {
        [Required(ErrorMessage ="Name is Required")]
        public string Name { get; set; }
        [Range(20,80, ErrorMessage ="Age must be between 20 & 80 ")]
        public int? Age { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Adress is Required")]
        public string Adress { get; set; }
        [Phone]
        public string Phone { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        [DisplayName("Hiring date")]
        public DateTime HiringDate { get; set; }
        [DisplayName("Date of create")]

        public DateTime CreateAt { get; set; }
        [DisplayName("Department")]

        public int? departmentId { get; set; }



    }
}
