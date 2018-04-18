<%@ Page Title="" Language="C#" MasterPageFile="~/Operations/emp.master" AutoEventWireup="true" CodeFile="Need-Help.aspx.cs" Inherits="NeedHelp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <div class="mainpanel">
        <div class="contentpanel">
            <ol class="breadcrumb breadcrumb-quirk">
                <li><a href="../Operations/Dashboard.aspx"><i class="fa fa-home mr5"></i>Home</a></li>
                <li class="active"><i class="fa fa-question-circle">&nbsp;</i>Need Help</li>
            </ol>

            <div class="row">
                <div class="col-sm-12">
                    <div class="panel">

                        <div class="panel-heading">
                            <h4 align="center" class="panel-title">Need Help?</h4>
                        </div>

                        <div class="panel-body">
                            <div class="col-md-3"></div>
                            <div class="col-md-6">
                                    <div class="col-md-12">
                                        <div class="floatings-labels">
                                            <asp:TextBox ID="UserName" runat="server" ReadOnly="true" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                            <label class="lbltop1 lbltop" for="nMobileNumber1">Your Name </label>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="floatings-labels">
                                            <asp:TextBox ID="UserEmail" runat="server" ReadOnly="true" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                            <label class="lbltop1 lbltop" for="nMobileNumber1">Your Email </label>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="floatings-labels">
                                            <asp:TextBox ID="UserSubject" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                            <label class="lbltop1 lbltop" for="nMobileNumber1">Subject</label>
                                        </div>
                                        <asp:RequiredFieldValidator ControlToValidate="UserSubject" ID="RequiredFieldValidator2" runat="server" ErrorMessage="* Please fill this field" Font-Size="12px" Display="Dynamic" Font-Names="Verdana;" ForeColor="orange"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-12" style="width: 100%">
                                        <div class="floatings-labels" style="width: 100%">
                                            <asp:TextBox ID="UserMessage" runat="server" CssClass="floating-input" Style="width: 100%" Width="100%" TextMode="MultiLine" placeholder=" "></asp:TextBox>
                                            <label class="lbltop1 lbltop" for="nMobileNumber1">Your Message</label>
                                        </div>
                                        <asp:RequiredFieldValidator ControlToValidate="UserMessage" ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Please fill this field" Font-Size="12px" Display="Dynamic" Font-Names="Verdana;" ForeColor="orange"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="col-md-12">
                                        <asp:Button ID="UserSubmitButton" runat="server" Text="Submit" class="btn btn-success btn-block" Style="margin-top: 10px;" OnClick="UserSubmitButton_Click" />
                                    </div>

                                </div>
                            <div class="clearfix"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" Runat="Server">
</asp:Content>

