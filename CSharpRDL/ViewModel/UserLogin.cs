using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace CSharpRDL.ViewModel
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Please enter Username")]
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter Password")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password did'nt match")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Please enter Email Address")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}