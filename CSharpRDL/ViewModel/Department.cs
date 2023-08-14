using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CSharpRDL.ViewModel
{
    public class Department
    {
        public int department_id { get; set; }
        [Display(Name = "Department Name")]
        [Required(ErrorMessage = "Please enter Department Name")]
        public string department_name { get; set; }
        public Boolean IsActive { get; set; }
        [Display(Name = "Department Description")]
        [Required(ErrorMessage = "Please enter Department Description")]
        public string Department_Description { get; set; }
    }
}