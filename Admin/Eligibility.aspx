<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true" CodeFile="Eligibility.aspx.cs" Inherits="Admin_Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ers3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
    <style>
        .modalBackground {
background-color:gray;
filter: alpha(opacity=100);
opacity: 0.9;
}
        .auto-style3 {
            width: 224px;
        }
        .auto-style4 {
            width: 253px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <div class="mainpanel">
        <div class="contentpanel">
		<ol class="breadcrumb breadcrumb-quirk">
                <li><a href="AdminViewClaims.aspx"><i class="fa fa-home mr5"></i>Home</a></li>
                <li class="active">Eligibility Criteria</li>
            </ol>
            <div class="row">
                     <div class="container a" id="alert" visible="true" runat="server">


<div class="alert alert-success alert-dismissable" id="alertmod" runat="server">
<a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
<strong>
<asp:Label ID="Label5" runat="server" Text="" ></asp:Label></strong>
<asp:Label ID="Label6" runat="server" Visible="true" Text=""></asp:Label>
</div>
</div>
                <br />
     <div class="col-sm-12">

                    <div class="panel">
                        <div class="panel-heading">

                            <h4 class="panel-title">Eligibility </h4>

                        </div>
                        <div class="panel-body">
                           
                            
                                <div class="form mb20"></div>
									 <div class="table-responsive">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" OnRowCommand="GridView1_RowCommand"  CssClass="table table-bordered table-striped    table-inverse nomargin" >
<Columns>
    <asp:BoundField DataField="Roles" HeaderText="Role" InsertVisible="False" ReadOnly="True"  />
                        <asp:BoundField DataField="Medical" HeaderText="Medical" />
                        <asp:BoundField DataField="TravelOutStation" HeaderText="Travel OutStation"  />
                        <asp:BoundField DataField="MiscellaneousAndEntertainmentExpenses" HeaderText="Miscellaneous And Entertainment Expenses"  />
                        <asp:BoundField DataField="RepairsAndMaintenance" HeaderText="Repairs And Maintenance" />
                         <asp:BoundField DataField="LocalTravel" HeaderText="Local Travel"  >
                  </asp:BoundField>
               
                 <asp:ButtonField ButtonType="Link" Text="<i class='fa fa-edit'></i>" ControlStyle-CssClass="btn btn-primary btn-stroke btn-icon" HeaderText="Action" HeaderStyle-CssClass="hidden-sm" ItemStyle-CssClass="hidden-sm" CommandName="Edit_Click" />
</Columns>
                                    </asp:GridView>
                                    <asp:LinkButton ID="LinkButton1" runat="server"></asp:LinkButton>
									 <asp:LinkButton ID="btnClose" runat="server"></asp:LinkButton>
                                    <div class="clearfix"></div>
										 

                               </div>



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
        <!-- contentpanel -->
    </div>
    <!-- mainpanel -->
    
    
    <asp:Panel ID="updatePanel" runat="server">
        <div class="modal-dialog  modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="panel panel-profile list-view" style="left: 0px; top: 0px">
                        <div class="panel-heading">
                            <div class="media">

                                <div class="media-body">
                                    <h4 class="media-heading">Eligibility Update</h4>
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
                                 <div class="well mb20">
                         
                                     <div class="clearfix mb20">
                                        
                                         <table class="form-group clearfix mb20" style="background:#D8DCE3"  >
                                             <tr>
                                                 <td class="auto-style4" style="padding:5px">
                                        
                                <asp:Label ID="desig"  runat="server" Text="Designation "  class="col-sm-4 control-label"  Font-Bold="true" ></asp:Label>
                            
                                                 </td>
                                                 <td style="padding:5px">
                                         <asp:Label ID="designation" runat="server" CssClass="col-sm-8 control-label "></asp:Label>
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td class="auto-style4" style="padding:5px" >
                                                                
                                <asp:Label ID="med" runat="server" Text="Medical" class="col-sm-4 control-label"  Font-Bold="true" ></asp:Label>
                            
                                                 </td>
                                                 <td style="padding:5px">
                                          <asp:TextBox ID="medical" runat="server" Height="24px" Width="148px"></asp:TextBox>
                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="" ControlToValidate="medical" ValidationGroup="one" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                          &nbsp;
                                          <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="medical" ErrorMessage="RegularExpressionValidator" ForeColor="Red" Text="enter valid amount" EnableClientScript="true" Enabled="true"  ValidationExpression="\d+" ValidationGroup="one"></asp:RegularExpressionValidator>
               
                       
                                          &nbsp;
                                          <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server"  EnableClientScript="true" Enabled="true" ControlToValidate="medical" ErrorMessage="RegularExpressionValidator" ForeColor="Red" Text="  should not be zero" ValidationExpression="^(0*[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*)$" ValidationGroup="one"></asp:RegularExpressionValidator>
               
                       
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td class="auto-style4" style="padding:5px" >
                                      
                                <asp:Label ID="Amailid" runat="server" Text="Travel Outstation" class="col-sm-4 control-label"  Font-Bold="true" ></asp:Label>
                          
                                                 </td>
                                                 <td style="padding:5px">
                                          <asp:TextBox ID="travelout" runat="server" Height="24px" Width="148px" ></asp:TextBox>
                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="RequiredFieldValidator"  EnableClientScript="true" Enabled="true" ControlToValidate="travelout" ValidationGroup="one" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                     &nbsp;
                                         <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="travelout" ValidationExpression="\d+" ForeColor="Red" Text="enter valid amount" ErrorMessage="RegularExpressionValidator" ValidationGroup="one"></asp:RegularExpressionValidator>
                                          &nbsp;
                                         <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="travelout" ValidationExpression="^(0*[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*)$" ForeColor="Red" Text="Amount should not be zero" ErrorMessage="RegularExpressionValidator" ValidationGroup="one"></asp:RegularExpressionValidator>
				
                
              
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td class="auto-style4" style="padding:5px" >
                                          <asp:Label ID="emailid" runat="server" Text="Miscellaneous And Entertainment  "  Font-Bold="true"></asp:Label>
			                                     </td>
                                                 <td style="padding:5px">
			                               <asp:TextBox ID="misc" runat="server" required="" Height="24px" Width="148px" ></asp:TextBox>
                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"  EnableClientScript="true" Enabled="true" ErrorMessage="RequiredFieldValidator" ControlToValidate="misc" ValidationGroup="one" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                     &nbsp;&nbsp;
                                           <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="misc"  EnableClientScript="true" Enabled="true" ErrorMessage="RegularExpressionValidator" ForeColor="Red" Text="enter valid amount" ValidationExpression="\d+" ValidationGroup="one"></asp:RegularExpressionValidator>
&nbsp;
                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"  EnableClientScript="true" Enabled="true" ControlToValidate="misc" ErrorMessage="RegularExpressionValidator" ForeColor="Red" Text="Amount should not be zero" ValidationExpression="^(0*[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*)$" ValidationGroup="one"></asp:RegularExpressionValidator>
				                                 </td>
                                             </tr>
                                             <tr>
                                                 <td class="auto-style4" style="padding:5px" >
				<asp:Label ID="edesignation" runat="server" Text="Repairs And Maintenance " Font-Bold="true" ></asp:Label>
			                                     </td>
                                                 <td style="padding:5px">
			<asp:TextBox ID="randm"   runat="server" required="" ValidationGroup="one" OnTextChanged="randm_TextChanged"></asp:TextBox>
                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="RequiredFieldValidator" ValidationGroup="one" Text="*"  EnableClientScript="true" Enabled="true" ForeColor="Red" ControlToValidate="randm"></asp:RequiredFieldValidator>
                    &nbsp;
                   
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="one" runat="server" ControlToValidate="randm" ValidationExpression="\d+" ForeColor="Red" Text="enter valid amount"  EnableClientScript="true" Enabled="true" ErrorMessage="RegularExpressionValidator"></asp:RegularExpressionValidator>
                                         &nbsp;
                                          <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server"  EnableClientScript="true" Enabled="true" ControlToValidate="randm" ErrorMessage="RegularExpressionValidator" ForeColor="Red" Text=" Amount should not be zero" ValidationExpression="^(0*[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*)$" ValidationGroup="one"></asp:RegularExpressionValidator>
				
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td class="auto-style4" style="padding:5px" >
				<asp:Label ID="edepartment" runat="server" Text="Local Travel " Font-Bold="true" ></asp:Label>
				                                 </td>
                                                 <td style="padding:5px">
                                         <asp:TextBox ID="local" runat="server"  required="" ValidationGroup="one"></asp:TextBox>
                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="RequiredFieldValidator" ValidationGroup="one" Text="*" ForeColor="Red"  EnableClientScript="true" Enabled="true" ControlToValidate="local"></asp:RequiredFieldValidator>
                                         &nbsp;
                                         <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="local" ErrorMessage="RegularExpressionValidator" ForeColor="Red" Text="enter valid amount" ValidationExpression="\d+"  EnableClientScript="true" Enabled="true" ValidationGroup="one"></asp:RegularExpressionValidator>
                                         &nbsp;
                                           <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"  EnableClientScript="true" Enabled="true" ControlToValidate="local" ErrorMessage="RegularExpressionValidator" ForeColor="Red" Text=" Amount should not be zero" ValidationExpression="^(0*[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*)$" ValidationGroup="one"></asp:RegularExpressionValidator>
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td class="auto-style4" style="padding:5px" >
				<asp:Button ID="Button1" runat="server" ValidationGroup="one" CausesValidation="true"  EnableClientScript="true" Enabled="true"  class="btn btn-success btn-block" style="margin-top: 15px;" OnClick="Update_Click" Text="Update"/>
				
            
              
			 
                                                 </td>
                                                
                                         </table>
                                 </div>
                                 
                   



                           
        </div>


                                     <div class="clearfix mb20"></div>
                                 
                   



                             </div>
                          <%--   <div class="col-md-6">
                                 
                                     <div class="col-md-12">
                                    <div class="floatings-labels">
                                        <asp:Label ID="Alevel" runat="server"  Text="Level"    Font-Bold="true"  ></asp:Label>
                                       
                            <asp:TextBox ID="Approverlevel" runat="server"  ReadOnly="false"  ></asp:TextBox>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Approverlevel" Text="*" ForeColor="Red" ValidationGroup="two"  ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
                            
                                        
                                      
                                    </div>
                                </div>

                                 
                                     <div class="col-md-12">
                                    <div class="floatings-labels">
                                         <asp:Label ID="Arole" runat="server"  Text="Designation"  Font-Bold="true" ></asp:Label>
                                <asp:TextBox ID="ApproverRole"  runat="server" required="" ReadOnly="false"    ></asp:TextBox>
								<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ApproverRole" Text="*" ForeColor="Red" runat="server" ValidationGroup="two" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
                           
                                       
                                    </div>
                                </div>

 
                                  <div class="clearfix"></div>
                                    <div class="col-sm-6 pull-right">
                                         <asp:Button ID="update" runat="server"  ValidationGroup="two" Text="Update" OnClick="update_Click"  />
                                   
                                </div>
                                        <div class="col-sm-6 pull-right">
                            <asp:Button ID="Delete" runat="server" Text="Delete"  ValidationGroup="two" CausesValidation="false" OnClick="Delete_Approver"  />
                                   
                                </div>
                                  
                                       

                             </div>--%>

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
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" Runat="Server">
    <script>
           $(document).ready(function () {
               $("#liadminexpenses").addClass("active");
               $("#liadmineligibility").addClass("active");
           });
    </script>
</asp:Content>
