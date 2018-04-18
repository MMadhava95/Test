<%@ Page Title="" Language="C#" MasterPageFile="emp.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="ApplyAdvancepay.aspx.cs" Inherits="ApplyAdvancepay" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ers3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
	<style>
		.dataTables_filter {
			float: right;
		}
                        .modalBackground {
background-color:gray;
filter: alpha(opacity=100);
opacity: 0.9;
}

		.table-responsive {
			overflow: unset;
		}

		#cphBody_GridView1_paginate {
			float: right !important;
		}
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
	<asp:Label ID="Label7" runat="server" Text="" Visible="false"></asp:Label>
	<div class="mainpanel">

		<div class="contentpanel">

			<ol class="breadcrumb breadcrumb-quirk">
				<li><a href="Dashboard.aspx"><i class="fa fa-home mr5"></i>Home</a></li>
				<li class="active">Advancepay </li>
			</ol>
			<div class="container a" style="padding-right: 100px;" id="alert" visible="false" runat="server">


				<div class="alert alert-success alert-dismissable" id="alertmod" runat="server">
					<a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
					<strong>
						<asp:Label ID="Label5" runat="server" Text=""></asp:Label></strong>
					<asp:Label ID="Label6" runat="server" Visible="true" Text=""></asp:Label>
				</div>
			</div>
			<div class="container " style="padding-right: 100px; background-color: #d7ecc6" id="Div1" visible="true" runat="server">
				<div class="alert alert-success alert-dismissable" style="background-color: #d7ecc6" id="Div2" runat="server">
					<a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
					<strong>

						<asp:Label ID="Label4" runat="server" Text="Do You Want To Delete?" ForeColor="black"></asp:Label></strong>

					<asp:Button ID="Button3" runat="server" Text="yes" BackColor="#1aff1a" Width="40px" ForeColor="white" OnClick="yes_click" />
					<asp:Button ID="Button4" runat="server" Text="no" BackColor="#ff5c33" Width="40px" ForeColor="White" OnClick="no_click" />
					<asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
				</div>
			</div>
			<div class="row">
				<div class="col-sm-12">

					<div class="panel">
						<div class="panel-heading">


							<asp:Button runat="server" class="btn btn-primary btn-sm tooltips pull-right" ID="advancepayrequest" type="button" Text="Pay Request" />
							<h4 class="panel-title">Advance Pay Data  </h4>

							<div class="panel-body">

								<div class="form mb20">
									<div class="col-md-3">
										<div class="floatings-labels">
											<asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="true" AppendDataBoundItems="true" CssClass="form-control" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged">
												<asp:ListItem Text="All" Value="All"></asp:ListItem>
												<asp:ListItem Text="Pending" Value="Pending"> </asp:ListItem>
												<asp:ListItem Text="Need Clarification" Value="Need Clarification"></asp:ListItem>
												<asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>
												<asp:ListItem Text="Rejected" Value="Rejected"></asp:ListItem>
											</asp:DropDownList>
											<label class="lbltop" for="txtMotherLang">Status    </label>
										</div>
									</div>
									<div class="clearfix"></div>
								</div>


								<div class="table-responsive">
									<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" EmptyDataText="No Records Found..." OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound"
										CssClass="table table-bordered table-striped    table-inverse nomargin">
										<Columns>
											<asp:BoundField DataField="ERSAdvanceID" HeaderText="AdvanceID" SortExpression="ERSAdvanceID" />
											<asp:BoundField DataField="ERSAdvanceReason" HeaderText="AdvanceReason" SortExpression="ERSAdvanceReason" />
											<asp:BoundField DataField="ERSGrossPay" HeaderText="GrossPay" SortExpression="ERSGrossPay" />
											<asp:BoundField DataField="ERSAdvanceAmount" HeaderText="AdvanceAmount" SortExpression="ERSAdvanceAmount" />
											<asp:BoundField DataField="ERSAdavanceRequestStatus" HeaderText="AdavanceRequestStatus" SortExpression="ERSAdavanceRequestStatus" />
											<asp:ButtonField ButtonType="Link" Text="<i class='fa fa-edit'></i>" ControlStyle-CssClass="btn btn-primary btn-stroke btn-icon" HeaderText="Action" HeaderStyle-CssClass="hidden-sm" ItemStyle-CssClass="hidden-sm" CommandName="Edit_Click" />
											<asp:ButtonField ButtonType="Link" Text="<i class='fa fa-trash-o'></i>" ControlStyle-CssClass="btn btn-danger btn-stroke btn-icon" HeaderText="Action" HeaderStyle-CssClass="hidden-sm" ItemStyle-CssClass="hidden-sm" CommandName="Delete_Click" />
										</Columns>
									</asp:GridView>

									<asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
									<asp:LinkButton ID="btnclose" runat="server"></asp:LinkButton>

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

	</div>
	<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
	<asp:Panel ID="payrequest" runat="server" DefaultButton="submit">
		<div class="modal-dialog  modal-lg" role="document">
			<div class="modal-content">
				<div class="modal-body">
					<div class="panel panel-profile list-view">
						<div class="panel-heading">
							<div class="media">

								<div class="media-body">
									<h4 class="media-heading">Pay Request   </h4>
								</div>
							</div>
							<!-- media -->
							<ul class="panel-options">
								<li><a href="" class="close" data-dismiss="modal" aria-hidden="true"><i class="fa fa-times"></i></a></li>
							</ul>
						</div>
						<!-- panel-heading -->

						<div class="panel-body people-info">
							<div class="row mb20">


								<div class="col-md-4">
									<div class="floatings-labels">
										<asp:TextBox ID="txtgrosspay" runat="server" required="" CssClass="floating-input" placeholder=" " ></asp:TextBox>
										<asp:Label runat="server" ID="advgrosspay" class="lbltop1 lbltop" for="nMobileNumber2">Gross Pay     </asp:Label>
									</div>
								</div>
									
								<div class="col-md-4">
									<div class="floatings-labels">
										<asp:TextBox ID="txtAmount" runat="server" required="" CssClass="floating-input" placeholder=" " OnTextChanged="txtAmount_TextChanged1" ></asp:TextBox>
										<asp:Label runat="server" ID="advanceamount" class="lbltop1 lbltop" for="nMobileNumber3">Amount  </asp:Label>
										<%--<asp:RangeValidator ID="RangeValidator1" runat="server" ValidationGroup="one" ForeColor="Red" ControlToValidate="txtAmount" MinimumValue="0" ErrorMessage="Amount should less than (or) equal to Grosspay" ></asp:RangeValidator>--%>
									</div>
									<asp:Label ID="Label2" runat="server" Text="" ForeColor="red"></asp:Label>
									<%--<asp:RequiredFieldValidator ID="checkRequiredFieldValidator" ValidationGroup="one" ForeColor="red" runat="server" ControlToValidate="txtAmount" ErrorMessage="Please Enter Valid Amount"></asp:RequiredFieldValidator>--%>
									<asp:CustomValidator ID="CustomValidator1" ControlToValidate="txtAmount" runat="server" ErrorMessage="none" ForeColor="red" ClientValidationFunction="javascript:checkfun();" ValidationGroup="one"></asp:CustomValidator>
								</div>
								<div class="col-md-4">
									<div class="floatings-labels">
										<ers3:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="date" />
										<asp:TextBox ID="date" required="" runat="server" CssClass="floating-input hasDatepicker" placeholder=" "></asp:TextBox>
										<asp:Label ID="advdate" runat="server" class="lbltop1 lbltop" for="nMobileNumber4">Required Date  </asp:Label>
									</div>
								</div>
								<div class="col-md-12">

									<div class="floatings-labels">

										<asp:TextBox ID="txtDescription" runat="server" Height="80" required="" CssClass="floating-textarea" TextMode="MultiLine" placeholder="Description"></asp:TextBox>
										<asp:Label ID="AdvancePayInfo" runat="server" ForeColor="#FF0066" Style="font-weight: bold; font-size: medium; margin-left: 85px;" Visible="false"></asp:Label>
									</div>
								</div>
								<div class="clearfix"></div>
								<div class="col-sm-3 pull-right">
									<asp:Button runat="server" ID="submit" ValidationGroup="one" CssClass="btn btn-success btn-block" Text="submit" OnClick="submit_Click" Style="margin-top: 15px;" />
								</div>
								<asp:Label ID="NoteText" runat="server" ForeColor="#FF0066" Style="font-weight: bold;"></asp:Label>
								<asp:Label ID="NoteLabel" runat="server" Font-Bold="true" ForeColor="Red" Style="font-size: 15px" Text="Note: Required Date should be atleast 3 Days from Today"></asp:Label>
							</div>


						</div>
					</div>

				</div>
			</div>
		</div>
	</asp:Panel>
	<ers3:ModalPopupExtender ID="Modalpopupextender1" runat="server" PopupControlID="payrequest" TargetControlID="advancepayrequest" CancelControlID="btnclose"
		BackgroundCssClass="modalBackground">
	</ers3:ModalPopupExtender>
	<asp:Panel runat="server" ID="UpdatePanel">
		<div class="modal-dialog  modal-lg" role="document">
			<div class="modal-content">
				<div class="modal-body">
					<div class="panel panel-profile list-view">
						<div class="panel-heading">
							<div class="media">

								<div class="media-body">
									<h4 class="media-heading">AdvancePay Details  </h4>
								</div>
							</div>
							<!-- media -->
							<ul class="panel-options">
								<li><a href="" class="close" data-dismiss="modal" aria-hidden="true"><i class="fa fa-times"></i></a></li>
							</ul>
						</div>
						<!-- panel-heading -->

						<div class="panel-body people-info">
							<div class="row mb20">
								<div class="col-md-6">

									<div class="well mb20">
										<div class="col-md-12">
											<label class="col-md-6 control-label"><b>AdvancePay ID</b></label>
											<asp:Label runat="server" ID="advanceid" CssClass="col-md-6 control-label"></asp:Label>

										</div>

										<div class="col-md-12">
											<label class="col-md-6 control-label"><b>GrossPay</b></label>
											<asp:Label runat="server" ID="advancegrosspay" CssClass="col-md-6 control-label"></asp:Label>

										</div>
										<div class="col-md-12">
											<label class="col-md-6 control-label"><b>status</b></label>
											<asp:Label runat="server" ID="advancestatus" CssClass="col-md-6 control-label"></asp:Label>

										</div>
										<div class="col-md-12">
											<label class="col-md-6 control-label"><b>ApplyDate</b></label>
											<asp:Label runat="server" ID="advanceapplydate" CssClass="col-md-6 control-label"></asp:Label>

										</div>
										<div class="col-md-12">
											<asp:Label runat="server" ID="advremarks" class="col-md-6 control-label"><b>Remarks</b></asp:Label>
											<asp:Label runat="server" ID="advanceremarks" CssClass="col-md-6 control-label"></asp:Label>

										</div>

										<div class="clearfix mb20"></div>
									</div>
								</div>


								<div class="col-md-6">
									<div class="col-md-12">
										<div class="floatings-labels">
											<asp:TextBox ID="AdvAmount" runat="server" CssClass="floating-input" placeholder=" " ></asp:TextBox>
											<label class="lbltop1 lbltop" for="nMobileNumber2">Amount   </label>
											<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="AdvAmount" Text="*" ForeColor="Red" ValidationGroup="Vgrp"></asp:RequiredFieldValidator>
											<%--   <asp:RangeValidator ID="RangeValidator2" runat="server" ValidationGroup="one" ForeColor="Red" ControlToValidate="AdvAmount" MinimumValue="0" ErrorMessage="Amount should less than (or) equal to Grosspay" ></asp:RangeValidator>--%>

											<asp:RegularExpressionValidator ID="RegularExpressionValidator5"
												ControlToValidate="AdvAmount" runat="server"
												ValidationExpression="((\d+)((\.\d{1,2})?))$" ValidationGroup="Vgrp"
												SetFocusOnError="true" ForeColor="Red" Text="Enter valid amount"></asp:RegularExpressionValidator>
											<%--<asp:RangeValidator runat="server" id="RangeValidator2" ValidationGroup="Vgrp"  controltovalidate="claimAmount" type="Double" minimumvalue="100" maximumvalue="15000" errormessage="Amount should be within 100 to 15000" ForeColor="#FF3300" />--%>
											<asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="CompareValidator" ControlToValidate="AdvAmount" ValueToCompare="2000" ValidationGroup="Vgrp" Operator="LessThanEqual" ForeColor="#FF3300" Text="Amount should be less than eligibility"></asp:CompareValidator>
										</div>
									</div>
									<div class="col-md-12">

										<div class="floatings-labels">

											<asp:TextBox ID="advreason" runat="server" Height="80" CssClass="floating-textarea" TextMode="MultiLine" placeholder="Remarks"></asp:TextBox>
											<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="Vgrp" ErrorMessage="RequiredFieldValidator" ControlToValidate="advreason" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
											<asp:Label ID="Label3" Font-Size="Medium" runat="server" Text="" Visible="true" ForeColor="Red"></asp:Label>
										</div>
									</div>


									<div class="clearfix"></div>
									<div class="col-sm-12 pull-right">
                                       <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>--%>
										<asp:Button ID="update" runat="server"  CssClass="btn btn-success btn-block" ValidationGroup="Vgrp" Style="margin-top: 15px;" Text="Update" OnClick="update_Click" />
										<%--<asp:Button runat="server" id="delete" CssClass="btn btn-sucess btn-block" Text="Delete" style="margin-top: 15px;"/>--%>
									<asp:TextBox ID="Amount" runat="server" Visible="false"></asp:TextBox>
                                                <%--</ContentTemplate>
                                        </asp:UpdatePanel>--%>
                                            </div>
                                    

								</div>


							</div>
						</div>

					</div>
				</div>
			</div>
		</div>
	</asp:Panel>
	<ers3:ModalPopupExtender ID="MPE" runat="server" PopupControlID="UpdatePanel" TargetControlID="lnkDummy" CancelControlID="btnclose"
		BackgroundCssClass="modalBackground">
	</ers3:ModalPopupExtender>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="server">
	<script type="text/javascript">
		window.onload = function () {
			var seconds = 6;
			setTimeout(function () {
				document.getElementById("<%=alert.ClientID %>").style.display = "none";
			}, seconds * 1000);
		};
	</script>
	<script>
		$(document).ready(function () {


            $("#liExpenses").addClass("active");
            $("#liApplyAdvpay").addClass("active");

			$('#cphBody_GridView1').DataTable();

			// Date Picker
			$('#cphBody_datepicker').datepicker();

		});
	</script>
    <script>
		function checkfun() {
			var amount = document.getElementById('<%=Amount.ClientID%>').value;
											var advamount = document.getElementById('<%=AdvAmount.ClientID%>').value;

                                            if (parseFloat(amount) < parseFloat(advamount)) {
												document.getElementById('<%=Label3.ClientID%>').innerText = "Please Enter Valid Amount";
												<%--document.getElementById('<%=txtAmount.ClientID%>').value = "";--%>
												document.getElementById('<%=AdvAmount.ClientID%>').focus();
												document.getElementById('<%=update.ClientID%>').disabled = true;
											}
                                            else if (parseFloat(amount) > parseFloat(advamount)) {
												document.getElementById('<%=Label3.ClientID%>').innerText = "";
												document.getElementById('<%=Label3.ClientID%>').value = "";
												document.getElementById('<%=advreason.ClientID%>').focus();
												document.getElementById('<%=update.ClientID%>').disabled = false;
											}
											else {
												document.getElementById('<%=AdvAmount.ClientID%>').value = "";

												document.getElementById('<%=AdvAmount.ClientID%>').focus();
												document.getElementById('<%=Label3.ClientID%>').innerText = "";
												document.getElementById('<%=Label3.ClientID%>').value = "";
												document.getElementById('<%=update.ClientID%>').disabled = false;
											}
		} window.onbeforeunload = DisableButton;
    </script>
</asp:Content>


