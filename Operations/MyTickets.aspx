<%@ Page Title="" Language="C#" MasterPageFile="~/Operations/emp.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="MyTickets.aspx.cs" Inherits="Operations_Default" %>

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


                            <div class="panel-body">
                                <div class="form mb20">

                                    <div>

                                        <div class="col-sm-3">
                                            <div class="panel panel-danger-full panel-updates">
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-xs-7 col-lg-8">
                                                            <h4 class="panel-title text-success">


                                                                <asp:LinkButton ID="LinkButton3" CssClass="smallButton" runat="server" OnClick="LinkButton1_Click">Critical</asp:LinkButton>



                                                            </h4>
                                                            <h3>
                                                                <asp:Label ID="Label2" runat="server" Text=""></asp:Label></h3>

                                                            <p>Sold products for this month: 1,203</p>
                                                        </div>
                                                        <div class="col-xs-5 col-lg-4 text-right">
                                                            <h1 class="fsize70"><i class="fa fa-ticket"></i></h1>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- panel -->
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="panel panel-warning-full panel-updates">
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-xs-7 col-lg-8">
                                                            <h4 class="panel-title text-success">


                                                                <asp:LinkButton ID="LinkButton4" CssClass="smallButton" runat="server" OnClick="LinkButton2_Click">
                                                              Department

                                                                </asp:LinkButton>


                                                            </h4>
                                                            <h3>
                                                                <asp:Label ID="Label3" runat="server" Text=""></asp:Label></h3>

                                                            <p class="colorwhite">This Month Tickets: 1,203</p>
                                                        </div>
                                                        <div class="col-xs-5 col-lg-4 text-right">
                                                            <h1 class="fsize70"><i class="fa fa-ticket"></i></h1>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- panel -->
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="panel panel-primary-full panel-updates">
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-xs-7 col-lg-8">
                                                            <h4 class="panel-title text-success">






                                                                <asp:LinkButton ID="LinkButton5" CssClass="smallButton" runat="server" OnClick="LinkButton3_Click">
    
    SLA Level1

                                                                </asp:LinkButton>




                                                            </h4>
                                                            <h3>
                                                                <asp:Label ID="Label1" runat="server" Text=""></asp:Label></h3>

                                                            <p class="colorwhite">This Month Tickets: 1,203</p>
                                                        </div>
                                                        <div class="col-xs-5 col-lg-4 text-right">
                                                            <h1 class="fsize70"><i class="fa fa-ticket"></i></h1>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- panel -->
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="panel panel-success-full panel-updates">
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-xs-7 col-lg-8">
                                                            <h4 class="panel-title text-success">






                                                                <asp:LinkButton ID="LinkButton6" CssClass="smallButton" runat="server" OnClick="LinkButton4_Click">
    SLA Level2

                                                                </asp:LinkButton>




                                                            </h4>
                                                            <h3>
                                                                <asp:Label ID="Label4" runat="server" Text=""></asp:Label></h3>

                                                            <p class="colorwhite">This Month Tickets: 1,203</p>
                                                        </div>
                                                        <div class="col-xs-5 col-lg-4 text-right">
                                                            <h1 class="fsize70"><i class="fa fa-ticket"></i></h1>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- panel -->
                                        </div>

                                    </div>

                                    <div class="clearfix"></div>


                                </div>

                                <div class="table-responsive">




                                    <asp:GridView ID="GridView1" AllowSorting="True" EmptyDataText="No Tickets Found"
                                        AutoGenerateColumns="true" runat="server"
                                        CssClass="table table-bordered table-striped    table-inverse nomargin" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand">
                                        <Columns>
                                            <asp:ButtonField ButtonType="Link" Text="<i class='fa fa-edit'></i>" ControlStyle-CssClass="btn btn-primary btn-stroke btn-icon" HeaderText="Action" HeaderStyle-CssClass="hidden-sm" ItemStyle-CssClass="hidden-sm" CommandName="Edit_Click" />
                                        </Columns>

                                    </asp:GridView>
                                    

                                    <br />
                                    <asp:TextBox ID="TextBox1" runat="server" Visible="False"></asp:TextBox>

                                    
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


            $('#cphBody_GridView1').DataTable();

        });
    </script>
</asp:Content>

