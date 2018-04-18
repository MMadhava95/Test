<%@ Page Title="" Language="C#" MasterPageFile="emp.master" AutoEventWireup="true" CodeFile="Rejected-assets.aspx.cs" Inherits="Rejectedassets" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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
                <li class="active">Rejected Assets</li>
            </ol>
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel">
                        <div class="panel-heading">
                            <h4 class="panel-title">Pending Assets</h4>
                            <div class="panel-body">
                                <div class="form "></div>
                                <div class="table-responsive">
                                    <asp:GridView ID="gvSoftList" runat="server" AutoGenerateColumns="False"
                                        CssClass="table table-bordered table-striped table-inverse nomargin"
                                        OnRowDataBound="gvSoftList_RowDataBound"
                                        OnSelectedIndexChanged="gvSoftList_SelectedIndexChanged"
                                        OnRowCommand="gvSoftList_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="EmployeeAssetRequestID" HeaderText="Request ID" SortExpression="EmployeeAssetRequestID" ReadOnly="True" />
                                            <asp:BoundField DataField="RequestStatus" HeaderText="Request Status" SortExpression="RequestStatus" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="RequestEmployeeName" HeaderText="Employee Name" ItemStyle-Width="450px" SortExpression="RequestEmployeeName" HeaderStyle-Width="12%" />
                                            <asp:BoundField DataField="RequestEmployeeId" HeaderText="Employee ID" SortExpression="RequestEmployeeId" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs" />
                                            <asp:BoundField DataField="RequestedEmployeeMailId" HeaderText="Employee Mail ID" SortExpression="RequestedEmployeeMailId" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs" HeaderStyle-Width="20%" />
                                            <asp:BoundField DataField="AssetID" HeaderText="Asset ID" SortExpression="AssetID" />
                                            <asp:BoundField DataField="AssetType" HeaderText="Asset Type" SortExpression="AssetType" />
                                            <asp:BoundField DataField="BusinessJustification" HeaderText="BusinessJustification" SortExpression="BusinessJustification" />
                                            <asp:BoundField DataField="RequestedDate" HeaderText="RequestedDate" SortExpression="RequestedDate" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="RequestRejectedDate" HeaderText="RequestRejectedDate" SortExpression="RequestRejectedDate" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />

                                            <asp:ButtonField ButtonType="Link" Text="<i class='fa fa-check'></i>" ControlStyle-CssClass="btn btn-success btn-stroke btn-icon" CommandName="Approve_click" HeaderText="Approve"></asp:ButtonField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:LinkButton Text="" ID="lnkFake1" runat="server" />
                                    <asp:LinkButton Text="" ID="lnkFake2" runat="server" />
                                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%-- Rejected Ajax Modal Popup --%>
    <cc1:ModalPopupExtender ID="Rejmp" runat="server" PopupControlID="RejpnlPopup" TargetControlID="lnkFake1" CancelControlID="lnkFake2" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="RejpnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="panel panel-profile list-view">
                        <div class="panel-heading" style="padding-left: 5%">
                            <div class="media">
                                <div class="media-body">
                                    <h4 class="media-heading">Rejected Asset Details</h4>
                                </div>
                            </div>
                            <!-- media -->
                            <ul class="panel-options" style="padding-right: 2%">
                                <li><a href="" class="close" data-dismiss="modal" aria-hidden="true"><i class="fa fa-times"></i></a></li>
                            </ul>
                        </div>
                        <!-- panel-heading -->
                        <div class="panel-body people-info">
                            <div class="row ">
                                <div class="col-md-12">
                                    <div class="well ">
                                        <div class="form-group">
                                            <div class="col-sm-12">
                                                <label class="col-sm-6 control-label" style="font-size: larger"><strong>Request ID</strong></label>
                                                <asp:Label ID="lblPAReqID" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="196"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6 control-label" style="font-size: larger"><strong>Status</strong></label>
                                                <asp:Label ID="lblPAReqStatus" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="196"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6 control-label" style="font-size: larger"><strong>Employee Name</strong></label>
                                                <asp:Label ID="lblPAReqEmpName" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="Visual Studio 2003"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6 control-label" style="font-size: larger"><strong>Employee ID</strong></label>
                                                <asp:Label ID="lblPAReqEmpID" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="7.1"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6 control-label" style="font-size: larger"><strong>Employee Mail ID</strong></label>
                                                <asp:Label ID="lblPAReqEmpMailID" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="Microsoft"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6 control-label" style="font-size: larger"><strong>Assets ID</strong></label>
                                                <asp:Label ID="lblPAReqAssetID" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="On-Premises "></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6 control-label" style="font-size: larger"><strong>Assets Type</strong></label>
                                                <asp:Label ID="lblPAReqAssetType" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="Licenced   "></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6 control-label" style="font-size: larger"><strong>Business Justification</strong></label>
                                                <asp:Label ID="lblPAReqBJ" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="Vijayawada"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6 control-label" style="font-size: larger"><strong>Requested Date</strong></label>
                                                <asp:Label ID="lblReqDate" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="12-12-2018 "></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6 control-label" style="font-size: larger"><strong>Rejected Date</strong></label>
                                                <asp:Label ID="lblReqRejDate" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="12-12-2018 "></asp:Label>
                                            </div>
                                        </div>
                                        <div class="clearfix "></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="Server">
    <script>
        $(document).ready(function () {
            
            $('#cphBody_gvSoftList').DataTable();
            $('#cphBody_gvHardList').DataTable();

            $("#liapproverassets").addClass("active");
            $("#liapproverRejectedassets").addClass("active");

        });
    </script>
</asp:Content>
