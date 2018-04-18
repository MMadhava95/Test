<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true" CodeFile="UploadContent.aspx.cs" Inherits="Admin_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
        <div class="mainpanel">
        <div class="contentpanel">
            <ol class="breadcrumb breadcrumb-quirk">
                <li><a href="ViewCourses.aspx"><i class="fa fa-home mr5"></i>Home</a></li>
                <li class="active">Add Course Content</li>
            </ol>
            <div class="row">
                <div class="col-sm-12">
                    <p>
                        <asp:Label ID="Label1" runat="server" Style="padding-left: 35px; font-size: 20px;" Text=""></asp:Label></p>
                    <div class="panel">
                        <div class="panel-heading">
                            <h4 class="panel-title">Add Course Content </h4>
                        </div>
                        <div class="panel-body people-info">
                            <div class="row mb20">
                                <div class="col-md-6">
                                    <div class="col-md-12">
                                        <div class="floatings-labels">
                                            <asp:DropDownList ID="DdlcourseType" CssClass="floating-select" runat="server" value=""
                                                AutoPostBack="True" AppendDataBoundItems="true" DataSourceID="SqlDataSource1"
                                                DataValueField="CourseType" required="required"
                                                DataTextField="CourseType" onclick="this.setAttribute('value', this.value);">
                                                <asp:ListItem Selected="True">Select CourseType</asp:ListItem>
                                            </asp:DropDownList>
                                            <%--<label class="lbltop" for="txtMotherLang">Course Type</label>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="floatings-labels">
                                            <asp:DropDownList ID="DdlcourseName" CssClass="floating-select" runat="server" value=""
                                                DataSourceID="SqlDataSource2" AutoPostBack="true" DataTextField="CourseName" required="required"
                                                DataValueField="CourseName" onclick="this.setAttribute('value', this.value);" OnDataBound="DdlcourseName_DataBound">
                                            </asp:DropDownList>
                                            <%--<label class="lbltop" for="txtMotherLang">Course Name    </label>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="floatings-labels">
                                            <asp:TextBox ID="TextBox1" runat="server" required="required" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                            <label class="lbltop1 lbltop" for="nMobileNumber1">Video Title   </label>
                                        </div>
                                    </div>
                                    <div class="col-md-12 mb20 ">
                                        <label class="text-info "><b>Upload Video File</b>  </label>
                                        <div class="clearfix"></div>
                                        <asp:FileUpload ID="FileUpload1" CssClass="fileContainer" runat="server" required="required" />
                                    </div>
                                </div>
                                <div class="col-md-6">

                                    <div class="col-md-12">
                                        <div class="floatings-labels">
                                            <asp:TextBox ID="TextBox2" runat="server"  CssClass="floating-input" placeholder=" "></asp:TextBox>
                                            <label class="lbltop1 lbltop" for="nMobileNumber1">Content Title </label>
                                        </div>
                                    </div>

                                    <div class="col-md-12 mb20 ">
                                        <label class="text-info "><b>Upload Content File</b>  </label>
                                        <div class="clearfix"></div>
                                        <asp:FileUpload ID="PdfUpload" CssClass="fileContainer" runat="server"  />
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="col-sm-12 pull-right">
                                        <%--  <button id="submit" class="btn btn-success btn-block" style="margin-top: 15px;"><i class="fa fa-check"></i>Submit  </button>--%>
                                        <asp:Button ID="submit" runat="server" Text="Submit" class="btn btn-success btn-quirk btn-block" OnClick="submit_Click" />
                                    </div>
                                </div>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SNPLDBSTRING %>" SelectCommand="SELECT DISTINCT [CourseType] FROM [CreateCourseTable]"></asp:SqlDataSource>
                                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:SNPLDBSTRING %>" SelectCommand="SELECT DISTINCT [CourseName] FROM [CreateCourseTable] WHERE ([CourseType] = @CourseType)">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="DdlcourseType" Name="CourseType" PropertyName="SelectedValue" Type="String" />
                                    </SelectParameters>
                                </asp:SqlDataSource>

                            </div>
                        </div>

                    </div>
                    <!-- panel -->
                </div>
                <!-- col-sm-6 -->
            </div>
            <!-- row -->
            <asp:Label ID="lblNote" Style="padding-left: 25px;" runat="server" ForeColor="Red" Text="*All Fields are Mandatory."></asp:Label>
        </div>
        <!-- contentpanel -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" Runat="Server">
    <script>
           $(document).ready(function () {
               $("#lilearning").addClass("active");
               $("#liadminuploadcontent").addClass("active");
           });
    </script>
</asp:Content>

