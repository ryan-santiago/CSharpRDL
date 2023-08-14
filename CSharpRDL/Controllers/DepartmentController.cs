using CSharpRDL.Models;
using CSharpRDL.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CSharpRDL.Controllers
{
    public class DepartmentController : Controller
    {
        DBEntities db = new DBEntities();
        // GET: Department
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddDepartment()
        {
            //if (Session["Username"] == null)
            //{
            //    return RedirectToAction("Login", "Login");
            //}

            return View();
        }

        [HttpPost]
        public ActionResult AddDepartment(Department obj)
        {
            if (ModelState.IsValid)
            {
                department dept = new department();

                dept.department_name = obj.department_name;
                dept.Department_Description = obj.Department_Description;
                dept.IsActive = true;
                
                db.departments.Add(dept);

                int count = db.SaveChanges();
                if (count > 0)
                {
                    db.SaveChanges();
                    TempData["msg"] = "<script>alert('Save Successfully !');</script>";
                    //int id = emp.EmployeeId;

                    return RedirectToAction("SystemConfiguration","Home");
                }
                else
                {
                    ViewBag.msg = "Not Saved";
                }

            }

            return View(obj);
        }

        [HttpGet]
        public ActionResult EditDepartment(int id)
        {
            //if (Session["Username"] == null)
            //{
            //    return RedirectToAction("Login", "Login");
            //}
            var department = db.departments.Find(id);

            if (department == null)
            {
                return HttpNotFound();
            }

            var Department = new Department
            {
                department_id = department.department_id,
                department_name = department.department_name,
                Department_Description = department.Department_Description,
                IsActive = (bool)department.IsActive,
            };

            return View("EditDepartment", Department);
        }
        [HttpPost]
        public ActionResult EditDepartment(Department obj)
        {
            if (ModelState.IsValid)
            {
                department dept = new department();

                dept.department_id = obj.department_id;
                dept.department_name = obj.department_name;
                dept.Department_Description = obj.Department_Description;
                dept.IsActive = true;

                db.Entry(dept).State = EntityState.Modified;
                db.SaveChanges();

                TempData["msg"] = "<script>alert('Updated Successfully !');</script>";
                return RedirectToAction("SystemConfiguration","Home");

            }
            return View();
        }

        public ActionResult DeleteDepartment(int id)
        {
            var department = db.departments.FirstOrDefault(x => x.department_id == id);

            if (department == null)
            {
                return HttpNotFound();
            }

            department.IsActive = false;
            db.SaveChanges();

            TempData["msg"] = "<script>alert('Deleted Successfully !');</script>";
            return RedirectToAction("SystemConfiguration", "Home");
        }
    }
}