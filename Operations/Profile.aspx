<%@ Page Title="" Language="C#" MasterPageFile="~/Operations/emp.master" AutoEventWireup="true" CodeFile="Profile.aspx.cs" Inherits="Operations_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
     <script type = "text/javascript">
		 window.onload = function () {
			 var seconds = 4;
			 setTimeout(function () {
				 document.getElementById("<%=alert.ClientID %>").style.display = "none";
		 }, seconds * 1000);
		 }; 
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
   <div class="mainpanel">

		<div class="contentpanel">

			<ol class="breadcrumb breadcrumb-quirk">
				<li><a href="index.html"><i class="fa fa-home mr5"></i>Home</a></li>
				<li class="active">Profile </li>
			</ol>

			<div class="row">
				<div class="col-sm-12">

					<div class="panel">
						<div class="panel-heading">

							<h4 class="panel-title">Profile</h4>

						</div>
						<div class="panel-body">

							<div class="form mb20">

								<div class="col-md-3">
									<div class="floatings-labels">
										<asp:TextBox ID="empid" runat="server" ReadOnly="true" CssClass="floating-input" placeholder=" "></asp:TextBox>
										<label class="lbltop1 lbltop" for="nMobileNumber1">Employee ID</label>
									</div>
								</div>
								<div class="col-md-3">
									<div class="floatings-labels">
										<asp:TextBox ID="name" runat="server" ReadOnly="true" CssClass="floating-input" placeholder=" "></asp:TextBox>
										<label class="lbltop1 lbltop" for="nMobileNumber1">Name </label>
									</div>
								</div>
								<div class="col-md-3">
									<div class="floatings-labels">
										<asp:TextBox ID="emailid" runat="server" ReadOnly="true" CssClass="floating-input" placeholder=" "></asp:TextBox>
										<label class="lbltop1 lbltop" for="nMobileNumber1">Email ID</label>
									</div>
								</div>

									<div class="col-md-3">
									<div class="floatings-labels">
										<asp:TextBox ID="designation" runat="server" ReadOnly="true" CssClass="floating-input" placeholder=" "></asp:TextBox>
										<label class="lbltop1 lbltop" for="nMobileNumber1">Designation  </label>
									</div>
								</div>
								<div class="col-md-3">
									<div class="floatings-labels">
										<asp:TextBox ID="Dept" runat="server" ReadOnly="true" CssClass="floating-input" placeholder=" "></asp:TextBox>
										<label class="lbltop1 lbltop" for="nMobileNumber1">Department </label>
									</div>
								</div>
								<div class="col-md-3">
									<div class="floatings-labels">
										<asp:TextBox ID="loc" runat="server" ReadOnly="true" CssClass="floating-input" placeholder=" "></asp:TextBox>
										<label class="lbltop1 lbltop" for="nMobileNumber1">Location</label>
									</div>
								</div>
								<div class="col-md-3">
									<div class="floatings-labels">
										<asp:TextBox ID="mobile" runat="server"  ToolTip="Should Contain 10 Digits Starts with 7,8,9" CssClass="floating-input" placeholder=" "></asp:TextBox>
										<label class="lbltop1 lbltop" for="nMobileNumber1">Mobile Number </label>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="mobile" Text="*" ForeColor="Red" ValidationGroup="one"></asp:RequiredFieldValidator>
									</div>
									
									<asp:RegularExpressionValidator ID="RegularExpressionValidator3"
										ControlToValidate="mobile" runat="server"
										ErrorMessage="InValid Mobile Number"
										ValidationExpression="[7,8,9]{1}[0-9]{9}$" ValidationGroup="one" SetFocusOnError="true" ForeColor="Red"></asp:RegularExpressionValidator>
								</div>
								
								<div class="col-md-3">
									<asp:Button ID="btnSignIn" runat="server" Text="Save" class="btn btn-success   btn-block" ValidationGroup="one" Style="margin-top: 10px;" OnClick="btnSignIn_Click1" />
								</div>
								<div class="clearfix"></div>
							</div>
                            <div class="container a" id="alert" visible="false" runat="server">
                                <div class="alert alert-success alert-dismissable" id="alertmod" runat="server">
                                    <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
                                    <strong>
                                        <asp:Label ID="Label5" runat="server" Text=""></asp:Label></strong>
                                    <asp:Label ID="Label2" runat="server" Visible="true" Text=""></asp:Label>
                                </div>
                            </div>
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
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="server">
</asp:Content>
