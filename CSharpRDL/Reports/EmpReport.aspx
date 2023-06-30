<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmpReport.aspx.cs" Inherits="CSharpRDL.Reports.EmpReport" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div align="center">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" AsyncRendering ="false" Width="50%"></rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
