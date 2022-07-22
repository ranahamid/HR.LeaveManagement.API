using System.ComponentModel.DataAnnotations;

namespace HR.LeaveManagement.MVC.Models
{
    public class LoginVm
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(dataType:DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
