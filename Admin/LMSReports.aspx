<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true" CodeFile="LMSReports.aspx.cs" Inherits="Admin_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <div class="mainpanel">
        <div class="contentpanel">
            <ol class="breadcrumb breadcrumb-quirk">
                <li><a href="ViewCourses.aspx"><i class="fa fa-home mr5"></i>Home</a></li>
                <li class="active">Report</li>
            </ol>
            <div class="row">
                <div class="col-sm-12">
                    <div class="panel">
                        <div class="panel-heading">
                            <h4 class="panel-title" style="text-align: center">Report for No.of Employees Registered for Each Course </h4>
                            <div class="panel-body">
                                <div id="chart" style="width: 100%; height: 50%;"></div>
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
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" Runat="Server">
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript" src="https://www.google.com/jsapi"></script>
<script type="text/javascript">
    google.load("visualization", "1", { packages: ["corechart"] });
    google.setOnLoadCallback(drawChart);
    function drawChart() {
        var options = {
            width: 600,
            height: 400,
            bar: { groupWidth: "95%" },
            legend: { position: "none" },
            isStacked: true
        };
        $.ajax({
            type: "POST",
            url: "LMSReports.aspx/GetChartData",
            data: '{}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                var data = google.visualization.arrayToDataTable(r.d);
                var chart = new google.visualization.BarChart($("#chart")[0]);
                chart.draw(data, options);
            },
            failure: function (r) {
                alert(r.d);
            },
            error: function (r) {
                alert(r.d);
            }
        });
    }
</script>

</asp:Content>

