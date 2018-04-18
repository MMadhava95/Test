<%@ Page Title="" Language="C#" MasterPageFile="admin.master" AutoEventWireup="true" CodeFile="EmployeeList.aspx.cs" Inherits="EmployeeList" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ers3" %>
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

        .modalBackground {
            background-color: gray;
            filter: alpha(opacity=100);
            opacity: 0.9;
        }
    </style>
    <script type="text/javascript">  
        window.onload = function () {
            var seconds = 6;
            setTimeout(function () {
                document.getElementById("<%=alert.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };

    </script>
    <script type="text/javascript">
        function openModal() {
            $('#divViewEmp').modal('Show');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">

    <div class="mainpanel">

        <div class="contentpanel">
            <ol class="breadcrumb breadcrumb-quirk">
                <li><a href="AdminViewClaims.aspx"><i class="fa fa-home mr5"></i>Home</a></li>
                <li class="active">Employee Data </li>
            </ol>
            <div class="container a" id="alert" visible="true" runat="server">


                <div class="alert alert-success alert-dismissable" id="alertmod" runat="server">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
                    <strong>
                        <asp:Label ID="Label3" runat="server" Text=""></asp:Label></strong>
                    <asp:Label ID="Label5" runat="server" Visible="true" Text=""></asp:Label>
                </div>
            </div>

            <div class="container " style="background-color: #d7ecc6" id="Div1" visible="true" runat="server">


                <div class="alert alert-success alert-dismissable" style="background-color: #d7ecc6" id="Div2" runat="server">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
                    <strong>

                        <asp:Label ID="Label4" runat="server" Text="Do You Want To Delete?" ForeColor="black" Font-Size="Medium"></asp:Label></strong>

                    <asp:Button ID="Button1" runat="server" Text="yes" BackColor="#1aff1a" Font-Size="Medium" Width="40px" ForeColor="white" OnClick="Button1_Click" />
                    <asp:Button ID="Button2" runat="server" Text="no" BackColor="#ff5c33" Font-Size="Medium" Width="40px" ForeColor="White" OnClick="Button2_Click" />
                    <asp:Label ID="Label6" runat="server" Text="Label" Visible="false" Font-Size="Medium"></asp:Label>
                </div>
            </div>

            <br />

            <div class="row">
                <div class="col-sm-12">

                    <div class="panel">
                        <div class="panel-heading">
                            <button class="btn btn-primary btn-sm tooltips pull-right" onclick="location.href='AddEmployee.aspx'" type="button" title="" data-placement="top" data-toggle="tooltip" data-original-title="Create Member"><i class="fa fa-pencil"></i>Add Employee </button>
                            <h4 class="panel-title">Employee Data </h4>
                            <div class="panel-body">
                                <div class="form mb20">
                                    <div class="col-md-6">
                                        <div class="floatings-labels">
                                            <asp:TextBox ID="txtsearch" CssClass="floating-input" placeholder=" " runat="server"></asp:TextBox>
                                            <label class="lbltop1 lbltop" for="nMobileNumber2">Search </label>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Button ID="gridsearch" OnClick="gridsearch_Click" CssClass="btn btn-success btn-block" Style="margin-top: 15px;" runat="server" Text="Search" UseSubmitBehavior="false" CausesValidation="false" /></asp:Button>
                                        
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Button ID="Cancelsearch" UseSubmitBehavior="false" CausesValidation="false" OnClick="Cancelsearch_Click" runat="server" Text="Clear" CssClass="btn btn-success btn-block" Style="margin-top: 15px;" />
                                    </div>
                                </div>
                                <div class="table-responsive">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" EmptyDataText="No records found" OnRowDataBound="GridView1_RowDataBound"
                                        CssClass="table table-bordered table-striped    table-inverse nomargin" PopupControlID="divViewEmp" DataKeyNames="EmployeeID" OnSelectedIndexChanging="GridView1_SelectedIndexChanging">
                                        <Columns>
                                            <asp:BoundField DataField="EmployeeID" HeaderText="EmployeeID" ReadOnly="True" SortExpression="EmployeeID" />
                                            <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" SortExpression="EmployeeName" />
                                            <asp:BoundField DataField="EmployeeMailID" HeaderText="Employee MailID" SortExpression="EmployeeMailID" />
                                            <asp:ButtonField ButtonType="Link" Text="<i class='fa fa-edit'></i>" ControlStyle-CssClass="btn btn-primary btn-stroke btn-icon" HeaderText="Action" HeaderStyle-CssClass="hidden-sm" ItemStyle-CssClass="hidden-sm" CommandName="Edit_Click" />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:LinkButton ID="LinkButton1" runat="server"></asp:LinkButton>
                                    <asp:LinkButton ID="btnClose" runat="server"></asp:LinkButton>
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

    <!--POP up for UPdate-->
    <asp:Panel ID="updatePanel" runat="server">
        <div class="modal-dialog  modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="panel panel-profile list-view">
                        <div class="panel-heading">
                            <div class="media">

                                <div class="media-body">
                                    <h4 class="media-heading">Employee  Details</h4>
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
                                            <asp:Label ID="id" runat="server" CssClass="col-sm-4 control-label " Font-Bold="true" Text="EmployeeID "></asp:Label>
                                            <asp:Label ID="EmployeeID2" runat="server" class="col-sm-8 control-label" ReadOnly="true"></asp:Label>

                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="ename" runat="server" Text="Name " Font-Bold="true" class="col-sm-4 control-label"></asp:Label>
                                            <asp:Label ID="EmployeeName" CssClass="col-sm-8 control-label" runat="server" ReadOnly="true"></asp:Label>

                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="emailid" runat="server" Text="MailID" Font-Bold="true" class="col-sm-4 control-label"></asp:Label>
                                            <asp:Label ID="EmployeeMailID" class="col-sm-8 control-label" runat="server" ReadOnly="true"></asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label1" runat="server" Text="Is Approver?" Font-Bold="true" class="col-sm-4 control-label"></asp:Label>
                                            <asp:Label ID="Label2" runat="server" Text="" CssClass="col-sm-8 control-label"></asp:Label>

                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label55" runat="server" Text="Location" class="col-sm-4 control-label" Font-Bold="true"></asp:Label>

                                            <asp:Label ID="Label7" runat="server" ReadOnly="true" CssClass="col-sm-8 control-label"></asp:Label>
                                        </div>

                                        <div class="clearfix mb20"></div>

                                    </div>
                                </div>
                                <div class="col-md-6">

                                    <div class="col-md-6">

                                        <div class="form-group">
                                            <asp:Label ID="edesignation" runat="server" Font-Bold="true"> Role</asp:Label>
                                            <asp:TextBox ID="EmployeeDesignation" Class="form-control1" runat="server" ReadOnly="false"></asp:TextBox>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="EmployeeDesignation" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>


                                    <div class="col-md-6">

                                        <div class="form-group">
                                            <asp:Label ID="edepartment" for="nMobileNumber1" runat="server" Font-Bold="true">Designation</asp:Label>
                                            <asp:TextBox ID="EmployeeDepartment" Class="form-control1" runat="server" ReadOnly="false"></asp:TextBox>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="EmployeeDepartment" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>



                                    <div class="clearfix"></div>
                                    <div class="col-sm-6 pull-right">
                                        <asp:Button ID="MakeApprover" runat="server" Text="Make Approver" class="btn btn-success btn-block" Style="margin-top: 15px;" OnClick="MakeApprover_Click" />

                                    </div>
                                    <div class="col-sm-6 pull-right">
                                        <asp:Button ID="update" runat="server" Text="Update" OnClick="update_Click" class="btn btn-success btn-block" Style="margin-top: 15px;" />

                                    </div>

                                    <div class="clearfix"></div>
                                    <div class="col-sm-12 pull-right">
                                        <asp:Button ID="Delete" CausesValidation="false" runat="server" OnClick="Delete_Employee" Text="delete" class="btn btn-success btn-block" Style="margin-top: 15px;" />

                                    </div>

                                </div>

                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <!--POP up for UPdate-->

    <ers3:ModalPopupExtender ID="MPE" runat="server" PopupControlID="updatePanel" TargetControlID="LinkButton1" CancelControlID="btnclose"
        BackgroundCssClass="modalBackground">
    </ers3:ModalPopupExtender>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="Server">
</asp:Content>

