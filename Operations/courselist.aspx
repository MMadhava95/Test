<%@ Page Title="" Language="C#" MasterPageFile="emp.master" AutoEventWireup="true" CodeFile="courselist.aspx.cs" Inherits="courselist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
    <style type="text/css">
        #status {
    width: 200px;
    height: 200px;
    position: absolute;
    left: 50%; /* centers the loading animation horizontally one the screen */
    top: 50%; /* centers the loading animation vertically one the screen */
    background-image: url(../../Images/status.gif); /* path to your loading animation */
    background-repeat: no-repeat; 
    background-position: center;
    margin: -100px 0 0 -100px; /* is width and height divided by two */
}
        .latest-news-box {
            background-color: #fff;
            border: 1px solid #ddd;
            overflow: hidden;
            padding: 20px 0px 20px 0px;
        }

        .item {
            position: relative;
            width: 100%;
            height: 60%;
            cursor: pointer;
            border: 1px solid #ddd;
            padding: 20px;
        }

        .latest-news-box .item img {
            width: 100%;
            height: 150px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <div class="mainpanel">
        <div class="contentpanel">
            <ol class="breadcrumb breadcrumb-quirk">
                <li><a href="courselist.aspx"><i class="fa fa-home mr5"></i>Home</a></li>
                
            </ol>
            <div class="row">
                <div class="col-md-12 padding10 paddingtop10">
                    <div class="clearfix"></div>
                    <div class="form mb20"></div>
                     <a class="btn btn-success pull-right btn-sm tooltips" href="mycourses.aspx"><i class="fa fa-graduation-cap mr5"></i>Go to my courses</a>
                    <h3 style="text-align: center">Our courses</h3>
                    <div>
                        <!--preloader---->
                       <%-- <div id="preloader">
                            <div id="status">&nbsp;</div>
                        </div>--%>
                        <!-- End preloader---->
                        
                        <asp:DataList ID="DataList1" Width="100%" runat="server" RepeatColumns="3" CellSpacing="3" RepeatLayout="Table">
                            <ItemTemplate> 
                                <div class="col-md-12 mb20">
                                    <div class="blog-entry2">
                                        <div class="blog-img">
                                            <img class="img-responsive"  src="<%#Eval("ImagePath") %>" alt="" style="width: 100%;">
                                        </div>
                                        <div class="blog-body">
                                             <div class="btn-group pull-right">
                                                <a  href='coursedetails.aspx?id=<%#Eval("CourseID") %>' class="btn btn-success" type="button" onclick="location.href='coursedetails.aspx'"><i class="fa fa-graduation-cap mr5"></i>Learn Now</a>
                                            </div>
                                            <h6 class="blog-category">Name : <%#Eval("CourseName") %></h6>
                                             <h6 class="blog-category">Faculty : <%#Eval("Faculty") %></h6>
                                             <h6 class="blog-category">Duration : <%#Eval("CourseDuration") %></h6>
                                            <div class="clearfix"></div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                        <asp:DataList ID="dlCourseList" Width="100%" runat="server" RepeatColumns="3" CellSpacing="3" RepeatLayout="Table">
                            <ItemTemplate>
                                <div class="col-md-12 mb20">
                                    <div class="blog-entry2">
                                        <div class="blog-img">
                                            <img class="img-responsive" src="<%#Eval("ImagePath") %>" alt="" style="width: 100%;">
                                        </div>
                                        <div class="blog-body">
                                            <div class="btn-group pull-right">
                                                <a  href='coursedetails.aspx?id=<%#Eval("CourseID") %>' class="btn btn-success"><i class="fa fa-graduation-cap mr5"></i>Learn Now</a>
                                            </div>
                                            <h6 class="blog-category">Name : <%#Eval("CourseName") %></h6>
                                             <h6 class="blog-category">Faculty : <%#Eval("Faculty") %></h6>
                                             <h6 class="blog-category">Duration : <%#Eval("CourseDuration") %></h6>
                                            <div class="clearfix"></div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                        
                        <asp:DataList ID="DataList2" Width="100%" runat="server" RepeatColumns="3" CellSpacing="3" RepeatLayout="Table">
                            <ItemTemplate> 
                                <div class="col-md-12 mb20">
                                    <div class="blog-entry2">
                                        <div class="blog-img">
                                            <img class="img-responsive" src="<%#Eval("ImagePath") %>" alt="" style="width: 100%;">
                                        </div>
                                         <div class="btn-group pull-right">
                                                <a  href='coursedetails.aspx?id=<%#Eval("CourseID") %>' class="btn btn-success" type="button" onclick="location.href='coursedetails.aspx'"><i class="fa fa-graduation-cap mr5"></i>Learn Now</a>
                                            </div>
                                            <h6 class="blog-category">Name : <%#Eval("CourseName") %></h6>
                                             <h6 class="blog-category">Faculty : <%#Eval("Faculty") %></h6>
                                             <h6 class="blog-category">Duration : <%#Eval("CourseDuration") %></h6>
                                            <div class="clearfix"></div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <input type="hidden" id="getuser" value="<%=Session["EmailID"] %>" />
                    <!-- col-sm-6 -->
                </div>
                <!-- row -->
            </div>
            <!-- contentpanel -->
        </div>
        <!-- mainpanel -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" Runat="Server">
    <script>
        $(document).ready(function () {
            
            $('#cphBody_gvList').DataTable();
            $("#lilearning").addClass("active");
            $("#licourselist").addClass("active");

            // Date Picker
            $('#cphBody_datepicker').datepicker();

        });
    </script>
</asp:Content>

