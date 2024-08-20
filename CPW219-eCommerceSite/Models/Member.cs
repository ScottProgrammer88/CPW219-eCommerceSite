using System.ComponentModel.DataAnnotations;

namespace CPW219_eCommerceSite.Models
{
    public class Member
    {
        [Key]
        public int MemberId { get; set; }
        
        public string Email { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        public string Username { get; set; }
    }

    public class RegisterViewModel  //This class is used to validate the data entered by the user in the registration form, but it does not store the data in the database.
    {
        [Required]
        [EmailAddress] //This attribute ensures that the email address entered by the user is in a valid format.
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [Compare(nameof (Email))] //This attribute ensures that the email address entered in the ConfirmEmail field matches the email address entered in the Email field.
        [Display(Name = "Confirm Email")]
        public string ConfirmEmail { get; set; }

        [Required]
        [DataType(DataType.Password)] //This attribute specifies that the Password field should be treated as a password field, which means that the characters entered by the user will be masked.
        [StringLength(75, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof (Password))] //This attribute ensures that the password entered in the ConfirmPassword field matches the password entered in the Password field.
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}


