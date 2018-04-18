<%@ Page Title="" Language="C#" MasterPageFile="~/Operations/emp.master" AutoEventWireup="true" CodeFile="ticket-info.aspx.cs" Inherits="Operations_Default" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <div class="mainpanel">
        <div class="contentpanel">
            <div class="row profile-wrapper">
                <div class="col-xs-12 col-md-3 col-lg-3 profile-left">
                    <div class="profile-left-heading">

                        <h2 class="profile-name">Ticket Details  </h2>
                        <h4 class="profile-designation">Software Engineer</h4>
                        <ul class="list-group">
                            <li class="list-group-item">Ticket ID <a href="timeline.html">
                                <asp:Label runat="server" ID="tckid"></asp:Label></a></li>

                            <li class="list-group-item">Priority <a href="people-directory.html">
                                <asp:Label runat="server" ID="prt"></asp:Label></a></li>
                            <li class="list-group-item">Host Name <a href="people-directory.html">
                                <asp:Label runat="server" ID="hstname"></asp:Label></a></li>
                            <li class="list-group-item">Status <a href="people-directory.html">
                                <asp:Label runat="server" ID="Sts"></asp:Label></a></li>
                            <li class="list-group-item">Employee ID<a href="people-directory-grid.html"><asp:Label runat="server" ID="Empid"></asp:Label></a></li>
                            <li class="list-group-item">Date <a href="people-directory-grid.html">
                                <asp:Label runat="server" ID="tckdate"></asp:Label></a></li>
                            <li class="list-group-item">Subject <a href="people-directory.html">
                                <asp:Label runat="server" ID="sub"></asp:Label></a></li>
                        </ul>
                        <asp:Button runat="server" class="btn btn-danger btn-quirk btn-block profile-btn-follow" Text="TICKET RE-SUBMIT" ID="ReSubmit"  Visible="false" OnClick="ReSubmit_Click" ></asp:Button>
                    </div>
                    <div class="profile-left-body">
                        <h4 class="panel-title">Description  </h4>
                        <p>
                            <asp:Label runat="server" ID="Desc"></asp:Label>
                        </p>
                        <hr class="fadeout">
                        <h4 class="panel-title">Location</h4>
                        <p><i class="glyphicon glyphicon-map-marker mr5"></i>
                            <asp:Label runat="server" ID="Lct"></asp:Label></p>
                        <hr class="fadeout">

                        <h4 class="panel-title">OwnerShip</h4>
                        <p><i class="glyphicon glyphicon-phone mr5"></i>
                            <asp:Label runat="server" ID="Onr"></asp:Label></p>

                    </div>

                    <div class="isa_error" id="errorDiv" runat="server">
                        <i class="fa fa-times-circle"></i>
                        <asp:Label ID="errorMessage" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                </div>
                <div class="col-md-9 col-lg-9 profile-right">
                              <%--           <div class="form mb20">
 
                                <div class="col-md-3">
                                    <div class="floatings-labels">
                                        <asp:DropDownList ID="DropDownList1" CssClass="floating-select" runat="server" value="" onclick="this.setAttribute('value', this.value);">
                                            <asp:ListItem Text=""> </asp:ListItem>
                                            <asp:ListItem Text="Approved"> </asp:ListItem>
                                            <asp:ListItem Text="Reject"> </asp:ListItem>

                                        </asp:DropDownList>

                                        <label class="lbltop" for="txtMotherLang">Status Update    </label>
                                    </div>
                                </div>
                                 <div class="col-md-3">

                                    <asp:Button ID="Button2" runat="server" Text="Update Ticket" class="btn btn-success   btn-block" Style="margin-top: 10px;" />
                                </div>

                                  <div class="col-md-3">
                                    <div class="floatings-labels">
                                        <asp:DropDownList ID="DropDownList2" CssClass="floating-select" runat="server" value="" onclick="this.setAttribute('value', this.value);">
                                            <asp:ListItem Text=""> </asp:ListItem>
                                            <asp:ListItem Text="Approved"> </asp:ListItem>
                                            <asp:ListItem Text="Reject"> </asp:ListItem>

                                        </asp:DropDownList>

                                        <label class="lbltop" for="txtMotherLang">Department    </label>
                                    </div>
                                </div> 
                               

                                <div class="col-md-3">

                                    <asp:Button ID="Button1" runat="server" Text="Reassign" class="btn btn-success   btn-block" Style="margin-top: 10px;" />
                                </div>

                                <div class="clearfix"></div>


                            </div>--%>
                    <div class="profile-right-body">
                        <!-- Nav tabs -->
                        <ul class="nav nav-tabs nav-justified nav-line">
                            <li class="active"><a href="#comments" data-toggle="tab"><strong><i class="fa fa-comments-o"></i>Comments</strong></a></li>
                            <li><a href="#photos" data-toggle="tab"><strong><i class="fa fa-envelope"></i>Mail</strong></a></li>
                            <li><a href="#music" data-toggle="tab"><strong><i class="fa fa-paperclip"></i>Attachment</strong></a></li>
                        </ul>
                        <!-- Tab panes -->
                        <div class="tab-content">
                            <div class="tab-pane active" id="comments">
                                <div class="panel panel-post-item">
                                    <div class="panel-heading">
                                        <div class="media">

                                            <div class="media-body">
                                                <h4 class="media-heading">User</h4>
                                                <p class="media-usermeta">
                                                    <span class="media-time">
                                                        <asp:TextBox ID="TextBox4" runat="server" Visible="false"></asp:TextBox></span>
                                                </p>
                                            </div>
                                        </div>
                                        <!-- media -->
                                    </div>
                                    <!-- panel-heading -->
                                    <div class="panel-body">
                                        <p>
                                            <asp:Label ID="txtRemarks" runat="server" Text="Label"></asp:Label>
                                        </p>
                                    </div>
                                    <div class="form-group">

                                        <asp:TextBox runat="server" ID="remarks" class="form-control" placeholder="Write some comments"></asp:TextBox>
                                        <asp:Button runat="server" ID="Comments" class="btn btn-success btn-quirk btn-block profile-btn-follow" Text="Send" OnClick="Comments_Click" ></asp:Button>
                                    </div>
                                </div>
                                <!-- panel panel-post -->

                            </div>
                            <!-- tab-pane -->
                            <div class="tab-pane" id="photos">
                                <div class="form mb20">
                                    <div class="col-md-6">
                                        <div class="floatings-labels">
                                            <asp:TextBox ID="Mailid" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                            <label class="lbltop1 lbltop" for="nMobileNumber1">Mail ID     </label>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="floatings-labels">
                                            <asp:TextBox ID="subject" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                            <label class="lbltop1 lbltop" for="nMobileNumber1">Subject     </label>
                                        </div>
                                    </div>

                                    <div class="col-md-12">
                                        <div class="floatings-labels">
                                            <asp:TextBox ID="Description" runat="server" TextMode="MultiLine" Height="100px" CssClass="form-control" placeholder="Description"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <asp:Button ID="MailToUser" runat="server" Text="Mail to User" class="btn btn-success   btn-block" Style="margin-top: 10px;" OnClick="MailToUser_Click"   />
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                            </div>
                            <div class="tab-pane" id="music">

                                <div class="col-md-12">
                                    <div class="col-md-12 mb20 ">

                                        <label class="text-info "><b>Upload File</b>  </label>
                                        <div class="clearfix"></div>
                                        <asp:FileUpload ID="FileUpload1" CssClass="fileContainer" runat="server" />
                                    </div>
                                    <div class="col-md-12 text-center mb20">

                                        <%--<img src="../images/Bill-Receipt-300x231--300x231.jpg" />--%>
                                        <asp:GridView ID="GridView2" runat="server" align="Center" AutoGenerateColumns="False" Height="75px" Width="321px" RowStyle-HorizontalAlign="Center" CellPadding="4" ForeColor="#333333" GridLines="None" >
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <%--<asp:BoundField DataField="TMSAttachment" HeaderText="Attachment" />--%>
                                                <asp:BoundField DataField="ContentName" HeaderText="File name">
                                                    <HeaderStyle BackColor="#2BBBAD" HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Download">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDownload" OnClick="lnkDownload_Click1" runat="server" Text=""  CommandArgument='<%# Eval("TMSAttachment") %>'><i class="fa fa-download" aria-hidden="true" style="font-size:large;color:#2bbbad" ></i></asp:LinkButton>

                                                    </ItemTemplate>
                                                    <HeaderStyle BackColor="#2BBBAD" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <EditRowStyle BackColor="#7C6F57" />
                                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle HorizontalAlign="left"></RowStyle>
                                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                            <SortedAscendingHeaderStyle BackColor="#246B61" />
                                            <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                            <SortedDescendingHeaderStyle BackColor="#15524A" />
                                        </asp:GridView>
                                        <br />
                                        <br />
                                        <br />
                                        <asp:Label ID="Label1" runat="server" Text="" ForeColor="#2bbbad" Font-Size="X-Large"></asp:Label>

                                    </div>




                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- row -->
        </div>
        <!-- contentpanel -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="Server">
</asp:Content>


