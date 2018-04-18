<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true" CodeFile="NewAddEmployee.aspx.cs" Inherits="Admin_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ers3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
	<%--<link resource="https://cdnjs.cloudflare.com/ajax/libs/parsley.js/2.8.1/parsley.js" />
	<link resource="https://cdnjs.cloudflare.com/ajax/libs/parsley.js/2.8.1/parsley.min.js"/>--%>
	<script type="text/javascript">
		window.onload = function () {
			var seconds = 6;
			setTimeout(function () {
				document.getElementById("<%=alert.ClientID %>").style.display = "none";
			}, seconds * 1000);
		};
	</script>
	<style type="text/css">
		.auto-style1 {
			position: relative;
			margin-bottom: 10px;
			margin-top: 14px;
			background-color: #fff;
			left: 0px;
			top: -9px;
		}

		.auto-style2 {
			border-bottom-right-radius: 2px;
			border-bottom-left-radius: 2px;
			height: 54px;
			border-left-color: transparent;
			border-right-color: transparent;
			border-top: 1px solid transparent;
			border-bottom-color: transparent;
			margin-top: 0px;
			padding: 20px;
			background-color: #f0f1f4;
		}
	</style>
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

				

				<div class="col-sm-12">
					<asp:LinkButton ID="back" runat="server" OnClick="back_Click"><i class="fa fa-arrow-left" style="font-size:20px;color:black"> Back</i></asp:LinkButton>
					<br />
					<br />
					<div class="panel">
						
						<div class="panel-heading">

							<h4 class="panel-title">Add Employee </h4>

						</div>

						<div class="panel-body">

							<div class="form mb20">
								<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
								<div class="col-md-3">
									<div class="floatings-labels">
										<asp:TextBox ID="empid" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
										<label class="lbltop1 lbltop" for="nMobileNumber1">Employee ID   </label>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="empid" ErrorMessage="" Text="*" ForeColor="Red" ValidationGroup="one"></asp:RequiredFieldValidator>
									</div>
								</div>
								<div class="col-md-3">
									<div class="floatings-labels">
										<asp:TextBox ID="empfname" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
										<label class="lbltop1 lbltop" for="nMobileNumber1">First Name </label>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="empfname" Text="*" ForeColor="Red" ValidationGroup="one"></asp:RequiredFieldValidator>

									</div>
								</div>
								<div class="col-md-3">
									<div class="floatings-labels">
										<asp:TextBox ID="empmname" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
										<label class="lbltop1 lbltop" for="nMobileNumber1">Middle Name </label>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="empmname" Text="*" ForeColor="Red" ValidationGroup="one"></asp:RequiredFieldValidator>

									</div>
								</div>
								<div class="col-md-3">
									<div class="floatings-labels">
										<asp:TextBox ID="emplname" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
										<label class="lbltop1 lbltop" for="nMobileNumber1">Last Name </label>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="emplname" Text="*" ForeColor="Red" ValidationGroup="one"></asp:RequiredFieldValidator>


									</div>
								</div>
								<div class="clearfix"></div>
								<div class="col-md-3">
									<div class="floatings-labels">
										<asp:DropDownList ID="gender" runat="server" AutoPostBack="false" AppendDataBoundItems="true" CssClass="form-control">
											<asp:ListItem Text="" Value=""></asp:ListItem>
											<asp:ListItem Text="Male" Value="Male"> </asp:ListItem>
											<asp:ListItem Text="Female" Value="Female"></asp:ListItem>
											<asp:ListItem Text="Other" Value="Other"></asp:ListItem>
										</asp:DropDownList>
										<label class="lbltop" for="txtMotherLang">Gender</label>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="gender" Text="*" ForeColor="Red" ValidationGroup="one"></asp:RequiredFieldValidator>

									</div>
								</div>
								<div class="col-md-3">
									<div class="floatings-labels">
										<ers3:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="doj" />
										<asp:TextBox ID="doj" required="" runat="server" CssClass="floating-input hasDatepicker" placeholder=" "></asp:TextBox>
										<asp:Label ID="dojdate" runat="server" class="lbltop1 lbltop" for="nMobileNumber4">Date of Join</asp:Label>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="RequiredFieldValidator" Text="*" ForeColor="Red" ValidationGroup="one" ControlToValidate="doj"> </asp:RequiredFieldValidator>
									</div>
								</div>

								<div class="col-md-3">
									<div class="floatings-labels">
										<asp:TextBox ID="emppmailid" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
										<label class="lbltop1 lbltop" for="nMobileNumber1">Personal Email ID</label>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="RequiredFieldValidator" Text="*" ForeColor="Red" ValidationGroup="one" ControlToValidate="emppmailid"> </asp:RequiredFieldValidator>
										<asp:RegularExpressionValidator ID="RegularExpressionValidator2"
											ControlToValidate="emppmailid" runat="server"
											ErrorMessage="Invalid Mail ID"
											ValidationExpression="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$" ForeColor="Red" ValidationGroup="one">
										</asp:RegularExpressionValidator>
									</div>
								</div>
								<div class="col-md-3">
									<div class="floatings-labels">
										<ers3:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="dob" />
										<asp:TextBox ID="dob" required="" runat="server" CssClass="floating-input hasDatepicker" placeholder=" "></asp:TextBox>
										<asp:Label ID="ldob" runat="server" class="lbltop1 lbltop" for="nMobileNumber4">Date of birth</asp:Label>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="RequiredFieldValidator" Text="*" ForeColor="Red" ValidationGroup="one" ControlToValidate="dob"></asp:RequiredFieldValidator>
									</div>
								</div>
								<div class="clearfix"></div>
								<div class="col-md-3">
									<div class="floatings-labels">
										<asp:TextBox ID="emphone" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
										<label class="lbltop1 lbltop" for="nMobileNumber1">Mobile Number </label>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="RequiredFieldValidator" Text="*" ForeColor="Red" ValidationGroup="one" ControlToValidate="emphone"></asp:RequiredFieldValidator>
										<asp:RegularExpressionValidator ID="RegularExpressionValidator3"
											ControlToValidate="emphone" runat="server"
											ErrorMessage="Invalid Mobile Number"
											ValidationExpression="[7,8,9]{1}[0-9]{9}$" ForeColor="Red" ValidationGroup="one">
										</asp:RegularExpressionValidator>
									</div>
								</div>
								<div class="col-md-3">
									<div class="floatings-labels">
										<asp:DropDownList ID="Bgroup" runat="server" AutoPostBack="false" AppendDataBoundItems="true" CssClass="form-control">
											<asp:ListItem Text="" Value=""></asp:ListItem>
											<asp:ListItem Text="O-positive" Value="O-positive"></asp:ListItem>
											<asp:ListItem Text="O-negative" Value="O-negative"> </asp:ListItem>
											<asp:ListItem Text="A-positive" Value="A-positive"></asp:ListItem>
											<asp:ListItem Text="A-negative" Value="A-negative"></asp:ListItem>
											<asp:ListItem Text="B-positive" Value="B-positive"></asp:ListItem>
											<asp:ListItem Text="B-negative" Value="B-negative"></asp:ListItem>
											<asp:ListItem Text="AB-positive" Value="AB-positive"></asp:ListItem>
											<asp:ListItem Text="AB-negative" Value="AB-negative"></asp:ListItem>

										</asp:DropDownList>
										<label class="lbltop" for="txtMotherLang">Blood Group</label>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="RequiredFieldValidator" Text="*" ForeColor="Red" ValidationGroup="one" ControlToValidate="Bgroup"></asp:RequiredFieldValidator>
									</div>
								</div>
								<div class="col-md-3">
									<div class="floatings-labels">
										<asp:TextBox ID="emobile" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
										<label class="lbltop1 lbltop" for="nMobileNumber1">Emergency Mobile Number </label>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="RequiredFieldValidator" Text="*" ForeColor="Red" ValidationGroup="one" ControlToValidate="emobile"></asp:RequiredFieldValidator>
										<asp:RegularExpressionValidator ID="RegularExpressionValidator1"
											ControlToValidate="emobile" runat="server"
											ErrorMessage="Invalid Mobile Number"
											ValidationExpression="[7,8,9]{1}[0-9]{9}$" ForeColor="Red" ValidationGroup="one">
										</asp:RegularExpressionValidator>
									</div>
								</div>
								<div class="col-md-3">
									<div class="floatings-labels">
										<asp:TextBox ID="Adharno" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
										<label class="lbltop1 lbltop" for="nMobileNumber1">Aadhar Number </label>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="RequiredFieldValidator" Text="*" ForeColor="Red" ValidationGroup="one" ControlToValidate="Adharno"></asp:RequiredFieldValidator>
									</div>
								</div>

								<div class="clearfix"></div>
								<div class="col-md-3">
									<div class="floatings-labels">
										<asp:TextBox ID="Panno" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
										<label class="lbltop1 lbltop" for="nMobileNumber1">Pan Number</label>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="RequiredFieldValidator" Text="*" ForeColor="Red" ValidationGroup="one" ControlToValidate="Panno"></asp:RequiredFieldValidator>
									</div>
								</div>
								<div class="col-md-3">
									<div class="floatings-labels">
										<asp:FileUpload ID="pic" runat="server" />
										<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="RequiredFieldValidator" Text="*" ForeColor="Red" ValidationGroup="one" ControlToValidate="pic"></asp:RequiredFieldValidator>
									</div>
								</div>


								<div class="clearfix"></div>
								<h4>Permanent Address </h4>
								<div class="col-md-3">
									<div class="floatings-labels">
										<asp:TextBox ID="Address" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
										<label class="lbltop1 lbltop" for="nMobileNumber1">Address</label>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="RequiredFieldValidator" Text="*" ForeColor="Red" ValidationGroup="one" ControlToValidate="Address"></asp:RequiredFieldValidator>
									</div>
								</div>
								<div class="col-md-3">
									<div class="floatings-labels">
										<asp:TextBox ID="Landmark" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
										<label class="lbltop1 lbltop" for="nMobileNumber1">Land Mark</label>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="RequiredFieldValidator" Text="*" ForeColor="Red" ValidationGroup="one" ControlToValidate="Landmark"></asp:RequiredFieldValidator>
									</div>
								</div>
								<div class="col-md-3">
									<div class="floatings-labels">
										<asp:TextBox ID="Country" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
										<label class="lbltop1 lbltop" for="nMobileNumber1">Country</label>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ErrorMessage="RequiredFieldValidator" Text="*" ForeColor="Red" ValidationGroup="one" ControlToValidate="Country"></asp:RequiredFieldValidator>
									</div>
								</div>
								<div class="col-md-3">
									<div class="floatings-labels">
										<asp:TextBox ID="state" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
										<label class="lbltop1 lbltop" for="nMobileNumber1">State</label>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ErrorMessage="RequiredFieldValidator" Text="*" ForeColor="Red" ValidationGroup="one" ControlToValidate="State"></asp:RequiredFieldValidator>
									</div>
								</div>
								<div class="clearfix"></div>
								<div class="col-md-3">
									<div class="floatings-labels">
										<asp:TextBox ID="City" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
										<label class="lbltop1 lbltop" for="nMobileNumber1">City</label>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ErrorMessage="RequiredFieldValidator" Text="*" ForeColor="Red" ValidationGroup="one" ControlToValidate="City"></asp:RequiredFieldValidator>
									</div>
								</div>
								<div class="clearfix"></div>
								<div class="col-md-3">
									<div class="floatings-labels">
										<asp:CheckBox ID="same" runat="server" Text="Corresponding Details " AutoPostBack="true" OnCheckedChanged="same_CheckedChanged" />
									</div>
								</div>
								<br />
								<br />
								<div class="col-md-03">
									<div class="auto-style1">
										<label class="lbltop1 lbltop" for="nMobileNumber1">Same as above</label>
									</div>
								</div>
								<br />
								<br />
								<div class="clearfix"></div>
								<div id="Perminent">
								<div class="col-md-3">
									<div class="floatings-labels">
										<asp:TextBox ID="padd" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
										<label class="lbltop1 lbltop" for="nMobileNumber1">Address</label>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ErrorMessage="RequiredFieldValidator" Text="*" ForeColor="Red" ValidationGroup="one" ControlToValidate="padd"></asp:RequiredFieldValidator>
									</div>
								</div>
								<div class="col-md-3">
									<div class="floatings-labels">
										<asp:TextBox ID="pland" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
										<label class="lbltop1 lbltop" for="nMobileNumber1">Land Mark</label>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ErrorMessage="RequiredFieldValidator" Text="*" ForeColor="Red" ValidationGroup="one" ControlToValidate="pland"></asp:RequiredFieldValidator>
									</div>
								</div>
								<div class="col-md-3">
									<div class="floatings-labels">
										<asp:TextBox ID="pcountry" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
										<label class="lbltop1 lbltop" for="nMobileNumber1">Country</label>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ErrorMessage="RequiredFieldValidator" Text="*" ForeColor="Red" ValidationGroup="one" ControlToValidate="pcountry"></asp:RequiredFieldValidator>
									</div>
								</div>
								<div class="col-md-3">
									<div class="floatings-labels">
										<asp:TextBox ID="pstate" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
										<label class="lbltop1 lbltop" for="nMobileNumber1">State</label>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ErrorMessage="RequiredFieldValidator" Text="*" ForeColor="Red" ValidationGroup="one" ControlToValidate="pstate"></asp:RequiredFieldValidator>
									</div>
								</div>
								<div class="clearfix"></div>
								<div class="col-md-3">
									<div class="floatings-labels">
										<asp:TextBox ID="pcity" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
										<label class="lbltop1 lbltop" for="nMobileNumber1">City</label>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ErrorMessage="RequiredFieldValidator" Text="*" ForeColor="Red" ValidationGroup="one" ControlToValidate="pcity"></asp:RequiredFieldValidator>
									</div>
								</div>
								<div class="clearfix"></div>
								</div>

								<h4>Qualification Details</h4>
								<%--<asp:Button runat="server" class="btn btn-primary btn-sm tooltips pull-right" type="button" title="" ID="Add" Text="Add More"></asp:Button>--%>
								<br />
								<div class="clearfix"></div>
								<div class="well">
									<div id="Quali">
									<div class="clearfix"></div>
									<div class="col-md-3">
										<div class="floatings-labels">
											<asp:TextBox ID="Qname" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
											<label class="lbltop1 lbltop" for="nMobileNumber1">Qualification Name</label>
										</div>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ErrorMessage="RequiredFieldValidator" Text="*" ForeColor="Red" ValidationGroup="one" ControlToValidate="Qname"></asp:RequiredFieldValidator>
									</div>
									<div class="col-md-3">
										<div class="floatings-labels">
											<asp:TextBox ID="year" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
											<label class="lbltop1 lbltop" for="nMobileNumber1">Year Of Passing</label>
										</div>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ErrorMessage="RequiredFieldValidator" Text="*" ForeColor="Red" ValidationGroup="one" ControlToValidate="year"></asp:RequiredFieldValidator>
									</div>

									<div class="col-md-3">
										<div class="floatings-labels">
											<asp:TextBox ID="marks" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
											<label class="lbltop1 lbltop" for="nMobileNumber1">% of Marks</label>
										</div>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ErrorMessage="RequiredFieldValidator" Text="*" ForeColor="Red" ValidationGroup="one" ControlToValidate="marks"></asp:RequiredFieldValidator>
									</div>
									<div class="col-md-3">
										<div class="floatings-labels">
											<asp:TextBox ID="uni" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
											<label class="lbltop1 lbltop" for="nMobileNumber1">University</label>
										</div>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ErrorMessage="RequiredFieldValidator" Text="*" ForeColor="Red" ValidationGroup="one" ControlToValidate="uni"></asp:RequiredFieldValidator>
									</div>
									<div class="clearfix"></div>
									<div class="col-md-3">
										<div class="floatings-labels">
											<asp:TextBox ID="Clz" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
											<label class="lbltop1 lbltop" for="nMobileNumber1">College</label>
										</div>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ErrorMessage="RequiredFieldValidator" Text="*" ForeColor="Red" ValidationGroup="one" ControlToValidate="clz"></asp:RequiredFieldValidator>
									</div>
									<div class="clearfix"></div>
										</div>
								</div>

								<h4>Experience Details</h4>
								<%--<asp:Button runat="server" class="btn btn-primary btn-sm tooltips pull-right" type="button" title="" ID="Button1" Text="Add More"></asp:Button>--%>
								<br />
								<div class="clearfix"></div>
								<div class="well">
									<div id="edu">
									<div class="clearfix"></div>
									<div class="col-md-3">
										<div class="floatings-labels">
											<asp:TextBox ID="Company" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
											<label class="lbltop1 lbltop" for="nMobileNumber1">Company Name</label>
										</div>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ErrorMessage="RequiredFieldValidator" Text="*" ForeColor="Red" ValidationGroup="one" ControlToValidate="company"></asp:RequiredFieldValidator>
									</div>
									<div class="col-md-3">
										<div class="floatings-labels">
											<asp:TextBox ID="designation" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
											<label class="lbltop1 lbltop" for="nMobileNumber1">Designation</label>
										</div>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ErrorMessage="RequiredFieldValidator" Text="*" ForeColor="Red" ValidationGroup="one" ControlToValidate="designation"></asp:RequiredFieldValidator>
									</div>

									<div class="col-md-3">
										<div class="floatings-labels">
											<asp:TextBox ID="location" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
											<label class="lbltop1 lbltop" for="nMobileNumber1">Location</label>
										</div>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ErrorMessage="RequiredFieldValidator" Text="*" ForeColor="Red" ValidationGroup="one" ControlToValidate="location"></asp:RequiredFieldValidator>
									</div>
									<div class="col-md-3">
										<div class="floatings-labels">
											<ers3:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="from" />
											<asp:TextBox ID="from" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
											<label class="lbltop1 lbltop" for="nMobileNumber1">Form</label>
										</div>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ErrorMessage="RequiredFieldValidator" Text="*" ForeColor="Red" ValidationGroup="one" ControlToValidate="uni"></asp:RequiredFieldValidator>
									</div>
									<div class="clearfix"></div>
									<div class="col-md-3">
										<div class="floatings-labels">
											<ers3:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="to" />
											<asp:TextBox ID="to" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
											<label class="lbltop1 lbltop" for="nMobileNumber1">TO</label>
										</div>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" ErrorMessage="RequiredFieldValidator" Text="*" ForeColor="Red" ValidationGroup="one" ControlToValidate="to"></asp:RequiredFieldValidator>
									</div>
									<div class="col-md-3">
										<div class="floatings-labels">
											<asp:TextBox ID="lsalary" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
											<label class="lbltop1 lbltop" for="nMobileNumber1">Last Drawn Salary</label>
										</div>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" ErrorMessage="RequiredFieldValidator" Text="*" ForeColor="Red" ValidationGroup="one" ControlToValidate="lsalary"></asp:RequiredFieldValidator>
									</div>

									<div class="clearfix"></div>
										</div>
								</div>
								<div class="col-md-3">
									<div class="floatings-labels">
										<asp:TextBox ID="RM" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
										<label class="lbltop1 lbltop" for="nMobileNumber1">Reporting Manager</label>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ErrorMessage="RequiredFieldValidator" Text="*" ForeColor="Red" ValidationGroup="one" ControlToValidate="RM"></asp:RequiredFieldValidator>
									</div>
								</div>
								<div class="col-md-3">
									<div class="floatings-labels">
										<asp:TextBox ID="dept" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
										<label class="lbltop1 lbltop" for="nMobileNumber1">Department</label>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server" ErrorMessage="RequiredFieldValidator" Text="*" ForeColor="Red" ValidationGroup="one" ControlToValidate="dept"></asp:RequiredFieldValidator>
									</div>
								</div>
								<div class="col-md-3">
									<div class="floatings-labels">
										<asp:TextBox ID="role" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
										<label class="lbltop1 lbltop" for="nMobileNumber1">Role</label>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" ErrorMessage="RequiredFieldValidator" Text="*" ForeColor="Red" ValidationGroup="one" ControlToValidate="role"></asp:RequiredFieldValidator>
									</div>
								</div>
								<div class="clearfix"></div>
								<div class="col-md-3">
									<div class="floatings-labels">
										<asp:TextBox ID="zone" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
										<label class="lbltop1 lbltop" for="nMobileNumber1">Zone</label>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator40" runat="server" ErrorMessage="RequiredFieldValidator" Text="*" ForeColor="Red" ValidationGroup="one" ControlToValidate="zone"></asp:RequiredFieldValidator>
									</div>
								</div>
								<div class="col-md-3">
									<div class="floatings-labels">
										<asp:TextBox ID="branch" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
										<label class="lbltop1 lbltop" for="nMobileNumber1">Branch</label>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator41" runat="server" ErrorMessage="RequiredFieldValidator" Text="*" ForeColor="Red" ValidationGroup="one" ControlToValidate="branch"></asp:RequiredFieldValidator>
									</div>
								</div>
								<div class="col-md-3">
									<div class="floatings-labels">
										<asp:TextBox ID="csalary" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
										<label class="lbltop1 lbltop" for="nMobileNumber1">Current Salary</label>
										<asp:RequiredFieldValidator ID="RequiredFieldValidator42" runat="server" ErrorMessage="RequiredFieldValidator" Text="*" ForeColor="Red" ValidationGroup="one" ControlToValidate="csalary"></asp:RequiredFieldValidator>
									</div>
								</div>
							</div>
						</div>
						<div class="panel-footer">
							<asp:Button ID="submit" runat="server" Text="Create Employee" ValidationGroup="one" class="btn btn-success pull-right" OnClick="submit_Click"></asp:Button>
							<asp:Button runat="server" class="btn btn-warning  tooltips pull-left" ID="clear" Text="Cancel" OnClick="clear_Click"></asp:Button>
							<div class="clearfix"></div>
						</div>

					</div>








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

