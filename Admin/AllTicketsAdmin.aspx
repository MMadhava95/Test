<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true" CodeFile="AllTicketsAdmin.aspx.cs" Inherits="Admin_Default" EnableEventValidation="false" %>

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
                <li class="active">My Tickets </li>
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

                                                                
                                                                <asp:LinkButton ID="LinkButton3"
                                                                    runat="server" OnClick="LinkButton1_Click"></asp:LinkButton>

                                                                Critical



                                                            </h4>
                                                            <h3><asp:Label ID="Label2" runat="server" Text=""></asp:Label></h3>

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
                                                                
                                                                <asp:LinkButton ID="LinkButton4"
                                                                    runat="server" OnClick="LinkButton2_Click">High</asp:LinkButton></h4>
                                                            <h3><asp:Label ID="Label3" runat="server" Text=""></asp:Label></h3>

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

                                                                
                                                                <asp:LinkButton ID="LinkButton5" runat="server"
                                                                    OnClick="LinkButton3_Click">Moderate</asp:LinkButton>

                                                            </h4>
                                                            <h3><asp:Label ID="Label1" runat="server" Text=""></asp:Label></h3>

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
                                                                <asp:LinkButton ID="LinkButton6" runat="server" OnClick="LinkButton4_Click">Low</asp:LinkButton>
                                                            </h4>
                                                            <h3><asp:Label ID="Label4" runat="server" Text=""></asp:Label></h3>
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
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true"
                                        CssClass="table table-bordered table-striped    table-inverse nomargin" onclick="this.setAttribute('value', this.value);">
                                        <Columns>
                                            <asp:TemplateField ControlStyle-Width="30">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ticketSelect" runat="server" headertext="Action" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
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
             <div class="row">
                <div class="col-sm-12">
                    <div class="panel">
                        <div class="panel-heading">
                            <h4 class="panel-title">Tickets  Assign</h4>
                            <div class="panel-body">
                                <div class="form mb20">
                                    <div class="col-md-3">
                                        <asp:DropDownList runat="server" ID="DropDownList1" Style="margin-top: 25px;" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" CssClass="floating-select" value=""  >
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem>Domestic Staffing Division</asp:ListItem>
                                                <asp:ListItem>Global Development Centre</asp:ListItem>
                                                <asp:ListItem>Administration</asp:ListItem>
                                                <asp:ListItem>Technical</asp:ListItem>
                                            </asp:DropDownList>
                                            <label class="lbltop" for="txtMotherLang">Select Department</label>
                                        
                                    </div>
                                    <div class="col-md-3">
                                       <asp:Label ID="Label5" runat="server" Visible="False"></asp:Label>
                                           <asp:DropDownList ID="DropDownList2" runat="server" CssClass="floating-select" Style="margin-top: 25px;" Value="" AutoPostBack="true"> 
                                               <asp:ListItem></asp:ListItem>
                                           </asp:DropDownList>
                                           <label class="lbltop" for="txtMotherLang">Select EmployeeID</label>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:LinkButton ID="CheckEmployee" runat="server"
                                            CssClass="btn btn-success btn-block" Style="margin-top: 25px;" OnClick="CheckEmployee_Click" >
                                            <i class="fa fa-check" ></i> check  </asp:LinkButton>
                                       
                                    </div>

                                    <div class="col-md-3">
                                         <asp:LinkButton ID="Assign" runat="server" CssClass="btn btn-success btn-block"
                                            Style="margin-top: 25px;" OnClick="Assign_Click" > <i class="fa fa-forward"></i>Assign </asp:LinkButton>

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
            $("#liadminalltickets").addClass("active");
            $("#liadminalltickets").addClass("active");
        });
    </script>


    <script>
        $(document).ready(function () {
            
            $('#cphBody_GridView1').DataTable();
            $("#liadminalltickets").addClass("active");
            $("#liadminalltickets").addClass("active");

        });
    </script>
</asp:Content>

