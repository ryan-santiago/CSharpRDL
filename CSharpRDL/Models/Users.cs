using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace CSharpRDL.Models
{
    public class Users
    {
        public int UserId { get; set; }
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Email { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}