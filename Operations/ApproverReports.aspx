<%@ Page Title="" Language="C#" MasterPageFile="~/Operations/emp.master" AutoEventWireup="true" CodeFile="ApproverReports.aspx.cs" Inherits="Operations_Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
    <style>
        .dataTables_filter {
            float: right;
        }

        .table-responsive {
            overflow: unset;
        }

        #cphBody_gvList_paginate {
            float: right !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">

    <div class="mainpanel">

        <div class="contentpanel">

            <ol class="breadcrumb breadcrumb-quirk">
                <li><a href="ProcessClaim.aspx"><i class="fa fa-home mr5"></i>Home</a></li>
                <li class="active">Reports   </li>
            </ol>

            <div class="row">
                <div class="col-sm-12">

                    <div class="panel">
                        <div class="panel-heading">
 <div class="container a" style="padding-right: 100px;" id="alert" visible="true" runat="server">


<div class="alert alert-success alert-dismissable" id="alertmod" runat="server">
<a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
<strong>
<asp:Label ID="Label1" runat="server" Text="" ></asp:Label></strong>
<asp:Label ID="Label5" runat="server" Visible="true" Text=""></asp:Label>
</div>
</div>
                    <h4 class="panel-title"> Claim Reports  </h4>


                            <div class="panel-body">
                                <div class="form mb20">
                                    
                                     <div class="col-md-3">
                                    <div class="floatings-labels">


                                       
                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                        <label class="lbltop1 lbltop" for="nMobileNumber2">From Date   </label>
                                        <asp:CalendarExtender ID="CalendarExtender1"
                                runat="server" TargetControlID="TextBox1" Format="yyyy/MM/dd" />
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage=" Select a date" Text="*" ControlToValidate="TextBox1" ForeColor="Red"></asp:RequiredFieldValidator>
                       
                                    </div>
                                </div>
                                    
                                    
                                     <div class="col-md-3">
                                    <div class="floatings-labels">

                                         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                        <asp:TextBox ID="TextBox2" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                        <label class="lbltop1 lbltop" for="nMobileNumber2">To Date   </label>
                                                     <asp:CalendarExtender ID="CalendarExtender2" Format="yyyy/MM/dd"  runat="server" TargetControlID="TextBox2" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Select a date" Text="*" ControlToValidate="TextBox2" ForeColor="Red"></asp:RequiredFieldValidator>
                
               
					<asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="CompareValidator" ControlToValidate="TextBox2" ControlToCompare="TextBox1" Operator="GreaterThanEqual" text=" ToDate should be After FromDate" ForeColor="Red"></asp:CompareValidator>
                                    </div>
                                </div> 

                                     
                                          
                                          <div class="col-md-3">
                                            <%--  <asp:Button ID="Button1" runat="server" Text="Generate Report"  CssClass="btn btn-success btn-block" OnClick="Button1_Click" OnClientClick="Button1_Click1"  style="margin-top: 15px;"><i class="fa fa-search"></i></asp:Button>--%>
                                              <asp:LinkButton ID="LinkButton1" runat="server"   CssClass="btn btn-success btn-block" OnClick="Button1_Click" OnClientClick="Button1_Click1"  style="margin-top: 15px;"> <i class="fa fa-search"></i> Generate Report</asp:LinkButton>
                                              
                                          </div>
                                    
                                          <div class="col-md-3">
                                                <asp:LinkButton ID="LinkButton2" runat="server"   CssClass="btn btn-success btn-block" OnClick="Button2_Click" OnClientClick="Button2_Click1"  style="margin-top: 15px;"> <i class="fa fa-file-excel-o"></i> Excel Report</asp:LinkButton>
                                               
                                          </div>
                                          <div class="clearfix"></div>

                                          
                                      </div>
                                <div class="form mb20"></div>
                                <hr />

                                <div class="table-responsive">
<asp:GridView ID="GridClaimData" runat="server" HeaderStyle-ForeColor="White" EmptyDataText="No records found" OnRowDataBound="GridClaimData_RowDataBound" CssClass="table table-bordered table-striped    table-inverse nomargin">
                                  
                                    </asp:GridView>

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

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="Server">
  <script>
        $(document).ready(function () {
            
          //  $('#cphBody_GridView1').DataTable();

            $("#liapproverExpenses").addClass("active");
            $("#lireports").addClass("active");
        });
    </script>
</asp:Content>