using CSharpRDL.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSharpRDL.Reports
{
    public partial class GenerateReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string searchText = string.Empty;

                if (Request.QueryString["searchText"] != null)
                {
                    searchText = Request.QueryString["searchText"].ToString();
                }

                List<EmployeeDetail> employee = null;
                using (var _context = new DBEntities())
                {
                    employee = _context.EmployeeDetails.Where(t => t.Department.Contains(searchText)).OrderBy(a => a.EmployeeID).ToList();
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/EmpReport.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rdc = new ReportDataSource("EmpDetails", employee);
                    ReportViewer1.LocalReport.DataSources.Add(rdc);
                    ReportViewer1.LocalReport.Refresh();
                    ReportViewer1.DataBind();
                }

                //List<Employee201file> employee = null;
                //DBEntities dB = new DBEntities();
                //var result = from s in dB.Employee201file where s.Department.Contains(searchText) select dB.Employee201file;
                ////employee = dB.Employee201file.Where(t => t.Firstname.Contains(searchText)).OrderBy(t => t.EmployeeId).ToList();
                ////employee = entities.Employee201file.Where(t => t.Department.Contains(searchText)).OrderBy(a => a.EmployeeId).ToList();
                //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/EmpReport.rdlc");
                //ReportViewer1.LocalReport.DataSources.Clear();
                //ReportDataSource rdc = new ReportDataSource("EmpData", employee);
                //ReportViewer1.LocalReport.DataSources.Add(rdc);
                //ReportViewer1.LocalReport.Refresh();
                //ReportViewer1.DataBind();

            }

        }
    }
}