using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Approvedassets : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
    string EmpMail, Assetindex;
    protected void Page_Load(object sender, EventArgs e)
    {
        //ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        if (Session["SessionUserMailID"] != null && Session["SessionUserPosition"].ToString() == "Employee")
        {
            if (!Page.IsPostBack)
            {
                bindAdds();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    void bindAdds()
    {
        DataTable swdt = new DataTable();
        using (SqlConnection swcon = new SqlConnection(constr))
        {
            swcon.Open();
            SqlCommand swcmd = new SqlCommand("SELECT * FROM [EmployeeAssetRequestTable] WHERE ([RequestStatus] = 'Approved')", swcon);
            SqlDataReader swdr = swcmd.ExecuteReader(CommandBehavior.CloseConnection);
            swdt.Load(swdr);
            gvSoftList.DataSource = swdt;
            gvSoftList.DataBind();
            swcon.Close();
        }

        if (gvSoftList.Rows.Count > 0)
        {
            gvSoftList.CssClass = "table table-bordered table-striped  table-inverse nomargin";
            gvSoftList.UseAccessibleHeader = true;
            gvSoftList.HeaderRow.TableSection = TableRowSection.TableHeader;
            gvSoftList.FooterRow.TableSection = TableRowSection.TableFooter;
        }
    }


    protected void gvSoftList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvSoftList, "Select$" + e.Row.RowIndex);
            e.Row.Attributes["style"] = "cursor:pointer";
        }
    }

    protected void gvSoftList_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblPAReqAssetID.Text = gvSoftList.SelectedRow.Cells[0].Text;
        lblPAReqStatus.Text = gvSoftList.SelectedRow.Cells[1].Text;
        lblPAReqEmpName.Text = gvSoftList.SelectedRow.Cells[2].Text;
        lblPAReqEmpID.Text = gvSoftList.SelectedRow.Cells[3].Text;
        lblPAReqEmpMailID.Text = gvSoftList.SelectedRow.Cells[4].Text;
        lblPAReqAssetID.Text = gvSoftList.SelectedRow.Cells[5].Text;
        lblPAReqAssetType.Text = gvSoftList.SelectedRow.Cells[6].Text;
        lblPAReqBJ.Text = gvSoftList.SelectedRow.Cells[7].Text;
        lblReqApprDate.Text = gvSoftList.SelectedRow.Cells[8].Text;
        APreqmp.Show();
    }

    protected void gvSoftList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            SqlConnection conn1 = new SqlConnection(constr);
            conn1.Open();
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvSoftList.Rows[index];
            Assetindex = row.Cells[5].Text;
            SqlCommand cmd11 = new SqlCommand("select * from EmployeeAssetRequestTable where ([RequestStatus] = 'Approved') AND ([EmployeeAssetRequestID] ='" + row.Cells[0].Text + "')", conn1);
            SqlDataReader cmd11dr = cmd11.ExecuteReader();
            cmd11dr.Read();
            Session["GridEmpReqID"] = cmd11dr["EmployeeAssetRequestID"];
            Session["GridEmpName"] = cmd11dr["RequestEmployeeName"];
            Session["GridEmpID"] = cmd11dr["RequestEmployeeId"];
            Session["GridEmpMailID"] = cmd11dr["RequestedEmployeeMailId"];
            EmpMail = Session["GridEmpMailID"].ToString();
            Session["GridAssetID"] = cmd11dr["AssetID"];
            Session["GridAssetType"] = cmd11dr["AssetType"];
            Session["GridBussinessJustification"] = cmd11dr["BusinessJustification"];
            cmd11dr.Close();

            SqlCommand cmd12 = new SqlCommand("select * from [Asset_Master] where ([AMS_AssetID] ='" + row.Cells[5].Text + "')", conn1);
            SqlDataReader cmd12dr = cmd12.ExecuteReader();
            cmd12dr.Read();
            Session["GridAssetName"] = cmd12dr["AMS_AssetName"];
            Session["GridCompanyLocation"] = cmd12dr["AMS_Company_Location"];
            Session["GridQuantity"] = cmd12dr["AMS_Num_of_Assets"];
            cmd12dr.Close();
            
            if (e.CommandName == "Reject_click")
            {
                var Rejcmd = "Update EmployeeAssetRequestTable set [RequestStatus] ='Rejected', [RequestRejectedDate] = '" + DateTime.Now.ToString("dd/MM/yyyy") + "' where EmployeeAssetRequestID = @parm1";
                using (SqlConnection Rejcon = new SqlConnection(constr))
                {
                    using (SqlCommand updatecmd = new SqlCommand(Rejcmd, Rejcon))
                    {
                        updatecmd.Parameters.AddWithValue("@parm1", row.Cells[0].Text);
                        Rejcon.Open();
                        updatecmd.ExecuteNonQuery();
                        Rejcon.Close();
                    }
                }
                try
                {
                    using (MailMessage mail = new MailMessage("helpdesk.snpl@gmail.com", EmpMail.ToString()))
                    {
                        if (Session["GridAssetType"].ToString() == "Hardware Assets")
                        {
                            retrievehwdata();
                            mail.Subject = "Asset Request Status";
                            mail.Body = reject_mailcontent_HW();
                            //mail.Body = AMS.Reporting_Manager.PendingAssets(reject_mailcontent_HW());
                        }
                        else if (Session["GridAssetType"].ToString() == "Software Assets")
                        {
                            retrieveswdata();
                            mail.Subject = "Asset Request Status";
                            mail.Body = reject_mailcontent_SW();
                        }
                        mail.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential("helpdesk.snpl@gmail.com", "jdzydiyrrvfnrjbf");
                        smtp.Send(mail);
                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Employee Request was Rejected');", true);
                    }
                    Response.Redirect("~/Operations/Approved-assets.aspx");
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please Try Again. Mail cannot be sent because of the server problem ');", true);
                    throw ex;
                }
            }
            conn1.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void retrievehwdata()
    {
        SqlConnection hwdatacon = new SqlConnection(constr);
        hwdatacon.Open();
        SqlCommand hwdatacmd = new SqlCommand("select * from Hardware_Physical_Assets where (AMSHW_PH_AssetID ='" + Assetindex + "')", hwdatacon);
        SqlDataReader hwdatadr = hwdatacmd.ExecuteReader();
        hwdatadr.Read();
        Session["GridAssetModel"] = hwdatadr["AMSHW_PH_Model"].ToString();
        hwdatadr.Close();
        hwdatacon.Close();
        hwdatacon.Dispose();
    }

    protected void retrieveswdata()
    {
        SqlConnection swdatacon = new SqlConnection(constr);
        swdatacon.Open();
        SqlCommand swdatacmd = new SqlCommand("select * from Software_Assets where (AMSSW_AssetID ='" + Assetindex + "')", swdatacon);
        SqlDataReader swdatadr = swdatacmd.ExecuteReader();
        swdatadr.Read();
        Session["GridAssetVersion"] = swdatadr["AMSSW_Version"].ToString();
        Session["GridAssetCategory"] = swdatadr["AMSSW_Category"].ToString();
        swdatadr.Close();
        swdatacon.Close();
        swdatacon.Dispose();
    }

    // mail contents request rejected hw & sw start....
    //hw
    protected string reject_mailcontent_HW()
    {
        try
        {
            string texthwrej = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("../Mail Content Pages/Mail Content Request Rejected to Emp_hw.html")))
            {
                texthwrej = reader.ReadToEnd();
            }
            texthwrej = texthwrej.Replace("[Employee]", Session["GridEmpName"].ToString());
            texthwrej = texthwrej.Replace("[RequestID]", Session["GridEmpReqID"].ToString());
            texthwrej = texthwrej.Replace("[Asset type]", Session["GridAssetType"].ToString());
            texthwrej = texthwrej.Replace("[Asset Name]", Session["GridAssetName"].ToString());
            texthwrej = texthwrej.Replace("[Model]", Session["GridAssetModel"].ToString());
            texthwrej = texthwrej.Replace("[Quantity]", Session["GridQuantity"].ToString());
            texthwrej = texthwrej.Replace("[Business Justification]", Session["GridBussinessJustification"].ToString());
            return texthwrej;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    //sw
    protected string reject_mailcontent_SW()
    {
        try
        {
            string textswrej = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("../Mail Content Pages/Mail Content Request Rejected to Emp_sw.html")))
            {
                textswrej = reader.ReadToEnd();
            }
            textswrej = textswrej.Replace("[Employee]", Session["GridEmpName"].ToString());
            textswrej = textswrej.Replace("[RequestID]", Session["GridEmpReqID"].ToString());
            textswrej = textswrej.Replace("[Asset type]", Session["GridAssetType"].ToString());
            textswrej = textswrej.Replace("[Asset Name]", Session["GridAssetName"].ToString());
            textswrej = textswrej.Replace("[Version]", Session["GridAssetVersion"].ToString());
            textswrej = textswrej.Replace("[Category]", Session["GridAssetCategory"].ToString());
            textswrej = textswrej.Replace("[Business Justification]", Session["GridBussinessJustification"].ToString());
            return textswrej;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
}