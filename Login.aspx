<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Login</title>
    <link rel="Shortcut Icon" type="image/png" href="../favicons/AMS house.PNG" />
    <link rel="Stylesheet" href="../lib/fontawesome/css/font-awesome.css" type="text/css" />
    <link rel="stylesheet" href="../css/quirk.css" type="text/css" />
    <!-- Favicon link -->
    <link rel="shortcut icon" type="image/png" href="../images/favicon.png" />
    <script src="../lib/modernizr.js" type="text/javascript"></script>
</head>
<body class="signwrapper">
    <form id="form1" runat="server">
        <%--<a href="../LatestUiImages/Login-Background.jpg"--%>
        <div class="sign-overlay"></div>
        <div class="signpanel"></div>
        <div class="panel signin">
             <div class="panel-heading">
              
                <h1>Login</h1>
            </div>
            <script type="text/javascript">
                function checkboxfun() {
                    
                }
            </script>
             <script type = "text/javascript">
              window.onload = function () {
              var seconds = 4;
              setTimeout(function () {
              document.getElementById("<%=alert.ClientID %>").style.display = "none";
              }, seconds * 1000);
              }; 
            </script>
            <div class="panel-body">
                <div class="form-group mb10">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="fa fa-envelope"></i></span>
                        <asp:TextBox ID="UserMailID" runat="server" placeholder="Email" AutoPostBack="true" CssClass="form-control" ToolTip="Please Enter Valid Email" required="required" OnTextChanged="UserMailID_TextChanged"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" class="error-msg" runat="server" SetFocusOnError="true" ErrorMessage="Please enter valid email" ControlToValidate="UserMailID" Font-Size="12px" ValidationExpression="^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*\s*$" Display="Dynamic" Font-Names="Verdana;" ForeColor="orange"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="form-group nomargin">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="fa fa-lock"></i></span>
                        <asp:TextBox ID="UserPassword" runat="server" placeholder="Password" CssClass="form-control" TextMode="Password" ToolTip="Please Enter Valid Password" required="required"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group nomargin">
                    <asp:CheckBox ID="Apprvckeck" runat="server" Visible="false" Text="Login as Approver" />
                </div>
                <div><a href="../ForgotPassword.aspx" class="forgot">Forgot Password?</a></div>
                <div class="form-group">
                    <asp:Button ID="btnSubmit" runat="server" class="btn btn-success btn-quirk btn-block" Text="Login" ToolTip="Click to Login" OnClick="btnSubmit_Click" />
                </div>
                <hr class="invisible" />
                <div class="form-group">
                    <a href="../Registration.aspx" class="btn btn-default btn-quirk btn-stroke btn-stroke-thin btn-block btn-sign" >New User Registration</a>
                </div>
                 <div class="form-group">
                <div class="container-fluid" id="alert" visible="true" runat="server" width="100%" height="20px" style="padding-top:15px">
                 <div class="alert alert-success alert-dismissable" id="alertmod" runat="server">
                  <a href="#" class="close" data-dismiss="alert" aria-label="close" style="align-content:flex-end;margin-top:5px;">x</a>
               <strong>

                <asp:Label ID="Label1" runat="server" Text="" style="font-size:medium;"  ></asp:Label></strong>
            <asp:Label ID="Label5" runat="server"  Visible="true" Text="" style="font-size:medium;" ></asp:Label>
        </div>
      </div>
    </div>
    
              
            </div>
        </div>
        <!-- panel -->
    </form>
</body>
</html>

