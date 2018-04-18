<%@ Page Title="" Language="C#" MasterPageFile="~/Operations/emp.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Operations_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" Runat="Server">
    <script>
        $(document).ready(function () {
            
            $("#liEmpDashboard").addClass("active");
        });
    </script>
</asp:Content>

