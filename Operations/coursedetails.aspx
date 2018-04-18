<%@ Page Title="" Language="C#" MasterPageFile="emp.master" AutoEventWireup="true" CodeFile="coursedetails.aspx.cs" Inherits="coursedetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">

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

        /*  VIDEO PLAYER CONTAINER
 		############################### */
        .vid-container {
            position: relative;
            padding-bottom: 52%;
            padding-top: 30px;
            height: 0;
        }

            .vid-container iframe,
            .vid-container object,
            .vid-container embed {
                position: absolute;
                top: 0;
                left: 0;
                width: 100%;
                height: 100%;
            }


        /*  VIDEOS PLAYLIST 
 		############################### */
        .vid-list-container {
            width: 92%;
            overflow: hidden;
            margin-top: 20px;
            margin-left: 4%;
            padding-bottom: 20px;
        }

        .vid-list {
            width: 1344px;
            position: relative;
            top: 0;
            left: 0;
        }

        .vid-item {
            display: block;
            width: 148px;
            height: 148px;
            float: left;
            margin: 0;
            padding: 10px;
        }

        .thumb {
            /*position: relative;*/
            overflow: hidden;
            height: 84px;
        }

            .thumb img {
                width: 100%;
                position: relative;
                top: -13px;
            }

        .vid-item .desc {
            color: #21A1D2;
            font-size: 15px;
            margin-top: 5px;
        }

        .vid-item:hover {
            background: #eee;
            cursor: pointer;
        }

        .arrows {
            position: relative;
            width: 100%;
        }

        .arrow-left {
            color: #fff;
            position: absolute;
            background: #777;
            padding: 15px;
            left: -25px;
            top: -130px;
            z-index: 99;
            cursor: pointer;
        }

        .arrow-right {
            color: #fff;
            position: absolute;
            background: #777;
            padding: 15px;
            right: -25px;
            top: -130px;
            z-index: 100;
            cursor: pointer;
        }

        .arrow-left:hover {
            background: #CC181E;
        }

        .arrow-right:hover {
            background: #CC181E;
        }


        @media (max-width: 624px) {
            body {
                margin: 15px;
            }

            .caption {
                margin-top: 40px;
            }

            .vid-list-container {
                padding-bottom: 20px;
            }

            /* reposition left/right arrows */
            .arrows {
                position: relative;
                margin: 0 auto;
                width: 96px;
            }

            .arrow-left {
                left: 0;
                top: -17px;
            }

            .arrow-right {
                right: 0;
                top: -17px;
            }
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">

    <div class="mainpanel">

        <div class="contentpanel">

            <ol class="breadcrumb breadcrumb-quirk">
                <li><a href="courselist.aspx"><i class="fa fa-home mr5"></i>Home</a></li>
                <li class="active">Course List</li>
            </ol>

            <%-- <!--preloader---->
            <div id="preloader">
                <div id="status">&nbsp;</div>
            </div>
            <!-- End preloader---->--%>

            <div class="row">
                <div class="col-md-8  ">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title" runat="server" id="h3Coursename">Course</h3>
                        </div>
                        <div class="panel-body">
                            <asp:DataList ID="dlCourseList" Width="100%" runat="server" RepeatColumns="3" CellSpacing="3" RepeatLayout="Table">
                                <ItemTemplate>
                                    <p class="blog-summary mb20"><strong>Description :</strong> <%#Eval("Coursedescription") %></p>
                                    <p class="blog-summary mb10"><strong>Summary :</strong> <%#Eval("Learncontent") %></p>

                                    <p class="blog-summary mb20"><strong>Prerequisites :</strong> <%#Eval("Prerequisites") %></p>

                                    <asp:LinkButton ID="BtnAddCourse" class="btn btn-success btn-sm tooltips pull-right" data-placement="top" data-toggle="tooltip" data-original-title="Add to your Courses" runat="server" OnClick="BtnAddCourse_Click"><i class="fa fa-plus"></i> Add Course</asp:LinkButton>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                    </div>
                </div>
                <div class="col-sm-4">
                    <div class="row blog-entry mb20">
                        <div class="col-md-12 padding10 paddingtop10">
                            <img class="img-responsive" src="../images/pc1.png" alt="" />
                            <div class="btn-group btn-block pull-left padding10">
                                <button class="btn btn-primary" type="button" data-toggle="modal" data-target="#divVideo" onclick="coursedetailsfun.call(this)">
                                    <i class="fa fa-video-camera mr5"></i>View Video / <i class="fa fa-file-pdf-o mr5"></i>PDF</button>
                            </div>

                        </div>
                    </div>
                </div>

                <!-- row -->
            </div>
            <div class="row">
                <div class="col-md-12">
                </div>
            </div>
            <!-- contentpanel -->
        </div>
        <!-- mainpanel -->
    </div>


    <div class="modal" tabindex="-1" role="dialog" id="divVideo">
        <div class="modal-dialog  modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="panel panel-profile list-view">
                        <div class="panel-heading">
                            <div class="media">
                                <div class="media-body">
                                    <h4 class="media-heading">Course  Video </h4>
                                </div>
                            </div>
                            <!-- media -->
                            <ul class="panel-options">
                                <li><a href="" class="close" data-dismiss="modal" aria-hidden="true"><i class="fa fa-times"></i></a></li>
                            </ul>
                        </div>
                        <!-- panel-heading -->

                        <div class="panel-body people-info">
                            <div class="row">

                                <ul class="nav nav-justified nav-tabs primary">
                                    <li class="active" id="livideo"><a href="#popular2" data-toggle="tab"><strong>Video's</strong></a></li>
                                    <li><a href="#recent2" data-toggle="tab"><strong>Documents</strong></a></li>
                                </ul>

                                <!-- Tab panes -->
                                <div class="tab-content mb20">
                                    <div class="tab-pane" id="popular2">
                                        <asp:Label ID="msglbl" runat="server" ForeColor="Orange" Font-Size="XX-Large" Text="" Visible="false"></asp:Label>
                                        <div class="vid-container">

                                            <video width="100%" controls id="vid_frame" autoplay controlslist="nodownload">
                                            </video>
                                        </div>

                                        <div class="vid-list-container">
                                            <div class="vid-list">
                                                <asp:DataList ID="DataList2" runat="server" RepeatDirection="Horizontal">
                                                    <ItemTemplate>
                                                        <div class="vid-item" onclick="document.getElementById('vid_frame').src='../<%#Eval("VideoPath")%>'">
                                                            <div class="thumb">
                                                                <img src="../images/pc1.png" />
                                                            </div>
                                                            <div class='desc'><%#Eval("VideoTitle")%>  </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </div>
                                        </div>
                                        <!-- LEFT AND RIGHT ARROWS -->
                                        <div class="arrows">
                                            <div class="arrow-left"><i class="fa fa-chevron-left fa-lg"></i></div>
                                            <div class="arrow-right"><i class="fa fa-chevron-right fa-lg"></i></div>
                                        </div>
                                    </div>

                                    <div class="tab-pane" id="recent2">
                                        <embed id="embPdf" src="../images/pc1.png" type="application/pdf" style="width: 100%; height: 400px;" />
                                        <div class="row">
                                            <asp:GridView ID="gvContentList" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped table-inverse nomargin">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="DocumentTitle" HeaderText="Document Title" SortExpression="DocumentTitle" />
                                                    <asp:BoundField DataField="CourseID" HeaderText="Course ID" SortExpression="CourseID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <a href="../<%#Eval("DocumentPath")%>" onclick="showGame(this);return false;"><i class="fa fa-eye"></i>View</a>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
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
        </div>
    </div>
    <asp:Label ID="Label3" runat="server" ForeColor="#009900" Visible="false"></asp:Label>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="Server">
    <script>


        $(document).ready(function () {


            $('#cphBody_gvList').DataTable();

            // Date Picker
            $('#cphBody_datepicker').datepicker();

        });

        $(document).ready(function () {
            $(".arrow-right").bind("click", function (event) {
                event.preventDefault();
                $(".vid-list-container").stop().animate({
                    scrollLeft: "+=336"
                }, 750);
            });
            $(".arrow-left").bind("click", function (event) {
                event.preventDefault();
                $(".vid-list-container").stop().animate({
                    scrollLeft: "-=336"
                }, 750);
            });
        });


        function showGame(whichgame) {
            var source = whichgame.getAttribute("href");
            var game = document.getElementById("embPdf");
            var clone = game.cloneNode(true);
            clone.setAttribute('src', source);
            game.parentNode.replaceChild(clone, game)
        }


    </script>

</asp:Content>

