using System;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel;


namespace MusiCloud.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password must be at least 4 characters")]
        [MinLength(4, ErrorMessage = "Password must be at least 4 characters")]
        [DisplayName("DisplayName")]
        public string DisplayName { get; set; }

        [DisplayName("Password")]
        [Required]
        [MinLength(8, ErrorMessage = "Minimum 8 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // Compare between both passwords
        [DisplayName("ConfirmPassword")]
        [Required]
        [MinLength(8, ErrorMessage = "Minimum 8 characters")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords must match!")]
        public string ConfirmPassword { get; set; }
    }
}
