using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace CSharpRDL.ViewModel
{
    public class Register
    {
        //public string EmployeeID { get; set; }
        [Display(Name = "Firstname")]
        [Required(ErrorMessage = "Please enter firstname")]
        public string Firstname { get; set; }
        [Display(Name = "Lastname")]
        [Required(ErrorMessage = "Please enter lastname")]
        public string Lastname { get; set; }
        [Display(Name = "Middle Name")]
        [Required(ErrorMessage = "Please enter middlename")]
        public string Middlename { get; set; }
        [Display(Name = "Suffix")]

        public string Suffix { get; set; }
        [Required(ErrorMessage = "Please enter Contact No")]
        [Display(Name = "Contact Number")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Invalid contact number.")]
        public string ContactNo { get; set; }
        [Required(ErrorMessage = "Please enter Address")]
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please enter Birthday")]
        [Display(Name = "Birthdate")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public string Birthdate { get; set; }
        [Editable(false)]

        public Nullable<int> Age { get; set; }
        [Display(Name = "Department")]
        [Required(ErrorMessage = "Please Select Department")]
        public string Department { get; set; }
        public byte[] ProfileImg { get; set; }

        public string ImagePath { get; set; }
        [Required(ErrorMessage = "Please Select Gender")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

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
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }

    }
}