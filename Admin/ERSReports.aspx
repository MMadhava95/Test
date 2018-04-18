<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true" CodeFile="ERSReports.aspx.cs" Inherits="Admin_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style>
        .dataTables_filter {
            float: right;
        }

        .table-responsive {
            overflow: unset;
        }

        #cphBody_gvList_paginate {
            float: right !important;
        }

        .auto-style1 {
            height: 64px;
        }

        .auto-style3 {
            width: 69px;
        }

        .group {
            position: relative;
            top: 0px;
            left: 0px;
            height: 75px;
        }

        .group1 {
            position: relative;
        }


        /* BOTTOM BARS ================================= */
        .bar {
            position: relative;
            display: block;
            width: 100px;
        }

            .bar:before, .bar:after {
                content: '';
                height: 2px;
                width: 0;
                bottom: 1px;
                position: absolute;
                background: #009688;
                transition: 0.2s ease all;
                -moz-transition: 0.2s ease all;
                -webkit-transition: 0.2s ease all;
            }

            .bar:before {
                left: 50%;
            }

        bar:after {
            right: 50%;
        }

        /* active state */
        .inputMaterial:focus ~ .bar:before, .inputMaterial:focus ~ .bar:after {
            width: 50%;
        }

        .inputMaterial {
            font-size: 18px;
            padding: 10px 10px 10px 5px;
            display: block;
            width: 300px;
            border: none;
            border-bottom: 1px solid #757575;
        }

            .inputMaterial:focus {
                outline: none;
            }

        /* LABEL ======================================= */

        label {
            color: black;
            font-size: 14px;
            font-weight: normal;
            position: absolute;
            pointer-events: none;
            left: 5px;
            top: 15px;
            transition: 0.2s ease all;
            -moz-transition: 0.2s ease all;
            -webkit-transition: 0.2s ease all;
        }

        /* active state */
        .inputMaterial:focus ~ label, .inputMaterial:valid ~ label {
            top: -20px;
            font-size: 14px;
            color: #009688;
        }


        /* active state */
        .inputMaterial:focus ~ .highlight {
            -webkit-animation: inputHighlighter 0.3s ease;
            -moz-animation: inputHighlighter 0.3s ease;
            animation: inputHighlighter 0.3s ease;
        }

        /* ANIMATIONS ================ */
        @-webkit-keyframes inputHighlighter {
            from {
                background: #5264AE;
            }

            to {
                width: 0;
                background: transparent;
            }
        }

        @-moz-keyframes inputHighlighter {
            from {
                background: #5264AE;
            }

            to {
                width: 0;
                background: transparent;
            }
        }

        @keyframes inputHighlighter {
            from {
                background: #5264AE;
            }

            to {
                width: 0;
                background: transparent;
            }
        }





        .vl {
            border-left: 6px solid green;
            height: 500px;
        }
    </style>
    <script src="http://code.jquery.com/jquery-1.8.2.js"></script>
    <script src="http://www.google.com/jsapi" type="text/javascript"></script>
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript"> 
        google.load('visualization', '1', { packages: ['corechart'] });
    </script>
    <script type="text/javascript"> 
        $(function () {
            $.ajax({
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                url: 'ERSReports.aspx/GetChartData',
                data: '{}',
                success:
                function (response) {
                    drawchart(response.d);
                },

                error: function () {
                    alert("Error loading data!");
                }
            });
        })
        $(function () {
            $.ajax({
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                url: 'ERSReports.aspx/GetChartData2',
                data: '{}',
                success:
                function (response) {

                    drawchart2(response.d);

                },

                error: function () {
                    alert("Error loading data!");
                }
            });
        })
        function drawchart(dataValues) {
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Column Name');
            data.addColumn('number', 'Column Value');
            for (var i = 0; i < dataValues.length; i++) {
                data.addRow([dataValues[i].ClaimType, dataValues[i].Total]);
            }
            new google.visualization.PieChart(document.getElementById('claimTypeChart')).
                draw(data, { title: "Based on cliam type" });
        }
        function drawchart2(dataValues) {
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Column Name');
            data.addColumn('number', 'Column Value');
            for (var i = 0; i < dataValues.length; i++) {
                data.addRow([dataValues[i].StatusType, dataValues[i].StatusTotal]);
            }
            new google.visualization.PieChart(document.getElementById('claimStatusChart')).
                draw(data, { title: "Based on cliam status" });
        }
        function checkDate(sender, args) {
            if (sender._selectedDate < new Date()) {
                alert("You cannot select a day earlier than today!");
                sender._selectedDate = new Date();
                // set the date back to the current date
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }
    </script>
    <script type="text/javascript">  
        window.onload = function () {
            var seconds = 6;
            setTimeout(function () {
                document.getElementById("<%=alert.ClientID %>").style.display = "none";
             }, seconds * 1000);
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="mainpanel">

        <div class="contentpanel">
			<ol class="breadcrumb breadcrumb-quirk">
                <li><a href="AdminViewClaims.aspx"><i class="fa fa-home mr5"></i>Home</a></li>
                <li class="active">Reports </li>
            </ol>
            <div class="container a" style="padding-right: 100px;" id="alert" visible="true" runat="server">


<div class="alert alert-success alert-dismissable" id="alertmod" runat="server">
<a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
<strong>
<asp:Label ID="Label5" runat="server" Text="" Font-Size="Medium" ></asp:Label></strong>
<asp:Label ID="Label10" runat="server" Visible="true" Text="" Font-Size="Medium"></asp:Label>
</div>
</div>
            <div class="row">
                <div class="col-sm-12">

                    <div class="panel">
                        <div class="panel-heading">
                            <h4 class="panel-title">Claim Reports  </h4>


                            <div class="panel-body">
                                <div class="form mb20">
                                    <div class="col-md-3">
                                        <div class="floatings-labels">
                                            <asp:DropDownList ID="DropDownList1" CssClass="floating-select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged1">
                                                <asp:ListItem Value="pieChartReport">Pie Chart </asp:ListItem>
                                                <asp:ListItem Value="employeeName">Employee Name</asp:ListItem>
                                                <asp:ListItem Value="claimDate">Claim Date</asp:ListItem>
                                                <asp:ListItem Value="status">Claim Status</asp:ListItem>

                                            </asp:DropDownList>

                                            <label class="lbltop" for="txtMotherLang">Report Using  </label>
                                        </div>
                                    </div>

                                    <%--<div class="col-md-3">
                                    <div class="floatings-labels">


                                        <asp:TextBox ID="TextBox2" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                        <label class="lbltop1 lbltop" for="nMobileNumber2">Employee Name     </label>
                                    </div>
                                </div>
                                     <div class="col-md-3">
                                    <div class="floatings-labels">


                                        <asp:TextBox ID="txtBillAmount" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                        <label class="lbltop1 lbltop" for="nMobileNumber2">From Date   </label>
                                    </div>
                                </div>
                                    
                                    
                                     <div class="col-md-3">
                                    <div class="floatings-labels">


                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="floating-input" placeholder=" "></asp:TextBox>
                                        <label class="lbltop1 lbltop" for="nMobileNumber2">To Date   </label>
                                    </div>
                                </div> 

                                      <div class="col-md-3">
                                    <div class="floatings-labels">
                                        <asp:DropDownList ID="DropDownList3" CssClass="floating-select" runat="server" value="" onclick="this.setAttribute('value', this.value);">
                                            <asp:ListItem Text=""> </asp:ListItem>
                                            <asp:ListItem Text="Employee Name"> </asp:ListItem>
                                            <asp:ListItem Text="Claim Date"> </asp:ListItem>
                                            <asp:ListItem Text="Claim Status"> </asp:ListItem> 

                                        </asp:DropDownList>

                                        <label class="lbltop" for="txtMotherLang">  Status    </label>
                                    </div>
                                </div>--%>

                                    <%-- <div class="col-md-3">

                                              <asp:LinkButton ID="LinkButton1" runat="server"   CssClass="btn btn-success btn-block"  style="margin-top: 15px;"> <i class="fa fa-search"></i> Generate Report</asp:LinkButton>
                                              
                                          </div>
                                    
                                          <div class="col-md-3">
                                                <asp:LinkButton ID="LinkButton2" runat="server"   CssClass="btn btn-success btn-block"  style="margin-top: 15px;"> <i class="fa fa-file-excel-o"></i> Excel Report</asp:LinkButton>
                                               
                                          </div>--%>
                                    <div class="clearfix"></div>


                                </div>



                            </div>
                        </div>
                        <!-- panel -->


                    </div>
                    <!-- col-sm-6 -->

                </div>
                <!-- row -->

            </div>
             <div class="row">
                <div class="col-sm-12">

                    <div class="panel">
                        <div class="panel-heading">
 
                    <h4 class="panel-title"> Claim Reports  </h4>


                            <div class="panel-body">
                               
                                <asp:Panel ID="pieChartPanel" runat="server">
                                    <%--<h4 class="card-title" style="font-size:medium">reports  </h4>--%>
                                    <table>
                                        <tr>
                                            <td style="border: thick; border-color: #5cd65c">
                                                <div id="claimTypeChart" style="width: 500px; height: 300px;"></div>
                                            </td>
                                            <td>
                                                <div id="claimStatusChart" style="width: 500px; height: 300px;"></div>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="employeePanel" runat="server" Width="100px" BackColor="white">

                                    <table style="background-color: white">
                                        <tr>
                                            <td class="auto-style1"><span class="highlight"></span><span class="bar "></span>
                                                <asp:Label ID="Label7" runat="server" Text="Employee Name:" ForeColor="Black" Font-Size="Medium"></asp:Label></td>
                                            <td>
                                                <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="SqlDataSource1" DataTextField="EmployeeName" DataValueField="EmployeeName"></asp:DropDownList>
                                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SNPLDBSTRING %>" SelectCommand="SELECT [EmployeeName] FROM [Employee]"></asp:SqlDataSource>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnEmployeeGenerateReports" runat="server" Text="Generate Reports" CssClass="btn btn-success btn-block" OnClick="btnEmployeeGenerateReports_Click" Width="169px" /></td>

                                            <td style="padding-left: 20px" class="auto-style3">
                                                <asp:Button ID="btnExportExcel" runat="server" Text="Export to Excel sheet" OnClick="btnExportExcel_Click" CssClass="btn btn-success btn-block" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>

 <asp:Panel ID="claimDatePanel" runat="server" BackColor="Transparent">
                <%--<h4 class="card-title" style="font-size:medium">Reports based on Claim date</h4>--%>
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <table style="background-color:white">

                    <tr>
                        
                         <%--<td class="group"><span class="highlight"></span><span class="bar "></span>
                            <asp:TextBox ID="BillAmount" runat="server" align="center" AutoComplete="off" class="ggg inputText inputMaterial " ForeColor="black" required="" style="background: transparent; border: none; border-bottom: 1px solid #000000; font-family: 'Times New Roman'" ToolTip="Login ID" type="text" width="230px"></asp:TextBox>
                            <label style="font-family:Arial;color:black">
                            Bill Amount</label><asp:Label ID="Label9" runat="server" ForeColor="Black" Text="Your eligibility is:"></asp:Label><asp:Label ID="Label3" ForeColor="Black" runat="server" Text=" "></asp:Label>
                             
                        </td>--%>
                        <td class="group" style="width:250px"><span class="highlight"></span><span class="bar "></span>
                            <asp:TextBox ID="TextBox1" runat="server" align="center" AutoComplete="off" class="ggg inputText inputMaterial " Visible="true" BackColor="Transparent" ForeColor="black" required="" Width="200px" Font-Size="Small"></asp:TextBox>
                            <label style="color:black">
                            From Date</label>
                            <asp:CalendarExtender ID="CalendarExtender1"
                                runat="server" TargetControlID="TextBox1" Format="yyyy/MM/dd" />
                          
                        </td>
                        
                            
                             <td class="group"><span class="highlight"></span><span class="bar "></span>
                                 <asp:TextBox ID="TextBox3" runat="server" Font-Size="Small" align="center" AutoComplete="off" BackColor="Transparent" class="ggg inputText inputMaterial " ForeColor="black" required="" Visible="true" Width="200px"></asp:TextBox>
                                 <label style="color:black">
                                 To Date</label>
                                 <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="yyyy/MM/dd" TargetControlID="TextBox3" />
                             </td>
                             <td>
                               <%--  <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="TextBox1" ControlToValidate="TextBox3" ErrorMessage="ToDate should be greater than FromDate" ForeColor="Red" Operator="GreaterThanEqual" Text=" ToDate should be After FromDate" Type="Date"></asp:CompareValidator>--%>
								 <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="TextBox3" ControlToCompare="Textbox1" Operator="GreaterThanEqual" Text="Todate cannot be lessthan fromdate" ForeColor="Red"></asp:CompareValidator>
								 </td>
                       
                    </tr>

                    

                   
                    

                    <tr>
                        <td>
                            <asp:Button ID="btnclaimDateGenerateReports" runat="server" Text="Generate Reports" OnClick="btnclaimDateGenerateReports_Click" CssClass="btn btn-success btn-block" Width="230px" />
                        </td>
                        
                        <td style="padding-left:20px">
                            
                            <asp:Button ID="Button1" runat="server" Text="Export to Excel sheet" CssClass="btn btn-success btn-block" OnClick="btnExportExcel_Click"  />

                        </td>
                    </tr>
                </table>
                <br /><br />
            </asp:Panel>

            <asp:Panel ID="claimStatusPanel" runat="server">
                <%--<h4 class="card-title" style="font-size:medium">Reports based on Claim status </h4>--%>
                <div class="dropdown">
                    <asp:Label ID="ClaimStatusLabel" runat="server" Text="Claim Status:" Visible="true" Font-Size="Medium"></asp:Label>
                    &nbsp;
                    <asp:DropDownList ID="ClaimStatusDropDown" runat="server" Visible="true" BackColor="Transparent" ForeColor="black" Height="22px" Width="117px" >
                        <asp:ListItem>select</asp:ListItem>
                        <asp:ListItem>Pending</asp:ListItem>
                        <asp:ListItem>Rejected</asp:ListItem>
                        <asp:ListItem>Approved</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ClaimStatusDropDown" InitialValue="select" runat="server" ErrorMessage=" Select Claim Status" ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
                <br />
                <br />
                <div class="row">
                <div class="col-sm-3">
                <asp:Button ID="btnStatusGenerateReport" runat="server" Text="Generate Reports" OnClick="btnStatusGenerateReport_Click" CssClass="btn btn-success btn-block" />
                </div>
                     <div class="col-sm-3">
                    <asp:Button ID="Button2" runat="server" Text="Export to Excel sheet" CssClass="btn btn-success btn-block" OnClick="btnExportExcel_Click"  />
              </div>
                         </div>
                    <br /><br />
            </asp:Panel>
    
                            </div>
                        </div>
                        <!-- panel -->
						<div class="table-responsive">
                        <asp:GridView ID="GridClaimData" runat="server" ForeColor="Black"  CssClass="table table-bordered table-striped    table-inverse nomargin" 
                                    AutoGenerateColumns="False"  EmptyDataText="No records found">
                                       <Columns>
                                          <asp:BoundField DataField="ERSClaimID" HeaderText="ClaimID" SortExpression="ERSClaimID" />
                                          <asp:BoundField DataField="ERSApproverName" HeaderText="Approvername" SortExpression="ERSApproverName" />
                                          <asp:BoundField DataField="EmployeeName" HeaderText="EmployeeName" SortExpression="ERSEmployeeName" />
                                          <asp:BoundField DataField="ERSClaimType" HeaderText="ClaimType" SortExpression="ERSClaimType" />
                                          <asp:BoundField DataField="ERSBillAmount" HeaderText="Amount" SortExpression="ERSBillAmount"/>
                                          <asp:BoundField DataField="ERSClaimStatus" HeaderText="status" SortExpression="ERSClaimStatus" />
                                      </Columns>
                                    </asp:GridView>
                        </div>
                    </div>
                    <!-- col-sm-6 -->

                </div>
                
               

              <%--  <div class="row">
                <div  class="col-sm-12">
                     <div class="panel">
                        <div class="panel-heading">
 
                    <h4 class="panel-title"> Claim Reports  </h4>


                            <div class="panel-body">
                   
<asp:GridView ID="GridClaimData" runat="server"  AutoGenerateColumns="False" 
                            EmptyDataText="No records found">
                                       
                                    </asp:GridView>

                </div>
                 

            </div>
            
      
                 </div>
            </div>
    
     </div>--%>
                 </div>
            </div>
        </div>
    


    
   

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="server">
     <script>
		 $(document).ready(function () {
			 $("#liReports").addClass("active");
			 $("#liers").addClass("active");
		 });
   </script>
	 <script>
		 $(document).ready(function () {


             $('#cphBody_GridClaimData').DataTable();

		 });
    </script>
</asp:Content>


