<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="ForgotPassword" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Supreme</title>

    <link rel="stylesheet" href="../lib/fontawesome/css/font-awesome.css" />

    <%-- Favicon link --%>
    <link rel="shortcut icon" type="image/png" href="../images/favicon.png" />

    <link rel="stylesheet" href="../css/quirk.css" />
    <script type="text/javascript">
        window.onload = function () {
            var seconds = 4;
            setTimeout(function () {
                document.getElementById("<%=alert.ClientID %>").style.display = "none";
             }, seconds * 1000);
        };
    </script>
    <script src="../lib/modernizr/modernizr.js"></script>
</head>
<body class="signwrapper">
    <form id="form1" runat="server">
        <div class="sign-overlay"></div>
        <div class="signpanel"></div>

        <div class="panel signin">
            <div class="panel-heading">
                <h1>Forgot Password</h1>
            </div>
            <div class="panel-body">

                <div class="form-group mb10">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:TextBox ID="empid" runat="server" CssClass="form-control" Style="text-transform:uppercase" placeholder="Employee ID"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group nomargin">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="fa fa-envelope"></i></span>
                        <asp:TextBox ID="emailid" runat="server" CssClass="form-control" placeholder="Email ID"></asp:TextBox>

                    </div>
                </div>
                <div style="padding: 10px 0px 0px 0px" Class="col-md-12">
                    <asp:RadioButtonList ID="empapprvradiobtnlist" AutoPostBack="true" ForeColor="#dfe2c3" Class="col-md-12" runat="server" 
                        RepeatDirection="Horizontal" RepeatLayout="Flow" TextAlign="Right" >
                        <asp:ListItem Class="col-md-6" Value="Emprdbtn" Text="&nbsp;Employee Password" Selected="True" Style="padding:0px 0px 1px 0px" ></asp:ListItem>
                        <asp:ListItem Class="col-md-6" Value="Apprvrdbtn" Text="&nbsp;Approver Password" ></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div><a href="Login.aspx" class="forgot">Login?</a></div>
                <div class="form-group">
                    <asp:Button ID="btnSignIn" runat="server" Text="Get Password" class="btn btn-success btn-quirk btn-block" OnClick="register_Click" />
                    <br />
                    <div class="container a" style="width: 300px;" id="alert" visible="false" runat="server">
                        <div class="alert alert-success alert-dismissable" id="alertmod" runat="server">
                            <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
                            <i class="fa fa-check-square-o" id="suc" runat="server" visible="false"></i><i class="fa-cross-square-o" id="fai" runat="server" visible="false"></i>
                            <strong>
                                <asp:Label ID="Label1" runat="server" Text=""></asp:Label></strong>
                            <asp:Label ID="Label5" runat="server" Visible="true" Text=""></asp:Label>
                        </div>
                    </div>
                </div>

                <hr class="invisible" />
                <div class="form-group">
                    <a href="../Registration.aspx" class="btn btn-default btn-quirk btn-stroke btn-stroke-thin btn-block btn-sign">Create an account!</a>
                </div>
            </div>
            <asp:TextBox ID="id2" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="pass" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="name2" runat="server" Visible="false"></asp:TextBox>
        </div>
        <!-- panel -->
    </form>
</body>
</html>
