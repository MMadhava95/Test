<%@ Page Title="" Language="C#" MasterPageFile="~/Operations/emp.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="Processclaim.aspx.cs" Inherits="Operations_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ers3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
   
    <style>
        .dataTables_filter {
            float: right;
        }

        .table-responsive {
            overflow: unset;
        }

        #cphBody_GridView1_paginate {
            float: right !important;
        }
        .modalBackground {
background-color:gray;
filter: alpha(opacity=100);
opacity: 0.9;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">

    <div class="mainpanel">

        <div class="contentpanel">

            <ol class="breadcrumb breadcrumb-quirk">
                <li><a href="ProcessClaim.aspx"><i class="fa fa-home mr5"></i>Home</a></li>
                <li class="active">Claim List   </li>
            </ol>

            <div class="row">
                <div class="container a" style="padding-right: 100px;" id="Alert" visible="true" runat="server">
	                                 <div class="alert alert-success alert-dismissable" id="alertmodal" runat="server">
                                    <i class="fa fa-check-square-o" id="suc" runat="server" visible="false"></i><i class="fa-cross-square-o" id="fai" runat="server" visible="false"></i>
                                    <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
                                    <strong>
                                        <asp:Label ID="Label7" runat="server" Text=""></asp:Label></strong>
                                    <asp:Label ID="Label9" runat="server" Visible="true" Text=""></asp:Label>
                                </div>
                            </div>

                <div class="col-sm-12">

                    <div class="panel">
                        <div class="panel-heading">
 
                    <h4 class="panel-title">Claims Details  </h4>

 
                            <div class="panel-body">
                                <div class="form mb20">
                                     <div class="col-md-3">
                                        <div class="floatings-labels">


                                            <asp:DropDownList ID="ddlstatus" runat="server" Style="color: black;" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged" CssClass="form-control">
                                                <asp:ListItem Text="All" Value="All"></asp:ListItem>
                                                <asp:ListItem Text="Pending" Value="Pending"> </asp:ListItem>
                                                <asp:ListItem Text="Need Clarification" Value="Need Clarification"></asp:ListItem>
                                                <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>
                                                <asp:ListItem Text="Rejected" Value="Rejected"></asp:ListItem>
                                            </asp:DropDownList>
                                            <label class="lbltop1 lbltop" for="nMobileNumber2">Status </label>
                                        </div>
                                    </div>
                                   

                                          <div class="clearfix"></div>

                                          
                                      </div> 
                                <hr />

                                <div class="table-responsive">




                                     <asp:GridView ID="GridView1" runat="server" HeaderStyle-ForeColor="White" AutoGenerateColumns="false"
                                        EmptyDataText="No records found" DataKeyNames="ERSClaimID" OnRowDataBound="GridView1_RowDataBound1"
                                        CssClass="table table-bordered table-striped table-inverse nomargin" Width="100%"
                                        OnRowCommand="GridView1_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="ERSClaimID" HeaderText="Claimid" SortExpression="ERSClaimID" />
                                            <asp:BoundField DataField="ERSClaimType" HeaderText="Claimtype" />
                                            <asp:BoundField DataField="ERSClaimDate" HeaderText="Date"  />
                                            <asp:BoundField DataField="ERSClaimStatus" HeaderText="Status" />
                                            <asp:BoundField DataField="ERSBillAmount" HeaderText="Amount" />
                                            <asp:ButtonField ButtonType="Link" Text="<i class='fa fa-edit'></i>" ControlStyle-CssClass="btn btn-primary btn-stroke btn-icon" HeaderText="Action" HeaderStyle-CssClass="hidden-sm" ItemStyle-CssClass="hidden-sm" CommandName="Edit_Click" />
                                           
                                        </Columns>
                                    </asp:GridView>
									 <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
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

    <asp:Panel runat="server" ID="updatePanel">
        <div class="modal-dialog  modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="panel panel-profile list-view">
                        <div class="panel-heading">
                            <div class="media">

                                <div class="media-body">
                                    <h4 class="media-heading">  Claim  Details</h4>
                                </div>
                            </div>
                            <!-- media -->
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
                                            <asp:Label runat="server" class="col-sm-4 control-label">Claim ID  </asp:Label>
                                            <asp:Label ID="claimId" runat="server" CssClass="col-sm-8 control-label "></asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" class="col-sm-4 control-label">Claim Type  </asp:Label>
                                            <asp:Label ID="claimtype" runat="server" CssClass="col-sm-8 control-label "></asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" class="col-sm-4 control-label">Eligibility  </asp:Label>
                                            <asp:Label ID="Label1" runat="server" CssClass="col-sm-8 control-label "></asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" class="col-sm-4 control-label">Amount</asp:Label>
                                            <asp:Label ID="Label8" runat="server" CssClass="col-sm-8 control-label "></asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" class="col-sm-4 control-label">Claim Date  </asp:Label>
                                            <asp:Label ID="applieddate" runat="server" CssClass="col-sm-8 control-label"></asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" class="col-sm-4 control-label">Status  </asp:Label>
                                            <asp:Label ID="claimstatus" runat="server" CssClass="col-sm-8 control-label"></asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" class="col-sm-4 control-label">Name</asp:Label>
                                            <asp:Label ID="Employee" runat="server" CssClass="col-sm-8 control-label"></asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" class="col-sm-4 control-label">  Description  </asp:Label>

                                            <asp:Label ID="Label3" runat="server" CssClass="col-sm-8 control-label"></asp:Label>

                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="cprocessdate" class="col-sm-4 control-label">Process Date</asp:Label>
                                            <asp:Label ID="ClaimProcessDate" runat="server" CssClass="col-sm-8 control-label"></asp:Label>
                                        </div>
									 <div class="form-group">
                                            <asp:Label runat="server" ID="premarks" class="col-sm-4 control-label" Visible="false" Text="">Remarks</asp:Label>
                                           <asp:Label ID="LevelApproverRemarks" runat="server" Visible="false" Text=""></asp:Label>
                                        </div>
									
                                        <div class="clearfix mb20"></div>
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
                                    <div class="col-md-12">
                                        <div class="floatings-labels" id="dropdown">
                                            <asp:DropDownList ID="ActionDropDown"  runat="server" ForeColor="black"  required="required"  CssClass="form-control">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem>Need Clarification</asp:ListItem>
                                                <asp:ListItem>Approved</asp:ListItem>
                                                <asp:ListItem>Rejected</asp:ListItem>
                                                <asp:ListItem>Forward to Next Level</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:label runat="server" class="lbltop" for="txtMotherLang" id="dropdownaction">Action  </asp:label>
                                        </div>
                                    </div>
                                    <div class="col-md-12">

                                        <div class="floatings-labels">

                                            <asp:TextBox ID="remarks" runat="server" Height="80" CssClass="floating-textarea" TextMode="MultiLine" placeholder="Remarks"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="col-sm-12 pull-right">
                                        <asp:Button runat="server" ID="Submit" CssClass="btn btn-success btn-block" Style="margin-top: 15px;" Text="Submit" OnClick="Submit_Click" />
                                    </div>
                                </div>
                            </div>
                            <asp:Label ID="Name" runat="server" Visible="false" Text="" Style="font-weight: bold;"></asp:Label>
                            <asp:Label ID="AppRemarks" Font-Size="Medium" runat="server" Text="" Font-Bold="true" Visible="false"></asp:Label>
                            
                        </div>

                       
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <ers3:modalpopupextender ID="MPE" runat="server" PopupControlID="updatePanel" TargetControlID="lnkDummy" CancelControlID="btnclose"
         BackgroundCssClass="modalBackground">
    </ers3:modalpopupextender>



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="Server">
    <script>
        $(document).ready(function () {
            
            $('#cphBody_GridView1').DataTable();

            $("#liapproverExpenses").addClass("active");
            $("#liprocessclaim").addClass("active");
        });
    </script>
</asp:Content>



