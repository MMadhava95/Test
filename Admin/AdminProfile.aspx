<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true" CodeFile="AdminProfile.aspx.cs" Inherits="Admin_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
    <script type="text/javascript">
        window.onload = function () {
            var seconds = 6;
            setTimeout(function () {
                document.getElementById("<%=alert.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <div class="mainpanel">

        <div class="contentpanel">

            <div class="row">
                <div class="container a" id="alert" visible="true" runat="server">
                    <div class="alert alert-success alert-dismissable" id="alertmod" runat="server">
                        <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
                        <strong>
                            <asp:Label ID="Label5" runat="server" Text=""></asp:Label></strong>
                        <asp:Label ID="Label6" runat="server" Visible="true" Text=""></asp:Label>
                    </div>
                </div>

                <br />

                <div class="col-sm-12">

                    <div class="panel">
                        <div class="panel-heading">

                            <h4 class="panel-title">Add Employee </h4>

                        </div>
                        <div class="panel-body">

                            <div class="form mb20">

                                <div class="col-md-3">
                                    <div class="floatings-labels">
                                        <asp:TextBox ID="empid" ReadOnly="true" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                        <label class="lbltop1 lbltop" for="nMobileNumber1">Employee ID   </label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="empid" ErrorMessage="" Text="*" ForeColor="Red" ValidationGroup="one"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="floatings-labels">
                                        <asp:TextBox ID="empname" ReadOnly="true" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                        <label class="lbltop1 lbltop" for="nMobileNumber1">Employee Name </label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="empname" Text="*" ForeColor="Red" ValidationGroup="one"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="empname"
                                            ValidationExpression="[a-zA-Z ]*$" ErrorMessage="Alphabets are valid" ValidationGroup="one" ForeColor="Red" />

                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="floatings-labels">
                                        <asp:TextBox ID="empmailid" runat="server" ReadOnly="true" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                        <label class="lbltop1 lbltop" for="nMobileNumber1">Email ID</label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="RequiredFieldValidator" Text="*" ForeColor="Red" ValidationGroup="one" ControlToValidate="empmailid"> </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                            ControlToValidate="empmailid" runat="server"
                                            ErrorMessage="Invalid Mail ID"
                                            ValidationExpression="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$" ForeColor="Red" ValidationGroup="one">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                
                                <div class="col-md-3">
                                    <div class="floatings-labels">
                                        <asp:TextBox ID="empdesignation" runat="server" ReadOnly="true" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                        <label class="lbltop1 lbltop" for="nMobileNumber1">Designation  </label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="RequiredFieldValidator" Text="*" ForeColor="Red" ValidationGroup="one" ControlToValidate="empdesignation"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="empdesignation"
                                            ValidationExpression="[a-zA-Z ]*$" ErrorMessage="Alphabets are valid" ForeColor="Red" ValidationGroup="one" />

                                    </div>
                                </div>
								<div class="clearfix"></div>
                                <div class="col-md-3">
                                    <div class="floatings-labels">
                                        <asp:TextBox ID="empdepartment" runat="server" ReadOnly="true" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                        <label class="lbltop1 lbltop" for="nMobileNumber1">Department </label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="RequiredFieldValidator" Text="*" ForeColor="Red" ValidationGroup="one" ControlToValidate="empdepartment"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="empdepartment"
                                            ValidationExpression="[a-zA-Z ]*$" ErrorMessage="Alphabets are valid" ForeColor="Red" ValidationGroup="one" />

                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="floatings-labels">
                                        <asp:TextBox ID="emplocation" runat="server" ReadOnly="true" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                        <label class="lbltop1 lbltop" for="nMobileNumber1">Location</label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="RequiredFieldValidator" Text="*" ForeColor="Red" ValidationGroup="one" ControlToValidate="emplocation"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="emplocation"
                                            ValidationExpression="[a-zA-Z ]*$" ErrorMessage="Alphabets are valid" ForeColor="Red" ValidationGroup="one" />

                                    </div>
                                </div>
								<div class="col-md-3">
                                    <div class="floatings-labels">
                                        <asp:TextBox ID="emphone" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                        <label class="lbltop1 lbltop" for="nMobileNumber1">Mobile Number </label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="RequiredFieldValidator" Text="*" ForeColor="Red" ValidationGroup="one" ControlToValidate="emphone"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3"
                                            ControlToValidate="emphone" runat="server"
                                            ErrorMessage="Invalid Mobile Number"
                                            ValidationExpression="[7,8,9]{1}[0-9]{9}$" ForeColor="Red" ValidationGroup="one">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                   <asp:Button ID="update" runat="server" Text="Update" OnClick="update_Click" ValidationGroup="one" class="btn btn-success   btn-block" Style="margin-top: 10px;" />
                                </div>

                                <div class="clearfix"></div>


                            </div>


                            <asp:LinkButton ID="back" runat="server" OnClick="back_Click" style="color:black;font-size:medium"><i class="fa fa-arrow-left" style="font-size:20px;color:black"></i>Back</asp:LinkButton>

                        </div>
                    </div>
                    <!-- panel -->


                </div>
                <!-- col-sm-6 -->

            </div>
            <!-- row -->

        </div>
        <!-- contentpanel -->
    </div>
    <!-- mainpanel -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="Server">
    <script>
        $(document).ready(function () {


            $('#cphBody_gvList').DataTable();

        });
    </script>
</asp:Content>


