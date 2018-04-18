using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Operations_Default : System.Web.UI.Page
{
    public string constring = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
    //public string constring = ConfigurationManager.ConnectionStrings["ERSDBConnectionString"].ConnectionString;
    string Appid;
    string Appname;
    public string fromlevel;
    public string tolevel;
    protected void Page_Load(object sender, EventArgs e)
	{
       
        //if (Session["ApproverID2"] != null)
        //{
        //    Session["ApproverID2"].ToString();
        //}
        //else
        //{
        //    Response.Redirect("Login.aspx");
        //}
        if (!IsPostBack)
        {
            ViewState["Filter"] = "All";
            BindGrid();
        }
         Alert.Visible = false;
        SqlConnection con3 = new SqlConnection(constring);
        //con3.ConnectionString = @"Data Source=HI-PC\SQLEXPRESS; Database =ERSDB; Integrated Security=True";
        //Copy your connection String here from the properties of the connection

        try
        {
            using (con3)
            {
                con3.Open();
                //id = int.Parse(empid.Text);
                int k = 0;
                DataTable dt2 = new DataTable();
                DataRow dr2;
                SqlDataAdapter da2 = new SqlDataAdapter("Select ERSApproverName from ERSApprover where ERSApproverID='" + Session["SessionEmployeeID"].ToString() + "'", con3);
                da2.Fill(dt2);
                dr2 = dt2.Rows[k];
                //Label9.Text = Convert.ToString(dr2[0]);
                name.Text = Convert.ToString(dr2[0]);
                // cmd2.ExecuteNonQuery();
                con3.Close();
            }
        }
        catch (Exception ex)
        {
            Console.Write(ex.Message);
        }
    }
    private void BindGrid()
    {
        //SqlConnection con = new SqlConnection(@"Data Source=HI-PC\SQLEXPRESS; Database =ERSDB; Integrated Security=True");
        SqlConnection con = new SqlConnection(constring);
        DataTable dt = new DataTable();
        SqlDataAdapter sda = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand("sp_AdavancePayGridview");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Filter", ViewState["Filter"].ToString());
        cmd.Parameters.AddWithValue("@ApproverID", Session["SessionEmployeeID"].ToString());
        cmd.Connection = con;
        sda.SelectCommand = cmd;
        sda.Fill(dt);
        GridView1.DataSource = dt;
        GridView1.DataBind();
        if (GridView1.Rows.Count > 0)
        {
            GridView1.CssClass = "table table-bordered table-striped  table-inverse nomargin";
            GridView1.UseAccessibleHeader = true;
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            GridView1.FooterRow.TableSection = TableRowSection.TableFooter;
        }
       
    }
  
    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlStatus = (DropDownList)sender;
        ViewState["Filter"] = ddlStatus.SelectedValue;
        this.BindGrid();
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        DateTime ProcessDate;
        DateTime AdvancePayCompletedDate;
        string Advanceemployeeid;
        //Update Code Write Here

        SqlConnection con1 = new SqlConnection(constring);
        DataTable dt = new DataTable();
        con1.Open();
        DateTime date = DateTime.Today;
        date = Convert.ToDateTime(date.ToShortDateString());
        //Fetching Claim Details from ERSClaim Table based on AdvanceID for Mail Triggering
        int i = 0;
        DataTable dt3 = new DataTable();
        DataRow dr3;
        SqlDataAdapter da3 = new SqlDataAdapter("Select ERSAdvanceReason,ERSGrossPay,ERSAdvanceAmount,ERSApproverName,ERSApproverMailID,ERSAdvanceRequestProcessDate,ERSAdvanceApproverRemarks,ERSApplyDate,ERSAdavanceRequestStatus,ERSApproverID,ERSEmployeeID from ERSAdvancePay where ERSApproverID='" + Session["SessionEmployeeID"].ToString() + "'and ERSAdvanceID='" + AdvanceID.Text + "'", con1);
        da3.Fill(dt3);
        if (dt3.Rows.Count > 0)
        {
            dr3 = dt3.Rows[i];
            string Advanceid = AdvanceID.Text;
            string reason = Convert.ToString(dr3[0]);
            string grosspay = Convert.ToString(dr3[1]);
            string Amount = Convert.ToString(dr3[2]);
            string name = Convert.ToString(dr3[3]);
            string mailiD = Convert.ToString(dr3[4]);
            string remarks = Convert.ToString(dr3[6]);
            string Applydate = Convert.ToString(dr3[7]);
            string Status = Convert.ToString(dr3[8]);
            string Approverid = Convert.ToString(dr3[9]);
            Advanceemployeeid = Convert.ToString(dr3[10]);

             string ename = Session["SessionEmployeeName"].ToString();
            string mailid = Session["SessionUserMailID"].ToString();


            if (ActionDropDown.SelectedValue == "Approved")
            {
                // Action.Visible = false;
                ActionDropDown.Visible = false;
                SqlDataReader myReader = null;
                ProcessDate = DateTime.Now;
                AdvancePayCompletedDate = ProcessDate.AddMonths(4);
                SqlCommand myCommand = new SqlCommand("update ERSAdvancePay set ERSAdavanceRequestStatus=@var2 ,ERSAdvanceApproverRemarks=@var3,AdvanceCompletedDate=@var5,AdvanceApproverActionDate=@var6 where ERSAdvanceID='" + AdvanceID.Text + "'", con1);

                myCommand.Parameters.AddWithValue("@var2", ActionDropDown.SelectedValue);
                myCommand.Parameters.AddWithValue("@var3", AdvanceRemarks.Text);
                myCommand.Parameters.AddWithValue("@var5", AdvancePayCompletedDate);
                myCommand.Parameters.AddWithValue("@var6", ProcessDate);
                //myCommand.Parameters.AddWithValue("@var4", "SNPL2048");
                myReader = myCommand.ExecuteReader();
                // Triggering a Mail to Employee Regarding Claim Status
                string subject = "ERS-Advance Pay Acknowledgement  Approved at <Level 1>";
                string body = "Dear  " + " " + ename + ",<br /><br />Your Advance Pay request with the following details has been Approved by Approver<br /><br /> AdvancePayID: " + Advanceid
                    + "<br />Claim Date: " + Applydate +
                    "<br />Claim Reason: " + reason + "<br />Claim Amount: " + Amount + "<br /><br />Claim Status: \tApproved<br /><br /> " + "Approver Remarks: " + AdvanceRemarks.Text +
                    "<br />Process Date: " + ProcessDate + "<br /> Approver: " + name +
                    "<br />Please click here to  <a href=\"http://192.168.0.105/Login.aspx\">login</a>" +
                    "<br /><br /><br />Thanks And Regards<br />ERS Team<br /><br />SNPL";
                sendMailToEmployee(subject, body, mailid);
                alertmodal.Style.Add("background-color", "#d7ecc6");
                Alert.Style.Add("background-color", "#d7ecc6");
                Label1.ForeColor = System.Drawing.ColorTranslator.FromHtml("green");
                Label6.ForeColor = System.Drawing.ColorTranslator.FromHtml("black");

                Label6.Text = "Advancepay has been approved";
                Label1.Text = "Success!";
                Alert.Visible = true;
                grid();
                // ClientScript.RegisterStartupScript(GetType(), "Alert", "Alert(' Advancepay has been approved');", true);


            }

            else if (ActionDropDown.SelectedValue == "Rejected")
            {
                //Action.Visible = false;
                ActionDropDown.Visible = false;

                SqlDataReader myReader = null;
                ProcessDate = DateTime.Now;
                AdvancePayCompletedDate = ProcessDate.AddMonths(4);

                //string Remarks = Request.Form["TextArea1"];
                //Label3.Text = "UnAuthorized Employee";
                SqlCommand myCommand = new SqlCommand("update ERSAdvancePay set ERSAdvanceRequestProcessDate=@var1 ,ERSAdavanceRequestStatus=@var2 ,ERSAdvanceApproverRemarks=@var3 where ERSAdvanceID='" + AdvanceID.Text + "'", con1);
                myCommand.Parameters.AddWithValue("@var1", ProcessDate);
                myCommand.Parameters.AddWithValue("@var2", ActionDropDown.SelectedValue);
                myCommand.Parameters.AddWithValue("@var3", AdvanceRemarks.Text);
                // myCommand.Parameters.AddWithValue("@var4", "SNPL2048");

                myReader = myCommand.ExecuteReader();
                //Triggering a Mail to Employee Regarding Claim Status
                // Triggering a Mail to Employee Regarding Claim Status
                string subject = "ERS-Advance Pay Acknowledgement Rejected at <Level 1>";
                string body = "Dear  " + " " + ename + ",<br /><br /> Your Claim request with the following details has been Rejected by Approver<br /><br /> Claim ID: " + Advanceid
                    + "<br />Apply Date:" + Applydate +
                    "<br />Claim Reason:" + reason + "<br />Claim Amount: <br />" + Amount + "\n<br />Claim Status: \tRejected\n\n<br /><br /> " + "Approver Remarks:" + AdvanceRemarks.Text +
                    "\n<br />Process Date:" + ProcessDate + "\n<br /> Approver: " + name +
                   "<br />Please click here to  <a href=\"http://192.168.0.105/Login.aspx\">login</a>" +
                    "<br /><br />Thanks And Regards\n<br />ERS Team\n<br />SNPL";
                sendMailToEmployee(subject, body, mailid);
                alertmodal.Style.Add("background-color", "#d7ecc6");
                Alert.Style.Add("background-color", "#d7ecc6");
                Label1.ForeColor = System.Drawing.ColorTranslator.FromHtml("green");
                Label6.ForeColor = System.Drawing.ColorTranslator.FromHtml("black");

                Label1.Text = "Success!";
                Label6.Text = "Advancepay has been rejected";
                Alert.Visible = true;
                grid();
                // ClientScript.RegisterStartupScript(GetType(), "Alert", "Alert('Advancepay has been rejected.');", true);
            }
            else if (ActionDropDown.SelectedValue == "Forward to Next Level")
            {
                SqlDataReader myReader = null;
                ProcessDate = DateTime.Now;
                AdvancePayCompletedDate = ProcessDate.AddMonths(4);


                //Start ... Code for find from Approver Action level and To Approver Action level while forwarding claim//
                if (Session["SessionEmployeeID"].ToString() == "SNPL-HYD-2048")
                {
                    Appname = "V S N Murthy";
                    Appid = "SNPL-HYD-2016";
                    fromlevel = "Level 1";
                    tolevel = "Level 2";
                }
                else if (Session["SessionEmployeeID"].ToString() == "SNPL-HYD-2016")
                {
                    Appname = "B V Chowdary";
                    Appid = "SNPL-HYD-1107";
                    fromlevel = "Level 2";
                    tolevel = "Level 3";
                }

                //End ... Code for find from Approver Action level and To Approver Action level while forwarding claim//


                //Label3.Text = "UnAuthorized Employee";
                // string Remarks = Request.Form["TextArea1"];
                SqlCommand myCommand = new SqlCommand("update ERSAdvancePay set ERSAdvanceRequestProcessDate=@var1 ,ERSAdavanceRequestStatus=@var2 ,ERSAdvanceApproverRemarks=@var3,ERSApproverID=@var4,ERSApproverName=@var5 where ERSAdvanceID='" + AdvanceID.Text + "'", con1);
                myCommand.Parameters.AddWithValue("@var1", ProcessDate);
                myCommand.Parameters.AddWithValue("@var2", "Pending");
                myCommand.Parameters.AddWithValue("@var3", AdvanceRemarks.Text);
                myCommand.Parameters.AddWithValue("@var4", Appid);
                myCommand.Parameters.AddWithValue("@var5", Appname);
                myReader = myCommand.ExecuteReader();
                //Triggering a Mail to Employee Regarding Claim Status
                string subject = "ERS-Advance Pay Acknowledgement Forwarded from <Level 1> To <Level 2>";
                string body = "Dear  " + " " + ename + ",\n\n<br /><br /> Your Claim request with the following details has been forwarded by Approver<br /><br />\n\n Claim ID" + Advanceid
                    + "<br />Claim Date:" + Applydate +
                    "<br />Claim Reason:" + reason + "<br />Claim Amount: " + Amount + "<br />Claim Status :\tForwarded To Next Level<br /> " + "<br />Approver Remarks: " + AdvanceRemarks.Text +
                    "<br />Process Date: " + ProcessDate + "<br /> Approver ID: " + name +
                    "<br />Please click here to  <a href=\"http://192.168.0.105/Login.aspx\">login</a>" +
                    "<br /><br /><br />Thanks & Regards<br />ERS Team<br />SNPL";
                sendMailToEmployee(subject, body, mailid);
                alertmodal.Style.Add("background-color", "#d7ecc6");
                Alert.Style.Add("background-color", "#d7ecc6");
                Label1.ForeColor = System.Drawing.ColorTranslator.FromHtml("green");
                Label6.ForeColor = System.Drawing.ColorTranslator.FromHtml("black");

                Label1.Text = "Success!";
                Label6.Text = "Advancepay has been Forwarded to next level of approver.";
                Alert.Visible = true;
                grid();
                // ClientScript.RegisterStartupScript(GetType(), "Alert", "Alert('Advancepay has been Forwarded to next level of approver.');", true);
            }
            AdvanceRemarks.Text = string.Empty;
            ActionDropDown.ClearSelection();
            con1.Close();
            BindGrid();

        }
        else
        {

        }
    }
    private void grid()
    {
        if (Session.IsNewSession)
        {
            BindGrid();
        }
        else
        {
            BindGrid();
        }
    }
    protected void sendMailToEmployee(string subject, string body, string mail)
    {
        using (MailMessage mm = new MailMessage("snplhelpdesk@gmail.com", mail))
        {

            mm.Subject = subject;

            string str = @"<html><body>" + body + "<br /><img src=\"cid:image1\"></body></html>";
            AlternateView av = AlternateView.CreateAlternateViewFromString(str, null, System.Net.Mime.MediaTypeNames.Text.Html);
            LinkedResource lr = new LinkedResource("images/logo1.png", MediaTypeNames.Image.Jpeg);
            lr.ContentId = "image1";
            av.LinkedResources.Add(lr);
            mm.AlternateViews.Add(av);
            mm.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            NetworkCredential NetworkCred = new NetworkCredential("snplhelpdesk@gmail.com", "cmtoovnzlmtoiwja");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;
            smtp.Send(mm);
            //ClientScript.RegisterStartupScript(GetType(), "Alert", "Alert('Email sent Successfull.');", true);

        }
    }




    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Style["display"] = "none";
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "Click to select the Claim";

        }

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = GridView1.Rows[index];
        AdvanceID.Text = row.Cells[1].Text;
        advancepayid.Text = AdvanceID.Text;
        advanceamount.Text = row.Cells[2].Text;
        status.Text = row.Cells[3].Text;
        advancereason.Text = row.Cells[4].Text;
        empname.Text = row.Cells[5].Text;
        //SqlConnection con = new SqlConnection(@"Data Source=HI-PC\SQLEXPRESS; Database =ERSDB;Integrated Security=true");
        SqlConnection con = new SqlConnection(constring);
        con.Open();
        SqlCommand cmd = new SqlCommand("select ERSAdvanceApproverRemarks,ERSApplyDate,ERSAdvanceRequestProcessDate,ERSAdavanceRequestStatus from ERSAdvancePay where ERSAdvanceID = '" + AdvanceID.Text + "'", con);
        SqlDataReader myReader = cmd.ExecuteReader();
        if (myReader.Read())
        {

            processdate.Text = myReader["ERSAdvanceRequestProcessDate"].ToString();
            advanceapplydate.Text = myReader["ERSApplyDate"].ToString();
            AdvanceRemarks.Text = myReader["ERSAdvanceApproverRemarks"].ToString();
            AdvanceclaimStatus.Text = myReader["ERSAdavanceRequestStatus"].ToString();
            myReader.Close();
            con.Close();
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        //Code to hide "Forward To Next Level" for ApproverID "SNPL1107"

        if (Session["SessionEmployeeID"].ToString() == "SNPL-HYD-1107")
        {

            //this.ActionDropDown.Items.FindByValue("1").Enabled = false;
            ActionDropDown.Items.Remove("Forward to Next Level");

        }

        if (AdvanceclaimStatus.Text == "Approved" || AdvanceclaimStatus.Text == "Rejected")
        {
            AdvanceclaimStatus.Text = row.Cells[2].Text;
            ActionDropDown.Visible = false;
            processdate.Visible = true;
            aprocessdate.Visible = true;
            aaction.Visible = false;
            Submit.Visible = false;
            AdvanceRemarks.ReadOnly = true;
            aremarks.Visible = true;
            AdvanceRemarks.BorderStyle = BorderStyle.None;
            AdvanceRemarks.BorderColor = System.Drawing.Color.WhiteSmoke;
            AdvanceRemarks.BackColor = System.Drawing.Color.WhiteSmoke;

            //claimAmount.ReadOnly = true;
            //UserRemarks.ReadOnly = true;
            //ClaimUpdate.Visible = false;
        }
        else
        {
            processdate.Visible = false;
            aprocessdate.Visible = false;
            ActionDropDown.Visible = true;
            aaction.Visible = true;
            Submit.Visible = true;
            AdvanceRemarks.ReadOnly = false;
            aremarks.Visible = true;
            AdvanceRemarks.BorderStyle = BorderStyle.Solid;
            AdvanceRemarks.BorderColor = System.Drawing.Color.LightGray;
            AdvanceRemarks.BackColor = System.Drawing.Color.White;

        }
        if (e.CommandName == "Edit_Click")
        {
            MPE.Enabled = true;
            UpdatePanel.Visible = true;
            MPE.Show();
        }
    }
}