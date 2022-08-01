using System.ComponentModel.DataAnnotations;

namespace net5test.Models.Auth
{
    public class LoginModel
    {
        [Required( ErrorMessageResourceName = "EmailRequired")]
        [EmailAddress( ErrorMessageResourceName = "EmailFormatError")]
        [Display( Name = "Email")]
        public string Email { get; set; }

        [Required( ErrorMessageResourceName = "PasswordRequired")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public long? RegionId { get; set; }

        public string Birthday { get; set; }
    }
}