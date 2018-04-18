<%@ Page Title="" Language="C#" MasterPageFile="emp.master" AutoEventWireup="true" CodeFile="ApplyClaim.aspx.cs" Inherits="ApplyClaim" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ers3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
    <style>
        .dataTables_filter {
            float: right;
        }
        .table-responsive {
            overflow: unset;
        }
        #cphBody_GridView1_paginate {
            float: right !important;
        }
		.zoom {
            padding-right: 150px;
            border: none;
            transition: transform .2s;
            width: 100px;
            height: 100px;
            margin: 0 auto;
            border-color: transparent;
        }

            .zoom:hover {
                -ms-transform: scale(2.0); /* IE 9 */
                -webkit-transform: scale(2.0); /* Safari 3-8 */
                transform: scale(2.0);
            }
            .modalBackground {
background-color:gray;
filter: alpha(opacity=100);
opacity: 0.9;
}

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">

    <div class="mainpanel">

        <div class="contentpanel">

            <ol class="breadcrumb breadcrumb-quirk">
                <li><a href="Dashboard.aspx"><i class="fa fa-home mr5"></i>Home</a></li>
                <li class="active">Claim List   </li>
            </ol>

            <div class="row">
                <div class="col-sm-12">
					<div class="container a" style="padding-right: 100px;" id="alert" visible="true" runat="server">
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

				<asp:Label ID="Label1" runat="server" Text="Do You Want To Delete?" ForeColor="black"></asp:Label></strong>
			<asp:LinkButton ID="Button" runat="server" Text="yes" BackColor="#1aff1a" Width="40px" BorderStyle="Solid" CausesValidation="false" ForeColor="white" OnClick="Button_Click"></asp:LinkButton>
			<asp:LinkButton ID="delete" runat="server" Text="no" BackColor="#ff5c33" Width="40px" BorderStyle="Solid" CausesValidation="false" ForeColor="White" OnClick="delete_Click" ></asp:LinkButton>
						<asp:Label ID="Label3" runat="server" Text="Label" Visible="false"></asp:Label>
		</div>
	</div>
                    <div class="panel">
                        <div class="panel-heading">
                             <button class="btn btn-primary btn-sm tooltips pull-right" type="button" title="" data-placement="top" data-original-title="Eligibility Criteria" data-toggle="modal" data-target="#divEligibility"><i class="fa fa-shield"></i>Eligibility Criteria  </button>
                            <asp:Button runat="server" class="btn btn-primary btn-sm tooltips pull-right" type="button" title="" ID="newclaim" Text="+ New Claim"></asp:Button>
    
                    <h4 class="panel-title">Claim Data  </h4>


                            <div class="panel-body">

                                <div class="form mb20">
									<div class="col-md-3">
                                        <div class="floatings-labels">
                                            <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged" CssClass="form-control">
                                                <asp:ListItem Text="All" Value="All"></asp:ListItem>
                                                <asp:ListItem Text="Pending" Value="Pending"> </asp:ListItem>
                                                <asp:ListItem Text="Need Clarification" Value="Need Clarification"></asp:ListItem>
                                                <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>
                                                <asp:ListItem Text="Rejected" Value="Rejected"></asp:ListItem>
                                            </asp:DropDownList>
                                            <label class="lbltop" for="txtMotherLang">Status</label>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>


                                <div class="table-responsive">




                                     <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found" CssClass="table table-bordered table-striped  table-inverse nomargin" OnRowDataBound="grid1_RowDataBound" OnRowCommand="grid1_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="ERSClaimID" HeaderText="ClaimID" SortExpression="ERSClaimID" />
                                            <asp:BoundField DataField="ERSClaimType" HeaderText="Type" SortExpression="ERSClaimType" />
                                            <asp:BoundField DataField="ERSClaimDate" HeaderText="ClaimDate" SortExpression="ERSClaimDate" />
                                            <asp:BoundField DataField="ERSClaimStatus" HeaderText="Status" SortExpression="ERSClaimStatus" />
                                            <asp:BoundField DataField="ERSBillAmount" HeaderText="BillAmount" SortExpression="ERSBillAmount" />
                                            <asp:BoundField DataField="ERSApproverName" HeaderText="ApproverName" SortExpression="ERSApproverName" />
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

    <asp:Panel runat="server" ID="Applyclaimpanel">
        <div class="modal-dialog  modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="panel panel-profile list-view">
                        <div class="panel-heading">
                            <div class="media">

                                <div class="media-body">
                                    <h4 class="media-heading">Aply Claim </h4>
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
                                         <asp:DropDownList ID="ClaimDropDown" runat="server" AutoPostBack="true" required="" OnSelectedIndexChanged="ClaimDropDown_SelectedIndexChanged" CssClass="form-control">
                                            <asp:ListItem Value="" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="Medical">Medical</asp:ListItem>
                                            <asp:ListItem Value="Miscellaneous and Entertainment">Miscellaneous and Entertainment</asp:ListItem>
                                            <asp:ListItem Value="Repairs and Maintenance">Repairs and Maintenance</asp:ListItem>
                                            <asp:ListItem Value="Local Travel">Local Conveyance</asp:ListItem>
                                            <asp:ListItem Value="TravelOutStation">OutStation Travel</asp:ListItem>
                                        </asp:DropDownList>

                                        <label class="lbltop" for="txtMotherLang">Select Claim type  </label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="one" InitialValue="Select Claim Type" SetFocusOnError="true" runat="server" ErrorMessage=" please select claim type" ControlToValidate="ClaimDropDown" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="floatings-labels">


                                        <asp:DropDownList ID="currencydropdown" runat="server" required="" CssClass="form-control">
                                            <asp:ListItem Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="INR">INR</asp:ListItem>
                                            <asp:ListItem Value="USD">USD</asp:ListItem>
                                            <asp:ListItem Value="EUR">EUR</asp:ListItem>
                                            <asp:ListItem Value="SARAND">SARAND</asp:ListItem>
                                            <asp:ListItem Value="CAD">CAD</asp:ListItem>
                                            <asp:ListItem Value="GBP">GBP</asp:ListItem>
                                            <asp:ListItem Value="JPY">JPY</asp:ListItem>
                                            <asp:ListItem Value="AUD">AUD</asp:ListItem>
                                        </asp:DropDownList>
                                        <label class="lbltop" for="txtMotherLang">Select Currency  </label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="one" InitialValue="Select Currency" SetFocusOnError="true" runat="server" ErrorMessage=" please select currency" ControlToValidate="currencydropdown" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>


                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="floatings-labels">
<asp:TextBox ID="txtBillAmount" Required="" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                        <label class="lbltop1 lbltop" for="nMobileNumber2">Bill Amount  </label>
                                        <ers3:FilteredTextBoxExtender ID="TextBox1_FilteredTextBoxExtender" runat="server"
                                            Enabled="True" TargetControlID="txtBillAmount" FilterType="Numbers, Custom" ValidChars="."></ers3:FilteredTextBoxExtender>
                                        <asp:Label ID="Label9" runat="server" ForeColor="Black" Text=""></asp:Label><asp:Label ID="Label2" ForeColor="Black" runat="server" Text=" "></asp:Label>
                                    </div>
                                </div>
</div>
                                <div class="row mb20">
                                <div class="col-md-4">
                                    <div class="floatings-labels">
                                        <asp:TextBox ID="txtRemarks" Required="" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                        <label class="lbltop1 lbltop" for="nMobileNumber3">Remarks  </label>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="auto-style1">
                                        <asp:Label runat="server" ID="Label11" Visible="true"></asp:Label>
                                        <asp:FileUpload ID="FileUpload1" Required="" CssClass="fileContainer" runat="server" Onchange="ShowImagePreview(this);" />
                                        <asp:Label ID="Label23" runat="server" Text="Note: upload .jpg, .bmp, .png and .gif files only" Font-Size="Medium" ForeColor="blue"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-4" style="border:0px solid none">
                                    <asp:Image ID="ImgPrv" Style="padding-left:80px" runat="server" BorderStyle="none" BorderWidth="0px" CssClass="zoom"  />
                                    <asp:Label ID="Label7" Visible="false" runat="server" Text="" ForeColor="Red"></asp:Label>
                                </div>

                            </div>
                            <div class="row mb20">
                                <div class="col-md-8">

                                    <div class="auto-style3">

                                        <asp:TextBox ID="TextBox2" runat="server" Required="" Height="150" CssClass="floating-textarea" TextMode="MultiLine" placeholder="Description"></asp:TextBox>
                                        
                                    </div>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="CompareValidator" ControlToValidate="txtBillAmount" ValueToCompare="0" ValidationGroup="one" Operator="GreaterThan" ForeColor="#FF3300" Text="Amount should be greater than zero"></asp:CompareValidator>
                                </div>

                                <%-- <div class="row mb20">--%>
                                <%--<div class="col-md-6">
                                  
                                </div>--%>
                                <div class="clearfix"></div>
                                <div class="col-sm-3 pull-right">
                                    <asp:Button ID="submit" CssClass="btn btn-success btn-block" Style="margin: 15px" ValidationGroup="one" runat="server" Text="submit" OnClick="submit_Click" />

                                </div>
                            </div>
                            <%--<div class="col-sm-3">
									<asp:Label ID="Label" runat="server" Visible="false"></asp:Label>
									
								</div>--%>
                            <div class="clearfix"></div>
                            <asp:Label ID="name" runat="server" Visible="false"></asp:Label>

                        </div>

                    </div>
                </div>
            </div>
        </div>

    </asp:Panel>
    <ers3:ModalPopupExtender ID="MPEapplyclaim" runat="server" PopupControlID="Applyclaimpanel" TargetControlID="newclaim" CancelControlID="btnclose"
        BackgroundCssClass="modalBackground">
    </ers3:ModalPopupExtender>

    
   <asp:Panel runat="server" ID="UpdatePannel">
        <div class="modal-dialog  modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="panel panel-profile list-view">
                        <div class="panel-heading">
                            <div class="media">

                                <div class="media-body">
                                    <h4 class="media-heading">Edit Claim </h4>
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
                                            <label class="col-md-6 control-label"><b>Claim ID</b></label>
                                            <asp:Label ID="claimID" runat="server" CssClass="col-md-6 control-label"></asp:Label>
                                        </div>
                                        <%--<div class="form-group">
                                            <label class="col-sm-4 control-label">Claim ID  </label>
                                            <asp:Label ID="claimID" runat="server" CssClass="col-sm-8 control-label"></asp:Label>
                                        </div>--%>
                                        <div class="col-md-12">
                                            <label class="col-md-6 control-label"><b>Claim Type</b>  </label>
                                            <asp:Label ID="claimtype" runat="server" CssClass="col-md-6 control-label" Text=""></asp:Label>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="col-md-6 control-label"><b>Claim Date</b>  </label>
                                            <asp:Label ID="claimdate" runat="server" CssClass="col-md-6 control-label" Text=""></asp:Label>
                                        </div>

                                        <div class="col-md-12">
                                            <label class="col-md-6 control-label"><b>Status</b>  </label>
                                            <asp:Label ID="claimStatus" runat="server" CssClass="col-md-6 control-label"></asp:Label>
                                        </div>
                                        <div class="col-md-12">
                                            <label class="col-md-6 control-label"><b>Description</b>  </label>
                                            <asp:Label ID="claimDescription" runat="server" CssClass="col-md-6 control-label" Text=""></asp:Label>
                                        </div>

                                        <div class="col-md-12">
                                            <label class="col-md-6 control-label"><b>Approver Name</b></label>
                                            <asp:Label ID="appname" runat="server" CssClass="col-md-6 control-label" Text=""></asp:Label>
                                        </div>
                                        <div class="col-md-12">
                                            <asp:Label runat="server" ID="appremarks" class="col-md-6 control-label"><b> Approver Remarks</b> </asp:Label>
                                            <asp:Label ID="ApproverRemarks" runat="server" CssClass="col-md-6 control-label " Text=""></asp:Label>
                                        </div>
                                        <div class="col-md-12">
                                            <asp:Label runat="server" ID="cpdate" class="col-md-6 control-label"><b> claimProcessdate</b> </asp:Label>
                                            <asp:Label ID="ClaimProcessDate" runat="server" CssClass="col-md-6 control-label " Text=""></asp:Label>
                                        </div>
                                        <div class="clearfix mb20"></div>
                                    </div>
                                </div>
                                    

                                <div class="col-md-6">
                                    <div class="col-md-12">
                                        <div class="floatings-labels">
                                            <asp:TextBox ID="claimAmount" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                            <label class="lbltop1 lbltop" for="nMobileNumber2">Amount   </label>
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="claimAmount" Text="*" ForeColor="Red" ValidationGroup="Vgrp"></asp:RequiredFieldValidator>
                                
                              
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5"
                                    ControlToValidate="claimAmount" runat="server"
                                    ValidationExpression="((\d+)((\.\d{1,2})?))$" ValidationGroup="Vgrp"
                                    SetFocusOnError="true" ForeColor="Red" Text="Enter valid amount"></asp:RegularExpressionValidator>
                                <%--<asp:RangeValidator runat="server" id="RangeValidator2" ValidationGroup="Vgrp"  controltovalidate="claimAmount" type="Double" minimumvalue="100" maximumvalue="15000" errormessage="Amount should be within 100 to 15000" ForeColor="#FF3300" />--%>
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="CompareValidator" ControlToValidate="claimAmount" ValueToCompare="0" ValidationGroup="Vgrp" Operator="GreaterThan" ForeColor="#FF3300" Text="Amount should be greater than zero"></asp:CompareValidator>
                                        </div>
                                    </div>
                                    <div class="col-md-12">

                                        <div class="floatings-labels">

                                            <asp:TextBox ID="UserRemarks" runat="server" Height="80" CssClass="floating-textarea" TextMode="MultiLine" placeholder="Remarks"></asp:TextBox>
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="Vgrp" ErrorMessage="RequiredFieldValidator" ControlToValidate="UserRemarks" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>


                                    <div class="clearfix"></div>
                                    <div class="col-sm-12 pull-right">
										<asp:LinkButton ID="update" runat="server" CssClass="btn btn-success btn-block" ValidationGroup="Vgrp" Style="margin-top: 15px;" Text="Update" OnClick="update_Click"></asp:LinkButton>
										<%--<asp:Button ID="update" runat="server" CssClass="btn btn-success btn-block" ValidationGroup="Vgrp" Style="margin-top: 15px;" Text="Update" OnClick="update_Click" />--%>
                                       <%-- <asp:Button ID="update" runat="server" CssClass="btn btn-success btn-block" ValidationGroup="Vgrp" Style="margin-top: 15px;" Text="Update" OnClick="update_Click" />--%>
                                    </div>
                                </div>

                            </div>


                        </div>
                    </div>

                </div>
            </div>
        </div>
        <asp:Label ID="Label4" Visible="false" runat="server" Text=""></asp:Label>
    </asp:Panel>
    <ers3:ModalPopupExtender ID="MPE" runat="server" PopupControlID="UpdatePannel" TargetControlID="lnkDummy" CancelControlID="btnclose"
        BackgroundCssClass="modalBackground">
    </ers3:ModalPopupExtender>


    <div class="modal" tabindex="-1" role="dialog" id="divEligibility">
        <div class="modal-dialog  modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="panel panel-profile list-view">
                        <div class="panel-heading">
                            <div class="media">

                                <div class="media-body">
                                    <h4 class="media-heading">ELIGIBILITY CRITERIA</h4>
                                </div>
                            </div>
                            <!-- media -->
                            <ul class="panel-options">
                                <li><a href="" class="close" data-dismiss="modal" aria-hidden="true"><i class="fa fa-times"></i></a></li>
                            </ul>
                        </div>
                        <!-- panel-heading -->

                        <div class="panel-body people-info">
                           <table class="table table-hover">


                                <thead>
                                    <th style="margin-left: 10px; color: black">All amounts in (<i class="fa fa-inr"></i>) </th>

                                    <th colspan="5" style="text-align: right;">Employee Cadre</th>

                                    <tr>

                                        <th>
                                            <asp:Label ID="lblrole" runat="server" Text="Expense Type" Style="color: black;"></asp:Label>
                                        </th>

                                        <th>
                                            <asp:Label ID="lbljunior" runat="server"  Text="Junior" Style="color: black;"></asp:Label>
                                        </th>
                                        <th>
                                            <asp:Label ID="lblmiddle" runat="server"  Text=" Middle" Style="color: black;"></asp:Label>
                                        </th>
                                        <th>
                                            <asp:Label ID="lblsenior" runat="server"  Text="Senior" Style="color: black;"></asp:Label>
                                        </th>

                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <th>
                                            <div style="color: black; font-weight: bold">Medical</div>
                                        </th>
                                        <td>
                                            <asp:Label ID="lblmjuniour" runat="server" Style="color: black;"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblmmiddle" runat="server" Style="color: black;"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblmseniour" runat="server" Style="color: black;"></asp:Label>
                                        </td>


                                    </tr>
                                    <tr>
                                        <th>
                                            <div style="color: black; font-weight: bold">
                                                Miscellaneous and
											<br />
                                                Entertainment Expenses
                                            </div>
                                        </th>
                                        <td>
                                            <asp:Label ID="lblmiscjuniour" runat="server" Style="color: black;"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblmiscmiddle" runat="server" Style="color: black;"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblmiscseniour" runat="server" Style="color: black;"></asp:Label>
                                        </td>

                                    </tr>

                                    <tr>
                                        <th>
                                            <div style="color: black; font-weight: bold">Repairs and Maintainance</div>
                                        </th>
                                        <td>
                                            <asp:Label ID="lblrjuniour" runat="server" Style="color: black;"></asp:Label>
                                        </td>
                                        <td>
            <asp:Label ID="lblrmiddle" runat="server" Style="color: black;"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblrseniour" runat="server" Style="color: black;"></asp:Label>
                                        </td>

                                    </tr>

                                    <tr>
                                        <th>
                                            <div style="color: black; font-weight: bold">Outstation Travel</div>
                                        </th>
                                        <td>
                                            <asp:Label ID="lbltjuniour" runat="server" Style="color: black;"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbltmiddle" runat="server" Style="color: black;"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbltseniour" runat="server" Style="color: black;"></asp:Label>
                                        </td>

                                    </tr>


                                    <tr>
                                        <th>
                                            <div style="color: black; font-weight: bold">Local conveyance</div>
                                        </th>
                                        <td>
                                            <asp:Label ID="lblljuniour" runat="server" Style="color: black;"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbllmiddle" runat="server" Style="color: black;"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbllseniour" runat="server" Style="color: black;"></asp:Label>
                                        </td>

                                    </tr>


                                </tbody>
                            </table>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="Server">
   <script src="//code.jquery.com/jquery-1.11.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
		function ShowImagePreview(input) {
			if (input.files && input.files[0]) {
				var reader = new FileReader();
				reader.onload = function (e) {
					$('#<%=ImgPrv.ClientID%>').prop('src', e.target.result)
						.width(160)
						.height(100);
				};
				reader.readAsDataURL(input.files[0]);
			}
		}
    </script>
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
            $("#liApplycliam").addClass("active");
            $("#ApplyAdvpaylist").addClass("active");
			$('#cphBody_GridView1').DataTable();

		});
    </script>

    <script type="text/javascript">
		function openModal() {
			$('#divEditClaim').modal('show');

			keyboard: "true";
			backdrop: "static";
		}
    </script>
</asp:Content>
