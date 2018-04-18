<%@ Page Title="" Language="C#" MasterPageFile="emp.master" AutoEventWireup="true" CodeFile="create-ticket.aspx.cs" Inherits="Operations_Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <div class="mainpanel">

        <div class="contentpanel">

            <ol class="breadcrumb breadcrumb-quirk">
                <li><a href="index.html"><i class="fa fa-home mr5"></i>Home</a></li>
                <li class="active">Create Ticket    </li>
            </ol>

            <div class="row">
                <div class="col-sm-12">

                    <div class="panel">
                        <div class="panel-heading">

                            <h4 class="panel-title">Create Ticket    </h4>

                        </div>
                        <div class="panel-body">

                            <div class="form mb20">

                                <div class="col-md-3">
                                    <div class="floatings-labels">
                                        <asp:TextBox ID="subject" runat="server" CssClass="floating-input" placeholder=" " required=""></asp:TextBox>
                                        <label class="lbltop1 lbltop" for="nMobileNumber1">Subject     </label>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="floatings-labels">
                                        <asp:DropDownList ID="department" CssClass="floating-select" placeholder="select " runat="server" value="" onclick="this.setAttribute('value', this.value);">
                                            <asp:ListItem Text=""> </asp:ListItem>
                                            <asp:ListItem Text="Domestic Staffing Division"> </asp:ListItem>
                                            <asp:ListItem Text="Global Development Centre"> </asp:ListItem>
                                            <asp:ListItem Text="Administration"> </asp:ListItem>
                                            <asp:ListItem Text="Technical"> </asp:ListItem>

                                        </asp:DropDownList>

                                        <label class="lbltop" for="txtMotherLang">Department    </label>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="floatings-labels">
                                        <asp:TextBox ID="description" runat="server" CssClass="floating-input" placeholder=" " required=""></asp:TextBox>
                                        <label class="lbltop1 lbltop" for="nMobileNumber1">Description</label>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="floatings-labels">
                                        <asp:TextBox ID="hostName" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                        <label class="lbltop1 lbltop" for="nMobileNumber1">Host Name </label>
                                    </div>
                                </div>

                                <div class="clearfix"></div>
                                <div class="col-md-3">
                                    <div class="floatings-labels">
                                        <asp:DropDownList ID="priority" CssClass="floating-select" runat="server" value="" onclick="this.setAttribute('value', this.value);">
                                            <asp:ListItem Text=""> </asp:ListItem>
                                            <asp:ListItem Text="Critical"> </asp:ListItem>
                                            <asp:ListItem Text="High"> </asp:ListItem>
                                            <asp:ListItem Text="Moderate"> </asp:ListItem>
                                            <asp:ListItem Text="Low"> </asp:ListItem>
                                        </asp:DropDownList>

                                        <label class="lbltop" for="txtMotherLang">Priority</label>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="floatings-labels">
                                        <asp:DropDownList ID="Location" CssClass="floating-select" runat="server" value="" onclick="this.setAttribute('value', this.value);">
                                            <asp:ListItem Text=""> </asp:ListItem>
                                            <asp:ListItem Text="Hyderabad"> </asp:ListItem>
                                            <asp:ListItem Text="Vijayawada"> </asp:ListItem>

                                        </asp:DropDownList>

                                        <label class="lbltop" for="txtMotherLang">Location</label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="floatings-labels">

                                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                        <asp:Button ID="btnclick" runat="server" class="btn btn-success btn-block" Text="Check SLA" />
                                    </div>
                                </div>

                                <div class="col-md-3">



                                    <label class="text-info"><b>Upload File</b>  </label>

                                    <div class="clearfix"></div>
                                    <asp:FileUpload ID="FileUpload1" CssClass="fileContainer" runat="server" onchange="ValidateSize(this)" Text="(Maximum 2MB)" />
                                </div>

                            </div>


                            <div class="col-md-3">

                                <asp:Button ID="submit" runat="server" Text="Submit" class="btn btn-success   btn-block" OnClick="submit_Click" Style="margin-top: 10px;" />
                            </div>

                            <div class="clearfix"></div>

                            <asp:Label ID="slaid" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="slastatus" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="empID" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="mailId" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="userName" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="level2" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="level3" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="Deviation" runat="server" Visible="False"></asp:Label>
                        </div>

                         <div id="popupDiv" BackColor:"white" Height="160px" Width="180px">
                             <table width="100%" cellspacing="10" cellpading="6" border="0" runat="server" style="background-color: white;">
                                 <tr style="background-color: #259dab">
                                     <td colspan="3" align="center" style="text-align: center">
                                         <h4 style="color: white; margin-top: 10px">SLA Details</h4>
                                     </td>
                                 </tr>
                                 <tr align="center">
                                     <td align="center"><b>SLA Type</b></td>
                                     <td align="center"><b>Response Time</b></td>
                                     <td align="center"><b>Resolution Time</b></td>
                                 </tr>
                                 <tr>
                                     <td align="center">Critical</td>
                                     <td align="center">0.5 hrs</td>
                                     <td align="center">2 hrs</td>
                                 </tr>
                                 <tr>
                                     <td align="center">High</td>
                                     <td align="center">2 hrs</td>
                                     <td align="center">8 hrs</td>
                                 </tr>
                                 <tr>
                                     <td align="center">Medium</td>
                                     <td align="center">6 hrs</td>
                                     <td align="center">12 hrs</td>
                                 </tr>
                                 <tr>
                                     <td align="center">Low</td>
                                     <td align="center">12 hrs</td>
                                     <td align="center">24 hrs</td>
                                 </tr>
                                 <tr style="background-color: #259dab">
                                     <td colspan="3" align="right" class="auto-style1">
                                         <asp:Button ID="btnclose" runat="server" Text="Close" BorderStyle="None" BackColor="#259dab" Height="25px" ForeColor="White" /></td>
                                 </tr>
                             </table>
                         </div>
                        <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
                            CancelControlID="btnclose" Enabled="True" PopupControlID="popupDiv" TargetControlID="btnclick" BackgroundCssClass="tableBackground">
                        </asp:ModalPopupExtender>
                    </div>


                </div>

            </div>
        </div>
    </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="Server">
    <script>
        $(document).ready(function () {
            $("#liticket").addClass("active");
            $("#licreateticket").addClass("active");
        });
    </script>
</asp:Content>

