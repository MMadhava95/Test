<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true" CodeFile="ViewCourses.aspx.cs" Inherits="Admin_Default" EnableEventValidation="false" %>
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
                <li class="active">Course List</li>
            </ol>
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel">
                        <div class="panel-heading">
                            <button class="btn btn-primary btn-sm tooltips pull-right" type="button" title="" onclick="location.href='../Admin/AddCourse.aspx'" data-placement="top" data-toggle="tooltip" data-original-title="Create Member"><i class="fa fa-plus"></i>&nbsp;Add Course </button>
                            <h4 class="panel-title">Course Details</h4>
                            <div class="panel-body">
                                <div class="form mb20">
                                    <div class="clearfix"></div>
                                </div>
                                <hr />
                                <div class="table-responsive">
                                    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="false"
                                        CssClass="table table-bordered table-striped table-inverse nomargin"
                                         OnSelectedIndexChanged="gvList_SelectedIndexChanged" 
                                        OnRowDataBound="gvList_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="CourseID" HeaderText="Course ID" SortExpression="CourseID" />
                                            <asp:BoundField DataField="CourseName" HeaderText="Name" SortExpression="CourseName" />
                                            <asp:BoundField DataField="CourseDuration" HeaderText="Duration" SortExpression="CourseDuration" />
                                            <asp:BoundField DataField="Faculty" HeaderText="Faculty" SortExpression="Faculty" />
                                            <asp:BoundField DataField="Prerequisites" HeaderText="Prerequisites" SortExpression="Prerequisites" />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:LinkButton ID="lnkfake1" runat="server"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkFake2" BackColor="Green" runat="server"></asp:LinkButton>
                                    <asp:ScriptManager ID="gridscriptforpopup" runat="server"></asp:ScriptManager>
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
    <cc1:ModalPopupExtender ID="coursesMP" runat="server" PopupControlID="pnlPopup" TargetControlID="lnkFake1" CancelControlID="lnkFake2"
        BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlPopup" runat="server">
        <div class="modal-dialog  modal-content" role="document">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="panel panel-profile list-view">
                        <div class="panel-heading">
                            <div class="media">
                                <div class="media-body">
                                    <h4 class="media-heading">Course Details</h4>
                                </div>
                            </div>
                            <!-- media -->
                            <ul class="panel-options">
                                <li><a href="" class="close" data-dismiss="modal" aria-hidden="true"><i class="fa fa-times"></i></a></li>
                            </ul>
                        </div>
                        <div class="panel-body people-info">
                         <div class="row mb20">
                             <div class="col-md-12">
                                 <div class="well mb20">
                                     <div class="form-group">
                                         <label class="col-sm-4 control-label">Course ID</label>
                                         <asp:TextBox ID="txtCourseId"  class="col-sm-4 control-label" runat="server" ReadOnly="true"></asp:TextBox>
                                     </div>
                                     <br />
                                      <div class="form-group">
                                         <label class="col-sm-4 control-label">Name </label>
                                          <asp:TextBox ID="txtName" class="col-sm-4 control-label" runat="server"></asp:TextBox>
                                     </div>
                                     <br />
                                      <div class="form-group">
                                         <label class="col-sm-4 control-label">Duration</label>
                                          <asp:TextBox ID="txtDuraiton" class="col-sm-4 control-label" runat="server"></asp:TextBox>
                                     </div>
                                       <br />
                                      <div class="form-group">
                                         <label class="col-sm-4 control-label">Faculty </label>
                                          <asp:TextBox ID="txtfaculty" class="col-sm-4 control-label" runat="server"></asp:TextBox>
                                     </div>
                                       <br />
                                      <div class="form-group">
                                         <label class="col-sm-4 control-label">PreRequsites  </label>
                                          <asp:TextBox ID="txtprerereq" TextMode="MultiLine" class="col-sm-4 control-label" runat="server"></asp:TextBox>
                                     </div>
                                     <br />
                                     <div class="form-group text-center">
                                         <asp:Button ID="btnupdate" runat="server" class="col-sm-6 btn btn-primary fa-align-center" Text="Update" OnClick="btnupdate_Click" />
                                     </div>
                                     <div class="clearfix mb20">

                                     </div>
                                 </div>
                             </div>
                             <%--<div class="col-md-6">
                                 <div class="col-md-12 text-center mb20">
                                     <img src="../images/Bill-Receipt-300x231--300x231.jpg" />
                                 </div>
                                 <div class="col-md-12">
                                     <div class="floatings-labels">
                                         <asp:DropDownList ID="DropDownList2" CssClass="floating-select" runat="server" value="" onclick="this.setAttribute('value', this.value);">
                                             <asp:ListItem Text=""> </asp:ListItem>
                                             <asp:ListItem Text="Approve"> </asp:ListItem>
                                             <asp:ListItem Text="Pending"> </asp:ListItem>
                                         </asp:DropDownList>
                                         <label class="lbltop" for="txtMotherLang">Action  </label>
                                     </div>
                                 </div>
                                 <div class="col-md-12">
                                     <div class="floatings-labels">
                                         <asp:TextBox ID="TextBox4" runat="server" Height="80" CssClass="floating-textarea" TextMode="MultiLine" placeholder="Remarks"></asp:TextBox>
                                     </div>
                                 </div>
                                 <div class="clearfix"></div>
                                 <div class="col-sm-12 pull-right">
                                     <button class="btn btn-success btn-block" style="margin-top: 15px;"><i class="fa fa-check"></i>Submit  </button>
                                 </div>
                             </div>--%>
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
            $("#lilearning").addClass("active");
            $("#liadminviewcourse").addClass("active");

        });
    </script>
</asp:Content>

