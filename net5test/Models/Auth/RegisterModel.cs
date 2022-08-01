using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace net5test.Models.Auth
{
    public class RegisterModel
    {

        [Required( ErrorMessageResourceName = "EmailRequired")]
        [EmailAddress( ErrorMessageResourceName = "EmailFormatError")]
        [Display( Name = "Email")]
        public string Email_Reg { get; set; }

        [Required( ErrorMessageResourceName = "PasswordRequired")]
        [DataType(DataType.Password)]
        [Display( Name = "Password")]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)[a-zA-Z0-9]{6,12}$", ErrorMessageResourceName = "PasswordFormatError")]
        public string Password_Reg { get; set; }

        [Required( ErrorMessageResourceName = "ConfirmPasswordRequired")]
        [DataType(DataType.Password)]
        [Display( Name = "ConfirmPassword")]
        [Compare("Password_Reg", ErrorMessageResourceName = "ConfirmationPasswordError")]

        public string ConfirmPassword { get; set; }


        [Required(ErrorMessageResourceName = "NameRequired")]
        [Display( Name = "Name")]
        public string Name { get; set; }
        [Required( ErrorMessageResourceName = "BirthdayRequired")]
        [DataType(DataType.Date)]
        [Display( Name = "Birthday")]
        public string Birthday { get; set; }
    }
    

}