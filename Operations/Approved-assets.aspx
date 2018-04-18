<%@ Page Title="" Language="C#" MasterPageFile="emp.master" AutoEventWireup="true" CodeFile="Approved-assets.aspx.cs" Inherits="Approvedassets" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
    <style>
        .dataTables_filter {
            float: right;
        }

        .table-responsive {
            overflow: unset;
        }

        #cphBody_gvSoftList_paginate {
            float: right !important;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <div class="mainpanel">
        <div class="contentpanel">
            <ol class="breadcrumb breadcrumb-quirk">
                <li><a href="index.html"><i class="fa fa-home mr5"></i>Home</a></li>
                <li class="active">Approved Assets</li>
            </ol>
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel">
                        <div class="panel-heading">
                            <h4 class="panel-title">Approved Assets </h4>
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
                                            <asp:BoundField DataField="BusinessJustification" HeaderText="Business Justification" SortExpression="BusinessJustification" />
                                            <asp:BoundField DataField="RequestedDate" HeaderText="RequestedDate" SortExpression="RequestedDate" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="RequestApprovedDate" HeaderText="Approved Date" SortExpression="RequestApprovedDate" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />

                                            <asp:ButtonField ButtonType="Link" Text="<i class='fa fa-close'></i>" ControlStyle-CssClass="btn btn-danger btn-stroke btn-icon" CommandName="Reject_click" HeaderText="Reject"></asp:ButtonField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:LinkButton Text="" ID="lnkFake" runat="server" />
                                    <asp:LinkButton Text="" ID="closeFake" runat="server" />
                                    <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%-- Ajax Modal Popup --%>
    <cc1:ModalPopupExtender ID="APreqmp" runat="server" PopupControlID="pnlPopup" TargetControlID="lnkFake" CancelControlID="closeFake" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>

    <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="panel panel-profile list-view">
                        <div class="panel-heading" style="padding-left: 5%">
                            <div class="media">
                                <div class="media-body">
                                    <h4 class="media-heading">Approved Asset Details</h4>
                                </div>
                            </div>
                            <ul class="panel-options" style="padding-right: 2%">
                                <li><a href="" class="close" data-dismiss="modal" aria-hidden="true"><i class="fa fa-times"></i></a></li>
                            </ul>
                        </div>

                        <div class="panel-body people-info">
                            <div class="row ">
                                <div class="col-md-12">
                                    <div class="well ">
                                        <div class="form-group">
                                            <div class="col-sm-12">
                                                <label class="col-sm-6 control-label" style="font-size: larger"><strong>Request Id</strong></label>
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
                                                <label class="col-sm-6 control-label" style="font-size: larger"><strong>Asset ID</strong></label>
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
                                                <asp:Label ID="Label1" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="12-12-2018 "></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6 control-label" style="font-size: larger"><strong>Approve Date</strong></label>
                                                <asp:Label ID="lblReqApprDate" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="12-12-2018 "></asp:Label>
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
        </div>
    </asp:Panel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="Server">
    <script>
        $(document).ready(function () {
            
            $('#cphBody_gvSoftList').DataTable();
            $("#liapproverassets").addClass("active");
            $("#liapproverapporvedassets").addClass("active");

        });
    </script>
</asp:Content>
