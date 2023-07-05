using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace CSharpRDL.Models
{
    public class Employee
    {
        [Editable(false)]

        public int EmployeeId { get; set; }
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
        public string Department { get; set; }
        public byte[] ProfileImg { get; set; }

        public string ImagePath { get; set; }
    }
}