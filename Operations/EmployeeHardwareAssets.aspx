<%@ Page Title="" Language="C#" MasterPageFile="emp.master" AutoEventWireup="true" CodeFile="EmployeeHardwareAssets.aspx.cs" Inherits="EmployeeHardwareAssets" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
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

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <div class="mainpanel">
        <div class="contentpanel">
            <ol class="breadcrumb breadcrumb-quirk">
                <li><a href="index.html"><i class="fa fa-home mr5"></i>Home</a></li>
                <li class="active">My Assets</li>
            </ol>

            <div class="row">
                <div class="col-sm-12">
                    <div class="panel">
                        <div class="panel-heading">
                    <h4 class="panel-title">Hardware Assets</h4>
                            <div class="panel-body">
                                <div class="form mb20"></div>
                                <div class="table-responsive">
                                    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False"
                                        CssClass="table table-bordered table-striped table-inverse nomargin" 
                                        OnRowDataBound="gvList_RowDataBound" OnSelectedIndexChanged="gvList_SelectedIndexChanged" >
                                        <Columns>
                                            <asp:BoundField DataField="AssetID" HeaderText="Asset ID" SortExpression="AssetID" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs" />
                                            <asp:BoundField DataField="AMS_AssetName" HeaderText="Asset Name" SortExpression="AMS_AssetName" />
                                            <asp:BoundField DataField="AMSHW_PH_Make" HeaderText="Manufacturer" SortExpression="AMSHW_PH_Make" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs" />
                                            <asp:BoundField DataField="AMSHW_PH_Model" HeaderText="Model" SortExpression="AMSHW_PH_Model" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs" />
                                            <asp:BoundField DataField="AMSHW_PH_Year_Of_Mfd" HeaderText="Year of Manufacture" SortExpression="AMSHW_PH_Year_Of_Mfd" />
                                            <asp:BoundField DataField="HW_Warrenty_Status" HeaderText="Warranty Status" SortExpression="HW_Warrenty_Status" />
                                            <asp:BoundField DataField="AMS_Company_Location" HeaderText="Location" SortExpression="AMS_Company_Location" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs" />
                                            <asp:BoundField DataField="AMS_Asset_Cost" HeaderText="Cost" SortExpression="AMS_Asset_Cost" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="HW_Serial_Number" HeaderText="Serial Number" SortExpression="HW_Serial_Number" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs" />
                                            <asp:BoundField DataField="AMS_Remarks" HeaderText="Remarks" SortExpression="AMS_Remarks" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />

                                            <asp:BoundField DataField="EmployeeAssetRequestID" HeaderText="Request ID" SortExpression="EmployeeAssetRequestID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="RequestStatus" HeaderText="Request Status" SortExpression="RequestStatus" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="RequestStatusReason" HeaderText="Status Reason" SortExpression="RequestStatusReason" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="BusinessJustification" HeaderText="Business Justification" SortExpression="BusinessJustification" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="RequestedDate" HeaderText="Requested Date" SortExpression="RequestedDate" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs" />
                                            <asp:BoundField DataField="RequestApprovedDate" HeaderText="Approved Date" SortExpression="RequestApprovedDate" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs" />
                                            <%--<asp:BoundField DataField="AssetType" HeaderText="Asset Type" SortExpression="AssetType" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />--%>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%-- Ajax Modal Popup --%>
    <cc1:ModalPopupExtender ID="EmpHAMP" runat="server" PopupControlID="EmpHADetails" TargetControlID="lnkfake1" CancelControlID="lnkfake2"
        BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>

    <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
    <asp:LinkButton ID="lnkfake1" runat="server"></asp:LinkButton>
    <asp:LinkButton ID="lnkfake2" runat="server"></asp:LinkButton>

    <%-- Popup Target Panel --%>
    <asp:Panel ID="EmpHADetails" runat="server">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="panel panel-profile list-view">
                        <div class="panel-heading" style="padding-left:5%">
                            <div class="media" >
                                <div class="media-body" >
                                    <h4 class="media-heading" id="Swasset" runat="server">
                                        <asp:Label runat="server" ID="lblheading"></asp:Label> Details</h4>
                                </div>
                            </div>
                            <ul class="panel-options" style="padding-right: 2%">
                                <li><a href="" class="close" data-dismiss="modal" aria-hidden="true"><i class="fa fa-times"></i></a></li>
                            </ul>
                        </div>
                        <div class="panel-body people-info">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="well">
                                        <div class="form-group">
                                            <%-- HA Table --%>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6 control-label" style="font-size:larger"><strong>Asset ID</strong></label>
                                                <asp:Label ID="Label1" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger"  Text="196"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6 control-label" style="font-size:larger"><strong>Name</strong></label>
                                                <asp:Label ID="Label2" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger"  Text="196"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6 control-label" style="font-size:larger"><strong>Manufacturer</strong></label>
                                                <asp:Label ID="Label3" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger"  Text="196"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6 control-label" style="font-size:larger"><strong>Model</strong></label>
                                                <asp:Label ID="Label4" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger"  Text="196"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6 control-label" style="font-size:larger"><strong>Year of Manufacture</strong></label>
                                                <asp:Label ID="Label5" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger"  Text="196"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6 control-label" style="font-size:larger"><strong>Warranty Status</strong></label>
                                                <asp:Label ID="Label6" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger"  Text="196"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6 control-label" style="font-size:larger"><strong>Location</strong></label>
                                                <asp:Label ID="Label7" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger"  Text="196"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6 control-label" style="font-size:larger"><strong>Cost</strong></label>
                                                <asp:Label ID="Label8" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger"  Text="196"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6 control-label" style="font-size:larger"><strong>Serial Number</strong></label>
                                                <asp:Label ID="Label9" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger"  Text="196"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6 control-label" style="font-size:larger"><strong>Remarks</strong></label>
                                                <asp:Label ID="Label10" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger"  Text="196"></asp:Label>
                                            </div>

                                            <%-- Employe Request Table --%>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6 control-label" style="font-size:larger"><strong>Request ID</strong></label>
                                                <asp:Label ID="Label11" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger"  Text="196"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6 control-label" style="font-size:larger"><strong>Request Status</strong></label>
                                                <asp:Label ID="Label12" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger"  Text="Visual Studio 2003"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6 control-label" style="font-size:larger"><strong>Approved Status Reason</strong></label>
                                                <asp:Label ID="Label13" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="Visual Studio 2003"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6 control-label" style="font-size:larger"><strong>Business Justification</strong></label>
                                                <asp:Label ID="Label14" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="On-Premises "></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6 control-label" style="font-size:larger"><strong>Requested Date</strong></label>
                                                <asp:Label ID="Label15" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="Vijayawada"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6 control-label" style="font-size:larger"><strong>Approved Date</strong></label>
                                                <asp:Label ID="Label16" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="Vijayawada"></asp:Label>
                                            </div>
                                        </div>

                                        <%--<div class="form-group">
                                            <label class="col-sm-6 control-label">Asset Type</label>
                                            <asp:Label ID="Label4" runat="server" CssClass="col-sm-8 control-label lead" Text="Microsoft"></asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-6 control-label">Request Status Reason</label>
                                            <asp:Label ID="Label6" runat="server" CssClass="col-sm-8 control-label lead" Text="Licenced   "></asp:Label>
                                        </div>--%>
                                        <div class="clearfix"></div>
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

            $('#cphBody_gvList').DataTable();

            $("#liAssets").addClass("active");
            $("#liEmpHardAssets").addClass("active");
        });
    </script>
</asp:Content>