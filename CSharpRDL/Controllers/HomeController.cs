﻿using CrystalDecisions.ReportAppServer.ReportDefModel;
using CSharpRDL.Models;
using CSharpRDL.MVCTestDataSetTableAdapters;
using CSharpRDL.ViewModel;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Security.Policy;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
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

            var AllEmp = db.EmployeeDetails.ToList();

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
            var department = db.departments.ToList();

            ViewBag.DepartmentList = new SelectList(department, "department_name", "department_name");

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
            //List<department> departments = new List<department>();
            //departments = db.departments.Where(c => c.IsActive == true).ToList();

            //ViewBag.DepartmentList = new SelectList(departments, "department_id", "department_name");

            var department = db.departments.ToList();

            ViewBag.DepartmentList = new SelectList(department, "department_id", "department_name");
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
                DateTime today = DateTime.Today;
                int countForm = db.EmployeeDetails
                    .Count(f => f.CreatedDate == today);

                int sequenceCount = countForm;
                sequenceCount++;
                string employeeId = "EMP" + DateTime.Now.ToString("yyyyMMdd") + sequenceCount.ToString("D3");

                int deptID = int.Parse(obj.Department);
                var selectedDept = db.departments.FirstOrDefault(c => c.department_id == deptID);


                string Filename = Path.GetFileName(imageFiles.FileName);
                string path = Path.Combine(Server.MapPath("~/Images/"), Filename);
                //Employee201file emp = new Employee201file();
                EmployeeDetail emp = new EmployeeDetail();

                emp.EmployeeID = employeeId;
                emp.Firstname = obj.Firstname;
                emp.Lastname = obj.Lastname;
                emp.Middlename = obj.Middlename;
                emp.Suffix = obj.Suffix;
                emp.Address = obj.Address;
                emp.Birthdate = obj.Birthdate;
                emp.Age = obj.Age;
                emp.Department = selectedDept.department_name;
                emp.ContactNo = obj.ContactNo;
                emp.ImagePath = path;
                emp.Gender = obj.Gender;
                emp.CreatedDate = DateTime.Today;

                imageFiles.SaveAs(Server.MapPath("~/Images/" + imageFiles.FileName));

                emp.ProfileImg = new byte[imageFiles.ContentLength];
                imageFiles.InputStream.Read(emp.ProfileImg, 0, imageFiles.ContentLength);

                //cus.user.IsActive = true;

                //db.Employee201file.Add(emp);
                UsersAccount user = new UsersAccount();
                var checkUser = db.UsersAccounts.FirstOrDefault(c => c.Username == obj.Username);
                if (checkUser != null)
                {
                    ViewBag.msg = "Username Already Exists !";

                    var dept1 = db.departments.ToList();
                    ViewBag.DepartmentList = new SelectList(dept1, "department_id", "department_name");

                    return View();
                }

                db.EmployeeDetails.Add(emp);
                db.SaveChanges();

                user.Username = obj.Username;
                user.Password = obj.Password;
                user.IsActive = true;
                user.Email = obj.Email;
                user.EmployeeID = emp.EmployeeID;
                db.UsersAccounts.Add(user);

                int count = db.SaveChanges();
                if (count > 0)
                {
                    TempData["msg"] = "<script>alert('Save Successfully !');</script>";
                    //int id = emp.EmployeeId;

                    return RedirectToAction("Dummy");
                }
                else
                {
                    ViewBag.msg = "Not Saved";
                }


            }
            var department = db.departments.ToList();
            ViewBag.DepartmentList = new SelectList(department, "department_id", "department_name");

            return View(obj);
        }
        [HttpGet]
        public ActionResult EditEmployee(int id)
        {
            //if (Session["Username"] == null)
            //{
            //    return RedirectToAction("Login", "Login");
            //}
            var employee = db.EmployeeDetails.Find(id);

            if (employee == null)
            {
                return HttpNotFound();
            }

            if (employee.ProfileImg != null)
            {
                employee.ImgBase64 = Convert.ToBase64String(employee.ProfileImg);
                employee.ImgUrl = string.Format("data:image/png;base64,{0}", employee.ImgBase64);
            }

            var departments = db.departments.Where(c => c.IsActive == true).ToList();
            var selectedDepartment = db.departments.FirstOrDefault(c => c.department_name == employee.Department);

            ViewBag.DeptList = new SelectList(departments, "department_name", "department_name", selectedDepartment?.department_name);

            return View("EditEmployee", employee);
        }
        [HttpPost]
        public ActionResult EditEmployee(EmployeeDetail emp, HttpPostedFileBase imageFiles)
        {
            if (ModelState.IsValid)
            {
                if (imageFiles != null && imageFiles.ContentLength > 0)
                {
                    string Filename = Path.GetFileName(imageFiles.FileName);
                    string path = Path.Combine(Server.MapPath("~/Images/"), Filename);

                    emp.ImagePath = path;

                    imageFiles.SaveAs(Server.MapPath("~/Images/" + imageFiles.FileName));

                    byte[] imageData = new byte[imageFiles.ContentLength];
                    imageFiles.InputStream.Read(imageData, 0, imageFiles.ContentLength);
                    emp.ProfileImg = imageData;

                }
                else
                {
                    string Url = emp.ImagePath;
                    byte[] convertedUrl;

                    WebClient w = new WebClient();
                    convertedUrl = w.DownloadData(Url);
                    emp.ProfileImg = convertedUrl;
                }

                emp.EditedDate = DateTime.Now;
                var selectedDept = db.departments.FirstOrDefault(c => c.department_name == emp.Department);
                emp.Department = selectedDept.department_name;


                db.Entry(emp).State = EntityState.Modified;
                db.SaveChanges();

                TempData["msg"] = "<script>alert('Updated Successfully !');</script>";
                return RedirectToAction("Dummy");

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