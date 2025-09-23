using System.ComponentModel.DataAnnotations;

namespace Company.G01.PL.Dtos
{
    public class CreatedepartmentDto
    {
        [Required(ErrorMessage ="Code is required !")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Name is required !")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Date is required !")]
        public DateTime CreateAt { get; set; }
    }
}
