<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="ersApprover.aspx.cs" Inherits="Admin_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ers3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphhead" runat="server">
    <style>
        .dataTables_filter {
        float: right;}
        .table-responsive {
        overflow: unset;}
        #cphBody_GridView1_paginate {
        float:right !important;}
                .modalBackground {
background-color:gray;
filter: alpha(opacity=100);
opacity: 0.9;
}
    </style>
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
                <li class="active">  Approver Data </li>
            </ol>
      <div class="container" id="alert" visible="true" runat="server">


        <div class="alert alert-success alert-dismissable" id="alertmod" runat="server">
            <a href="#" class="close" data-dismiss="alert" aria-label="close" style="align-content:flex-end">×</a>
            <strong>

                <asp:Label ID="Label1" runat="server" Text="" style="font-size:medium;"  ></asp:Label></strong>
            <asp:Label ID="Label5" runat="server"  Visible="true" Text="" style="font-size:medium;" ></asp:Label>
        </div>
    </div>
       <div class="container " style="padding-right: 100px;background-color:#d7ecc6" id="Div1"  visible="true" runat="server">


        <div class="alert alert-success alert-dismissable" style=" background-color:#d7ecc6" id="Div2" runat="server">
            <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
            <strong>

                <asp:Label ID="Label4" runat="server" Text="Do You Want To Delete?" ForeColor="black" ></asp:Label></strong>

            <asp:Button ID="yes" runat="server" Text="yes" backcolor="#1aff1a" Width="40px" Font-Size="Medium" ForeColor="white" OnClick="yes_Click1"/>
            <asp:Button ID="no" runat="server" Text="no"  BackColor="#ff5c33" Width="40px" Font-Size="Medium"  ForeColor="White" OnClick="no_Click1"  />
            <asp:Label ID="Label6" runat="server" Text="Label" Visible="false"></asp:Label>
            </div>
           </div>
        <br />
      <div class="row">
        <div class="col-sm-12">
            <div class="panel">
                <div class="panel-heading">
                    <h4 class="panel-title">  Approver Data </h4>
                <div class="panel-body">
                  <div class="form mb20"> </div>
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ERSApproverID" OnRowCommand="GridView1_RowCommand"
                        CssClass="table table-bordered table-striped    table-inverse nomargin"  OnRowDataBound="GridView1_RowDataBound1" >
                        <Columns>
                           <asp:BoundField DataField="ERSApproverID" HeaderText="ApproverID" ReadOnly="True" SortExpression="ERSApproverID" />
                        <asp:BoundField DataField="ERSApproverName" HeaderText=" Name" SortExpression="ERSApproverName" />
                        <asp:BoundField DataField="ERSApproverMailID" HeaderText="MailID" SortExpression="ERSApproverMailID" />
                        <asp:BoundField DataField="ERSApproverRole" HeaderText="Designation" SortExpression="ERSApproverRole" />
                        <asp:BoundField DataField="ERSApproverLevel" HeaderText=" Level" SortExpression="ERSApproverLevel" />
                             <asp:ButtonField ButtonType="Link" Text="<i class='fa fa-edit'></i>" ControlStyle-CssClass="btn btn-primary btn-stroke btn-icon" HeaderText="Action" HeaderStyle-CssClass="hidden-sm" ItemStyle-CssClass="hidden-sm" CommandName="Edit_Click" />
							  <asp:ButtonField ButtonType="Link" Text="<i class='fa fa-trash-o'></i>" ControlStyle-CssClass="btn btn-danger btn-stroke btn-icon" HeaderText="Action" HeaderStyle-CssClass="hidden-sm" ItemStyle-CssClass="hidden-sm" CommandName="Delete_Click" />
                        </Columns>

                    </asp:GridView> 
                         <asp:LinkButton ID="LinkButton1" runat="server"></asp:LinkButton>
                          <asp:LinkButton ID="btnClose" runat="server"></asp:LinkButton>
                    </div>

                    
                </div>
            </div><!-- panel -->


        </div><!-- col-sm-6 -->

      </div><!-- row -->

    </div><!-- contentpanel -->
  </div><!-- mainpanel -->

      </div>
 
    <asp:Panel ID="updatePanel" runat="server">
        <div class="modal-dialog  modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="panel panel-profile list-view" style="left: 0px; top: 0px">
                        <div class="panel-heading">
                            <div class="media">

                                <div class="media-body">
                                    <h4 class="media-heading">  Employee  Details</h4>
                                </div>
                            </div>
                             <ul class="panel-options">
                                  <li><a href="" class="close" data-dismiss="modal" aria-hidden="true"><i class="fa fa-times"></i></a></li>
                            </ul>
                            <!-- media -->
                            
                        </div>
                        <!-- panel-heading -->

                        <div class="panel-body people-info">
                         <div class="row mb20">

                             <div class="col-md-6">

                                 <div class="well mb20">
                                     <div class="form-group">
                                        
                                <asp:Label ID="Aid"  runat="server" Text="ApproverID "  class="col-sm-4 control-label"  Font-Bold="true" ></asp:Label>
                            
                                <asp:Label ID="approverid"  runat="server"  CssClass="col-sm-8 control-label "></asp:Label>
                                  </div>
                                      <div class="form-group">                                                         
                                <asp:Label ID="Aname" runat="server" Text="Name" class="col-sm-4 control-label"  Font-Bold="true" ></asp:Label>
                               <asp:Label ID="Approvername" runat="server" CssClass="col-sm-8 control-label " ></asp:Label>
                                      </div>
                                      <div class="form-group">
                                      
                                <asp:Label ID="Amailid" runat="server" Text="MailID  " class="col-sm-4 control-label"  Font-Bold="true" ></asp:Label>
                          
                                <asp:Label ID="Approvermailid" runat="server"  CssClass="col-sm-8 control-label"></asp:Label>
                 
                                          </div>
                                   
                                      


                                     <div class="clearfix mb20"></div>
                                 </div>
                   



                             </div>
                             <div class="col-md-6">
                                 
                                     <div class="col-md-12">
                                    <div class="floatings-labels">
                                        <asp:Label ID="Alevel" runat="server"  Text="Level"  Style="padding-right:50px"  Font-Bold="true"  ></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="Approverlevel" runat="server"  ReadOnly="false"  ></asp:TextBox>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Approverlevel" Text="*" ForeColor="Red" ValidationGroup="two"  ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
                            
                                        
                                      
                                    </div>
                                </div>

                                 
                                     <div class="col-md-12">
                                    <div class="floatings-labels">
                                         <asp:Label ID="Arole" runat="server"  Text="Designation"  Style="padding-right:50px"  Font-Bold="true" ></asp:Label>
                                <asp:TextBox ID="ApproverRole"  runat="server" required="" ReadOnly="false"    ></asp:TextBox>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ApproverRole" Text="*" ForeColor="Red" runat="server" ValidationGroup="two" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
                           
                                       
                                    </div>
                                </div>

 
                                  <div class="clearfix"></div>
                                    <div class="col-sm-6 pull-right">
                                         <asp:Button ID="update" runat="server" class="btn btn-success btn-block" style="margin-top: 15px;" ValidationGroup="two" Text="Update" OnClick="update_Click"  />
                                   
                                </div>
                                        
                                       

                             </div>

                         </div>


                        </div>
                    </div>

                </div>
            </div>
        </div>
    </asp:Panel>
     <ers3:modalpopupextender ID="MPE" runat="server" PopupControlID="updatePanel" TargetControlID="LinkButton1" CancelControlID="btnclose"
        BackgroundCssClass="modalBackground">
   </ers3:modalpopupextender>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="server">
	<script>
		$(document).ready(function () {


			$('#cphBody_GridView1').DataTable();

		});
</script>
      <script>
$(document).ready(function() {
    
    $('#cphBody_gvList').DataTable();
          $("#liadminexpenses").addClass("active");
          $("#liadminersapprover").addClass("active");
     
});
</script>
</asp:Content>

