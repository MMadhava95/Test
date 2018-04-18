<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true" CodeFile="Assessment.aspx.cs" Inherits="Admin_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
        <script type="text/javascript">
      <%--  var link = document.getElementById('<%=Master.FindControl("HyperLink1").ClientID %>');
        link.style.color = '#3851bc';--%>
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<div class="mainpanel">
        <div class="contentpanel">
            <ol class="breadcrumb breadcrumb-quirk">
                <li><a href="index.html"><i class="fa fa-home mr5"></i>Home</a></li>
                <li class="active">Add Question</li>
            </ol>
            <div class="row">
                <div class="col-sm-12">
                    <p>
                        <asp:Label ID="lblmessage" runat="server" ForeColor="Green" Text="" Style="padding-left: 30px; font-size: 20px;"></asp:Label>
                    </p>
                    <div class="panel">
                        <div class="panel-heading">
                            <h4 class="panel-title">Add Question </h4>
                        </div>
                        <div class="panel-body">
                            <div class="form mb20">
                                <div class="col-md-4">
                                    <div class="floatings-labels">
                                        <asp:DropDownList ID="DDcoursetype" AutoPostBack="true" CssClass="floating-select" runat="server" onclick="this.setAttribute('value', this.value);">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>Technical</asp:ListItem>
                                            <asp:ListItem>Business</asp:ListItem>
                                            <asp:ListItem>Development</asp:ListItem>
                                        </asp:DropDownList>
                                        <label class="lbltop" for="txtMotherLang">Select Course Type</label>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="floatings-labels">
                                        <asp:DropDownList ID="ddlCourseName" AutoPostBack="true" CssClass="floating-select" runat="server" onclick="this.setAttribute('value', this.value);" DataSourceID="ddCourseName" DataTextField="CourseName" DataValueField="CourseName" OnSelectedIndexChanged="ddlCourseName_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="ddCourseName" runat="server" ConnectionString="<%$ ConnectionStrings:SNPLDBSTRING %>" SelectCommand="SELECT [CourseName] FROM [CreateCourseTable] WHERE ([CourseType] = @CourseType)">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="DDcoursetype" Name="CourseType" PropertyName="SelectedValue" Type="String" DefaultValue=" " />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                        <label class="lbltop" for="txtMotherLang">Select Course Name</label>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="floatings-labels">
                                        <asp:TextBox ID="tbQn_no" runat="server" ReadOnly="true" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                        <label class="lbltop1 lbltop" for="nMobileNumber1">Question no  </label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbQn_no" ValidationGroup="Group1" ForeColor="Red">*Please Enter Question Number</asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="col-md-8">
                                    <div class="floatings-labels">
                                        <asp:TextBox ID="tbQuestion" runat="server" TextMode="MultiLine" Height="89px" CssClass="form-control" placeholder="Question " Width="683px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="tbQuestion" ValidationGroup="Group1" ForeColor="Red">* Please Enter Question</asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="floatings-labels">
                                        <asp:TextBox ID="tbOption1" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                        <label class="lbltop1 lbltop" for="nMobileNumber1">Option1  </label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="tbOption1" ValidationGroup="Group1" ForeColor="Red">*Please Fill Out this Field</asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <div class="col-md-4">
                                    <div class="floatings-labels">
                                        <asp:TextBox ID="tbOption2" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                        <label class="lbltop1 lbltop" for="nMobileNumber1">Option2   </label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="tbOption2" ValidationGroup="Group1" ForeColor="Red">*Please Fill Out this Field</asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="floatings-labels">
                                        <asp:TextBox ID="tbOption3" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                        <label class="lbltop1 lbltop" for="nMobileNumber1">Option3  </label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="tbOption3" ValidationGroup="Group1" ForeColor="Red">*Please Fill Out this Field</asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="floatings-labels">
                                        <asp:TextBox ID="tbOption4" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                        <label class="lbltop1 lbltop" for="nMobileNumber1">Option4 </label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbOption4" ValidationGroup="Group1" ForeColor="Red">*Please Fill Out this Field</asp:RequiredFieldValidator>

                                    </div>
                                </div>
                              
                                    <div class="col-md-4">
                                        <div class="floatings-labels">
                                            <asp:TextBox ID="tb_answar" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                            <label class="lbltop1 lbltop" for="nMobileNumber1">Answer </label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tb_answar" ValidationGroup="Group1" ForeColor="Red">*Please Fill Out this Field</asp:RequiredFieldValidator>

                                        </div>
                                    </div>
                                    <div class="clearfix"></div>


                                    <div class="col-md-4">
                                        <asp:Button ID="btnSubmitQn" runat="server" Text="Add" class="btn btn-success   btn-block" Style="margin-top: 10px;" OnClick="btnSubmitQn_Click2" />
                                    </div>          
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


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" Runat="Server">
    <script>
        $(document).ready(function () {
            $("#lilearning").addClass("active");
            $("#liadminassessment").addClass("active");
        });
    </script>
</asp:Content>

