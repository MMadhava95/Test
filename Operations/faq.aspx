<%@ Page Title="" Language="C#" MasterPageFile="emp.master" AutoEventWireup="true" CodeFile="faq.aspx.cs" Inherits="faq" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <div class="mainpanel">

        <div class="contentpanel">

            <ol class="breadcrumb breadcrumb-quirk">
                <li><a href="HistoryPage.html"><i class="fa fa-home mr5"></i>Home</a></li>
                <li class="active">FAQ'S</li>
            </ol>

            <div class="row">
                <div class="col-sm-12">

                    <div class="panel">
                        <div class="panel-heading">

                            <h4 class="panel-title">FAQ'S</h4>

                        </div>
                        <div class="panel-body">
                            <div class="col-md-3"></div>
                            <div class="col-md-6 col-sm-6">
                                <div class="panel-group" id="accordion4">
                                    <div class="panel panel-success">
                                        <div class="panel-heading">
                                            <h4 class="panel-title">
                                                <a data-toggle="collapse" data-parent="#accordion4" href="#collapseOne4">
                                                    <strong>Q: How can I keep track of tickets?  </strong>
                                                </a>
                                            </h4>
                                        </div>
                                        <div id="collapseOne4" class="panel-collapse collapse in">
                                            <div class="panel-body">
                                                <span style="color: orangered">A:</span> You can track your ticket on History page and click on Ticket you raised  there you can find a pop up with Ticket Details.
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel panel-success">
                                        <div class="panel-heading">
                                            <h4 class="panel-title">
                                                <a data-toggle="collapse" class="collapsed" data-parent="#accordion4" href="#collapseTwo4">
                                                    <strong>Q: How do I choose Ticket Priority? </strong>
                                                </a>
                                            </h4>
                                        </div>
                                        <div id="collapseTwo4" class="panel-collapse collapse">
                                            <div class="panel-body">
                                                <span style="color: orangered">A:</span> Please refer Service Level Aggrement (SLA) to choose your ticket priority.
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color: orangered"><br />
                                                    <b>Example:</b></span>&nbsp;&nbsp;(Critical/High/Modarate/Low).
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel panel-success">
                                        <div class="panel-heading">
                                            <h4 class="panel-title">
                                                <a data-toggle="collapse" class="collapsed" data-parent="#accordion4" href="#collapseThree4">
                                                    <strong>Q: How can I change my Mobile Number?  </strong>
                                                </a>
                                            </h4>
                                        </div>
                                        <div id="collapseThree4" class="panel-collapse collapse">
                                            <div class="panel-body">
                                                <span style="color: orangered">A:</span> Please go to Profile Page.To change your Mobile number click on settings 
                                                 there you can change.
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel panel-success">
                                        <div class="panel-heading">
                                            <h4 class="panel-title">
                                                <a data-toggle="collapse" class="collapsed" data-parent="#accordion4" href="#collapsefour4">
                                                    <strong>Q: How can I change my Password?  </strong>
                                                </a>
                                            </h4>
                                        </div>
                                        <div id="collapsefour4" class="panel-collapse collapse">
                                            <div class="panel-body">
                                                <span style="color: orangered">A:</span> Please go to Profile Page. To change your password click on settings there 
                                               you can change.
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel panel-success">
                                        <div class="panel-heading">
                                            <h4 class="panel-title">
                                                <a data-toggle="collapse" class="collapsed" data-parent="#accordion4" href="#collapsefive4">
                                                    <strong>Q: where can I find my raised tickets? </strong>
                                                </a>
                                            </h4>
                                        </div>
                                        <div id="collapsefive4" class="panel-collapse collapse">
                                            <div class="panel-body">
                                                <span style="color: orangered">A:</span> Go to History page there you find your raised tickets.
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel panel-success">
                                        <div class="panel-heading">
                                            <h4 class="panel-title">
                                                <a data-toggle="collapse" class="collapsed" data-parent="#accordion4" href="#collapsesix4">
                                                    <strong>Q: How can I Attach files?  </strong>
                                                </a>
                                            </h4>
                                        </div>
                                        <div id="collapsesix4" class="panel-collapse collapse">
                                            <div class="panel-body">
                                                <span style="color: orangered">A:</span> When you raise a ticket you find the attchments field where you can upload 
                                                 your files.
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color: orangered"><br />
                                                    <b>Note:</b></span>&nbsp;&nbsp;All the files are 
                                                 accepted except .exe,.vb,and script files and size should be below 2MB.

                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel panel-success">
                                        <div class="panel-heading">
                                            <h4 class="panel-title">
                                                <a data-toggle="collapse" class="collapsed" data-parent="#accordion4" href="#collapseseven4">
                                                    <strong>Q: How can I ReSubmit same ticket as genarated in previous?  </strong>
                                                </a>
                                            </h4>
                                        </div>
                                        <div id="collapseseven4" class="panel-collapse collapse">
                                            <div class="panel-body">
                                                <span style="color: orangered">A:</span> Please go to History Page.There you can find the text box with a button named
                                                    as ReSubmit. 
                                                     By entering your previous TicketID and Click on 
                                                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ReSubmit button. You can Resubmit the ticket
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel panel-success">
                                        <div class="panel-heading">
                                            <h4 class="panel-title">
                                                <a data-toggle="collapse" class="collapsed" data-parent="#accordion4" href="#collapseeight4">
                                                    <strong>Q:What is History in Ticket Details and Ticket Info Pages?  </strong>
                                                </a>
                                            </h4>
                                        </div>
                                        <div id="collapseeight4" class="panel-collapse collapse">
                                            <div class="panel-body">
                                                <span style="color: orangered">A:</span> You can ask for more details from analyst to user and viceversa here.
                                              <br />
                                                Just send a message that you require more Details,Attachments and links etc... 
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <!-- col-md-4 -->
                            <div class="col-md-3"></div>


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

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="Server">
    <script>
        $(document).ready(function () {
            
            $("#lifaq").addClass("active");
        });
    </script>
</asp:Content>


