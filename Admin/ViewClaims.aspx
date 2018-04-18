<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true" CodeFile="ViewClaims.aspx.cs" Inherits="Admin_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ers3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphhead" runat="server">

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
        .modalBackground {
background-color:gray;
filter: alpha(opacity=100);
opacity: 0.9;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
      <div class="mainpanel">

        <div class="contentpanel">
			<ol class="breadcrumb breadcrumb-quirk">
                <li><a href="AdminViewClaims.aspx"><i class="fa fa-home mr5"></i>Home</a></li>
                <li class="active"> View Claims</li>
            </ol>
           
            <div class="row">
                <div class="col-sm-12">

                    <div class="panel">
                        <div class="panel-heading">
 
                    <h4 class="panel-title">Claims Details  </h4>
                            

                            <div class="panel-body">
                                  <div class="form mb20">
                                     

                                  <%--   <div class="col-md-6">
                                    <div class="floatings-lab-els">
                                         <asp:TextBox ID="txtsearch" CssClass="floating-input" placeholder=" "  runat="server"  ></asp:TextBox> 
                                        <label class="lbltop1 lbltop" for="nMobileNumber2">  Name,Status,Type </label>
                                    </div>
                                </div>

                                          <div class="col-md-3">
                                              <asp:Button ID="gridsearch" OnClick="gridsearch_Click"  runat="server"   Text="Search" CssClass="btn btn-success btn-block"   ></asp:Button>
                                              
                                              
                                          </div>
                                                                              <div class="col-md-3">
<asp:Button ID="Cancelsearch"  OnClick="Cancelsearch_Click"  runat="server" Text="Clear"  CssClass="btn btn-success btn-block"    />
                                              
                                              
                                          </div>--%>
                                         <div class="clearfix" ></div>

                                          
                                      </div> 
                             

                                <div class="table-responsive">

                                    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" OnRowCommand="gvList_RowCommand"
                                        CssClass="table table-bordered table-striped    table-inverse nomargin" DataKeyNames="ERSClaimID" OnRowDataBound="gvList_RowDataBound" OnSelectedIndexChanged="gvList_SelectedIndexChanged" >
                                        <Columns>
                                                                
                        <asp:BoundField DataField="ERSClaimID" HeaderText="ClaimID" InsertVisible="False" ReadOnly="True" SortExpression="ERSClaimID" />
                        <asp:BoundField DataField="ERSClaimType" HeaderText="Claim Type" SortExpression="ERSClaimType" />
                        <asp:BoundField DataField="ERSClaimDate" HeaderText="Date" SortExpression="ERSClaimDate" />
                        <asp:BoundField DataField="ERSBillAmount" HeaderText="BillAmount" SortExpression="ERSBillAmount" />
                        <asp:BoundField DataField="ERSClaimStatus" HeaderText="Status" SortExpression="ERSClaimStatus" />
                         <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name"  >
                  </asp:BoundField>
               
                <asp:BoundField DataField="ERSApproverName" HeaderText="Approver Name"  >
                </asp:BoundField>
                
                <asp:BoundField DataField="ERSClaimProcessDate" HeaderText="ProcessDate"   DataFormatString="{0:MM/dd/yyyy}">
                </asp:BoundField>
              
                                        <asp:ButtonField ButtonType="Link" Text="<i class='fa fa-edit'></i>" ControlStyle-CssClass="btn btn-primary btn-stroke btn-icon" HeaderText="Action" HeaderStyle-CssClass="hidden-sm" ItemStyle-CssClass="hidden-sm" CommandName="Edit_Click" />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:LinkButton ID="LinkButton1" runat="server"></asp:LinkButton>
                                   
									<asp:LinkButton ID="btnclose" runat="server"></asp:LinkButton>
                                  

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

    </div>



    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    
    <asp:Panel ID="updatePanel" runat="server">
        <div class="modal-dialog  modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-body" >
                    
                        <div class="panel panel-profile list-view">
                        <div class="panel-heading">
                            <div class="media">

                                <div class="media-body">
                                  
                                    <h4 class="media-heading">  Claim  Details</h4>
                                </div>
                            </div>
                           <ul class="panel-options">
                             
                                <li><a href="" class="close" data-dismiss="modal" aria-hidden="true"><i class="fa fa-times"></i></a></li>
                           </ul>
                        </div>
                        <!-- panel-heading -->

                        <div class="panel-body people-info">
                         <div class="row mb20">

                             <div class="col-md-6">


                                 <div class="well mb20">
                                    <div class="form-group">
                                           <asp:Label runat="server" class="col-sm-4 control-label" Font-Bold="true">Claim ID  </asp:Label>
                                           <asp:Label ID="claimId" runat="server" CssClass="col-sm-8 control-label "></asp:Label>
                                       </div>
                                     <div class="form-group">
                                           <asp:Label runat="server" class="col-sm-4 control-label" Font-Bold="true"> Claim Type  </asp:Label>
                                           <asp:Label ID="type" runat="server" CssClass="col-sm-8 control-label "></asp:Label>
                                       </div>
                                     <div class="form-group">
                                           <asp:Label runat="server" class="col-sm-4 control-label" Font-Bold="true">Date of Apply </asp:Label>
                                           <asp:Label ID="date" runat="server" CssClass="col-sm-8 control-label "></asp:Label>
                                       </div>
                                     <div class="form-group">
                                           <asp:Label runat="server" class="col-sm-4 control-label" Font-Bold="true">Status  </asp:Label>
                                           <asp:Label ID="status" runat="server" CssClass="col-sm-8 control-label "></asp:Label>
                                       </div>
                                     <div class="form-group">
                                           <asp:Label runat="server" class="col-sm-4 control-label" Font-Bold="true">Amount  </asp:Label>
                                           <asp:Label ID="amount" runat="server" CssClass="col-sm-8 control-label "></asp:Label>
                                       </div>
                                     <div class="form-group">
                                           <asp:Label runat="server" class="col-sm-4 control-label" Font-Bold="true">Employee Name  </asp:Label>
                                           <asp:Label ID="empname" runat="server" CssClass="col-sm-8 control-label "></asp:Label>
                                       </div>
                                     <div class="form-group">
                                           <asp:Label runat="server" class="col-sm-4 control-label" Font-Bold="true">Approver Name  </asp:Label>
                                           <asp:Label ID="appname" runat="server" CssClass="col-sm-8 control-label "></asp:Label>
                                       </div>
                                     <div class="form-group">
                                           <asp:Label runat="server" class="col-sm-4 control-label" Font-Bold="true">User Remarks</asp:Label>
                                           <asp:Label ID="userrm" runat="server" CssClass="col-sm-8 control-label "></asp:Label>
                                       </div>
                                     <div class="form-group">
                                           <asp:Label runat="server" class="col-sm-4 control-label" Font-Bold="true">Approver Remarks  </asp:Label>
                                           <asp:Label ID="apprm" runat="server" CssClass="col-sm-8 control-label "></asp:Label>
                                       </div>

                                          <div class="clearfix mb20">

                                          </div>     

                                
    
                                 </div>
                   



                             </div>
                            
                             <div class="col-md-6">
                                   <div class="col-md-12 text-center mb20">
                                       <div class="w3-container" style="color:black">
                        <asp:Image ID="Image1" runat="server"  Height="80px" ImageUrl='<%#"data:Image/png;base64," + Convert.ToBase64String((byte[])Eval("img"))%>' AlternateText="No Image" style="width:70%;height:50%;cursor:zoom-in;font-size:medium;" 
                                 onclick="document.getElementById('modal01').style.display='block'"/>
                                 <div id="modal01" class="w3-modal" onclick="this.style.display='none'">
                                <span class="w3-button w3-hover-white w3-xlarge w3-display-topright">&times;</span>
                               <div class="w3-modal-content w3-animate-zoom">
                                     <asp:Image ID="Image2" runat="server" ImageUrl='<%#"data:Image/png;base64,"+ Convert.ToBase64String((byte[])Eval("img"))%>'  style="width:100%"/>
                                 </div>
                               </div>
                              </div>
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

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="server">
     <script>
        $(document).ready(function () {
            
            $('#cphBody_gvList').DataTable();
            $("#liadminexpenses").addClass("active");
            $("#liadminclaimlist").addClass("active");

        });
    </script>
</asp:Content>


