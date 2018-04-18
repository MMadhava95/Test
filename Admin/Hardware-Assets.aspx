<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true" CodeFile="Hardware-Assets.aspx.cs" Inherits="HardwareAssets" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
    <style>
        .dataTables_filter {
            float: right;
        }

        .table-responsive {
            overflow: unset;
        }

        #cphBody_HWgvList_paginate {
            float: right !important;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">

    <div class="mainpanel">

        <div class="contentpanel">

            <ol class="breadcrumb breadcrumb-quirk">
                <li><a href="../Admin/AdminDashboard.aspx"><i class="fa fa-home mr5"></i>Home</a></li>
                <li class="active">Hardware Assets</li>
            </ol>

            <div class="row">
                <div class="col-sm-12">

                    <div class="panel">
                        <div class="panel-heading">
                            <button class="btn btn-primary btn-sm tooltips pull-right" type="button" title="" onclick="location.href='add-hardware-assets.aspx'" data-placement="top" data-toggle="tooltip" data-original-title="Create Member"><i class="fa fa-plus"></i>&nbsp;Add New Asset</button>
                            <h4 class="panel-title">Hardware Assets</h4>
                            <div class="panel-body">
                                <div class="form "></div>
                                <div class="table-responsive">
                                    <asp:GridView ID="HWgvList" runat="server" AutoGenerateColumns="False"
                                        CssClass="table table-bordered table-striped table-inverse nomargin"
                                        OnRowDataBound="OnRowDataBound1" OnSelectedIndexChanged="OnSelectedIndexChanged1" 
                                        OnRowCommand="HWgvList_RowCommand">
                                        <Columns>

                                            <asp:BoundField DataField="AMS_AssetID" HeaderText="Asset ID" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs" ItemStyle-Wrap="true" HeaderStyle-Width="1%" HeaderStyle-Wrap="true" SortExpression="AMS_AssetID" />
                                            <asp:BoundField DataField="AMS_AssetName" HeaderText="Name" SortExpression="AMS_AssetName" />
                                            <asp:BoundField DataField="AMSHW_PH_Make" HeaderText="Manufacture" SortExpression="AMSHW_PH_Make" />
                                            <asp:BoundField DataField="AMSHW_PH_Model" HeaderText="Model" SortExpression="AMSHW_PH_Model" />

                                            <%--<asp:BoundField DataField="AMS_Asset_Cost" HeaderText="Cost" SortExpression="AMS_Asset_Cost" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />--%>
                                            <asp:TemplateField HeaderText="Cost" HeaderStyle-CssClass="hidden-xs" SortExpression="AMS_Asset_Cost" ItemStyle-CssClass="hidden-xs">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" ForeColor="" BackColor="Transparent" font-color="white" CssClass="fa fa-inr" Text='<%# Bind("AMS_Asset_Cost", " {0:0.00}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="AMS_Date_of_Purchase" HeaderText="Purchased Date" SortExpression="AMS_Date_of_Purchase" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="AMSHW_PH_Characterstics" HeaderText="Characteristics" SortExpression="AMSHW_PH_Characterstics" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs" />
                                            <asp:BoundField DataField="AMS_Company_Location" HeaderText="Location" SortExpression="AMS_Company_Location" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs" />
                                            <asp:BoundField DataField="HW_Warrenty_Status" HeaderText="Warranty Status" SortExpression="HW_Warrenty_Status" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs" />
                                            <asp:BoundField DataField="AMSHW_PH_AssetType" HeaderText="Asset Type" SortExpression="AMSHW_PH_AssetType" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="HW_Serial_Number" HeaderText="Serial Number" SortExpression="HW_Serial_Number" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />

                                            <asp:ButtonField ButtonType="Link" Text="<i class='fa fa-edit'></i>" HeaderText="Edit" ControlStyle-CssClass="btn btn-primary btn-stroke btn-icon" CommandName="Edit_Click" />
                                            <asp:ButtonField ButtonType="Link" Text="<i class='fa fa-trash-o'></i>" HeaderText="Delete" ControlStyle-CssClass="btn btn-danger btn-stroke btn-icon" CommandName="Delete_Click" />

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
    <%-- Bootstrap Modal Popup --%>
    
    <%-- Ajax Modal Popup --%>
    <cc1:ModalPopupExtender ID="hwmp" runat="server" PopupControlID="pnlPopup" TargetControlID="lnkFake1" CancelControlID="lnkFake2" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>

    <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">

        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="panel panel-profile list-view">
                        <div class="panel-heading" style="padding-left: 5%">
                            <div class="media">
                                <div class="media-body">
                                    <h4 class="media-heading">Hardware Asset Details</h4>
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
                                        <div class="form-group">
                                        <div class="col-sm-12">
                                            <label class="col-sm-6   control-label" style="font-size: larger"><strong>Asset Id </strong></label>
                                            <asp:Label ID="lblID" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="196"></asp:Label>
                                        </div>
                                        <div class="col-sm-12">
                                            <label class="col-sm-6   control-label" style="font-size: larger"><strong>Name  </strong></label>
                                            <asp:Label ID="lblName" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="Visual Studio 2003"></asp:Label>
                                        </div>
                                        <div class="col-sm-12">
                                            <label class="col-sm-6   control-label" style="font-size: larger"><strong>Manufacturer</strong></label>
                                            <asp:Label ID="lblMake" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="7.1"></asp:Label>
                                        </div>
                                        <div class="col-sm-12">
                                            <label class="col-sm-6   control-label" style="font-size: larger"><strong>Model</strong></label>
                                            <asp:Label ID="lblModel" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="Microsoft"></asp:Label>
                                        </div>
                                        <div class="col-sm-12">
                                            <label class="col-sm-6   control-label" style="font-size: larger"><strong>Cost</strong></label>
                                            <asp:Label ID="lblCost" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="On-Premises "></asp:Label>
                                        </div>
                                        <div class="col-sm-12">
                                            <label class="col-sm-6   control-label" style="font-size: larger"><strong>Characteristics</strong></label>
                                            <asp:Label ID="lblSpecs" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="Licenced   "></asp:Label>
                                        </div>
                                        <div class="col-sm-12">
                                            <label class="col-sm-6   control-label" style="font-size: larger"><strong>Location  </strong></label>
                                            <asp:Label ID="lblLocation" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="Vijayawada"></asp:Label>
                                        </div>
                                        <div class="col-sm-12">
                                            <label class="col-sm-6   control-label" style="font-size: larger"><strong>Purchased Date  </strong></label>
                                            <asp:Label ID="lblDOP" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="12-12-2018 "></asp:Label>
                                        </div>
                                        <div class="col-sm-12">
                                            <label class="col-sm-6   control-label" style="font-size: larger"><strong>Warranty Status  </strong></label>
                                            <asp:Label ID="lblwsts" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="info@snpl.com"></asp:Label>
                                        </div>
                                        <div class="col-sm-12">
                                            <label class="col-sm-6   control-label" style="font-size: larger"><strong>Serial Number</strong></label>
                                            <asp:Label ID="lblSno" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="info@snpl.com"></asp:Label>
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
            
            $('#cphBody_HWgvList').DataTable();
            $("#liadminassets").addClass("active");
            $("#liadminhw").addClass("active");

        });
    </script>
</asp:Content>
