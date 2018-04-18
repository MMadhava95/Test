<%@ Page Title="" Language="C#" MasterPageFile="emp.master" AutoEventWireup="true" CodeFile="EmployeePenReqAssets.aspx.cs" Inherits="EmployeePenReqAssets" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
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
                    <h4 class="panel-title">Pending & Rejected Assets </h4>
                            <div class="panel-body">
                                <div class="form mb20"></div>
                                <div class="table-responsive">

                                    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False"
                                        CssClass="table table-bordered table-striped    table-inverse nomargin" 
                                        OnRowDataBound="gvList_RowDataBound" OnSelectedIndexChanged="gvList_SelectedIndexChanged">
                                        <%--<Columns>
                                            <asp:TemplateField HeaderText="Claim ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="30px" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTitle" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEstdYr" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAffil" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Approver Name">
                                                <ItemTemplate>
                                                    <itemtemplate>
                                    <asp:Label ID="lblDesc" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                </itemtemplate>
                                                    <itemstyle horizontalalign="Left" />
                                                    <headerstyle horizontalalign="Left" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Actions">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbtnEdit" runat="server" ToolTip="Edit" CommandName="Edit"
                                                        data-placement="top" title="" data-original-title="Edit"  data-toggle="modal" data-target="#divEditClaim"
                                                        CssClass="btn btn-primary btn-stroke btn-icon" CommandArgument='<%#Eval("Id")%>'><span class="fa fa-edit"></span> </asp:LinkButton>
                                                    <asp:LinkButton ID="lnkbtnDelete" runat="server" ToolTip="Delete" CommandName="Delete IRD"
                                                        data-toggle="tooltip" data-placement="top" title="" data-original-title="Delete"
                                                        CssClass="btn btn-danger btn-stroke btn-icon" CommandArgument='<%#Eval("Id")%>'><span class="fa fa-trash-o"></span> </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>--%>
                                        <Columns>
                                    <asp:BoundField DataField="EmployeeAssetRequestID" HeaderText="Request ID" SortExpression="EmployeeAssetRequestID" />
                                    <asp:BoundField DataField="RequestStatus" HeaderText="Request Status" SortExpression="RequestStatus" />
                                    <asp:BoundField DataField="AssetID" HeaderText="Asset ID" SortExpression="AssetID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                    <asp:BoundField DataField="AssetType" HeaderText="Asset Type" SortExpression="AssetType" />
                                    <asp:BoundField DataField="BusinessJustification" HeaderText="Business Justification" SortExpression="BusinessJustification" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs" />
                                    <asp:BoundField DataField="RequestStatusReason" HeaderText="Request Reason" SortExpression="RequestStatusReason" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                    <asp:BoundField DataField="RequestedDate" HeaderText="Requested Date" SortExpression="RequestedDate" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs" />
                                    <asp:BoundField DataField="RequestRejectedDate" HeaderText="Rejected Date" SortExpression="RequestRejectedDate" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs" />
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
    <cc1:ModalPopupExtender ID="EmpPenReqMP" runat="server" PopupControlID="EmppenReqDetails" TargetControlID="lnkfake1" CancelControlID="lnkfake2"
        BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:LinkButton ID="lnkfake1" runat="server"></asp:LinkButton>
    <asp:LinkButton ID="lnkfake2" runat="server"></asp:LinkButton>
    <%-- Popup Target Panel --%>
    <asp:Panel ID="EmppenReqDetails" runat="server">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="panel panel-profile list-view">
                        <div class="panel-heading" style="padding-left:5%">
                            <div class="media">
                                <div class="media-body">
                                    <h4 class="media-heading" id="Swasset" runat="server">
                                        <asp:Label runat="server" ID="lblheading">Pending</asp:Label>
                                        Asset Details</h4>
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
                                    <div class="well">
                                        <div class="form-group">
                                            <div class="col-sm-12">
                                                <label class="col-sm-6 control-label" style="font-size:larger"><strong>Request Id</strong></label>
                                                <asp:Label ID="Label1" runat="server" CssClass="col-sm-6 control-label " Font-Size="Larger"  Text="196"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6 control-label" style="font-size:larger"><strong>Request Status</strong></label>
                                                <asp:Label ID="Label2" runat="server" CssClass="col-sm-6 control-label " Font-Size="Larger"  Text="Visual Studio 2003"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6 control-label" style="font-size:larger"><strong>Asset ID</strong></label>
                                                <asp:Label ID="Label3" runat="server" CssClass="col-sm-6 control-label " Font-Size="Larger"  Text="7.1"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6 control-label" style="font-size:larger"><strong>Asset Type</strong></label>
                                                <asp:Label ID="Label4" runat="server" CssClass="col-sm-6 control-label " Font-Size="Larger"  Text="Microsoft"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6 control-label" style="font-size:larger"><strong>Business Justification</strong></label>
                                                <asp:Label ID="Label5" runat="server" CssClass="col-sm-6 control-label " Font-Size="Larger"  Text="On-Premises "></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6 control-label" style="font-size:larger"><strong>Request Reason</strong></label>
                                                <asp:Label ID="Label6" runat="server" CssClass="col-sm-6 control-label " Font-Size="Larger"  Text="Licenced   "></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label class="col-sm-6 control-label" style="font-size:larger"><strong>Requested Date</strong></label>
                                                <asp:Label ID="Label7" runat="server" CssClass="col-sm-6 control-label " Font-Size="Larger"  Text="Vijayawada"></asp:Label>
                                            </div>
                                            <div class="col-sm-12" id="divrejectdate" runat="server">
                                                <label id="lblrejdate" runat="server" class="col-sm-6 control-label" style="font-size:larger"><strong></strong></label>
                                                <asp:Label ID="Label8" runat="server" CssClass="col-sm-6 control-label " Font-Size="Larger"  Text=""></asp:Label>
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
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" Runat="Server">
    <script>
        $(document).ready(function () {

            $('#cphBody_gvList').DataTable();
            $("#liAssets").addClass("active");
            $("#liEmpPenReqAssets").addClass("active");

        });
    </script>
</asp:Content>

