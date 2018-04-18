<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true" CodeFile="Asset-Reports.aspx.cs" Inherits="AssetReports" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
    <style>
        .dataTables_filter {
            float: right;
        }

        .table-responsive {
            overflow: unset;
        }

        #cphBody_GridTicketData_paginate {
            float: right !important;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <div class="mainpanel">
        <div class="contentpanel">
            <ol class="breadcrumb breadcrumb-quirk">
                <li><a href="../Admin/AdminDashboard.aspx"><i class="fa fa-home mr5"></i>Home</a></li>
                <li class="active">Asset Reports</li>
            </ol>

            <div class="row">
                <div class="col-sm-12">
                    <div class="panel">
                        <div class="panel-heading">
                            <h4 class="panel-title">Asset Reports</h4>
                            <div class="panel-body">
                                <div class="form mb20">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="floatings-labels">
                                                <asp:DropDownList CssClass="floating-select" CausesValidation="false" value="" ID="ReportAssetType" AutoPostBack="true" runat="server" ToolTip="Select Asset Type" required="required" OnSelectedIndexChanged="ReportAssetType_SelectedIndexChanged1" onclick="this.setAttribute('value', this.value);">
                                                    <asp:ListItem Value=""></asp:ListItem>
                                                    <asp:ListItem>Software Assets</asp:ListItem>
                                                    <asp:ListItem>Hardware Assets</asp:ListItem>
                                                </asp:DropDownList>
                                                <label class="lbltop" for="txtMotherLang">Asset Type</label>
                                                <asp:RequiredFieldValidator ControlToValidate="ReportAssetType" ID="ReportAssetTypeRequiredFieldValidator" runat="server" ErrorMessage="Please fill this Field" Font-Size="12px" Display="Dynamic" ForeColor="orange" Font-Names="Verdana;"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="floatings-labels">
                                                <asp:DropDownList ID="Dateorotherlist" CssClass="floating-select" value="" CausesValidation="false" Visible="true" AutoPostBack="true" runat="server" ToolTip="Select Asset Type" required="required" OnSelectedIndexChanged="Dateorotherlist_SelectedIndexChanged" onclick="this.setAttribute('value', this.value);">
                                                    <asp:ListItem Value=""></asp:ListItem>
                                                    <asp:ListItem>Get Report By Date</asp:ListItem>
                                                    <asp:ListItem>Others</asp:ListItem>
                                                </asp:DropDownList>
                                                <label class="lbltop" for="txtMotherLang">Report Type</label>
                                            </div>
                                        </div>
                                        <div class="col-md-6" id="searchdiv" runat="server" visible="false">
                                            <div class="floatings-labels">
                                                <asp:TextBox ID="txtFindVendor" runat="server" Visible="true" CssClass="floating-input" PlaceHolder=" " ToolTip="Please enter some keywords and Search.." OnTextChanged="txtFindVendor_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                <label class="lbltop" for="txtMotherLang">Search with Company Location,ID,Name,Purchased Date...</label>
                                            </div>
                                        </div>
                                        <div id="divDate" runat="server" visible="false">
                                            <div class="col-md-3">
                                                <div class="floatings-labels">
                                                    <asp:TextBox ID="fromDate" CssClass="floating-input" placeholder=" " ToolTip="Select From Date of an Asset" runat="server"></asp:TextBox>
                                                    <asp:ScriptManager ID="ScriptManager1" runat="server" />
                                                    <cc1:CalendarExtender ID="eswar" runat="server" PopupButtonID="imgPopup" Format="dd/MM/yyyy" TargetControlID="fromDate" />
                                                    <label class="lbltop1 lbltop" for="nMobileNumber2">From Date</label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="floatings-labels">
                                                    <asp:TextBox ID="todate" required="required" CssClass="floating-input" placeholder=" " ToolTip="Select To Date of an Asset" runat="server"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender2"
                                                        runat="server" PopupButtonID="imgPopup" Format="dd/MM/yyyy"
                                                        TargetControlID="todate" />
                                                    <label class="lbltop1 lbltop" for="nMobileNumber2">To Date</label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3 mb5">
                                        <asp:LinkButton runat="server" Visible="false" ToolTip="Click here to Get Report" ID="LinkButton3" CssClass="btn btn-success btn-block"
                                            OnClick="LinkButton3_Click"><i class="fa fa-file-text" aria-hidden="true">&nbsp;</i>Get Report</asp:LinkButton>
                                    </div>
                                    <div class="col-md-3 mb5">
                                        <asp:LinkButton runat="server" Visible="false" ToolTip="Click here to Download Asset Reports in Excel file" ID="exportToExcel" CssClass="btn btn-success btn-block"
                                            OnClick="exportToExcel_Click1"><i class="fa fa-download" aria-hidden="true"></i> Export To Excel</asp:LinkButton>
                                    </div>
                                </div>

                                <br />
                                <div class="col-md-3">
                                    <asp:LinkButton ID="Otherexporttoexcell" CssClass="btn btn-success btn-block" Visible="false" ToolTip="Click here to Download Asset Reports in Excel file"
                                        runat="server" OnClick="Otherexporttoexcell_Click"> <i class="fa fa-download" aria-hidden="true"></i>Export To Excel</asp:LinkButton>
                                </div>
                                <div class="table-responsive mt5">
                                    <asp:GridView ID="GridTicketData" runat="server"
                                        CssClass="table table-bordered table-striped table-inverse nomargin" PageSize="5"
                                        AllowPaging="true" OnPageIndexChanging="GridTicketData_PageIndexChanging" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="AMS_AssetID" ItemStyle-Width="1px" HeaderText="Asset ID" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs" />
                                            <asp:BoundField DataField="AMS_AssetName" HeaderText="Name" />
                                            <asp:BoundField DataField="AMS_AssetType" HeaderText="Type" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs" />
                                            <asp:BoundField DataField="AMS_Company_Location" HeaderText="Company Location" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs" />
                                            <asp:BoundField DataField="AMS_Num_of_Assets" HeaderText="Number of Assets" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <asp:TemplateField HeaderText=" Total Cost" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" ForeColor="white" BackColor="Transparent" font-color="white" CssClass="fa fa-inr" Text='<%# Bind("AMS_Asset_Cost", " {0:0.00}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="AMS_Date_of_Purchase" HeaderText="Purchase Date" />
                                            <asp:BoundField DataField="AMS_Remarks" HeaderText="Remarks" HeaderStyle-CssClass="hidden-xs" ItemStyle-CssClass="hidden-xs" />
                                        </Columns>
                                        <PagerStyle CssClass="header" ForeColor="#FFFFFF" HorizontalAlign="Center" />
                                        <PagerSettings PageButtonCount="10" Visible="true" FirstPageText="<i class='fa fa-arrow-left'></i>" LastPageText="<i class='fa fa-arrow-right'></i>" Mode="NumericFirstLast" NextPageText="Next" PreviousPageText="Prev" />
                                    </asp:GridView>

                                    <asp:GridView ID="Othersgridview" runat="server" PageSize="5"
                                        CssClass="table table-bordered table-striped table-inverse nomargin" AutoGenerateColumns="True" OnRowDataBound="Othersgridview_RowDataBound" >
                                        <Columns>
                                            <%--<asp:BoundField DataField="AMS_AssetID" HeaderText="Asset ID" InsertVisible="False" ReadOnly="True" SortExpression="AMS_AssetID" />
                                            <asp:BoundField DataField="AMS_AssetType" HeaderText="Asset Type" ReadOnly="True" SortExpression="AMS_AssetType" />
                                            <asp:BoundField DataField="AMS_AssetName" HeaderText="Name" SortExpression="AMS_AssetName" />
                                            <asp:BoundField DataField="AMS_Company_Location" HeaderText="Location" SortExpression="AMS_Company_Location" />
                                            <asp:BoundField DataField="AMS_Remarks" HeaderText="Remarks" SortExpression="AMS_Remarks" />
                                            <asp:BoundField DataField="AMS_Date_of_Purchase" HeaderText="Purchased Date" SortExpression="AMS_Date_of_Purchase" />--%>
                                        </Columns>
                                        <PagerStyle CssClass="header" ForeColor="#FFFFFF" HorizontalAlign="Center" />
                                        <PagerSettings PageButtonCount="10" Visible="true" FirstPageText="<i class='fa fa-arrow-left'></i>" LastPageText="<i class='fa fa-arrow-right'></i>" Mode="NumericFirstLast" NextPageText="Next" PreviousPageText="Prev" />
                                    </asp:GridView>

                                    <%--<asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:SNPLDBSTRING %>" 
                                        SelectCommand="SELECT [AMS_AssetID], [AMS_AssetType], [AMS_AssetName], [AMS_Company_Location], [AMS_Remarks], [AMS_Date_of_Purchase] FROM [Asset_Master] WHERE (([AMS_AssetName] LIKE '%' + @AMS_AssetName + '%') AND ([AMS_Company_Location] LIKE '%' + @AMS_Company_Location + '%') AND ([AMS_AssetType] = @AMS_AssetType))">
                                        <SelectParameters>
                                            <asp:Parameter Name="AMS_AssetName" Type="String" />
                                            <asp:Parameter Name="AMS_Company_Location" Type="String" />
                                            <asp:ControlParameter ControlID="txtFindVendor" Name="AMS_AssetType" PropertyName="Text" Type="String" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>--%>

                                    <asp:SqlDataSource ID="SWSqlDataSource" runat="server"
                                        ConnectionString="<%$ ConnectionStrings:NEWTESTINGConnectionString %>"
                                        SelectCommand="SELECT * FROM [SoftwareAssetsView] 
                                        WHERE (([AMS_AssetID] LIKE '%' + @AMS_AssetID + '%') 
                                        OR ([AMS_AssetName] LIKE '%' + @AMS_AssetName + '%') 
                                        OR ([AMS_Company_Location] LIKE '%' + @AMS_Company_Location + '%') 
                                        OR ([AMS_Date_of_Purchase] LIKE '%' + @AMS_Date_of_Purchase + '%') 
                                        OR ([AMSSW_Category] LIKE '%' + @AMSSW_Category + '%') 
                                        OR ([AMSSW_LicensedType] LIKE '%' + @AMSSW_LicensedType + '%') 
                                        OR ([AMSSW_Vendor] LIKE '%' + @AMSSW_Vendor + '%') 
                                        OR ([AMSSW_Version] LIKE '%' + @AMSSW_Version + '%') 
                                        OR ([AMSSW_VendorMail] LIKE '%' + @AMSSW_VendorMail + '%'))">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="txtFindVendor" Name="AMS_AssetID" PropertyName="Text" Type="String" />
                                            <asp:ControlParameter ControlID="txtFindVendor" Name="AMS_AssetName" PropertyName="Text" Type="String" />
                                            <asp:ControlParameter ControlID="txtFindVendor" Name="AMS_Company_Location" PropertyName="Text" Type="String" />
                                            <asp:ControlParameter ControlID="txtFindVendor" Name="AMS_Date_of_Purchase" PropertyName="Text" Type="String" />
                                            <asp:ControlParameter ControlID="txtFindVendor" Name="AMSSW_Category" PropertyName="Text" Type="String" />
                                            <asp:ControlParameter ControlID="txtFindVendor" Name="AMSSW_LicensedType" PropertyName="Text" Type="string" />
                                            <asp:ControlParameter ControlID="txtFindVendor" Name="AMSSW_Vendor" PropertyName="Text" Type="string" />
                                            <asp:ControlParameter ControlID="txtFindVendor" Name="AMSSW_Version" PropertyName="Text" Type="string" />
                                            <asp:ControlParameter ControlID="txtFindVendor" Name="AMSSW_VendorMail" PropertyName="Text" Type="string" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>

                                    <asp:SqlDataSource ID="HWSqlDataSource" runat="server"
                                        ConnectionString="<%$ ConnectionStrings:NEWTESTINGConnectionString %>"
                                        SelectCommand="SELECT * FROM [HardwareAssetsView] 
                                        WHERE (([AMS_Asset_Cost] LIKE '%' + @AMS_Asset_Cost + '%') 
                                        OR ([AMS_AssetID] LIKE '%' + @AMS_AssetID + '%') 
                                        OR ([AMS_AssetName] LIKE '%' + @AMS_AssetName + '%') 
                                        OR ([AMS_Company_Location] LIKE '%' + @AMS_Company_Location + '%') 
                                        OR ([AMS_Date_of_Purchase] LIKE '%' + @AMS_Date_of_Purchase + '%') 
                                        OR ([AMSHW_PH_Characterstics] LIKE '%' + @AMSHW_PH_Characterstics + '%') 
                                        OR([AMSHW_PH_Make] LIKE '%' + @AMSHW_PH_Make + '%') 
                                        OR ([AMSHW_PH_Model] LIKE '%' + @AMSHW_PH_Model + '%') 
                                        OR ([HW_Serial_Number] LIKE '%' + @HW_Serial_Number + '%') 
                                        OR ([HW_Warrenty_Status] LIKE '%' + @HW_Warrenty_Status + '%'))">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="txtFindVendor" Name="AMS_Asset_Cost" PropertyName="Text" Type="String" />
                                            <asp:ControlParameter ControlID="txtFindVendor" Name="AMS_AssetID" PropertyName="Text" Type="String" />
                                            <asp:ControlParameter ControlID="txtFindVendor" Name="AMS_AssetName" PropertyName="Text" Type="String" />
                                            <asp:ControlParameter ControlID="txtFindVendor" Name="AMS_Company_Location" PropertyName="Text" Type="String" />
                                            <asp:ControlParameter ControlID="txtFindVendor" Name="AMS_Date_of_Purchase" PropertyName="Text" Type="string" />
                                            <asp:ControlParameter ControlID="txtFindVendor" Name="AMSHW_PH_Characterstics" PropertyName="Text" Type="string" />
                                            <asp:ControlParameter ControlID="txtFindVendor" Name="AMSHW_PH_Make" PropertyName="Text" Type="string" />
                                            <asp:ControlParameter ControlID="txtFindVendor" Name="AMSHW_PH_Model" PropertyName="Text" Type="string" />
                                            <asp:ControlParameter ControlID="txtFindVendor" Name="HW_Serial_Number" PropertyName="Text" Type="string" />
                                            <asp:ControlParameter ControlID="txtFindVendor" Name="HW_Warrenty_Status" PropertyName="Text" Type="String" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </div>
                            </div>
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

            $('#cphBody_Othersgridview').DataTable();
            $('#liReports').addClass("active");
            $('#liadminreports').addClass("active");

        });
    </script>
</asp:Content>
