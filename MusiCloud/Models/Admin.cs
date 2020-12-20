using System;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel;

namespace MusiCloud.Models
{
    public class Admin
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

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

        public string UserType { get; set; }
    }
}
