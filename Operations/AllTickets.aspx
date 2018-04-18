<%@ Page Title="" Language="C#" MasterPageFile="~/Operations/emp.master" AutoEventWireup="true" CodeFile="AllTickets.aspx.cs" Inherits="Operations_Default" %>

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
                                                            <h3> <asp:Label ID="Label1" runat="server" Text=""></asp:Label></h3>

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
                                                            <h3> <asp:Label ID="Label2" runat="server" Text=""></asp:Label></h3>

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
                                                            <h3>  <asp:Label ID="Label3" runat="server" Text=""></asp:Label></h3>

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
                                                            <h3>  <asp:Label ID="Label4" runat="server" Text=""></asp:Label></h3>

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
                        
                                     <asp:GridView id="GridView1" runat="server" autogeneratecolumns="true"   emptydatatext="No Records Found" cssclass="table table-bordered table-striped  table-inverse nomargin">
                                        <Columns>

                                            <asp:TemplateField ControlStyle-Width="10">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ticketSelect" runat="server" />
                                                </ItemTemplate>
                                        </asp:TemplateField>
                                          
                                       
                                        </Columns>
                                        
                                    </asp:GridView>


                                    <asp:Label ID="Label6" runat="server" Visible="False"></asp:Label>
                                </div>


                            </div>
                        </div>
                        <!-- panel -->


                    </div>
                    <!-- col-sm-6 -->

                </div>
                <!-- row -->

            </div>



            <div class="row">
                <div class="col-sm-12">

                    <div class="panel">
                        <div class="panel-heading">
                            <h4 class="panel-title">Tickets  Assign</h4>
                            <div class="panel-body">
                                <div class="form mb20">
                                    <div class="col-md-3">
                                        <div class="floatings-labels">
                                            <asp:Label ID="Label5" runat="server" Visible="False"></asp:Label>
                                           
                                            <asp:DropDownList ID="DropDownList1" CssClass="floating-select" runat="server" >
                                            </asp:DropDownList>
                                           


                                            <label class="lbltop" for="txtMotherLang">Select Analyst</label>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:LinkButton ID="Button2" runat="server"
                                            CssClass="btn btn-success btn-block" Style="margin-top: 15px;" OnClick="Button2_Click">
                                            <i class="fa fa-check" ></i> check  </asp:LinkButton>
                                       
                                    </div>

                                    <div class="col-md-3">
                                         <asp:LinkButton ID="Button1" runat="server" CssClass="btn btn-success btn-block"
                                            Style="margin-top: 15px;" OnClick="Button1_Click"> <i class="fa fa-forward"></i>Assign </asp:LinkButton>

                                        <asp:Label ID="department" runat="server" Visible="False"></asp:Label>

                                    </div>
                                    <div class="clearfix"></div>


                                </div>
                                <div class="form mb20"></div>
                                <hr />
                                <div class="table-responsive">
                                   <asp:Table ID="Table1" runat="server" Visible="false"  CssClass="table table-bordered table-striped    table-inverse nomargin">
                                    <asp:TableRow BackColor="#2bbbad">
                                        <asp:TableCell  align="Center"><asp:Label ID="Label7" runat="server" Text="Employee ID" ForeColor="White"></asp:Label></asp:TableCell>
                                        <asp:TableCell  align="Center"><asp:Label ID="Label8" runat="server" Text="Department"  ForeColor="White"></asp:Label></asp:TableCell>
                                        <asp:TableCell  align="Center"><asp:Label ID="Label9" runat="server" Text="New" ForeColor="White"></asp:Label></asp:TableCell>
                                        <asp:TableCell align="Center"><asp:Label ID="Label10" runat="server" Text="Pending"  ForeColor="White"></asp:Label></asp:TableCell>
                                        <asp:TableCell  align="Center"><asp:Label ID="Label11" runat="server" Text="Waiting Customer Reply"  ForeColor="White"></asp:Label></asp:TableCell>
                                        <asp:TableCell  align="Center"><asp:Label ID="Label12" runat="server" Text="Resolved"  ForeColor="White"></asp:Label></asp:TableCell>
                                        <asp:TableCell  align="Center"><asp:Label ID="Label13" runat="server" Text="Closed"  ForeColor="White"></asp:Label></asp:TableCell>
                                     </asp:TableRow>
                                     <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="empID" runat="server"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="depart" runat="server"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="newTKTCount" runat="server"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="pendingTKTCount" runat="server" ></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="wcrTKTCount" runat="server"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="resolvedTKTCount" runat="server"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="closedTKTCount" runat="server"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>



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

            $("#liapproverticket").addClass("active");
            $("#lialltickets").addClass("active");

            $('#cphBody_GridView1').DataTable();
            $('#cphBody_GridView1').DataTable();

        });
    </script>
</asp:Content>

