<%@ Page Title="" Language="C#" MasterPageFile="emp.master" AutoEventWireup="true" CodeFile="change-password.aspx.cs" Inherits="changepassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script src="Script/jquery-1.3.2.min.js" type="text/javascript"></script>
    
    <script type="text/javascript">  
        window.onload = function () {
            var seconds = 6;
            setTimeout(function () {
                document.getElementById("<%=alert.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };
    </script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphBody" runat="server">
    <div class="mainpanel">
        <div class="contentpanel">
            
            <ol class="breadcrumb breadcrumb-quirk">
                <li><a href="../Admin/AdminDashboard.aspx"><i class="fa fa-home mr5"></i>Home</a></li>
                <li><a href="../Admin/Admin-Change-Password.aspx">Change Password</a></li>
            </ol>

            <div class="row">
                <div class="container a" id="alert" visible="false" runat="server">
                    <div class="alert alert-success alert-dismissable" id="alertmod" runat="server">
                        <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
                        <strong>
                            <asp:Label ID="Label5" runat="server" Visible="true" Text=""></asp:Label>
                            <asp:Label ID="Label2" runat="server" Text=""></asp:Label></strong>
                    </div>
                </div>
                <br />
                <div class="col-sm-12">
                    <div class="panel">
                        <div class="panel-heading">
                            <h4 align="center" class="panel-title">Change Password</h4>
                        </div>
                        <div class="panel-body">
                            <div class="col-md-3"></div>
                            <div class="col-md-6">
                                <div class="form mb20">
                                    <%--Checking TextBoxes whether empty or not... and priortising the fields for reducing time--%>
                                    <script>
                                        function checkTextField(field) {
                                            var val = document.getElementById('<%=currentPassword.ClientID%>').value;
                                            if (val === "") {
                                                document.getElementById("error").innerText =
                                                    (field.value !== "") ? "Please fill this field first." : "";
                                                document.getElementById('<%=currentPassword.ClientID%>').focus();
                                            }
                                        }

                                        function pwdvisible() {
                                            if (document.getElementById('<%=newpwdeye.ClientID%>').classList.contains('fa-eye')) {
                                                document.getElementById('<%=newpwdeye.ClientID%>').classList.add('fa-eye-slash');
                                                document.getElementById('<%=newpwdeye.ClientID%>').classList.remove('fa-eye');

                                                //document.getElementById('<%=newpassword.ClientID%>').attributes.add("TextMode") = "SingleLine";
                                                document.getElementById('<%=newpassword.ClientID%>').type = "text";
                                            }
                                            else if (document.getElementById('<%=newpwdeye.ClientID%>').classList.contains('fa-eye-slash')) {
                                                document.getElementById('<%=newpwdeye.ClientID%>').classList.remove('fa-eye-slash');
                                                document.getElementById('<%=newpwdeye.ClientID%>').classList.add('fa-eye');
                                                document.getElementById('<%=newpassword.ClientID%>').type = "Password";
                                            }
                                        }
                                    </script>

                                    <div class="col-md-12">
                                        <div class="floatings-labels">
                                            <asp:TextBox ID="currentPassword" type="password" runat="server" autocomplete="off" required="" AutoPostBack="true" CssClass="floating-input" placeholder=" " OnTextChanged="currentPassword_TextChanged" ></asp:TextBox>
                                            <p id="error" style="color:orange"></p>
                                            <label class="lbltop1 lbltop" for="nMobileNumber1">Current Password</label>
                                            <asp:RequiredFieldValidator ControlToValidate="currentPassword" ID="currentPasswordRequiredFieldValidator" runat="server" ErrorMessage="Please fill this Field" Font-Size="12px" Display="Dynamic" ForeColor="orange" Font-Names="Verdana;"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="floatings-labels">
                                        <div class="form-group ">
                                            <div class="input-group">
                                                
                                                    <asp:TextBox ID="newpassword" runat="server" required="" autocomplete="off" CssClass="floating-input form-control" placeholder=" " TextMode="Password" onblur="checkTextField(this);"></asp:TextBox>
                                                    <label class="lbltop1 lbltop" for="nMobileNumber1">New Password</label>
                                                <a href="#" id="newpwdclick" runat="server" class="input-group-addon" onclick="pwdvisible();" ><i id="newpwdeye" runat="server" class="fa fa-eye"></i></a>
                                                </div>
                                            </div><asp:RequiredFieldValidator ControlToValidate="newpassword" ID="newpasswordRequiredFieldValidator" runat="server" ErrorMessage="Please fill this Field" Font-Size="12px" Display="Dynamic" ForeColor="orange" Font-Names="Verdana;"></asp:RequiredFieldValidator>
                                        </div>

                                    </div>

                                    <%--<div class="col-md-12" >
                                        <div class="form-group" style="height:10px; border:solid 1px #808080"></div>
                                    </div>--%>

                                    <div class="col-md-12">
                                        <div class="floatings-labels">
                                            <asp:TextBox ID="confirmpassword" runat="server" required="" autocomplete="off" CssClass="floating-input" placeholder=" " TextMode="Password"></asp:TextBox>
                                            <label class="lbltop1 lbltop" for="nMobileNumber1">Confirm Password</label>
                                            <asp:RequiredFieldValidator ControlToValidate="confirmpassword" ID="confirmpasswordRequiredFieldValidator" runat="server" ErrorMessage="Please fill this Field" Font-Size="12px" Display="Dynamic" ForeColor="orange" Font-Names="Verdana;"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="confirmpasswordCompareValidator" runat="server" ValidationGroup="one" ControlToCompare="newpassword" ControlToValidate="confirmpassword" ErrorMessage="Password does not match." ForeColor="orange"></asp:CompareValidator>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <asp:Button ID="pwdSavebtn" runat="server" Text="Save" class="btn btn-success btn-block" ValidationGroup="one" Style="margin-top: 5px;" OnClick="pwdSavebtn_Click" />
                                    </div>

                                    <div class="clearfix"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="server">
</asp:Content>
