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

public partial class Rejectedassets : System.Web.UI.Page
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
            SqlCommand swcmd = new SqlCommand("SELECT * FROM [EmployeeAssetRequestTable] WHERE ([RequestStatus] = 'Rejected')", swcon);
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
        lblReqDate.Text = gvSoftList.SelectedRow.Cells[8].Text;
        lblReqRejDate.Text = gvSoftList.SelectedRow.Cells[9].Text;
        Rejmp.Show();
    }

    protected void gvSoftList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            SqlConnection gvsw_hwRejcon = new SqlConnection(constr);
            gvsw_hwRejcon.Open();
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvSoftList.Rows[index];
            Assetindex = row.Cells[5].Text;

            SqlCommand gvsw_hwRejcmd = new SqlCommand("select * from EmployeeAssetRequestTable where ([RequestStatus] = 'Rejected') AND ([EmployeeAssetRequestID] ='" + row.Cells[0].Text + "')", gvsw_hwRejcon);
            SqlDataReader gvsw_hwRejdr = gvsw_hwRejcmd.ExecuteReader();
            gvsw_hwRejdr.Read();
            Session["GridEmpReqID"] = gvsw_hwRejdr["EmployeeAssetRequestID"];
            Session["GridEmpName"] = gvsw_hwRejdr["RequestEmployeeName"];
            Session["GridEmpID"] = gvsw_hwRejdr["RequestEmployeeId"];
            Session["GridEmpMailID"] = gvsw_hwRejdr["RequestedEmployeeMailId"];
            EmpMail = Session["GridEmpMailID"].ToString();
            Session["GridAssetID"] = gvsw_hwRejdr["AssetID"];
            Session["GridAssetType"] = gvsw_hwRejdr["AssetType"];
            Session["GridBussinessJustification"] = gvsw_hwRejdr["BusinessJustification"];
            gvsw_hwRejdr.Close();

            SqlCommand gvsw_hwRejex1cmd = new SqlCommand("select * from [Asset_Master] where ([AMS_AssetID] ='" + row.Cells[5].Text + "')", gvsw_hwRejcon);
            SqlDataReader gvsw_hwRejex1dr = gvsw_hwRejex1cmd.ExecuteReader();
            gvsw_hwRejex1dr.Read();
            Session["GridAssetName"] = gvsw_hwRejex1dr["AMS_AssetName"];
            Session["GridCompanyLocation"] = gvsw_hwRejex1dr["AMS_Company_Location"];
            Session["GridQuantity"] = gvsw_hwRejex1dr["AMS_Num_of_Assets"];
            gvsw_hwRejex1dr.Close();

            SqlCommand gvsw_hwRejex2cmd = new SqlCommand("select EmployeeMailID from Employee where Position = 'Admin' AND isactive = '1'", gvsw_hwRejcon);
            SqlDataReader gvsw_hwRejex2dr = gvsw_hwRejex2cmd.ExecuteReader();
            gvsw_hwRejex2dr.Read();
            Session["AdminMailID"] = gvsw_hwRejex2dr["EmployeeMailID"];
            AdminMail = Session["AdminMailID"].ToString();
            gvsw_hwRejex2dr.Close();

            if (e.CommandName == "Approve_click")
            {
                var apprcmd = "Update EmployeeAssetRequestTable set [RequestStatus] ='Approved', [RequestApprovedDate] = '" + DateTime.Now.ToString("dd/MM/yyyy") + "' where EmployeeAssetRequestID = @parm1";
                using (SqlConnection apprcon = new SqlConnection(constr))
                {
                    using (SqlCommand updatecmd = new SqlCommand(apprcmd, apprcon))
                    {
                        updatecmd.Parameters.AddWithValue("@parm1", row.Cells[0].Text);
                        apprcon.Open();
                        updatecmd.ExecuteNonQuery();
                        apprcon.Close();
                        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alertInvalidFormula", "alert('The formula you created already exists under the column name: ');", true);
                    }
                }

                try
                {
                    using (MailMessage mm = new MailMessage("helpdesk.snpl@gmail.com", EmpMail + "," + AdminMail))
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
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            gvsw_hwRejcon.Close();
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
}