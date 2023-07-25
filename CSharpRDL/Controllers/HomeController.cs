using CrystalDecisions.ReportAppServer.ReportDefModel;
using CSharpRDL.Models;
using CSharpRDL.MVCTestDataSetTableAdapters;
using CSharpRDL.ViewModel;
using iTextSharp.text;
using iTextSharp.text.pdf;
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

        public ActionResult ViewDetails(int id)
        {
            //if (Session["Username"] == null)
            //{
            //    return RedirectToAction("Login", "Login");
            //}
            var employeeDetails = db.EmployeeDetails.Find(id);
            return View(employeeDetails);
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

                HashPassword hashPassword = new HashPassword();
                //hashPassword.Password(obj.Password); 

                user.Username = obj.Username;
                //user.Password = obj.Password;
                user.Password = hashPassword.Password(obj.Password);
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

        public ActionResult ViewReport(int id)
        {
            //if (Session["Username"] == null)
            //{
            //    return RedirectToAction("Login", "Login");
            //}
            EmployeeDetail employee = db.EmployeeDetails.FirstOrDefault(e => e.ID == id);

            LocalReport report = new LocalReport();
            report.ReportPath = Server.MapPath("~/Reports/Emp201File.rdlc");
            report.DataSources.Add(new ReportDataSource("Emp201", new[] { employee }));
            //report.SetParameters(new ReportParameter("PageCount", "1"));


            byte[] pdfBytes = report.Render("PDF");
            return File(pdfBytes, "application/pdf");
        }

        public ActionResult GenReport()
        {
            // Fetch the employee data from the database using Entity Framework
            List<EmployeeDetail> employees = GetEmployees();

            // Set the report data source
            ReportDataSource reportDataSource = new ReportDataSource("Emp201", employees);

            // Provide the RDLC report file path
            string reportPath = Server.MapPath("~/Reports/Emp201File.rdlc");

            // Create the report viewer and set its properties
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = reportPath;
            localReport.DataSources.Add(reportDataSource);

            // Render the report
            byte[] renderedBytes;
            string mimeType;
            string encoding;
            string fileNameExtension;

            renderedBytes = localReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out string[] streams, out Warning[] warnings);

            // Return the report as a FileResult
            return File(renderedBytes, "application/pdf", "Employee201Form.pdf");
        }

        public ActionResult GenRep()
        {
            using (var dbContext = new DBEntities())
            {
                var employees = dbContext.EmployeeDetails.ToList();
                if (employees.Count == 0)
                    return HttpNotFound();

                LocalReport localReport = new LocalReport();
                localReport.ReportPath = Server.MapPath("~/Reports/Emp201File.rdlc");
                string reportType = "PDF";
                string mimeType;
                string encoding;
                string fileNameExtension;
                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                List<byte[]> employeeReports = new List<byte[]>();

                foreach (var employee in employees)
                {
                    localReport.DataSources.Clear();
                    localReport.DataSources.Add(new ReportDataSource("Emp201", new List<EmployeeDetail> { employee }));
                    renderedBytes = localReport.Render(reportType, "", out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
                    employeeReports.Add(renderedBytes);
                }

                byte[] mergedReports = MergePDFs(employeeReports);

                string base64 = Convert.ToBase64String(mergedReports);
                string dataUrl = "data:application/pdf;base64," + base64;

                ViewBag.PDFData = dataUrl;

                return View();

            }
        }

        private byte[] MergePDFs(List<byte[]> pdfs)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Document document = new Document();
                PdfCopy copy = new PdfCopy(document, memoryStream);
                document.Open();

                foreach (var pdf in pdfs)
                {
                    using (PdfReader reader = new PdfReader(pdf))
                    {
                        for (int page = 1; page <= reader.NumberOfPages; page++)
                        {
                            PdfImportedPage importedPage = copy.GetImportedPage(reader, page);
                            copy.AddPage(importedPage);
                        }
                    }
                }

                document.Close();
                return memoryStream.ToArray();
            }
        }

        public ActionResult DownloadReport()
        {
            using (var dbContext = new DBEntities())
            {
                var employees = dbContext.EmployeeDetails.ToList();
                if (employees.Count == 0)
                    return HttpNotFound();

                LocalReport localReport = new LocalReport();
                localReport.ReportPath = Server.MapPath("~/Reports/Emp201File.rdlc");
                string reportType = "PDF";
                string mimeType;
                string encoding;
                string fileNameExtension;
                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                List<byte[]> employeeReports = new List<byte[]>();

                foreach (var employee in employees)
                {
                    localReport.DataSources.Clear();
                    localReport.DataSources.Add(new ReportDataSource("Emp201", new List<EmployeeDetail> { employee }));
                    renderedBytes = localReport.Render(reportType, "", out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
                    employeeReports.Add(renderedBytes);
                }

                byte[] mergedReports = MergePDFs(employeeReports);

                return File(mergedReports, "application/pdf", "EmployeeReports.pdf");
            }
        }

        private List<EmployeeDetail> GetEmployees()
        {
            List<EmployeeDetail> employees;

            employees = db.EmployeeDetails.ToList();

            return employees;
        }
    }
}