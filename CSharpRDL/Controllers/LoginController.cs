using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CSharpRDL.Models;

namespace CSharpRDL.Controllers
{
    public class LoginController : Controller
    {
        DBEntities db = new DBEntities();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UsersAccount user)
        {
            if (ModelState.IsValid)
            {
                var cred = db.UsersAccounts.Where(model => model.Username == user.Username && model.Password == user.Password && model.IsActive == true).FirstOrDefault();
                if (cred != null)
                {
                    var EmployeeID = cred.EmployeeID;
                    var details = db.EmployeeDetails.Where(e => e.EmployeeID == EmployeeID).FirstOrDefault();
                    Session["Firstname"] = details.Firstname;
                    Session["Lastname"] = details.Lastname;
                    Session["Middlename"] = details.Middlename;
                    Session["Username"] = user.Username;
                    Session["ProfileImg"] = details.ProfileImg;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorMessage = "Login Failed!";
                    return View();
                }
            }
            return View();
        }
    }
}