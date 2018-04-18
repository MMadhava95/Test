<%@ Page Title="" Language="C#" MasterPageFile="emp.master" AutoEventWireup="true" CodeFile="history.aspx.cs" Inherits="history" EnableEventValidation="false" %>

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
                <li><a href="index.html"><i class="fa fa-home mr5"></i>Home</a></li>
                <li class="active">History     </li>
            </ol>

            <div class="row">
                <div class="col-sm-12">

                    <div class="panel">
                        <div class="panel-heading">
                            <h4 class="panel-title">History</h4>
                            <div class="panel-body">

                                <div class="form mb20"></div>


                                <div class="table-responsive">




                                    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found"
                                        CssClass="table table-bordered table-striped  table-inverse nomargin" OnRowDataBound="gvList_RowDataBound"
                                        OnRowCommand="gvList_RowCommand">
                                        <Columns>

                                            <asp:BoundField DataField="TMSTicketID" HeaderText="Ticket ID" ReadOnly="True" SortExpression="TMSTicketID" />
                                            <asp:BoundField DataField="TMSTicketSubject" HeaderText="Subject" SortExpression="TMSTicketSubject" />
                                            <asp:BoundField DataField="TMSTicketPriority" HeaderText="Priority" SortExpression="TMSTicketPriority" />
                                            <asp:BoundField DataField="TMSTicketStatus" HeaderText="Status" SortExpression="TMSTicketStatus" />
                                            <asp:BoundField DataField="TMSTicketDate" HeaderText="Generated Date" SortExpression="TMSTicketDate" />
                                            <asp:ButtonField ButtonType="Link" Text="<i class='fa fa-edit'></i>" ControlStyle-CssClass="btn btn-primary btn-stroke btn-icon" HeaderText="Action" HeaderStyle-CssClass="hidden-sm" ItemStyle-CssClass="hidden-sm" CommandName="Edit_Click" />
                                            <%-- <asp:ButtonField ButtonType="Link" Text="<i class='glyphicon glyphicon-retweet'></i>" ControlStyle-CssClass="btn btn-danger btn-stroke btn-icon" HeaderText="Action" HeaderStyle-CssClass="hidden-sm" ItemStyle-CssClass="hidden-sm" CommandName="Delete_Click" />--%>
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
            <!-- contentpanel -->
        </div>
        <!-- mainpanel -->

    </div>


    <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>

    <br />
    <asp:TextBox ID="TextBox1" runat="server" Visible="False"></asp:TextBox>
    <br />




</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="Server">
    <script>
        $(document).ready(function () {


            $('#cphBody_gvList').DataTable();
            $("#liticket").addClass("active");
            $("#litickethistory").addClass("active");
        });
    </script>
</asp:Content>

