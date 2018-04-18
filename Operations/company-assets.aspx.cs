using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class companyassets : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        //ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        if (Session["SessionUserMailID"] != null && (Session["SessionUserPosition"].ToString() == "Employee" || Session["SessionUserSecondryPosition"].ToString() == "Approver"))
        {
            if (!Page.IsPostBack)
            {
                hwassets();
                bindAdds();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    void hwassets()
    {
        SqlConnection con = new SqlConnection(constr);
        con.Open();
        SqlCommand hwcmd = new SqlCommand("select * from HardwareAssetsView " +
            "where AMS_AssetID NOT IN " +
            "(select AssetID from EmployeeAssetRequestTable where (RequestStatus like 'A%'))", con);
        SqlDataReader hwdr = hwcmd.ExecuteReader(CommandBehavior.CloseConnection);
        DataTable hwdt = new DataTable();
        hwdt.Load(hwdr);
        gvHardware.DataSource = hwdt;
        gvHardware.DataBind();

        con.Close();
    }

    void bindAdds()
    {
        SqlConnection con = new SqlConnection(constr);
        con.Open();
        SqlCommand swcmd = new SqlCommand("select * from SoftwareAssetsView " +
            "where AMS_AssetID NOT IN " +
            "(select AssetID from EmployeeAssetRequestTable where (RequestStatus LIKE 'A%'))", con);
        SqlDataReader swdr = swcmd.ExecuteReader(CommandBehavior.CloseConnection);

        DataTable swdt = new DataTable();
        swdt.Load(swdr);
        gvList.DataSource = swdt;
        gvList.DataBind();

        con.Close();


        if (gvList.Rows.Count > 0)
        {
            gvList.CssClass = "table table-bordered table-striped  table-inverse nomargin";
            gvList.UseAccessibleHeader = true;
            gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
            gvList.FooterRow.TableSection = TableRowSection.TableFooter;
        }

        if (gvHardware.Rows.Count > 0)
        {
            gvHardware.CssClass = "table table-bordered table-striped  table-inverse nomargin";
            gvHardware.UseAccessibleHeader = true;
            gvHardware.HeaderRow.TableSection = TableRowSection.TableHeader;
            gvHardware.FooterRow.TableSection = TableRowSection.TableFooter;
        }
    }

    protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvList, "Select$" + e.Row.RowIndex);
            e.Row.Attributes["style"] = "cursor:pointer";
        }
    }

    protected void gvHardware_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvHardware, "Select$" + e.Row.RowIndex);
            e.Row.Attributes["style"] = "cursor:pointer";
        }
    }

    protected void gvList_SelectedIndexChanged(object sender, EventArgs e)
    {
        swlbl1.Text = gvList.SelectedRow.Cells[0].Text;
        swlbl2.Text = gvList.SelectedRow.Cells[1].Text;
        swlbl3.Text = gvList.SelectedRow.Cells[2].Text;
        swlbl4.Text = gvList.SelectedRow.Cells[3].Text;
        swlbl5.Text = gvList.SelectedRow.Cells[4].Text;
        swlbl6.Text = gvList.SelectedRow.Cells[5].Text;
        swlbl7.Text = gvList.SelectedRow.Cells[6].Text;
        swlbl8.Text = gvList.SelectedRow.Cells[7].Text;
        swlbl9.Text = gvList.SelectedRow.Cells[8].Text;
        ModalPopupExtender1.Show();
        swtxtBJ.Focus();
        swtxtBJ.Text = string.Empty;
    }

    protected void gvHardware_SelectedIndexChanged(object sender, EventArgs e)
    {
        hwlbl1.Text = gvHardware.SelectedRow.Cells[0].Text;
        hwlbl2.Text = gvHardware.SelectedRow.Cells[1].Text;
        hwlbl3.Text = gvHardware.SelectedRow.Cells[2].Text;
        hwlbl4.Text = gvHardware.SelectedRow.Cells[3].Text;
        hwlbl5.Text = gvHardware.SelectedRow.Cells[4].Text;
        hwlbl6.Text = gvHardware.SelectedRow.Cells[5].Text;
        hwlbl7.Text = gvHardware.SelectedRow.Cells[6].Text;
        hwlbl8.Text = gvHardware.SelectedRow.Cells[7].Text;
        hwlbl9.Text = gvHardware.SelectedRow.Cells[8].Text;
        Hwextender.Show();
        hwtxtBJ.Focus();
        hwtxtBJ.Text = string.Empty;
    }

    protected void SWButton_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(constr);
        con.Open();
        SqlCommand testcmd = new SqlCommand("select * from EmployeeAssetRequestTable where AssetID='" + swlbl1.Text + "' and RequestStatus ='Pending' and RequestedEmployeeMailId='" + Session["SessionUserMailID"] + "'", con);
        SqlDataReader testdr = testcmd.ExecuteReader();
        testdr.Read();

        if (testdr.HasRows)
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('You already requested this asset, Request Status still Pending. Please wait until your Approver react');", true);
            Response.Redirect("~/Operations/company-assets.aspx");
        }
        else
        {
            testdr.Close();
            testdr.Dispose();

            //Checking whether the initial Request status is Rejected when update command is worked.
            SqlCommand Checkcmd = new SqlCommand("select * from EmployeeAssetRequestTable where AssetID='" + swlbl1.Text + "' AND RequestStatus ='Rejected' ", con);
            SqlDataReader CheckifRejCmd = Checkcmd.ExecuteReader();
            CheckifRejCmd.Read();
            if (CheckifRejCmd.HasRows)
            {
                var UpdateCmd = "UPDATE EmployeeAssetRequestTable SET RequestStatus = 'Pending' WHERE AssetID = @parm1 and RequestStatus = 'Rejected'";
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    using (SqlCommand cmd11 = new SqlCommand(UpdateCmd, cnn))
                    {
                        cmd11.Parameters.AddWithValue("@parm1", swlbl1.Text);
                        cnn.Open();
                        int count = cmd11.ExecuteNonQuery();
                        if (count > 0)
                        {
                            SendMailfunSW();
                        }
                        cnn.Close();
                    }
                }
            }
            else
            {
                CheckifRejCmd.Close();
                CheckifRejCmd.Dispose();
                var InsertCmd = "Insert into EmployeeAssetRequestTable(AssetID, AssetType, RequestStatus, RequestEmployeeName, RequestEmployeeId, BusinessJustification, RequestedEmployeeMailId,RequestedDate) " +
                    "values(@parm1,@parm2,@parm3,@parm4,@parm5,@parm6,@parm7,@parm8)";
                using (SqlConnection insertcon = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand(InsertCmd, insertcon))
                    {
                        cmd.Parameters.AddWithValue("@parm1", swlbl1.Text);
                        cmd.Parameters.AddWithValue("@parm2", "Software Assets");
                        cmd.Parameters.AddWithValue("@parm3", "Pending");
                        cmd.Parameters.AddWithValue("@parm4", Session["SessionEmployeeName"].ToString());
                        cmd.Parameters.AddWithValue("@parm5", Session["SessionEmployeeID"].ToString());
                        cmd.Parameters.AddWithValue("@parm6", swtxtBJ.Text);
                        cmd.Parameters.AddWithValue("@parm7", Session["SessionUserMailID"].ToString());
                        cmd.Parameters.AddWithValue("@parm8", DateTime.Now.ToString("dd/MM/yyyy"));

                        insertcon.Open();
                        cmd.ExecuteNonQuery();

                        insertcon.Close();
                        insertcon.Dispose();
                        SendMailfunSW();
                    }
                }
            }
        }

    }

    protected void HwButton_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(constr);
        con.Open();
        SqlCommand testcmd = new SqlCommand("select * from EmployeeAssetRequestTable where AssetID='" + hwlbl1.Text + "' and RequestStatus ='Pending' and RequestedEmployeeMailId='" + Session["SessionUserMailID"] + "'", con);
        SqlDataReader testdr = testcmd.ExecuteReader();
        testdr.Read();

        if (testdr.HasRows)
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('You already requested this asset, Request staus still Pending. Please wait until your Approver react');", true);
            Response.Redirect("~/Operations/company-assets.aspx");
        }
        else
        {
            testdr.Close();
            testdr.Dispose();
            //Checking whether the Request status is Rejected when update command were used.
            SqlCommand Checkcmd = new SqlCommand("select * from EmployeeAssetRequestTable where AssetID='" + hwlbl1.Text + "' and RequestStatus ='Rejected' ", con);
            SqlDataReader CheckifRejCmd = Checkcmd.ExecuteReader();
            CheckifRejCmd.Read();

            if (CheckifRejCmd.HasRows)
            {
                var UpdateCmd = "UPDATE EmployeeAssetRequestTable SET RequestStatus = 'Pending' WHERE AssetID = @parm1 and RequestStatus = 'Rejected'";
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    using (SqlCommand cmd11 = new SqlCommand(UpdateCmd, cnn))
                    {
                        cmd11.Parameters.AddWithValue("@parm1", hwlbl1.Text);
                        cnn.Open();
                        SendMailfunHW();
                        cnn.Close();
                    }
                }
                CheckifRejCmd.Close();
                CheckifRejCmd.Dispose();
            }
            else
            {

                var InsertCmd = "Insert into EmployeeAssetRequestTable(" +
                    "AssetID, AssetType, RequestStatus, RequestEmployeeName, RequestEmployeeId, BusinessJustification, RequestedEmployeeMailId,RequestedDate) " +
                    "values(@parm1,@parm2,@parm3,@parm4,@parm5,@parm6,@parm7,@parm8)";
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand(InsertCmd, cnn))
                    {
                        cmd.Parameters.AddWithValue("@parm1", hwlbl1.Text);
                        cmd.Parameters.AddWithValue("@parm2", "Hardware Assets");
                        cmd.Parameters.AddWithValue("@parm3", "Pending");
                        cmd.Parameters.AddWithValue("@parm4", Session["SessionEmployeeName"].ToString());
                        cmd.Parameters.AddWithValue("@parm5", Session["SessionEmployeeID"].ToString());
                        cmd.Parameters.AddWithValue("@parm6", hwtxtBJ.Text);
                        cmd.Parameters.AddWithValue("@parm7", Session["SessionUserMailID"].ToString());
                        cmd.Parameters.AddWithValue("@parm8", DateTime.Now.ToString("dd/MM/yyyy"));

                        cnn.Open();
                        cmd.ExecuteNonQuery();

                        cnn.Close();
                        SendMailfunHW();
                    }
                }
            }
        }

    }

    protected void SendMailfunSW()
    {
        //The following command used for fetching the Approver Mail ID & send mail content for the requested assets (either hw or sw)
        try
        {
            SqlConnection conRM = new SqlConnection(constr);
            conRM.Open();
            SqlCommand cmdRM = new SqlCommand("select EmployeeMailID from Employee where SecondaryPosition = 'Approver' AND isactive = '1'", conRM);
            SqlDataReader drRM = cmdRM.ExecuteReader();
            drRM.Read();

            //Mail sent to Approver(s)
            while (drRM.Read())
            {
                using (MailMessage apprvrmail = new MailMessage("helpdesk.snpl@gmail.com", drRM["EmployeeMailID"].ToString()))
                {
                    apprvrmail.Subject = "Software Asset Request";
                    apprvrmail.Body = EmpReq_toRM_mailcontent_SW();
                    apprvrmail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("helpdesk.snpl@gmail.com", "jdzydiyrrvfnrjbf");
                    smtp.Send(apprvrmail);
                }
            }

            drRM.Close();
            drRM.Dispose();
            conRM.Close();
            conRM.Dispose();

            //Mail sent to Requested Employeee
            using (MailMessage empmail = new MailMessage("helpdesk.snpl@gmail.com", Session["SessionUserMailID"].ToString()))
            {
                empmail.Subject = "Software Asset request";
                empmail.Body = EmpReq_toEmp_mailcontent_SW();
                empmail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("helpdesk.snpl@gmail.com", "jdzydiyrrvfnrjbf");
                smtp.Send(empmail);
            }
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Your request sent to the Approver');", true);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        Response.Redirect("~/Operations/company-assets.aspx");
        //Page.Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
    }

    protected void SendMailfunHW()
    {
        try
        {
            //The following command used for fetching the RM mail ID and send mail content for the request assets (either hw or sw)
            SqlConnection conRM = new SqlConnection(constr);
            conRM.Open();
            SqlCommand cmdRM = new SqlCommand("select EmployeeMailID from Employee where SecondaryPosition = 'Approver' AND isactive = '1'", conRM);
            SqlDataReader drRM = cmdRM.ExecuteReader();
            drRM.Read();

            //Mail sent to Approver(s)
            while (drRM.Read())
            {
                using (MailMessage apprvrmail = new MailMessage("helpdesk.snpl@gmail.com", drRM["EmployeeMailID"].ToString()))
                {
                    apprvrmail.Subject = "Hardware Asset request";
                    apprvrmail.Body = EmpReq_toRM_mailcontent_HW();
                    apprvrmail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("helpdesk.snpl@gmail.com", "jdzydiyrrvfnrjbf");
                    smtp.Send(apprvrmail);
                }
            }

            drRM.Close();
            drRM.Dispose();
            conRM.Close();
            conRM.Dispose();

            //Mail sent to Requested Employeee
            using (MailMessage empmail = new MailMessage("helpdesk.snpl@gmail.com", Session["SessionUserMailID"].ToString()))
            {
                empmail.Subject = "Hardware Asset Request";
                empmail.Body = EmpReq_toEmp_mailcontent_HW();
                empmail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("helpdesk.snpl@gmail.com", "jdzydiyrrvfnrjbf");
                smtp.Send(empmail);
            }
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Your request sent to the Approver');", true);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        Response.Redirect("~/Operations/company-assets.aspx");
    }
    
    //sw mails sending (To RM)
    protected string EmpReq_toRM_mailcontent_SW()
    {
        string textbodyemtormsw = string.Empty;
        using (StreamReader reader = new StreamReader(Server.MapPath("~/Mail Content Pages/Mail Content Employee Request to RM SW.html")))
        {
            textbodyemtormsw = reader.ReadToEnd();
        }
        textbodyemtormsw = textbodyemtormsw.Replace("[Employee]", Session["SessionEmployeeName"].ToString());
        textbodyemtormsw = textbodyemtormsw.Replace("[Asset type]", "Software Assets");
        textbodyemtormsw = textbodyemtormsw.Replace("[Asset Name]", swlbl2.Text);
        textbodyemtormsw = textbodyemtormsw.Replace("[Version]", swlbl3.Text);
        textbodyemtormsw = textbodyemtormsw.Replace("[Category]", swlbl5.Text);
        textbodyemtormsw = textbodyemtormsw.Replace("[Business Justification]", swtxtBJ.Text);
        return textbodyemtormsw;
    }

    //hw mails sending (To RM)
    protected string EmpReq_toRM_mailcontent_HW()
    {
        try
        {
            string textbodyemtorm = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/Mail Content Pages/Mail Content Employee Request to RM HW.html")))
            {
                textbodyemtorm = reader.ReadToEnd();
            }
            textbodyemtorm = textbodyemtorm.Replace("[Employee]", Session["SessionEmployeeName"].ToString());
            textbodyemtorm = textbodyemtorm.Replace("[Asset type]", "Hardware Assets");
            textbodyemtorm = textbodyemtorm.Replace("[Asset Name]", hwlbl2.Text);
            textbodyemtorm = textbodyemtorm.Replace("[Model]", hwlbl4.Text);
            textbodyemtorm = textbodyemtorm.Replace("[Quantity]", "1");
            textbodyemtorm = textbodyemtorm.Replace("[Business Justification]", hwtxtBJ.Text);
            return textbodyemtorm;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //sw mails sending (To EMP)
    protected string EmpReq_toEmp_mailcontent_SW()
    {
        //Temporary retrieving data from EART table for EmployeeRequestID
        try
        {
            SqlConnection tempcon = new SqlConnection(constr);
            tempcon.Open();
            SqlCommand tempcmd = new SqlCommand("select * from EmployeeAssetRequestTable where AssetID='" + swlbl1.Text + "' and RequestStatus='Pending' and RequestedEmployeeMailId ='" + Session["SessionUserMailID"] + "'", tempcon);
            SqlDataReader tempereader = tempcmd.ExecuteReader();
            tempereader.Read();
            string textbodyemtoem = string.Empty.ToString();
            if (tempereader.HasRows)
            {
                using (StreamReader reader = new StreamReader(Server.MapPath("~/Mail Content Pages/Mail Content Employee Request to Employee SW.html")))
                {
                    textbodyemtoem = reader.ReadToEnd();
                }
                textbodyemtoem = textbodyemtoem.Replace("[EmployeeName]", Session["SessionEmployeeName"].ToString());
                textbodyemtoem = textbodyemtoem.Replace("[EmpID]", Session["SessionEmployeeID"].ToString());
                textbodyemtoem = textbodyemtoem.Replace("[RequestID]", tempereader["EmployeeAssetRequestID"].ToString());
                textbodyemtoem = textbodyemtoem.Replace("[Asset type]", "Software Assets");
                textbodyemtoem = textbodyemtoem.Replace("[Asset Name]", swlbl2.Text);
                textbodyemtoem = textbodyemtoem.Replace("[Version]", swlbl3.Text);
                textbodyemtoem = textbodyemtoem.Replace("[Category]", swlbl5.Text);
                textbodyemtoem = textbodyemtoem.Replace("[Business Justification]", swtxtBJ.Text);
            }
            tempereader.Close();
            tempereader.Dispose();
            tempcon.Close();
            tempcon.Dispose();
            return textbodyemtoem;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //hw mails sending (To EMP)
    protected string EmpReq_toEmp_mailcontent_HW()
    {
        //Temporary retrieving data from EART table for EmployeeRequestI ID
        try
        {
            SqlConnection tempcon = new SqlConnection(constr);
            tempcon.Open();
            SqlCommand tempcmd = new SqlCommand("select * from EmployeeAssetRequestTable where AssetID='" + hwlbl1.Text + "' and RequestStatus='Pending' and RequestedEmployeeMailId ='" + Session["SessionUserMailID"] + "'", tempcon);
            SqlDataReader tempereader = tempcmd.ExecuteReader();
            tempereader.Read();
            string textbodyemtoem = string.Empty.ToString();
            if (tempereader.HasRows)
            {
                using (StreamReader reader = new StreamReader(Server.MapPath("~/Mail Content Pages/Mail Content Employee Request to Employee HW.html")))
                {
                    textbodyemtoem = reader.ReadToEnd();
                }

                textbodyemtoem = textbodyemtoem.Replace("[EmployeeName]", Session["SessionEmployeeName"].ToString());
                textbodyemtoem = textbodyemtoem.Replace("[RequestID]", tempereader["EmployeeAssetRequestID"].ToString());
                textbodyemtoem = textbodyemtoem.Replace("[Asset type]", "Hardware Assets");
                textbodyemtoem = textbodyemtoem.Replace("[Asset Name]", hwlbl2.Text);
                textbodyemtoem = textbodyemtoem.Replace("[Model]", hwlbl4.Text);
                textbodyemtoem = textbodyemtoem.Replace("[Quantity]", "1");
                textbodyemtoem = textbodyemtoem.Replace("[Business Justification]", hwtxtBJ.Text);
            }
            tempereader.Close();
            tempereader.Dispose();
            tempcon.Close();
            tempcon.Dispose();
            return textbodyemtoem;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}