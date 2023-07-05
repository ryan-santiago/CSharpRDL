﻿using CSharpRDL.Models;
using CSharpRDL.MVCTestDataSetTableAdapters;
using CSharpRDL.ViewModel;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using static System.Net.WebRequestMethods;

namespace CSharpRDL.Controllers
{
    public class HomeController : Controller
    {
        DBEntities db = new DBEntities();
        // GET: Home
        public ActionResult Index()
        {
            //if (Session["Username"] == null)
            //{
            //    return RedirectToAction("Login", "Login");
            //}
            return View();
        }

        public ActionResult Dummy()
        {
            //if (Session["Username"] == null)
            //{
            //    return RedirectToAction("Login", "Login");
            //}

            var AllEmp = db.Employee201file.ToList();

            return View(AllEmp);
        }

        public ActionResult BIR_BCS()
        {
            //if (Session["Username"] == null)
            //{
            //    return RedirectToAction("Login", "Login");
            //}
            return View();
        }

        public ActionResult ReportGenerator()
        {
            //if (Session["Username"] == null)
            //{
            //    return RedirectToAction("Login", "Login");
            //}

            //var employee = db.Employee201file.ToList();

            //LocalReport report = new LocalReport();
            //report.ReportPath = Server.MapPath("~/Reports/Report.rdlc");
            //report.DataSources.Add(new ReportDataSource("Employee", employee));

            //ViewBag.ReportViewer = report;


            return View();


        }

        public ActionResult Loader()
        {
            //if (Session["Username"] == null)
            //{
            //    return RedirectToAction("Login", "Login");
            //}
            return View();
        }

        public ActionResult SystemConfiguration()
        {
            //if (Session["Username"] == null)
            //{
            //    return RedirectToAction("Login", "Login");
            //}
            return View();
        }

        public ActionResult EmployeeReport()
        {
            //if (Session["Username"] == null)
            //{
            //    return RedirectToAction("Login", "Login");
            //}
            return View();
        }

        public ActionResult AddEmployee()
        {
            //if (Session["Username"] == null)
            //{
            //    return RedirectToAction("Login", "Login");
            //}
            ViewBag.department = db.departments.ToList();

            return View();
        }

        public ActionResult ViewDetails()
        {
            //if (Session["Username"] == null)
            //{
            //    return RedirectToAction("Login", "Login");
            //}
            return View();
        }

        [HttpPost]
        public ActionResult AddEmployee(Register obj, HttpPostedFileBase imageFiles)
        {
            if (ModelState.IsValid)
            {
                string Filename = Path.GetFileName(imageFiles.FileName);
                string path = Path.Combine(Server.MapPath("~/Images/"), Filename);
                Employee201file emp = new Employee201file();
                

                emp.Firstname = obj.Firstname;
                emp.Lastname = obj.Lastname;
                emp.Middlename = obj.Middlename;
                emp.Suffix = obj.Suffix;
                emp.Address = obj.Address;
                emp.Birthdate = obj.Birthdate;
                emp.Age = obj.Age;
                emp.Department = obj.Department;
                emp.ContactNo = obj.ContactNo;
                emp.ImagePath = path;

                imageFiles.SaveAs(Server.MapPath("~/Images/" + imageFiles.FileName));

                emp.ProfileImg = new byte[imageFiles.ContentLength];
                imageFiles.InputStream.Read(emp.ProfileImg, 0, imageFiles.ContentLength);

                //cus.user.IsActive = true;

                db.Employee201file.Add(emp);
                db.SaveChanges();

                User u = new User();
                u.Username = obj.Username;
                u.Password = obj.Password;
                u.IsActive = true;
                u.Email = obj.Email;
                u.EmployeeId = emp.EmployeeId;
                db.UsersAccount.Add(u);

                int count = db.SaveChanges();
                if (count > 0)
                {
                    TempData["msg"] = "<script>alert('Save Successfully !');</script>";
                    int id = emp.EmployeeId;

                    return RedirectToAction("Dummy");
                }
                else
                {
                    ViewBag.msg = "Not Saved";
                }


            }

            return View(obj);
        }
        [HttpGet]
        public ActionResult EditEmployee(int id)
        {
            //if (Session["Username"] == null)
            //{
            //    return RedirectToAction("Login", "Login");
            //}
            var Employee = db.Employee201file.Find(id);
            if (Employee == null)
            {
                //return View();
                return HttpNotFound();

            }
            return View("EditEmployee", Employee);
        }
        [HttpPost]
        public ActionResult EditEmployee(Employee201file emp, HttpPostedFileBase imageFiles)
        {
            if (ModelState.IsValid)
            {
                string Filename = Path.GetFileName(imageFiles.FileName);
                string path = Path.Combine(Server.MapPath("~/Images/"), Filename);

                emp.ImagePath = path;

                imageFiles.SaveAs(Server.MapPath("~/Images/" + imageFiles.FileName));

                emp.ProfileImg = new byte[imageFiles.ContentLength];
                imageFiles.InputStream.Read(emp.ProfileImg, 0, imageFiles.ContentLength);
                db.Entry(emp).State = EntityState.Modified;
                db.SaveChanges();
                int count = db.SaveChanges();
                if (count > 0)
                {
                    TempData["msg"] = "<script>alert('Updated Successfully !');</script>";
                    int id = emp.EmployeeId;

                    return RedirectToAction("Dummy");
                }
                else
                {
                    ViewBag.msg = "Not Saved";
                }

            }
            return View();
        }

        public ActionResult EmpReport()
        {
            //db.Entry(emp).State = EntityState.Modified;
            //db.SaveChanges();
            return RedirectToAction("Dummy");
        }

        public ActionResult TEST()
        {
            //db.Entry(emp).State = EntityState.Modified;
            //db.SaveChanges();
            return RedirectToAction("Dummy");
        }
    }
}