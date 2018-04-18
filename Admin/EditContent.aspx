<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true" CodeFile="EditContent.aspx.cs" Inherits="Admin_Default" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
    <style>
        .dataTables_filter {
            float: right;
        }

        .table-responsive {
            overflow: unset;
        }

        #cphBody_gvVideoList_paginate {
            float: left !important;
        }
        #cphBody_gvContentList_paginate {
            float: right !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
        <div class="mainpanel">

        <div class="contentpanel">

            <ol class="breadcrumb breadcrumb-quirk">
                <li><a href="ViewCourses.aspx"><i class="fa fa-home mr5"></i>Home</a></li>
                <li class="active">Course Content</li>
            </ol>
            <asp:Label ID="Alert" style="color:green;font-size:20px;padding-left:30px;" runat="server" Text=""></asp:Label>
            <div class="row">

                <div class="col-sm-6">

                    <div class="panel">
                         
                        <div class="panel-heading">
                            <h4 class="panel-title">Videos</h4>
                            <div class="panel-body">
                                <div class="form mb20"></div>
                                <div class="table-responsive">
                                    <asp:GridView ID="gvVideoList" runat="server" AutoGenerateColumns="False"
                                        CssClass="table table-bordered table-striped table-inverse nomargin" OnRowDataBound="OnRowDataBound" OnSelectedIndexChanged="gvVideoList_SelectedIndexChanged" >
                                        <Columns>
                                            <asp:BoundField DataField="Id" SortExpression="Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" HeaderText="AMS_AssetID" />
                                            <asp:BoundField DataField="CourseID" HeaderText="Course ID" SortExpression="CourseID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="CourseType" HeaderText="Course Type" SortExpression="CourseType"/>
                                            <asp:BoundField DataField="CourseName" HeaderText="Name" SortExpression="CourseName" />
                                            <asp:BoundField DataField="VideoTitle" HeaderText="Title" SortExpression="VideoTitle" />
                                            <asp:BoundField DataField="Duration" HeaderText="Duration" SortExpression="Duration"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="VideoPath" HeaderText="Video Path" SortExpression="VideoPath" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="TitleLink" HeaderText="Title Link" SortExpression="TitleLink" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:ScriptManager ID="videoextender" runat="server"></asp:ScriptManager>
                                    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="videopanel" TargetControlID="lnkfake1" CancelControlID="lnkfake2">
                                    </cc1:ModalPopupExtender>
                                </div>
                            </div>
                        </div>
                        <!-- panel -->
                    </div>
                    <!-- col-sm-6 -->
                </div>
                <!-- row -->

                <div class="col-sm-6">
                    <div class="panel">
                        
                        <div class="panel-heading">
                            <h4 class="panel-title">Documents</h4>
                            <div class="panel-body">
                                <div class="form mb20"></div>
                                <div class="table-responsive">
                                    <asp:GridView ID="gvContentList" runat="server" AutoGenerateColumns="False"
                                        CssClass="table table-bordered table-striped table-inverse nomargin" OnRowDataBound="OnRowDataBound2" OnSelectedIndexChanged="gvContentList_SelectedIndexChanged">
                                        <Columns>
                                            <asp:BoundField DataField="Id" SortExpression="Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" HeaderText="AMS_AssetID" />
                                            <asp:BoundField DataField="CourseID" HeaderText="Course ID" SortExpression="CourseID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="CourseType" HeaderText="Course Type" SortExpression="CourseType"/>
                                            <asp:BoundField DataField="CourseName" HeaderText="Name" SortExpression="CourseName" />
                                            <asp:BoundField DataField="DocumentTitle" HeaderText="Title" SortExpression="DocumentTitle" />
                                            <asp:BoundField DataField="DocumentTitleLink" HeaderText="Title Link" SortExpression="DocumentTitleLink"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                            <asp:BoundField DataField="DocumentPath" HeaderText="Document Path" SortExpression="DocumentPath" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                          </Columns>
                                    </asp:GridView>
                                    <asp:LinkButton ID="lnkfake1" runat="server"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkfake2" runat="server"></asp:LinkButton>
                                    <cc1:ModalPopupExtender ID="documentextender" runat="server" TargetControlID="lnkfake1" CancelControlID="lnkfake2" PopupControlID="contentpanel"></cc1:ModalPopupExtender>
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

    <!--Pop up -->

    <asp:Panel ID="videopanel" runat="server">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="panel panel-profile list-view">
                        <div class="panel-heading">
                            <div class="media">
                                <div class="media-body">
                                    <h4 class="media-heading" id="Swasset" runat="server">Video Details</h4>
                                    <asp:Label ID="videos" runat="server" Text="Video Details" Visible="false"></asp:Label>
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
                                <div class="col-md-12">
                                    <div class="well mb20">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label"> Id  </label>
                                            <asp:Label ID="Label6" runat="server" CssClass="col-sm-8 control-label lead" ></asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label">Title </label>
                                            <asp:TextBox ID="videotitle" runat="server"></asp:TextBox>
                                        </div>
                                        <%--<div class="form-group">
                                            <label class="col-sm-4 control-label">Duration</label>
                                            <asp:Label ID="Label8" runat="server" CssClass="col-sm-8 control-label lead"></asp:Label>
                                        </div>--%>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label">Upload File </label>
                                            <asp:FileUpload ID="Videofile" runat="server" />
                                        </div>

                                        <div class="form-group">
                                            <asp:Button ID="videoedit" runat="server" Text="Submit" CssClass="btn btn-primary   btn-block pull-right" OnClick="videoedit_Click"  />
                                        </div>
                                        <div class="clearfix mb20"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </asp:Panel>
    <asp:Panel ID="contentpanel" runat="server">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="panel panel-profile list-view">
                        <div class="panel-heading">
                            <div class="media">
                                <div class="media-body">
                                    <h4 class="media-heading">Document Details</h4>
                                    <asp:Label ID="documentdetails" runat="server" Visible="false" Text="Document Details"></asp:Label>
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
                                <div class="col-md-12">
                                    <div class="well mb20">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label">Id  </label>
                                            <asp:Label ID="Label9" runat="server" CssClass="col-sm-8 control-label lead"></asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label">Title  </label>
                                            <asp:TextBox ID="documenttitle" runat="server"></asp:TextBox>
                                        </div>
                                          <div class="form-group">
                                            <label class="col-sm-4 control-label">Upload File </label>
                                            <asp:FileUpload ID="FileUpload1" runat="server"/>
                                        </div>
                                        <div class="form-group">
                                            <asp:Button ID="documentedit" runat="server" Text="Submit" CssClass="btn btn-primary   btn-block pull-right" OnClick="documentedit_Click"  />
                                        </div>
                                        <div class="clearfix mb20"></div>
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
            
            $('#cphBody_gvVideoList').DataTable();
            $('#cphBody_gvContentList').DataTable();

            $("#lilearning").addClass("active");
            $("#liadmineditcontent").addClass("active");

        });
    </script>
</asp:Content>

