<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Supreme Netsoft Pvt Ltd.,</title>
    <link rel="stylesheet" href="../lib/fontawesome/css/font-awesome.css" />
    <link rel="stylesheet" href="../css/quirk.css" />

    <%-- Favicon link --%>
    <link rel="shortcut icon" type="image/png" href="../images/favicon.png" />

    <script src="../lib/modernizr/modernizr.js"></script>
</head>
<body class="signwrapper">
    <form id="form1" runat="server">
        <div class="sign-overlay"></div>
        <div class="signpanel"></div>
        <div class="panel signin">
            <div class="panel-heading">
                <h1>Register</h1>
            </div>
            <div class="panel-body">
                <div class="form-group mb10">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:TextBox ID="Empid" runat="server" CssClass="form-control" AutoPostBack="true" placeholder="Employee ID" Style="text-transform: uppercase" OnTextChanged="Empid_TextChanged" ></asp:TextBox>
                    </div>
                </div>
                <div class="form-group nomargin">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="fa fa-envelope"></i></span>
                        <asp:TextBox ID="Emailid" runat="server" CssClass="form-control" placeholder="Email ID"></asp:TextBox>
                    </div>
                </div>
                <div><a href="Login.aspx" class="forgot">Login?</a></div>
                <div class="form-group">
                    <asp:Button ID="btnSignIn" runat="server" Text="Register" OnClick="register_Click" class="btn btn-success btn-quirk btn-block" />
                </div>
                <hr class="invisible" />
            </div>
        </div>
        <!-- panel -->



        <div class="container a" style="width: 300px;" id="alert" visible="true" runat="server">
            <div class="alert alert-success alert-dismissable" id="alertmod" runat="server">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
                <i class="fa fa-check-square-o" id="suc" runat="server" visible="false"></i><i class="fa-cross-square-o" id="fai" runat="server" visible="false"></i>
                <strong>
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label></strong>
                <asp:Label ID="Label5" runat="server" Visible="true" Text=""></asp:Label>
            </div>
        </div>


        <asp:TextBox ID="pwdtemp" runat="server" Visible="false" display="dynamic" Width="0px" Height="0px"></asp:TextBox>
    </form>
    
</body>
</html>
