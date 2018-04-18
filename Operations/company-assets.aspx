<%@ Page Title="" Language="C#" MasterPageFile="emp.master" AutoEventWireup="true" CodeFile="company-assets.aspx.cs" Inherits="companyassets" EnableEventValidation="false" %>

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
                <li><a href="HomePage.aspx"><i class="fa fa-home mr5"></i>Home</a></li>
                <li class="active">Company Assets</li>
            </ol>
            <div class="row">
                <div class="col-sm-6">
                    <div class="panel">
                        <div class="panel-heading">
                            <h4 class="panel-title">Software Assets </h4>
                            <div class="panel-body">
                                <div class="form mb20"></div>
                                <div class="table-responsive">
                                    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False"
                                        CssClass="table table-bordered table-striped table-inverse nomargin"
                                        OnRowDataBound="gvList_RowDataBound" OnSelectedIndexChanged="gvList_SelectedIndexChanged">
                                        <Columns>
                                            <asp:BoundField DataField="AMS_AssetID" SortExpression="Asset ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" HeaderText="AMS_AssetID" />
                                            <asp:BoundField DataField="AMS_AssetName" HeaderText="Name" SortExpression="AMS_AssetName" />
                                            <asp:BoundField DataField="AMSSW_Version" HeaderText="Version" SortExpression="AMSSW_Version" />
                                            <asp:BoundField DataField="AMSSW_Vendor" HeaderText="Vendor" SortExpression="AMSSW_Vendor" />
                                            <asp:BoundField DataField="AMSSW_Category" HeaderText="Licensed Category" SortExpression="AMSSW_Category" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="AMSSW_LicensedType" HeaderText="LicensedType" SortExpression="AMSSW_LicensedType" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs" />
                                            <asp:BoundField DataField="AMS_Company_Location" HeaderText="Location" SortExpression="AMS_Company_Location" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="AMS_Date_of_Purchase" HeaderText="Purchased Date" SortExpression="AMS_Date_of_Purchase" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="AMSSW_VendorMail" HeaderText="Vendor Mail ID" SortExpression="AMSSW_VendorMail" HeaderStyle-CssClass=" hidden hidden-xs" ItemStyle-CssClass="hidden hidden-xs" />
                                            <asp:BoundField DataField="AMSSW_AssetType" HeaderText="Asset Type" SortExpression="AMSSW_AssetType" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Swpanel" TargetControlID="lnkfake1" CancelControlID="lnkfake2"
                                        BackgroundCssClass="modalBackground">
                                    </cc1:ModalPopupExtender>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="panel">
                        <div class="panel-heading">
                            <h4 class="panel-title">Hardware Assets</h4>
                            <div class="panel-body">
                                <div class="form mb20"></div>
                                <div class="table-responsive">
                                    <asp:GridView ID="gvHardware" runat="server" AutoGenerateColumns="False"
                                        CssClass="table table-bordered table-striped table-inverse nomargin"
                                        OnRowDataBound="gvHardware_RowDataBound" OnSelectedIndexChanged="gvHardware_SelectedIndexChanged">
                                        <Columns>
                                            <asp:BoundField DataField="AMS_AssetID" HeaderText="Asset ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" ItemStyle-Wrap="true" HeaderStyle-Width="1%" HeaderStyle-Wrap="true" SortExpression="AMS_AssetID" />
                                            <asp:BoundField DataField="AMS_AssetName" HeaderText="Name" SortExpression="AMS_AssetName" />
                                            <asp:BoundField DataField="AMSHW_PH_Make" HeaderText="Make" SortExpression="AMSHW_PH_Make" />
                                            <asp:BoundField DataField="AMSHW_PH_Model" HeaderText="Model" SortExpression="AMSHW_PH_Model" />
                                            <asp:BoundField DataField="AMS_Asset_Cost" HeaderText="Cost" SortExpression="AMS_Asset_Cost" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="AMS_Date_of_Purchase" HeaderText="Purchased Date" SortExpression="AMS_Date_of_Purchase" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="AMSHW_PH_Characterstics" HeaderText="Characterstics" SortExpression="AMSHW_PH_Characterstics" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs" />
                                            <asp:BoundField DataField="AMS_Company_Location" HeaderText="Location" SortExpression="AMS_Company_Location" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="HW_Warrenty_Status" HeaderText="Warranty Status" SortExpression="HW_Warrenty_Status" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="AMSHW_PH_AssetType" HeaderText="Asset Type" SortExpression="AMSHW_PH_AssetType" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:LinkButton ID="lnkfake1" runat="server"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkfake2" runat="server"></asp:LinkButton>
                                    <cc1:ModalPopupExtender ID="Hwextender" runat="server" TargetControlID="lnkfake1" CancelControlID="lnkfake2" PopupControlID="Hwpanel"></cc1:ModalPopupExtender>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:Panel ID="Swpanel" runat="server">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="panel panel-profile list-view">
                        <div class="panel-heading" style="padding-left: 5%">
                            <div class="media">
                                <div class="media-body">
                                    <h4 class="media-heading" id="Swasset" runat="server">Software Asset Details</h4>
                                    <asp:Label ID="Assettype" runat="server" Text="Software Assets" Visible="false"></asp:Label>
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
                                        <div class=" form-group">
                                            <div class="col-sm-12">
                                                <label class="col-sm-6   control-label" style="font-size: larger"><strong>Asset ID</strong></label>
                                                <asp:Label ID="swlbl1" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="196"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6   control-label" style="font-size: larger"><strong>Name</strong></label>
                                                <asp:Label ID="swlbl2" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="Visual Studio 2003"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6   control-label" style="font-size: larger"><strong>Version</strong></label>
                                                <asp:Label ID="swlbl3" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="7.1"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6   control-label" style="font-size: larger"><strong>Vendor</strong></label>
                                                <asp:Label ID="swlbl4" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="Microsoft"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6   control-label" style="font-size: larger"><strong>Licensed Category</strong></label>
                                                <asp:Label ID="swlbl5" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="On-Premises "></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6   control-label" style="font-size: larger"><strong>Licensed Type</strong></label>
                                                <asp:Label ID="swlbl6" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="Licenced   "></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6   control-label" style="font-size: larger"><strong>Locaiton</strong></label>
                                                <asp:Label ID="swlbl7" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="Vijayawada"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6   control-label" style="font-size: larger"><strong>Purchased Date</strong></label>
                                                <asp:Label ID="swlbl8" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="12-12-2018 "></asp:Label>
                                            </div>
                                            <div class="col-sm-12" runat="server" visible="false">
                                                <label class="col-sm-6   control-label" style="font-size: larger"><strong>Vendor MailID</strong></label>
                                                <asp:Label ID="swlbl9" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="info@snpl.com"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6   control-label" style="font-size: larger"><strong>Business Justification</strong></label>
                                                <asp:TextBox ID="swtxtBJ" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-12" style="padding-top: 1%">
                                                <asp:Button ID="SWButton" runat="server" Text="Submit" CssClass="btn btn-primary btn-block pull-right" OnClick="SWButton_Click" />
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

    <asp:Panel ID="Hwpanel" runat="server">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="panel panel-profile list-view">
                        <div class="panel-heading" style="padding-left: 5%">
                            <div class="media">
                                <div class="media-body">
                                    <h4 class="media-heading">Hardware Asset Details</h4>
                                    <asp:Label ID="HardwareAssettype" runat="server" Visible="false" Text="Hardware Assets"></asp:Label>
                                </div>
                            </div>
                            <!-- media -->
                            <ul class="panel-options" style="padding-right: 2%">
                                <li><a href="" class="close" data-dismiss="modal" aria-hidden="true"><i class="fa fa-times"></i></a></li>
                            </ul>
                        </div>

                        <div class="panel-body people-info">
                            <div class="row ">
                                <div class="col-md-12">
                                    <div class="well ">
                                        <div class=" form-group">
                                            <div class="col-sm-12">
                                                <label class="col-sm-6   control-label" style="font-size: larger"><strong>Asset Id</strong></label>
                                                <asp:Label ID="hwlbl1" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="196"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6   control-label" style="font-size: larger"><strong>Name</strong></label>
                                                <asp:Label ID="hwlbl2" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="Visual Studio 2003"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6   control-label" style="font-size: larger"><strong>Make</strong></label>
                                                <asp:Label ID="hwlbl3" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="7.1"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6   control-label" style="font-size: larger"><strong>Modal</strong></label>
                                                <asp:Label ID="hwlbl4" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="Microsoft"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6   control-label" style="font-size: larger"><strong>Cost</strong></label>
                                                <asp:Label ID="hwlbl5" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="On-Premises "></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6   control-label" style="font-size: larger"><strong>Characteristics</strong></label>
                                                <asp:Label ID="hwlbl6" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="Licenced   "></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6   control-label" style="font-size: larger"><strong>Location</strong></label>
                                                <asp:Label ID="hwlbl7" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="Vijayawada"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6   control-label" style="font-size: larger"><strong>Purchased Date  </strong></label>
                                                <asp:Label ID="hwlbl8" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="12-12-2018 "></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6   control-label" style="font-size: larger"><strong>Warranty Status</strong></label>
                                                <asp:Label ID="hwlbl9" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="info@snpl.com"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6   control-label" style="font-size: larger"><strong>Business Justification</strong></label>
                                                <asp:TextBox ID="hwtxtBJ" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-12" style="padding-top: 1%">
                                                <asp:Button ID="HwButton" runat="server" Text="Submit" CssClass="btn btn-primary btn-block pull-right" OnClick="HwButton_Click" />
                                            </div>
                                        </div>
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
            $('#cphBody_gvHardware').DataTable();

            $("#liAssets").addClass("active");
            $("#liEmpCmpnyAssets").addClass("active");

            $("#liapproverassets").addClass("active");
            $("#liapprovercompanyassets").addClass("active");

        });
    </script>
</asp:Content>
