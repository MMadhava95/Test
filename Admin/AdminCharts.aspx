<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true" CodeFile="AdminCharts.aspx.cs" Inherits="Admin_Default" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
   <script src="http://code.jquery.com/jquery-1.8.2.js"></script> 
<script src="http://www.google.com/jsapi" type="text/javascript"></script> 
<script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    
    
<script type="text/javascript"> 
    google.load('visualization', '1', { packages: ['corechart'] }); 
</script> 
    <script type="text/javascript"> 
        $(function () {
            $.ajax({
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                url: '../Admin/AdminCharts.aspx/GetTicketStatusData',
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
                url: '../Admin/AdminCharts.aspx/GetTicketPriorityData',
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

        $(function () {
            $.ajax({
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                url: '../Admin/AdminCharts.aspx/GetTicketLocationData',
                data: '{}',
                success:
                function (response) {
                    drawLocationChart(response.d);
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
                url: '../Admin/AdminCharts.aspx/GetTicketDepartmentData',
                data: '{}',
                success:
                function (response) {
                    drawDepartmentChart(response.d);
                },

                error: function () {
                    alert("Error loading data!");
                }
            });
        })
        //status chart
        function drawchart(dataValues) {
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Column Name');
            data.addColumn('number', 'Column Value');
            for (var i = 0; i < dataValues.length; i++) {
                data.addRow([dataValues[i].StatusType, dataValues[i].StatusTotal]);
            }
            new google.visualization.PieChart(document.getElementById('TicketStatusChart')).
                draw(data, { title: "Based on ticket status" });
        }

        //priority chart
        function drawchart2(dataValues) {
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Column Name');
            data.addColumn('number', 'Column Value');
            for (var i = 0; i < dataValues.length; i++) {
                data.addRow([dataValues[i].PriorityType, dataValues[i].PriorityTotal]);
            }
            new google.visualization.BarChart(document.getElementById('TicketPriorityChart')).
                draw(data, { title: "Based on Priority SLACrossed Tickets" });
        }

        //Location chart
        function drawLocationChart(dataValues) {
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Column Name');
            data.addColumn('number', 'Column Value');
            for (var i = 0; i < dataValues.length; i++) {
                data.addRow([dataValues[i].LocationName, dataValues[i].LocationTotal]);
            }
            new google.visualization.AreaChart(document.getElementById('TicketLocationChart')).
                draw(data, { title: "Based on tickets raised in Location" });
        }

        //Department chart
        function drawDepartmentChart(dataValues) {
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Column Name');
            data.addColumn('number', 'Column Value');
            for (var i = 0; i < dataValues.length; i++) {
                data.addRow([dataValues[i].DepartmentName, dataValues[i].DepartmentTotal]);
            }
            new google.visualization.LineChart(document.getElementById('TicketDepartmentChart')).
                draw(data, { title: "Based on tickets raised in Department" });
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">

    <div class="mainpanel">
        <div class="contentpanel">
            <ol class="breadcrumb breadcrumb-quirk">
                <li><a href="index.html"><i class="fa fa-home mr5"></i>Home</a></li>
                <li class="active">  Charts     </li>
            </ol>
            <div class="row">
                <div class="col-sm-6">
                    <div class="panel">
                        <div class="panel-heading">
                            <h4 class="panel-title">Ticket Status Chart  </h4>
                            <div class="panel-body">
                              <div id="TicketStatusChart" style="width: 500px; height: 300px;"></div>
                            </div>
                        </div>
                    </div>
                </div>
                    <div class="col-sm-6">
                    <div class="panel">
                        <div class="panel-heading">
                            <h4 class="panel-title">Ticket Priority </h4>
                            <div class="panel-body">
                               <div id="TicketPriorityChart" style="width: 500px; height: 300px;"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            
            <div class="row">
                <div class="col-sm-6">
                    <div class="panel">
                        <div class="panel-heading">
                            <h4 class="panel-title">Ticket Location </h4>
                            <div class="panel-body">
                               <div id="TicketLocationChart" style="width: 500px; height: 300px;"></div>
                            </div>
                        </div>
                    </div>
                </div>
                    <div class="col-sm-6">
                    <div class="panel">
                        <div class="panel-heading">
                            <h4 class="panel-title"> Ticket Departments Chart </h4>
                            <div class="panel-body">
                               <div id="TicketDepartmentChart" style="width: 500px; height: 300px;"></div>
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


            $('#cphBody_gvList').DataTable();

        });
    </script>
</asp:Content>


