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

public partial class PendingAssets : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
    string EmpMail, AdminMail, Assetindex;
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
            SqlCommand swcmd = new SqlCommand("SELECT [EmployeeAssetRequestID], [AssetID], [AssetType], [RequestStatus], [RequestEmployeeName], [RequestEmployeeId], [RequestedEmployeeMailId], [BusinessJustification], [RequestedDate] FROM [EmployeeAssetRequestTable] WHERE ([RequestStatus] = 'Pending')", swcon);
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
        try
        {
            lblPAReqAssetID.Text = gvSoftList.SelectedRow.Cells[0].Text;
            lblPAReqStatus.Text = gvSoftList.SelectedRow.Cells[1].Text;
            lblPAReqEmpName.Text = gvSoftList.SelectedRow.Cells[2].Text;
            lblPAReqEmpID.Text = gvSoftList.SelectedRow.Cells[3].Text;
            lblPAReqEmpMailID.Text = gvSoftList.SelectedRow.Cells[4].Text;
            lblPAReqAssetID.Text = gvSoftList.SelectedRow.Cells[5].Text;
            lblPAReqAssetType.Text = gvSoftList.SelectedRow.Cells[6].Text;
            lblPAReqBJ.Text = gvSoftList.SelectedRow.Cells[7].Text;
            lblPAReqAssetDate.Text = gvSoftList.SelectedRow.Cells[8].Text;
            PAhw_swmp.Show();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void gvSoftList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            SqlConnection gvsoftPAcon = new SqlConnection(constr);

            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvSoftList.Rows[index];
            gvsoftPAcon.Open();
            Assetindex = row.Cells[5].Text;
            SqlCommand gvsoftPAcmd = new SqlCommand("select * from EmployeeAssetRequestTable where ([RequestStatus] = 'Pending') AND ([EmployeeAssetRequestID] ='" + row.Cells[0].Text + "')", gvsoftPAcon);
            SqlDataReader gvsoftPAdr = gvsoftPAcmd.ExecuteReader();
            gvsoftPAdr.Read();
            if (gvsoftPAdr.HasRows)
            {
                Session["GridEmpReqID"] = gvsoftPAdr["EmployeeAssetRequestID"].ToString();
                Session["GridEmpName"] = gvsoftPAdr["RequestEmployeeName"].ToString();
                Session["GridEmpID"] = gvsoftPAdr["RequestEmployeeId"].ToString();
                Session["GridEmpMailID"] = gvsoftPAdr["RequestedEmployeeMailId"].ToString();
                EmpMail = Session["GridEmpMailID"].ToString().ToString();
                Session["GridAssetID"] = gvsoftPAdr["AssetID"].ToString();
                Session["GridAssetType"] = gvsoftPAdr["AssetType"].ToString();
                Session["GridBussinessJustification"] = gvsoftPAdr["BusinessJustification"].ToString();

            }
            gvsoftPAdr.Close();

            SqlCommand gvsoftPAex1cmd = new SqlCommand("select * from [Asset_Master] where ([AMS_AssetID] ='" + row.Cells[5].Text + "')", gvsoftPAcon);
            SqlDataReader gvsoftPAex1dr = gvsoftPAex1cmd.ExecuteReader();
            gvsoftPAex1dr.Read();
            Session["GridAssetName"] = gvsoftPAex1dr["AMS_AssetName"].ToString();
            Session["GridCompanyLocation"] = gvsoftPAex1dr["AMS_Company_Location"].ToString();
            Session["GridQuantity"] = gvsoftPAex1dr["AMS_Num_of_Assets"].ToString();
            gvsoftPAex1dr.Close();

            SqlCommand gvsoftPAex2cmd = new SqlCommand("select EmployeeMailID from Employee where Position = 'Admin' AND isactive = '1'", gvsoftPAcon);
            SqlDataReader gvsoftPAex2dr = gvsoftPAex2cmd.ExecuteReader();
            gvsoftPAex2dr.Read();
            Session["AdminMailID"] = gvsoftPAex2dr["EmployeeMailID"].ToString();
            AdminMail = Session["AdminMailID"].ToString();
            gvsoftPAex2dr.Close();

            if (e.CommandName == "Approve_click")
            {
                var cmd1 = "Update EmployeeAssetRequestTable set [RequestStatus] ='Approved', [RequestApprovedDate] = '" + DateTime.Now.ToString("dd/MM/yyyy") + "' " +
                    "where EmployeeAssetRequestID = @parm1";
                using (SqlConnection conn = new SqlConnection(constr))
                {
                    using (SqlCommand updatecmd = new SqlCommand(cmd1, conn))
                    {
                        updatecmd.Parameters.AddWithValue("@parm1", row.Cells[0].Text);
                        conn.Open();
                        updatecmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }

                try
                {
                    using (MailMessage mm = new MailMessage("helpdesk.snpl@gmail.com", EmpMail + "," + AdminMail ))
                    {
                        //string body;
                        if (Session["GridAssetType"].ToString() == "Hardware Assets")
                        {
                            retrievehwdata();
                            mm.Subject = "Asset Request Status";
                            mm.Body = approve_mailcontent_HW();

                        }
                        else if (Session["GridAssetType"].ToString() == "Software Assets")
                        {
                            retrieveswdata();
                            mm.Subject = "Asset Request Status";
                            mm.Body = approve_mailcontent_SW();
                        }
                        mm.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential("helpdesk.snpl@gmail.com", "jdzydiyrrvfnrjbf");
                        smtp.Send(mm);
                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Employee Request was Approved');", true);
                        Response.Write("<script>alert('Employee Request was Approved');</script>");
                    }
                    //Response.Write("<script>alert('Employee Request was Approved');</script>");
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Employee Request was Approved');", true);
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Please Try Again. Mail cannot be sent because of the server problem. ');</script>");
                    throw ex;
                }
            }
            else if (e.CommandName == "Reject_click")
            {
                var Rejcmd = "Update EmployeeAssetRequestTable set [RequestStatus] ='Rejected', [RequestRejectedDate] = '" + DateTime.Now.ToString("dd/MM/yyyy") + "' " +
                    "where EmployeeAssetRequestID = @parm1";
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
                        //string body;
                        if (Session["GridAssetType"].ToString() == "Hardware Assets")
                        {
                            retrievehwdata();
                            mail.Subject = "Asset Request Status";
                            mail.Body = reject_mailcontent_HW();
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
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Please Try Again. Mail cannot be sent because of the server problem');</script>");
                    throw ex;
                }
            }
            gvsoftPAcon.Close();
            gvsoftPAcon.Dispose();
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

    // mail contents request approval hw & sw start....
    //hw
    protected string approve_mailcontent_HW()
    {
        try
        {
            string texthwappr = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("../Mail Content Pages/Mail Content Request Approved to Emp_hw.html")))
            {
                texthwappr = reader.ReadToEnd();
            }
            texthwappr = texthwappr.Replace("[RequestID]", Session["GridEmpReqID"].ToString());
            texthwappr = texthwappr.Replace("[Asset type]", Session["GridAssetType"].ToString());
            texthwappr = texthwappr.Replace("[Asset Name]", Session["GridAssetName"].ToString());
            texthwappr = texthwappr.Replace("[Model]", Session["GridAssetModel"].ToString());
            texthwappr = texthwappr.Replace("[Quantity]", Session["GridQuantity"].ToString());
            texthwappr = texthwappr.Replace("[Business Justification]", Session["GridBussinessJustification"].ToString());
            return texthwappr;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    //sw
    protected string approve_mailcontent_SW()
    {
        try
        {
            string textswappr = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("../Mail Content Pages/Mail Content Request Approved to Emp_sw.html")))
            {
                textswappr = reader.ReadToEnd();
            }
            textswappr = textswappr.Replace("[RequestID]", Session["GridEmpReqID"].ToString());
            textswappr = textswappr.Replace("[Asset type]", Session["GridAssetType"].ToString());
            textswappr = textswappr.Replace("[Asset Name]", Session["GridAssetName"].ToString());
            textswappr = textswappr.Replace("[Version]", Session["GridAssetVersion"].ToString());
            textswappr = textswappr.Replace("[Category]", Session["GridAssetCategory"].ToString());
            textswappr = textswappr.Replace("[Business Justification]", Session["GridBussinessJustification"].ToString());
            return textswappr;
        }
        catch (Exception ex)
        {
            throw ex;
        }

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

