<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true" CodeFile="AddCourse.aspx.cs" Inherits="Admin_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <div class="mainpanel">
        <div class="contentpanel">
            <ol class="breadcrumb breadcrumb-quirk">
                <li><a href="index.html"><i class="fa fa-home mr5"></i>Home</a></li>
                <li class="active">Add Course</li>
            </ol>
            <div class="row">
                <div class="col-sm-12">
                    <p>
                        <asp:Label ID="lblmessage" runat="server" ForeColor="Green" Text="" Style="padding-left: 30px; font-size: 20px;"></asp:Label>
                    </p>
                    <div class="panel">
                        <div class="panel-heading">
                            <h4 class="panel-title">Add Course </h4>
                        </div>
                        <div class="panel-body">
                            <div class="form mb20">
                                <div class="col-md-4">
                                    <div class="floatings-labels">
                                        <asp:DropDownList ID="DDcoursetype" CssClass="floating-select" runat="server" value="" onclick="this.setAttribute('value', this.value);">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>Technical</asp:ListItem>
                                            <asp:ListItem>Business</asp:ListItem>
                                            <asp:ListItem>Development</asp:ListItem>
                                        </asp:DropDownList>
                                        <label class="lbltop" for="txtMotherLang">Course Type</label>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="floatings-labels">
                                        <asp:TextBox ID="Tbcoursename" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                        <label class="lbltop1 lbltop" for="nMobileNumber1">Course Name </label>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="floatings-labels">
                                        <asp:TextBox ID="tbcouseduration" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                        <label class="lbltop1 lbltop" for="nMobileNumber1">Duration  </label>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4"
                                            ControlToValidate="tbcouseduration" runat="server"
                                            ErrorMessage="Duration starts with a number" Display="Dynamic" ForeColor="Red"
                                            ValidationExpression="^[0-9 _][a-zA-Z0-9 _]*">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <div class="col-md-4">
                                    <div class="floatings-labels">
                                        <asp:TextBox ID="tbCourseFaculty" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                        <label class="lbltop1 lbltop" for="nMobileNumber1">Faculty   </label>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                            ControlToValidate="tbCourseFaculty" runat="server"
                                            ErrorMessage="Name starts with a letter and have atleast 3 letters" Display="Dynamic" ForeColor="Red"
                                            ValidationExpression="[a-zA-Z _]*">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="floatings-labels">
                                        <asp:TextBox ID="tbCourseContent" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                        <label class="lbltop1 lbltop" for="nMobileNumber1">Summary  </label>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="floatings-labels">
                                        <asp:TextBox ID="CoursePrerequisites" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                        <label class="lbltop1 lbltop" for="nMobileNumber1">Pre requisites </label>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <div class="col-md-8">
                                    <div class="floatings-labels">
                                        <asp:TextBox ID="txtcont" runat="server" TextMode="MultiLine" Height="120px" CssClass="form-control" placeholder="Course Description "></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <label class="text-info "><b>Upload Course Image </b></label>
                                    <div class="clearfix"></div>
                                    <asp:FileUpload ID="FUimageupload" CssClass="fileContainer" runat="server" />
                                    <asp:RegularExpressionValidator ID="uplValidator" runat="server" ControlToValidate="FUimageupload"
                                        ErrorMessage=".jpg, .png, .jpeg formats are allowed" ForeColor="red" Display="Dynamic"
                                        ValidationExpression="^.*\.(jpg|JPG|png|PNG|jpeg|JPEG)$"></asp:RegularExpressionValidator>
                                    <asp:CustomValidator ID="CustomValidator1" OnServerValidate="ValidateFileSize" ForeColor="Red" runat="server" />
                                </div>
                                <div class="col-md-4">
                                    <asp:Button ID="btnSignIn" runat="server" Text="Submit" class="btn btn-success   btn-block" Style="margin-top: 10px;" OnClick="btnSignIn_Click" />
                                </div>
                                <div class="clearfix"></div>
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
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="Server">
    <script>
        $(document).ready(function () {
            $("#lilearning").addClass("active");
            $("#liadminaddcourse").addClass("active");
        });
    </script>
</asp:Content>

