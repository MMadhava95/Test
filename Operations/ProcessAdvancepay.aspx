<%@ Page Title="" Language="C#" MasterPageFile="~/Operations/emp.master" AutoEventWireup="true"  CodeFile="ProcessAdvancepay.aspx.cs" Inherits="Operations_Default" %>

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
                <li><a href="ProcessClaim.aspx"><i class="fa fa-home mr5"></i>Home</a></li>
                <li class="active">Advancepay list </li>
            </ol>

            <div class="row">
                <div class="col-sm-12">

                    <div class="panel">
                        <div class="panel-heading">
<div class="container a" style="padding-right: 100px;" id="Alert" visible="true" runat="server">


                                <div class="alert alert-success alert-dismissable" id="alertmodal" runat="server">
                                    <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
                                    <i class="fa fa-check-square-o" id="suc" runat="server" visible="false"></i><i class="fa-cross-square-o" id="fai" runat="server" visible="false"></i>
                                    <strong>
                                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label></strong>
                                    <asp:Label ID="Label6" runat="server" Visible="true" Text=""></asp:Label>
                                </div>
                            </div>
                    <h4 class="panel-title">Advance Pay Data  </h4>


                            <div class="panel-body">
                                 <div class="form mb20">
                                     
    <div class="col-md-3">
                                    <div class="floatings-labels">
                                        <asp:DropDownList ID="ddlstatus" runat="server" Style="color: black;" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged" CssClass="form-control">
                                                <asp:ListItem Text="All" Value="All"></asp:ListItem>
                                                <asp:ListItem Text="Pending" Value="Pending"> </asp:ListItem>
                                                <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>
                                                <asp:ListItem Text="Rejected" Value="Rejected"></asp:ListItem>
                                            </asp:DropDownList>
                                            <label class="lbltop" for="txtMotherLang">Status    </label>
                                    </div>
                                </div>
                                                                
                 <div class="clearfix"></div>

                                          
                                      </div> 
                                <div class="form mb20"></div>


                                <div class="table-responsive">
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" OnRowDataBound="GridView1_RowDataBound"
                                        EmptyDataText="No records found" DataKeyNames="ERSAdvanceID" OnRowCommand="GridView1_RowCommand"
                                        CssClass="table table-bordered table-striped table-inverse nomargin" Width="100%">
                                        <HeaderStyle BackColor="#505B72" />
                                        <Columns>
                                            <asp:BoundField />
                                            <asp:BoundField DataField="ERSAdvanceID" HeaderText="AdvanceID" SortExpression="ERSAdvanceID" />
                                            <asp:BoundField DataField="ERSAdvanceAmount" HeaderText="Amount" SortExpression="ERSAdvanceAmount" />
                                            <asp:BoundField DataField="ERSAdavanceRequestStatus" HeaderText="Status" SortExpression="ERSAdavanceRequestStatus" />
                                            <asp:BoundField DataField="ERSAdvanceReason" HeaderText="Reason" SortExpression="ERSAdvanceReason" />
                                            <asp:BoundField DataField="EmployeeName" HeaderText="EmployeeName" SortExpression="EmployeeName" />
                                             <asp:ButtonField ButtonType="Link" Text="<i class='fa fa-edit'></i>" ControlStyle-CssClass="btn btn-primary btn-stroke btn-icon" HeaderText="Action" HeaderStyle-CssClass="hidden-sm" ItemStyle-CssClass="hidden-sm" CommandName="Edit_Click" />
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


    <!-- Pop Up -->
   <asp:Panel runat="server" ID="UpdatePanel"> 
        <div class="modal-dialog  modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="panel panel-profile list-view">
                        <div class="panel-heading">
                            <div class="media">

                                <div class="media-body">
                                    <h4 class="media-heading">  Advance Pay  Details</h4>
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
                                      <div class="form-group">
                                            <asp:Label runat="server" class="col-sm-4 control-label" ID="aid">Advance Pay ID  </asp:Label>
                                            <asp:Label ID="advancepayid" runat="server" CssClass="col-sm-8 control-label "></asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" class="col-sm-4 control-label" ID="aamount">Amount</asp:Label>
                                            <asp:Label ID="advanceamount" runat="server" CssClass="col-sm-8 control-label "></asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" class="col-sm-4 control-label" ID="astatus">Status</asp:Label>
                                            <asp:Label ID="status" runat="server" CssClass="col-sm-8 control-label "></asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" class="col-sm-4 control-label" ID="aclaimdate">Claim Date</asp:Label>
                                            <asp:Label ID="advanceapplydate" runat="server" CssClass="col-sm-8 control-label "></asp:Label>
                                        </div>

                                        <div class="form-group">
                                            <asp:Label runat="server" class="col-sm-4 control-label" ID="areason">Reason</asp:Label>

                                            <asp:Label ID="advancereason" runat="server" CssClass="col-sm-8 control-label "></asp:Label>

                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" class="col-sm-4 control-label" ID="aemployeename">Employee Name</asp:Label>

                                            <asp:Label ID="empname" runat="server" CssClass="col-sm-8 control-label "></asp:Label>

                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" class="col-sm-4 control-label" ID="aprocessdate">Process Date</asp:Label>

                                            <asp:Label ID="processdate" runat="server" CssClass="col-sm-8 control-label "></asp:Label>

                                        </div>
                                        <div class="clearfix mb20"></div>
                                    </div>

                               
                                </div>
                                <div class="col-md-6">
                                    <div class="col-md-12">
                                        <div class="floatings-labels">
                                            
                                            <asp:DropDownList ID="ActionDropDown" required="required" CssClass="floating-select" runat="server" value="" onclick="this.setAttribute('value', this.value);">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem>Approved</asp:ListItem>
                                                <asp:ListItem>Rejected</asp:ListItem>
                                                <asp:ListItem>Forward to Next Level</asp:ListItem>
                                            </asp:DropDownList>

                                            <asp:Label runat="server" class="lbltop" for="txtMotherLang" ID="aaction">Action  </asp:Label>
                                        </div>
                                    </div>

                                    <div class="col-md-12">

                                        <div class="floatings-labels">

                                            <asp:TextBox ID="AdvanceRemarks" runat="server" Height="80" CssClass="floating-textarea" TextMode="MultiLine"></asp:TextBox>

                                             <asp:Label runat="server" class="lbltop" for="txtMotherLang" ID="aremarks">Remarks  </asp:Label>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="col-sm-12 pull-right">
                                        <asp:Button ID="Submit" runat="server" OnClick="Submit_Click" class="btn btn-success btn-block" Style="margin-top: 15px;" Text="Submit" />

                                        <asp:TextBox ID="AdvanceID" Class="form-control1" runat="server" ReadOnly="true" ForeColor="white" Visible="false"></asp:TextBox>
                                        <asp:Label ID="AdvanceclaimStatus" runat="server" Visible="false" ForeColor="Black"></asp:Label>
                                        <asp:Label ID="name" runat="server" Visible="false" Text="" Style="font-weight: bold;"></asp:Label>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>

                </div>
            </div>

        </div>
    </asp:Panel>
     <ers3:modalpopupextender ID="MPE" runat="server" PopupControlID="updatePanel" TargetControlID="lnkDummy" CancelControlID="btnclose"
         BackgroundCssClass="modalBackground">
    </ers3:modalpopupextender>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="Server">
	<script>
        $(document).ready(function () {
            
            $('#cphBody_GridView1').DataTable();
            $("#liapproverExpenses").addClass("active");
            $("#liprocessadvpay").addClass("active");
            // Date Picker
            $('#cphBody_datepicker').datepicker();

        });
    </script>
</asp:Content>
