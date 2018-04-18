<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true" CodeFile="Add-Hardware-Assets.aspx.cs" Inherits="AddHardwareAssets" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">

    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%= AMS_Asset_Name.ClientID %>').autocomplete({
                source: '../Handlers/HW-Asset-Name-Handler.ashx'
            });
        });
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%= AMS_PH_Make.ClientID %>').autocomplete({
                source: '../Handlers/HW-Asset-Manufacturer-Handler.ashx'
            });
        });
    </script>

    <script type="text/javascript">
           $(document).ready(function () {
               $('#<%= AMS_PH_Model.ClientID %>').autocomplete({
                   source: '../Handlers/HW-Asset-Model-Handler.ashx'
               });
           });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">

    <div class="mainpanel">
        <div class="contentpanel">
            <ol class="breadcrumb breadcrumb-quirk">
                <li><a href="index.html"><i class="fa fa-home mr5"></i>Home</a></li>
                <li><a href="../Admin/Hardware-Assets.aspx">Hardware Assets</a></li>
                <li class="active"><asp:Label ID="lbladdorupdate" runat="server" Text="Add"></asp:Label> Hardware Asset </li>
            </ol>
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel">
                        <div class="panel-heading">
                        </div>
                        <div class="panel-body">
                            <h4 class="panel-title">Hardware Details</h4><br />
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="floatings-labels">
                                        <asp:TextBox ID="AMS_Asset_Name" runat="server" AutoPostBack="true" CssClass="floating-input" placeholder=" " OnTextChanged="AMS_Asset_Name_TextChanged"></asp:TextBox>
                                        <label class="lbltop1 lbltop" for="nMobileNumber1">Asset Name</label>
                                        <asp:RequiredFieldValidator ID="AssetNameRequiredFieldValidator" runat="server" ErrorMessage="Please fill this field" ControlToValidate="AMS_Asset_Name" Font-Size="12px" Display="Dynamic" Font-Names="Verdana;" ForeColor="orange"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="AssetNameRegularExpressionValidator" class="error-msg" runat="server" SetFocusOnError="true" ErrorMessage="Please enter valid name" ControlToValidate="AMS_Asset_Name" Font-Size="12px" ValidationExpression="^([a-zA-Z]+(_[a-zA-Z0-9]+)*)(\s([a-zA-Z0-9]+(_[a-zA-Z]+)*))*$" Display="Dynamic" Font-Names="Verdana;" ForeColor="orange"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="floatings-labels">
                                        <asp:TextBox ID="AMS_PH_Make" runat="server" CssClass="floating-input" AutoPostBack="true" placeholder=" " OnTextChanged="AMS_PH_Make_TextChanged"></asp:TextBox>
                                        <label class="lbltop1 lbltop" for="nMobileNumber1">Manufacturer Name</label>
                                        <asp:RequiredFieldValidator ControlToValidate="AMS_PH_Make" ID="MakeRequiredFieldValidator" runat="server" ErrorMessage="Please fill this field" Font-Size="12px" Display="Dynamic" Font-Names="Verdana;" ForeColor="orange"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="floatings-labels">
                                        <asp:TextBox ID="AMS_PH_Model" runat="server" CssClass="floating-input" placeholder=" " ></asp:TextBox>
                                        <label class="lbltop1 lbltop" for="nMobileNumber1">Model</label>
                                        <asp:RequiredFieldValidator ControlToValidate="AMS_PH_Model" ID="ModelRequiredFieldValidator" runat="server" ErrorMessage="Please fill this field" Font-Size="12px" Display="Dynamic" Font-Names="Verdana;" ForeColor="orange"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="floatings-labels">
                                        <asp:TextBox ID="AMS_PH_yearofManufacturer" AutoPostBack="true" runat="server" CssClass="floating-input" placeholder=" " OnTextChanged="AMS_PH_yearofManufacturer_TextChanged" ></asp:TextBox>
                                        <label class="lbltop1 lbltop" for="nMobileNumber1">Year of Manufacturing</label>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="imgPopup" Format="yyyy-MM-dd" TargetControlID="AMS_PH_yearofManufacturer" />
                                        <asp:RequiredFieldValidator ControlToValidate="AMS_PH_yearofManufacturer" ID="YOMRequiredFieldValidator" runat="server" ErrorMessage="Please fill this field" Font-Size="12px" Display="Dynamic" Font-Names="Verdana;" ForeColor="orange"></asp:RequiredFieldValidator>
                                        <asp:Label ID="YOMLabel" Visible="false" runat="server" Text="Invalid date or format" Font-Size="12px" Display="Dynamic" ForeColor="orange" Font-Names="Verdana;"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="floatings-labels">
                                        <asp:TextBox ID="AMS_Characteristics" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                        <label class="lbltop1 lbltop" for="nMobileNumber1">Characteristics</label>
                                        <asp:RequiredFieldValidator ControlToValidate="AMS_Characteristics" ID="SpecRequiredFieldValidator" runat="server" ErrorMessage="Please fill this field" Font-Size="12px" Display="Dynamic" Font-Names="Verdana;" ForeColor="orange"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="SpecRegularExpressionValidator" runat="server" class="error-msg" ControlToValidate="AMS_Characteristics" Display="Dynamic" ErrorMessage="please enter valid data" Font-Names="Verdana;" Font-Size="12px" SetFocusOnError="true" ValidationExpression="^([a-zA-Z0-9-.]+(_[a-zA-Z0-9-.]+)*)(\s([a-zA-Z0-9-.]+(_[a-zA-Z0-9-.]+)*))*$" ForeColor="orange"></asp:RegularExpressionValidator>
                                    </div>
                                </div>

                                <div class="clearfix"></div>


                            </div>
                            <br />

                            <h4 class="panel-title">Purchase Details</h4><br />
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="floatings-labels">
                                        <asp:TextBox ID="AMS_Asset_Cost" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                        <label class="lbltop1 lbltop" for="nMobileNumber1">Cost</label>
                                        <asp:RequiredFieldValidator ControlToValidate="AMS_Asset_Cost" ID="CostRequiredFieldValidator" runat="server" ErrorMessage="please enter valid data" Font-Size="12px" Display="Dynamic" Font-Names="Verdana;" ForeColor="Orange"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="CostRegularExpressionValidator" class="error-msg" runat="server" SetFocusOnError="true" ErrorMessage="Please Enter positive numbers only" ControlToValidate="AMS_Asset_Cost" Font-Size="12px" ValidationExpression="^\s*(?=.*[1-9])\d*(?:\.\d{1,2})?\s*$" Display="Dynamic" Font-Names="Verdana;" ForeColor="orange"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="floatings-labels">
                                        <asp:DropDownList ID="AMS_Asset_Locationn" CssClass="floating-select" runat="server" value="" onclick="this.setAttribute('value', this.value);">
                                            <asp:ListItem Selected="True"></asp:ListItem>
                                            <asp:ListItem>Hyderabad</asp:ListItem>
                                            <asp:ListItem>Bengaluru</asp:ListItem>
                                            <asp:ListItem>Mumbai</asp:ListItem>
                                            <asp:ListItem>Vijayawada</asp:ListItem>
                                            <asp:ListItem>Boston</asp:ListItem>
                                            <asp:ListItem>Dubai</asp:ListItem>
                                            <asp:ListItem>Germany</asp:ListItem>
                                            <asp:ListItem>South Africa</asp:ListItem>
                                        </asp:DropDownList>
                                        <label class="lbltop" for="txtMotherLang">Asset Location</label>
                                        <asp:RequiredFieldValidator ID="LocationRequiredFieldValidator" runat="server" InitialValue="" ControlToValidate="AMS_Asset_Locationn" ErrorMessage="Select Asset Location" Font-Size="12px" Display="Dynamic" Font-Names="Verdana;" ForeColor="orange"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="floatings-labels">
                                        <asp:TextBox ID="AMS_Date_of_Purchase" runat="server" CssClass="floating-input" AutoPostBack="true" placeholder=" " OnTextChanged="AMS_Date_of_Purchase_TextChanged"></asp:TextBox>
                                        <label class="lbltop1 lbltop" for="nMobileNumber1">Date of Purchase</label>
                                        <cc1:CalendarExtender ID="CalendarExtender2"
                                            runat="server" PopupButtonID="imgPopup" Format="yyyy-MM-dd"
                                            TargetControlID="AMS_Date_of_Purchase" />
                                        <asp:RequiredFieldValidator ControlToValidate="AMS_Date_of_Purchase" ID="DOPRequiredFieldValidator" runat="server" ErrorMessage="Please fill this field" Font-Size="12px" Display="Dynamic" Font-Names="Verdana;" ForeColor="orange"></asp:RequiredFieldValidator>
                                        <asp:Label ID="DOPLabel" Visible="false" runat="server" Text="Invalid date or format" Font-Size="12px" Display="Dynamic" ForeColor="orange" Font-Names="Verdana;"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="floatings-labels">
                                        <asp:TextBox ID="AMS_PH_Warranty" runat="server" CssClass="floating-input" placeholder=" " onkeypress="return isNumberKey(event)"></asp:TextBox>
                                        <label class="lbltop1 lbltop" for="nMobileNumber1">Warranty</label>
                                        <asp:RegularExpressionValidator ID="ymRegularExpressionValidator" class="error-msg" runat="server" SetFocusOnError="true" ErrorMessage="Enter Positive Numbers only" ControlToValidate="AMS_PH_Warranty" Font-Size="12px" ValidationExpression="^[1-9]+[0-9]*$" Display="dynamic" Font-Names="Verdana;" ForeColor="orange"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="floatings-labels">
                                        <asp:DropDownList ID="selectyears" CssClass="floating-select" runat="server" value="" onclick="this.setAttribute('value', this.value);">
                                            <asp:ListItem Value=""></asp:ListItem>
                                            <asp:ListItem>years</asp:ListItem>
                                            <asp:ListItem>months</asp:ListItem>
                                        </asp:DropDownList>
                                        <label class="lbltop" for="txtMotherLang">Years and Months</label>
                                        
                                        <asp:RequiredFieldValidator ControlToValidate="AMS_PH_Warranty" ID="ymRequiredFieldValidator" runat="server" ErrorMessage="Please fill these fields" Font-Size="12px" Display="Dynamic" Font-Names="Verdana;" ForeColor="orange"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="floatings-labels">
                                        <asp:DropDownList ID="Warrenty_Status" CssClass="floating-select" runat="server" value="" onclick="this.setAttribute('value', this.value);">
                                            <asp:ListItem Value=""></asp:ListItem>
                                            <asp:ListItem>Working</asp:ListItem>
                                            <asp:ListItem>Expired</asp:ListItem>
                                        </asp:DropDownList>
                                        <label class="lbltop" for="txtMotherLang">Warranty Status</label>
                                        <asp:RequiredFieldValidator ID="WarrentyStatusRequiredFieldValidator" runat="server" InitialValue="" ControlToValidate="Warrenty_Status" ErrorMessage="Please select category" Font-Size="12px" Display="Dynamic" ForeColor="orange" Font-Names="Verdana;"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                </div>
                            <div class="row">
                                <%--<div class="col-md-3">
                                    <div class="floatings-labels">
                                        <asp:TextBox ID="TextBox4" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                        <label class="lbltop1 lbltop" for="nMobileNumber1">Allocated Employe Name  </label>
                                    </div>
                                </div>--%>
                                
                                <div class="col-md-4">
                                    <div class="floatings-labels">
                                        <asp:TextBox ID="Remarks" runat="server" CssClass="floating-input" TextMode="MultiLine" placeholder=" "></asp:TextBox>
                                        <label class="lbltop1 lbltop" for="nMobileNumber1">Remarks (optional)</label>
                                        <asp:RegularExpressionValidator ID="RemarksRegularExpressionValidator" runat="server" class="error-msg" ControlToValidate="Remarks" Display="Dynamic" ErrorMessage="Please Enter Alphabets" Font-Names="Verdana;" Font-Size="12px" SetFocusOnError="true" ValidationExpression="^([a-zA-Z0-9,]+(_[a-zA-Z0-9,]+)*)(\s([a-zA-Z0-9,]+(_[a-zA-Z0-9,]+)*))*$" ForeColor="orange"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                            </div>

                            <div class="row">
                                
                                <div class="col-md-2" id="Resetbutton_div" runat="server">
                                    <asp:Button ID="Reset" runat="server" Text="Reset" CausesValidation="false" class="btn btn-warning btn-block" Style="margin-top: 10px;" OnClick="Reset_Click" />
                                </div>
                                
                                <div class="col-md-2" id="sw_Insertbutton_div" runat="server">
                                    <asp:Button ID="sw_Insert" runat="server" Text="Insert" class="btn btn-success btn-block" Style="margin-top: 10px;" OnClick="Insert_Click" />
                                </div>

                                <div class="col-md-2" id="updatebutton_div" runat="server">
                                    <asp:Button ID="Update" runat="server" Text="Update" Visible="false" class="btn btn-primary btn-block" Style="margin-top: 10px;" OnClick="Update_Click" />
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
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="Server">
    <script>
           $(document).ready(function () {
               //$("#liadminassets").addClass("active");
               //$("#liadminaddhw").addClass("active");
               $("#liadminassets").addClass("active");
               $("#liadminhw").addClass("active");
           });
    </script>
</asp:Content>
