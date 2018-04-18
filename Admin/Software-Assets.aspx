<%@ Page Title="" Language="C#" MasterPageFile="admin.master" AutoEventWireup="true" CodeFile="Software-Assets.aspx.cs" Inherits="SoftwareAssets" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
    <style>
        .dataTables_filter {
            float: right;
        }

        .table-responsive {
            overflow: unset;
        }

        #cphBody_SWgvList_paginate {
            float: right !important;
        }
    </style>

    <%-- Gridview Tooltip --%>
    <%--<script src="https://code.jquery.com/jquery-1.11.3.js"></script>
    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css"/>
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $('[id*=SWgvList] tr').each(function () {
                $(this).find("td").each(function () {
                    $(this).tooltip({ title: $(this).html(), placement: "bottom" });
                });
            });
        });
    </script>--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">

    <div class="mainpanel">
        <div class="contentpanel">
            <ol class="breadcrumb breadcrumb-quirk">
                <li><a href="../Admin/AdminDashboard.aspx"><i class="fa fa-home mr5"></i>Home</a></li>
                <li class="active">Software Assets</li>
            </ol>

            <div class="row">
                <div class="col-sm-12">
                    <div class="panel">
                        <div class="panel-heading">
                            <button class="btn btn-primary btn-sm tooltips pull-right" type="button" title="" onclick="location.href='Add-Software-Assets.aspx'" data-placement="top" data-toggle="tooltip" data-original-title="Click here to Add New Asset"><i class="fa fa-plus"></i>&nbsp;Add New Asset</button>
                            <h4 class="panel-title">Software Assets</h4>
                            <div class="panel-body">
                                <div class="form "></div>
                                <div class="table-responsive">
                                    <asp:GridView ID="SWgvList" runat="server" AutoGenerateColumns="False"
                                        CssClass="table table-bordered table-striped table-inverse nomargin"
                                        OnRowDataBound="OnRowDataBound" OnRowCommand="SWgvList_RowCommand"
                                        OnSelectedIndexChanged="OnSelectedIndexChanged">
                                        <Columns>
                                            <asp:BoundField DataField="AMS_AssetID" SortExpression="Asset ID" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs" HeaderText="Asset ID" />
                                            <asp:BoundField DataField="AMS_AssetName" HeaderText="Name" SortExpression="AMS_AssetName" />
                                            <asp:BoundField DataField="AMSSW_Version" HeaderText="Version" SortExpression="AMSSW_Version" />
                                            <asp:BoundField DataField="AMSSW_Vendor" HeaderText="Vendor" SortExpression="AMSSW_Vendor" />
                                            <asp:BoundField DataField="AMSSW_Category" HeaderText="Licensed Category" SortExpression="AMSSW_Category" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs" />
                                            <asp:BoundField DataField="AMSSW_LicensedType" HeaderText="License Type" SortExpression="AMSSW_LicensedType" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs" />
                                            <asp:BoundField DataField="AMS_Company_Location" HeaderText="Location" SortExpression="AMS_Company_Location" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs" />
                                            <asp:BoundField DataField="AMS_Date_of_Purchase" HeaderText="Purchased Date" SortExpression="AMS_Date_of_Purchase" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs" />
                                            <asp:BoundField DataField="AMSSW_VendorMail" HeaderText="Vendor Mail ID" SortExpression="AMSSW_VendorMail" HeaderStyle-CssClass=" hidden" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="AMSSW_AssetType" HeaderText="Asset Type" SortExpression="AMSSW_AssetType" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />

                                            <asp:ButtonField ButtonType="Link" Text="<i class='fa fa-edit'></i>" ControlStyle-CssClass="btn btn-primary btn-stroke btn-icon" HeaderText="Edit" HeaderStyle-CssClass="hidden-sm" ItemStyle-CssClass="hidden-sm" CommandName="Edit_Click" />
                                            <asp:ButtonField ButtonType="Link" Text="<i class='fa fa-trash-o'></i>" ControlStyle-CssClass="btn btn-danger btn-stroke btn-icon" HeaderText="Delete" CommandName="Delete_Click" />
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
    <%-- Ajax Popup --%>
    <cc1:ModalPopupExtender ID="swmp" runat="server" PopupControlID="pnlPopup" TargetControlID="lnkFake1" CancelControlID="lnkFake2"
        BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>

    <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
        <%--<div class="modal" tabindex="-1" role="dialog" id="SWdetailspopup">--%>
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="panel panel-profile list-view">
                        <div class="panel-heading" style="padding-left: 5%">
                            <div class="media">

                                <div class="media-body">
                                    <h4 class="media-heading">Software Asset Details</h4>
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
                                                <label class="col-sm-6   control-label" style="font-size: larger"><strong>Asset Id</strong></label>
                                                <asp:Label ID="lblID" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="196"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6   control-label" style="font-size: larger"><strong>Name</strong></label>
                                                <asp:Label ID="lblName" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="Visual Studio 2003"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6   control-label" style="font-size: larger"><strong>Version</strong></label>
                                                <asp:Label ID="lblVersion" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="7.1"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6   control-label" style="font-size: larger"><strong>Vendor</strong></label>
                                                <asp:Label ID="lblVendor" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="Microsoft"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6   control-label" style="font-size: larger"><strong>Licensed Category</strong></label>
                                                <asp:Label ID="lblCategory" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="On-Premises "></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6   control-label" style="font-size: larger"><strong>Licensed Type</strong></label>
                                                <asp:Label ID="lblLicense" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="Licenced   "></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6   control-label" style="font-size: larger"><strong>Locaiton</strong></label>
                                                <asp:Label ID="lblLocation" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="Vijayawada"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6   control-label" style="font-size: larger"><strong>Purchased Date</strong></label>
                                                <asp:Label ID="lblDOP" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="12-12-2018 "></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6   control-label" style="font-size: larger"><strong>Vendor MailID</strong></label>
                                                <asp:Label ID="lblMail" runat="server" CssClass="col-sm-6 control-label" Font-Size="Larger" Text="info@snpl.com"></asp:Label>
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
    <%--<link href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/css/footable.min.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/js/footable.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $('[id*=SWgvList]').footable();
        });
    </script>--%>
    <script>
        $(document).ready(function () {
            
            $('#cphBody_SWgvList').DataTable();
            $("#liadminassets").addClass("active");
            $("#liadminsw").addClass("active");

        });
    </script>
</asp:Content>
