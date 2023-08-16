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
        public int UserID { get; set; }
        //[Required(ErrorMessage = "Please enter Username")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Display(Name = "Old Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter Old Password")]
        public string OldPassword { get; set; }

        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter New Password")]
        public string NewPassword { get; set; }
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Password did'nt match")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Email Address")]
        //[Required(ErrorMessage = "Please enter Email Address")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }
        public string EmployeeId { get; set; }
        public Boolean IsActive { get; set; }
    }
}