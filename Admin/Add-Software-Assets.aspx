<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true" CodeFile="Add-Software-Assets.aspx.cs" Inherits="AddSoftwareAssets" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">

    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%= AMS_Asset_Name.ClientID %>').autocomplete({
                source: '../Handlers/SW-Asset-Name-Handler.ashx'
            });
        });
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%= AMS_SW_Asset_Version.ClientID %>').autocomplete({
                source: '../Handlers/SW-Asset-Version-Handler.ashx'
            });
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <div class="mainpanel">
        <div class="contentpanel">
            <ol class="breadcrumb breadcrumb-quirk">
                <li><a href="index.html"><i class="fa fa-home mr5"></i>Home</a></li>
                <li><a href="../Admin/Software-Assets.aspx">Software Assets</a></li>
                <li class="active"><asp:Label ID="lbladdorupdate" runat="server" Text="Add"></asp:Label> Software Asset </li>
            </ol>
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel">
                        <div class="panel-heading">
                        </div>
                        <div class="panel-body">
                            <h4 class="panel-title">Software Details</h4>
                            <br />
                            <div class="row">
                                <div class="col-md-3" style="padding-bottom: 5px">
                                    <div class="floatings-labels">
                                        <asp:TextBox ID="AMS_Asset_Name" runat="server" AutoPostBack="true" CssClass="floating-input" placeholder=" " OnTextChanged="AMS_Asset_Name_TextChanged"></asp:TextBox>
                                        <label class="lbltop1 lbltop" for="nMobileNumber1">Asset Name</label>
                                        <asp:RequiredFieldValidator ControlToValidate="AMS_Asset_Name" ID="AssetNameRequiredFieldValidator" runat="server" ErrorMessage="Please fill this Field" Font-Size="12px" Display="Dynamic" ForeColor="orange" Font-Names="Verdana;"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3" style="padding-bottom: 5px">
                                    <div class="floatings-labels">
                                        <asp:TextBox ID="AMS_SW_Vendor" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                        <label class="lbltop1 lbltop" for="nMobileNumber1">Vendor</label>
                                        <asp:RequiredFieldValidator ControlToValidate="AMS_SW_Vendor" ID="VendorRequiredFieldValidator" runat="server" ErrorMessage="Please fill this Field" Font-Size="12px" Display="Dynamic" Font-Names="Verdana;" ForeColor="orange"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3" style="padding-bottom: 5px">
                                    <div class="floatings-labels">
                                        <asp:TextBox ID="AMS_SW_Asset_Version" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                        <label class="lbltop1 lbltop" for="nMobileNumber1">Version</label>
                                        <asp:RequiredFieldValidator ControlToValidate="AMS_SW_Asset_Version" ID="VersionRequiredFieldValidator" runat="server" ErrorMessage="Please fill this Field" Font-Size="12px" Display="Dynamic" Font-Names="Verdana;" ForeColor="orange"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" class="error" runat="server" SetFocusOnError="true" ErrorMessage="Please Enter Valid Version" ControlToValidate="AMS_SW_Asset_Version" Font-Size="12px" ValidationExpression="^\s*(?=.*[1-9])\d*(?:\.\d{1,2})?\s*$" Display="dynamic" Font-Names="Verdana;" ForeColor="orange"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-md-3" style="padding-bottom: 5px">
                                    <div class="floatings-labels">
                                        <asp:DropDownList ID="AMSCategory" CssClass="floating-select"  AutoPostBack="true" placeholder=" " value="" runat="server" OnSelectedIndexChanged="AMSCategory_SelectedIndexChanged" onclick="this.setAttribute('value', this.value);">
                                            <%--<asp:ListItem Text="Select an option... "></asp:ListItem>--%>
                                            <asp:ListItem Value="" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="On-Premises">On-Premises</asp:ListItem>
                                            <asp:ListItem>Cloud</asp:ListItem>
                                            <asp:ListItem>Hybrid</asp:ListItem>
                                        </asp:DropDownList>
                                        <label class="lbltop" for="txtMotherLang">Category</label>
                                        <asp:RequiredFieldValidator ID="CategoryRequiredFieldValidator" runat="server" InitialValue="" ControlToValidate="AMSCategory" ErrorMessage="Please Choose an option" Font-Size="12px" Display="Dynamic" Font-Names="Verdana;" ForeColor="orange"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>


                            <div class="row">

                                <asp:Panel ID="CloudPanel" runat="server" Visible="false">
                                    <div class="col-md-2" style="padding-bottom: 5px">
                                        <div class="floatings-labels">
                                            <asp:DropDownList ID="TypeofCloud" CssClass="floating-select" value="" runat="server" AutoPostBack="true" onclick="this.setAttribute('value', this.value);" OnSelectedIndexChanged="TypeofCloud_SelectedIndexChanged">
                                                <asp:ListItem Value="" Selected="True"></asp:ListItem>
                                                <asp:ListItem>Iaas</asp:ListItem>
                                                <asp:ListItem>Paas</asp:ListItem>
                                                <asp:ListItem>Saas</asp:ListItem>
                                            </asp:DropDownList>
                                            <label class="lbltop" for="txtMotherLang">Cloud type</label>
                                            <asp:RequiredFieldValidator ID="cloudtypeRequiredFieldValidator" runat="server" ControlToValidate="TypeofCloud" ErrorMessage="Please Choose an option" Font-Size="12px" Display="Dynamic" ForeColor="orange" Font-Names="Verdana;"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-md-2" style="padding-bottom: 5px">
                                        <div class="floatings-labels">
                                            <asp:DropDownList ID="Cloudlicense" AutoPostBack="true" CssClass="floating-select" value="" runat="server" OnSelectedIndexChanged="Cloudlicense_SelectedIndexChanged" onclick="this.setAttribute('value', this.value);">
                                                <%--<asp:ListItem Text="Select an option... "></asp:ListItem>--%>
                                                <asp:ListItem Value="" Selected="True"></asp:ListItem>
                                                <asp:ListItem>Trial</asp:ListItem>
                                                <asp:ListItem>Purchase</asp:ListItem>
                                            </asp:DropDownList>
                                            <label class="lbltop" for="txtMotherLang">Cloud License Type</label>
                                            <asp:RequiredFieldValidator ID="CloudlicenseRequiredFieldValidator" runat="server" ControlToValidate="Cloudlicense" ErrorMessage="Please Choose an option" Font-Size="12px" Display="Dynamic" ForeColor="orange" Font-Names="Verdana;"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <asp:Panel class="col-md-2" runat="server" ID="cloudtrialdayspanel" Visible="false" Style="padding-bottom: 5px">
                                        <div class="floatings-labels">
                                            <asp:TextBox ID="TrailDays" CssClass="floating-input" runat="server" placeholder=" " ToolTip="Please enter the License Server details of Software" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            <label class="lbltop" for="txtMotherLang">Trial Period in Days</label>
                                            <asp:RequiredFieldValidator ControlToValidate="TrailDays" ID="TrialdaysRequiredFieldValidator" runat="server" ErrorMessage="Please fill this Field" Font-Size="12px" Display="Dynamic" Font-Names="Verdana;" ForeColor="orange"></asp:RequiredFieldValidator>
                                        </div>
                                    </asp:Panel>

                                    <asp:Panel class="col-md-2" runat="server" ID="cloudcostpanel" Visible="false" Style="padding-bottom: 5px">
                                        <div class="floatings-labels">
                                            <asp:TextBox ID="Purchasedcosts" CssClass="floating-input" runat="server" placeholder=" " ToolTip="Please enter the License Server details of Software" autoscroll="no"></asp:TextBox>
                                            <label class="lbltop1 lbltop" for="nMobileNumber1">Cloud Cost <i class="fa fa-inr"></i></label>
                                            <asp:RequiredFieldValidator ControlToValidate="Purchasedcosts" ID="CloudCostRequiredFieldValidator" runat="server" ErrorMessage="Please fill this Field" Font-Size="12px" Display="Dynamic" Font-Names="Verdana;" ForeColor="orange"></asp:RequiredFieldValidator>
                                        </div>
                                    </asp:Panel>

                                    <div class="col-md-3" style="padding-bottom: 5px">
                                        <div class="floatings-labels">
                                            <asp:TextBox ID="cloudstarttime" CssClass="floating-input" placeholder=" " runat="server" ToolTip="Please enter the License Server details of Software" autoscroll="no" ></asp:TextBox>
                                            
                                            <label class="lbltop1 lbltop" for="nMobileNumber1">Cloud Start Time</label>
                                            <cc1:CalendarExtender ID="CalendarExtender3"
                                                runat="server" PopupButtonID="imgPopup" Format="yyyy-MM-dd"
                                                TargetControlID="cloudstarttime" />
                                            <asp:RequiredFieldValidator ControlToValidate="cloudstarttime" ID="RequiredFieldValidator14" runat="server" ErrorMessage="Please fill this Field" Font-Size="12px" Display="Dynamic" Font-Names="Verdana;" ForeColor="orange"></asp:RequiredFieldValidator>
                                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator8" class="error-msg" runat="server" SetFocusOnError="true" ErrorMessage="Please Enter Valid data" ControlToValidate="cloudstarttime" Font-Size="12px" ValidationExpression="^([a-zA-Z]+(_[a-zA-Z0-9]+)*)(\s([a-zA-Z0-9]+(_[a-zA-Z]+)*))*$" Display="Dynamic" Font-Names="Verdana;" ForeColor="orange"></asp:RegularExpressionValidator>--%>

                                        </div>
                                    </div>
                                    <div class="col-md-3" style="padding-bottom: 5px">
                                        <div class="floatings-labels">
                                            <asp:TextBox ID="Billingtime" CssClass="floating-input" runat="server" placeholder=" " ToolTip="Please enter the License Server details of Software" autoscroll="no"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender2"
                                                runat="server" PopupButtonID="imgPopup" Format="yyyy-MM-dd"
                                                TargetControlID="Billingtime" />
                                            <label class="lbltop1 lbltop" for="nMobileNumber1">Cloud Billing Time</label>
                                            <asp:RequiredFieldValidator ControlToValidate="Billingtime" ID="RequiredFieldValidator16" runat="server" ErrorMessage="Please fill this Field" Font-Size="12px" Display="Dynamic" Font-Names="Verdana;" ForeColor="orange"></asp:RequiredFieldValidator>
                                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator10" class="error-msg" runat="server" SetFocusOnError="true" ErrorMessage="Please Enter Valid data" ControlToValidate="cloudstarttime" Font-Size="12px" ValidationExpression="^([a-zA-Z]+(_[a-zA-Z0-9]+)*)(\s([a-zA-Z0-9]+(_[a-zA-Z]+)*))*$" Display="Dynamic" Font-Names="Verdana;" ForeColor="orange"></asp:RegularExpressionValidator>--%>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>

                            <%-- License Type --%>
                            <div class="row">
                                <div class="col-md-2" style="padding-bottom: 5px">

                                    <div class="floatings-labels">
                                        <asp:DropDownList ID="AMS_SW_License_Type" CssClass="floating-select" placeholder=" " value="" AutoPostBack="true" runat="server" OnSelectedIndexChanged="AMS_SW_License_Type_SelectedIndexChanged">
                                            <%--<asp:ListItem Text="Select an option... "></asp:ListItem>--%>
                                            <asp:ListItem Value="" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Free Ware"></asp:ListItem>
                                            <asp:ListItem Text="Trail Version"></asp:ListItem>
                                            <asp:ListItem Text="Licensed"></asp:ListItem>
                                        </asp:DropDownList>
                                        <label class="lbltop" for="txtMotherLang">License Type</label>
                                        <asp:RequiredFieldValidator ID="LicenseTypeRequiredFieldValidator" runat="server" InitialValue="" ControlToValidate="AMS_SW_License_Type" ErrorMessage="Please Choose an option" Font-Size="12px" Display="Dynamic" Font-Names="Verdana;" ForeColor="orange"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <asp:Panel ID="TrailVersionPanel" runat="server" Visible="false">
                                    <div class="col-md-2" style="padding-bottom: 5px">
                                        <div class="floatings-labels">
                                            <asp:TextBox ID="AMS_SW_NumofdaysforTrial" CssClass="floating-input" runat="server" Placeholder=" " ToolTip="Please enter no of days for trial of an asset" MaxLength="3" MinLength="2" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            <label class="lbltop" for="nMobileNumber1">Trial Period in Days</label>
                                            <asp:RequiredFieldValidator ControlToValidate="AMS_SW_NumofdaysforTrial" ID="RequiredFieldValidator15" runat="server" ErrorMessage="Please fill this Field" Font-Size="12px" Display="Dynamic" Font-Names="Verdana;" ForeColor="orange"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" class="error-msg" runat="server" SetFocusOnError="true" ErrorMessage="Please Enter Valid data" ControlToValidate="AMS_SW_NumofdaysforTrial" Font-Size="12px" ValidationExpression="^[1-9]+[0-9]*$" Display="Dynamic" Font-Names="Verdana;" ForeColor="orange"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="LicensedPanel" runat="server" Visible="false">
                                    <div class="col-md-2" style="padding-bottom: 5px">
                                        <div class="floatings-labels">
                                            <asp:TextBox ID="AMS_Asset_Cost" CssClass="floating-input" Visible="true" runat="server" Placeholder=" " ToolTip="Please Enter Asset Cost" MaxLength="10" MinLenght="2"></asp:TextBox>
                                            <label class="lbltop" for="nMobileNumber1" id="floatlblcost" runat="server">Cost</label>
                                            <asp:RequiredFieldValidator ControlToValidate="AMS_Asset_Cost" ID="costRequiredFieldValidator" runat="server" ErrorMessage="Please fill this Field" Font-Size="12px" Display="Dynamic" ForeColor="orange" Font-Names="Verdana;"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="costRegularExpressionValidator" class="error" runat="server" SetFocusOnError="true" ErrorMessage="Please Enter Valid Data" ControlToValidate="AMS_Asset_Cost" Font-Size="12px" ValidationExpression="^\s*(?=.*[1-9])\d*(?:\.\d{1,2})?\s*$" Display="dynamic" Font-Names="Verdana;" ForeColor="orange"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="col-md-4" style="padding-bottom: 5px">
                                        <div class="floatings-labels">
                                            <asp:TextBox ID="LicenseNumber" runat="server" CssClass="floating-input" placeholder=" " ToolTip="Please enter the License key of Software" MaxLength="29"></asp:TextBox>
                                            <label class="lbltop" for="nMobileNumber1" id="floatlbllicNo" runat="server">License Number</label>
                                            <asp:RequiredFieldValidator ControlToValidate="LicenseNumber" ID="LnoRequiredFieldValidator" runat="server" ErrorMessage="Please fill this Field" Font-Size="12px" Display="Dynamic" Font-Names="Verdana;" ForeColor="orange"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="LnoRegularExpressionValidator" class="error" runat="server" SetFocusOnError="true" ErrorMessage="Please Enter Valid data" ControlToValidate="LicenseNumber" Font-Size="12px" ValidationExpression="^[a-zA-Z0-9-]+$" Display="Dynamic" Font-Names="Verdana;" ForeColor="orange"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="col-md-4" style="padding-bottom: 5px">
                                        <div class="floatings-labels">
                                            <asp:TextBox ID="LicenseServerDetails" CssClass="floating-input" Visible="true" Placeholder=" " TextMode="MultiLine" Style="background-color: transparent; min-width: 100%; max-width: 100%; max-height: 100%; min-height: 100%; overflow: hidden" runat="server" ToolTip="Please enter the License Server details of Software" autoscroll="no"></asp:TextBox>
                                            <label class="lbltop" for="txtMotherLang" id="floatlblServDets" runat="server">License Server Details</label>
                                            <asp:RequiredFieldValidator ControlToValidate="LicenseServerDetails" ID="LdetRequiredFieldValidator" runat="server" ErrorMessage="Please fill this Field" Font-Size="12px" Display="Dynamic" Font-Names="Verdana;" ForeColor="orange"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="LdetRegularExpressionValidator" class="error-msg" runat="server" SetFocusOnError="true" ErrorMessage="Please Enter Valid data" ControlToValidate="LicenseServerDetails" Font-Size="12px" ValidationExpression="^([a-zA-Z]+(_[a-zA-Z0-9]+)*)(\s([a-zA-Z0-9]+(_[a-zA-Z]+)*))*$" Display="Dynamic" Font-Names="Verdana;" ForeColor="orange"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                            <div class="clearfix"></div>


                            <h4 class="panel-title">Purchase & Vendor Details</h4>
                            <br />
                            <br />
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="floatings-labels">
                                        <asp:TextBox ID="AMS_Date_of_Purchase" runat="server" CssClass="floating-input" AutoPostBack="true" placeholder=" " OnTextChanged="AMS_Date_of_Purchase_TextChanged"></asp:TextBox>
                                        <label class="lbltop" for="nMobileNumber1">Date of Purchase</label>
                                        <cc1:CalendarExtender ID="CalendarExtender1"
                                            runat="server" PopupButtonID="imgPopup" Format="yyyy-MM-dd"
                                            TargetControlID="AMS_Date_of_Purchase" />
                                        <asp:RequiredFieldValidator ControlToValidate="AMS_Date_of_Purchase" ID="DOPRequiredFieldValidator" runat="server" ErrorMessage="Please fill this Field" Font-Size="12px" Display="Dynamic" ForeColor="orange" Font-Names="Verdana;"></asp:RequiredFieldValidator>
                                        <asp:Label ID="DOPLabel" runat="server" Visible="false" Text="Invalid date or format" Font-Size="12px" Display="Dynamic" ForeColor="orange" Font-Names="Verdana;"></asp:Label>
                                    </div>

                                </div>
                                <div class="col-md-3">
                                    <div class="floatings-labels">
                                        <asp:TextBox ID="VendorMail" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                        <label class="lbltop" for="nMobileNumber1">Vendor Mail ID</label>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="floatings-labels">
                                        <asp:TextBox ID="VendorContact" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                        <label class="lbltop" for="nMobileNumber1">Vendor Contact (optional)</label>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="floatings-labels">
                                        <asp:TextBox ID="Asset_Remarks" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                        <label class="lbltop" for="nMobileNumber1">Remarks (optional)</label>
                                    </div>
                                </div>

                                <div class="clearfix"></div>
                            </div>
                            <div class="row">
                                <div class="col-md-2" id="Resetbutton_div" runat="server">
                                    <asp:Button ID="Reset" runat="server" Text="Reset" CausesValidation="false" class="btn btn-warning btn-block" Style="margin-top: 10px;" OnClick="Reset_Click1" />
                                </div>
                                
                                <div class="col-md-2" id="sw_Insertbutton_div" runat="server">
                                    <asp:Button ID="sw_Insert" runat="server" Text="Insert" class="btn btn-success btn-block" Style="margin-top: 10px;" OnClick="sw_Insert_Click" />
                                </div>

                                <div class="col-md-2" id="updatebutton_div" runat="server">
                                    <asp:Button ID="Update" runat="server" Text="Update" Visible="false" class="btn btn-primary btn-block" Style="margin-top: 10px;" OnClick="Update_Click" />
                                </div>
                            </div>

                        </div>
                    </div>
                    <!-- panel -->


                </div>
                <!-- col-sm-6 -->

            </div>
            <!-- row -->

        </div>
        <asp:ScriptManager ID="scr" runat="server"></asp:ScriptManager>
        <!-- contentpanel -->
    </div>
    <!-- mainpanel -->

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="Server">
    <script>
        $(document).ready(function () {
            //$("#liadminassets").addClass("active");
            //$("#liadminaddsw").addClass("active");
            $("#liadminassets").addClass("active");
            $("#liadminsw").addClass("active");
        });
    </script>
</asp:Content>

