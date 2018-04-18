<%@ Page Title="" Language="C#" MasterPageFile="~/Operations/emp.master" AutoEventWireup="true" CodeFile="Assessment.aspx.cs" Inherits="Operations_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">

    <link rel="stylesheet" href="../css/jquery.countdown.css"/>
     <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
     <style>
        .navbar {
            overflow: hidden;
            background-color:#337ab7;
            position: fixed;
            top: 50px;
            width: 100%;
        }

            .navbar a {
                float: left;
                display: block;
                color: #f2f2f2;
                text-align: center;
                padding: 14px 16px;
                text-decoration: none;
                font-size: 17px;
            }
    </style>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
<script src="../js/jquery.plugin.min.js"></script>
<script src="../js/jquery.countdown.js"></script>
    <script>
        $(function () {
            
            var time = 62;
            var left = $('#defaultCountdown').countdown({ until: time, onExpiry: liftOff });
           
           function liftOff() {
             
               document.getElementById('<%=btnsubmitAss.ClientID %>').click();
               alert("Time is up!!");
           }
          
        });
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <form id="form1" runat="server">
        <div>
             <!-- Navbar (sit on top) -->
<div class="w3-top">
  <div class="w3-bar w3-white w3-card" id="myNavbar">
      <a href="home.aspx" style="text-decoration: none">
                    <asp:Label ID="headinglabel" runat="server" Text="Learning Management System" Style="min-height: 43px; max-height: 11px; position: relative; font-size: xx-large; padding-right: 50px;"></asp:Label></a>
                 
    <!-- Right-sided navbar links -->
    <div class=" w3-right w3-hide-small"> 
       <a href="..\home.aspx" class="w3-bar-item w3-button">HOME</a>
               <%-- <a href="..\login.aspx" class="w3-bar-item w3-button">LOGIN</a>--%>
        <div class="w3-dropdown-hover w3-right">
            <button class="w3-button"><span class="fa fa-user"></span><asp:Label ID="UserName" runat="server" Font-Bold="true" ForeColor="Green" ></asp:Label></button>
                <div class="w3-dropdown-content w3-bar-block w3-card-4 "style="right:0;line-height:1.0">
                    <button type="button" class="w3-bar-item w3-button" data-toggle="modal" data-target="#myModal"><span class="glyphicon glyphicon-cog" aria-hidden="true"></span> User Settings</button>
                    <a href="..\Index.aspx" class="w3-bar-item w3-button"><span class="glyphicon glyphicon-log-out" aria-hidden="true"></span> Logout</a>
                </div>
       </div>
       </div>
       <div class="navbar-form navbar-right">
           <%--<div class="input-group form-style-8">
                        <asp:TextBox class="w3-input w3-border" ID="TextBox1" runat="server" placeholder="Search" TextMode="Search" OnTextChanged="CourseName_TextChanged" Width="250px"></asp:TextBox>
                        <span class="input-group-addon"><i class="fa fa-search"></i></span>
          </div>--%>
       </div>
    <!-- Hide right-floated links on small screens and replace them with a menu icon -->
    <a href="javascript:void(0)" class="w3-bar-item w3-button w3-right w3-hide-large w3-hide-medium" onclick="w3_open()">
      <i class="fa fa-bars"></i>
    </a>
  </div>
</div>
        <!-- Modal -->
        <div class="modal fade" id="myModal" role="dialog">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Update Profile</h4>
                    </div>
                    <div class="modall-body">
                        <br />
                        <div class="responsive">
                            <iframe src="..\MyEdit.aspx" height="360" width="600" frameborder="0"></iframe>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
        <!--=======================================================================================-->
        <div>
            <h6 id="htag" style="color: green">Assessment</h6>
             <div class="navbar" >
                <div>
                    <h3>
                        <asp:Label ID="lblcoursename" runat="server" ForeColor="White" Text="" ></asp:Label>&nbsp;&nbsp;&nbsp;
                        <span style="color:white; padding-left:650px; background-color:#337ab7;border:none">Time Left:</span> <span id="defaultCountdown" style="background-color:#337ab7; color:white; float:right;width:250px;"></span>
                        </h3>
               </div>
            </div>
          <br />
            
            <p id="para"></p>
            <asp:GridView ShowHeader="false" AutoGenerateColumns="false" ID="GridView1" runat="server" GridLines="None">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                           <b> <asp:Label ID="QuestionId" Style="padding-left: 20px" runat="server" Visible="false" Text='<%#Eval("[Question_Id]") %>' />.</b>
                            <b> <asp:Label ID="Label1" Style="padding-left: 20px" runat="server"  Text='<%#Eval("[Question_no]") %>' />.</b>
                            <asp:Label ID="Question" runat="server" Text='<%#Eval("Question") %>' /><br />
                            <asp:RadioButton GroupName="Rboptions" Style="padding-left: 30px" Text='<%#Eval("ans1") %>' ID="Rbans1" runat="server" /><br />
                            <asp:RadioButton GroupName="Rboptions" Style="padding-left: 30px" Text='<%#Eval("ans2") %>' ID="Rbans2" runat="server" /><br />
                            <asp:RadioButton GroupName="Rboptions" Style="padding-left: 30px" Text='<%#Eval("ans3") %>' ID="Rbans3" runat="server" /><br />
                            <asp:RadioButton GroupName="Rboptions" Style="padding-left: 30px" Text='<%#Eval("ans4") %>' ID="Rbans4" runat="server" /><br />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnsubmitAss" runat="server" class="w3-btn w3-#337ab7" BackColor="#337ab7" style="border-radius:8px;align-items:center" ForeColor="White" Text="submit" OnClick="btnsubmitAss_Click" Width="131px" />


            <h2>
                <asp:Label ID="lblmessage" runat="server" ForeColor="#009900"></asp:Label></h2>
            <asp:Label ID="lblmail" runat="server" Text=""></asp:Label>
        </div>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" Runat="Server">
</asp:Content>

