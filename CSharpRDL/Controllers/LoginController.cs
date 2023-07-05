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
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                var cred = db.UsersAccount.Where(model => model.Username == user.Username && model.Password == user.Password && model.IsActive == true).FirstOrDefault();
                if (cred != null)
                {
                    Session["Username"] = user.Username;
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