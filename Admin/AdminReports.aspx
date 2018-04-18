<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="AdminReports.aspx.cs" Inherits="Admin_Default" %>
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

        .fsize70 {
            font-size: 70px;
        }

        .colorwhite {
            color: #ffffff !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">

    <div class="mainpanel">

        <div class="contentpanel">

            <ol class="breadcrumb breadcrumb-quirk">
                <li><a href="index.html"><i class="fa fa-home mr5"></i>Home</a></li>
                <li class="active">My Tickets     </li>
            </ol>

            <div class="row">
                <div class="col-sm-12">

                    <div class="panel">
                        <div class="panel-heading">







                            <h4 class="panel-title">My Tickets </h4>
                             <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                            <div class="panel-body">


                                <div class="form mb20">
                                    <div class="col-md-2">
                                        <div class="floatings-labels">
                                            <asp:TextBox ID="fromDate" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="fromDate" />
                                            <label class="lbltop1 lbltop" for="focusedinput">From</label>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="floatings-labels">
                                            <asp:TextBox ID="todate" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="todate" />
                                            <label class="lbltop1 lbltop" for="nMobileNumber1">To</label>
                                        </div>
                                    </div>


                                    <div class="col-md-2">
                                        <div class="floatings-labels">
                                            <asp:DropDownList ID="DropDownList1" CssClass="floating-select" runat="server">
                                                <asp:ListItem>Select</asp:ListItem>
                                                <asp:ListItem>All</asp:ListItem>
                                                <asp:ListItem>Critical</asp:ListItem>
                                                <asp:ListItem>High</asp:ListItem>
                                                <asp:ListItem>Moderate</asp:ListItem>
                                                <asp:ListItem>Low</asp:ListItem>
                                                <asp:ListItem>Sla</asp:ListItem>

                                            </asp:DropDownList>

                                            <label class="lbltop" for="focusedinput">Priority      </label>
                                        </div>
                                    </div>





                                    <div class="col-md-2">

                                        <asp:LinkButton ID="generateReport" OnClick="generateReport_Click" runat="server" CssClass="btn btn-success btn-block" Style="margin-top: 15px;"> <i class="fa fa-file-o"></i> Generate Report  </asp:LinkButton>

                                    </div>

                                    <div class="col-md-2">
                                        <asp:LinkButton ID="exportToExcel" OnClick="exportToExcel_Click" Visible="false" runat="server" CssClass="btn btn-success btn-block" Style="margin-top: 15px;"> <i class="fa fa-file-excel-o"></i> Export To Excel  </asp:LinkButton>

                                    </div>

                                    <div class="col-md-2">
                                        <asp:LinkButton ID="exportToWord" OnClick="exportToWord_Click" Visible="false" runat="server" CssClass="btn btn-success btn-block" Style="margin-top: 15px;"> <i class="fa fa-file-pdf-o"></i> Export To Word  </asp:LinkButton>

                                    </div>
                                    <div class="clearfix"></div>


                                </div>

                                <div class="table-responsive">




                                    <asp:GridView ID="GridTicketData"
                                        runat="server"
                                        GridLines="None"
                                        AutoGenerateColumns="true"
                                        AllowPaging="true"
                                        OnPageIndexChanging="GridTicketData_PageIndexChanging"
                                        CssClass="table table-bordered table-striped    table-inverse nomargin">
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

    <link href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/css/footable.min.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/js/footable.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $('[id*=GridView1]').footable();
        });
    </script>

    <script>
        $(document).ready(function () {


            $('#cphBody_gvList').DataTable();

        });
    </script>
</asp:Content>

