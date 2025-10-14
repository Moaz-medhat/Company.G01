using System.ComponentModel.DataAnnotations;

namespace Company.G01.PL.Dtos
{
    public class ResetPasswordDto
    {
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]

        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "Confirm Password does not match the password")]
        public string ConfirmPassword { get; set; }
    }
}
