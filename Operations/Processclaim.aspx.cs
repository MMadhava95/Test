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
    public string fromlevel;
    public string tolevel;
    public string actionlevel;
    public string type1;
    public string emplodesg;
    public string employee123;
    public string employeeid;
    public string Appid;
    public string constring = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
    string Appname;

    protected void Page_Load(object sender, EventArgs e)
	{
        Alert.Visible = false;
        
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
          SqlConnection con3 = new SqlConnection(constring);
        try
        {
            using (con3)
            {
                con3.Open();
                int k = 0;
                DataTable dt2 = new DataTable();
                DataRow dr2;
                SqlDataAdapter da2 = new SqlDataAdapter("Select ERSApproverName from ERSApprover where ERSApproverID='" + Session["SessionEmployeeID"].ToString() + "'", con3);
                da2.Fill(dt2);
                dr2 = dt2.Rows[k];
                Name.Text = Convert.ToString(dr2[0]);
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
        SqlConnection con = new SqlConnection(constring);
        DataTable dt = new DataTable();
        SqlDataAdapter sda = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand("sp_Approvergriddropdownlist");
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
     private void select(string claimtype)
    {
        SqlConnection con2 = new SqlConnection(constring);
        DataTable dt18 = new DataTable();
        DataRow dr18;
        int i = 0;
        SqlDataAdapter sd18 = new SqlDataAdapter("Select Employeelevel from Employee where EmployeeID='" + employee123 + "'", con2);
        sd18.Fill(dt18);
        if (dt18.Rows.Count > 0)
        {
            dr18 = dt18.Rows[i];
            emplodesg = Convert.ToString(dr18[0]);
        }
        //Fetching Eligibility based on Claim Type and Employee Designation
        if (claimtype == "Medical")
        {
            DataTable dt19 = new DataTable();
            DataRow dr19;
            SqlDataAdapter sd19 = new SqlDataAdapter("Select Medical from Eligibility2 where Roles='" + emplodesg + "'", con2);
            sd19.Fill(dt19);
            if (dt19.Rows.Count > 0)
            {
                dr19 = dt19.Rows[i];
                Label1.Text = Convert.ToString(dr19[0]);

            }
        }
        else if (claimtype == "TravelOutStation")
        {
            DataTable dt20 = new DataTable();
            DataRow dr20;
            SqlDataAdapter sd20 = new SqlDataAdapter("Select TravelOutStation from Eligibility2 where Roles='" + emplodesg + "'", con2);
            sd20.Fill(dt20);
            if (dt20.Rows.Count > 0)
            {
                dr20 = dt20.Rows[i];
                Label1.Text = Convert.ToString(dr20[0]);

            }
        }
        else if (claimtype == "Miscellaneous and Entertainment")
        {
            DataTable dt21 = new DataTable();
            DataRow dr21;
            SqlDataAdapter sd21 = new SqlDataAdapter("Select MiscellaneousAndEntertainmentExpenses from Eligibility2 where Roles='" + emplodesg + "'", con2);
            sd21.Fill(dt21);
            if (dt21.Rows.Count > 0)
            {
                dr21 = dt21.Rows[i];
                Label1.Text = Convert.ToString(dr21[0]);

            }
        }
        else if (claimtype == "Repairs and Maintenance")
        {
            DataTable dt22 = new DataTable();
            DataRow dr22;
            SqlDataAdapter sd22 = new SqlDataAdapter("Select RepairsAndMaintenance from Eligibility2 where Roles='" + emplodesg + "'", con2);
            sd22.Fill(dt22);
            if (dt22.Rows.Count > 0)
            {
                dr22 = dt22.Rows[i];
                Label1.Text = Convert.ToString(dr22[0]);

            }
        }
        else if (claimtype == "Local Travel")
        {
            DataTable dt23 = new DataTable();
            DataRow dr23;
            SqlDataAdapter sd23 = new SqlDataAdapter("Select LocalTravel from Eligibility2 where Roles='" + emplodesg + "'", con2);
            sd23.Fill(dt23);
            if (dt23.Rows.Count > 0)
            {
                dr23 = dt23.Rows[i];
                Label1.Text = Convert.ToString(dr23[0]);

            }
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
    protected void AssignDetails(string claimid3)
    {
        SqlConnection con2 = new SqlConnection(constring);
        DataTable dt29 = new DataTable();
        DataRow dr29;
        int i = 0;
        SqlDataAdapter sda29 = new SqlDataAdapter("select ERSClaimApproverRemarks from ERSClaim where ERSClaimID = '" + claimId.Text + "'", con2);
        sda29.Fill(dt29);
        if (dt29.Rows.Count > 0)
        {
            dr29 = dt29.Rows[i];
            LevelApproverRemarks.Text = Convert.ToString(dr29[0]);
        }
        else
        {
            LevelApproverRemarks.Text = "";
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

        SqlConnection con1 = new SqlConnection(constring);
        //SqlConnection con1 = new SqlConnection(@"Data Source=HI-PC\SQLEXPRESS; Database =ERSDB; Integrated Security=True");
        DataTable dt = new DataTable();
        con1.Open();
        DateTime date = DateTime.Now;
        date = Convert.ToDateTime(date.ToShortDateString());
        //Fetching Claim Details from ERSClaim Table based on ClaimID
        int i = 0;
        DataTable dt3 = new DataTable();
        DataRow dr3;
        SqlDataAdapter da3 = new SqlDataAdapter("Select ERSClaimType,ERSClaimDate,ERSBillAmount,ERSClaimStatus,ERSClaimApproverRemarks,ERSClaimProcessDate,ERSDescription,ERSApproverID,ERSEmployeeID from ERSClaim where ERSApproverID='" + Session["SessionEmployeeID"].ToString() + "'and ERSClaimID='" + claimId.Text + "'", con1);
        da3.Fill(dt3);
        dr3 = dt3.Rows[i];
        string claimid = claimId.Text;
        string Type = Convert.ToString(dr3[0]);
        string Date = Convert.ToString(dr3[1]);
        string BillAmount = Convert.ToString(dr3[2]);
        string ClaimStatus = Convert.ToString(dr3[3]);
        string ApproverRemarks = Convert.ToString(dr3[4]);
        // string ProcessDate = Convert.ToString(dr3[5]);
        string Description = Convert.ToString(dr3[6]);
        string Approverid = Convert.ToString(dr3[7]);
        employeeid = Convert.ToString(dr3[8]);
        DataTable dt4 = new DataTable();
        DataRow dr4;
        SqlDataAdapter da4 = new SqlDataAdapter("Select EmployeeName,EmployeeMailID from Employee where EmployeeID='" + employeeid + "'", con1);
        da4.Fill(dt4);
        dr4 = dt4.Rows[i];
        string ename = Convert.ToString(dr4[0]);
        string mailid = Convert.ToString(dr4[1]);
        if (Session["SessionEmployeeID"].ToString() == "SNPL-HYD-2048")
        {
            actionlevel = "Level 1";
        }
        else if (Session["SessionEmployeeID"].ToString() == "SNPL-HYD-2016")
        {
            actionlevel = "Level 2";
        }
        else if (Session["SessionEmployeeID"].ToString() == "SNPL-HYD-1107")
        {
            actionlevel = "Level 3";
        }
        if (ActionDropDown.SelectedValue == "Need Clarification")
        {
            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand("update ERSClaim set ERSClaimStatus=@var2 ,ERSClaimApproverRemarks=@var3 where ERSClaimID='" + claimId.Text + "'", con1);
            myCommand.Parameters.AddWithValue("@var2", ActionDropDown.SelectedValue);
            myCommand.Parameters.AddWithValue("@var3", remarks.Text);
            myReader = myCommand.ExecuteReader();

            // Triggering a Mail to Employee Regarding Claim Status
            string subject = "ERS- Claim Request Process Acknowledgement";
            string body = "Dear \t" + " " + ename + ",<br /><br /> Your Claim request with the following details should require clarification\n\n<br /><br /> Claim ID: \t" + claimid
                + "\n<br />Date: \t" + Date + "\n<br />Type: \t" + Type +
                "\n<br />Description: \t" + Description + "\n<br />Amount: \t" + BillAmount +
                "\n<br />Status: \t" + ActionDropDown.SelectedValue + "\n\n<br /><br />" +
                "\n<br /> Approver ID: \t" + Approverid + "\n<br />Approver Remarks: \t" + remarks.Text +
                "<br />Please click here to  <a href=\"http://192.168.0.105/Login.aspx\">login</a>" +
                "\n\n\n<br /><br />Thanks And Regards<br />\nERS Team<br />\nSNPL";
            sendMailToEmployee(subject, body, mailid);
            ActionDropDown.ClearSelection();
            alertmodal.Style.Add("background-color", "#d7ecc6");
            Alert.Style.Add("background-color", "#d7ecc6");
            Label7.ForeColor = System.Drawing.ColorTranslator.FromHtml("green");
            Label9.ForeColor = System.Drawing.ColorTranslator.FromHtml("black");
            //Label2.Text = "Success!";
            //Label5.Text = "Employee has been Deleted";
            //alert.Visible = true;
            remarks.Text = " ";
            // suc.Visible = true;
            Label7.Text = "Success!";
            Label9.Text = "Clarification request is sent ";
            Alert.Visible = true;
            grid();
            // ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Clarification request is sent ');", true);
        }
        //Start ..... Code for find Approver Action Level//
        if (ActionDropDown.SelectedValue == "Approved")
        {
           

            //End ..... Code for find Approver Action Level//
            DateTime CPdate = DateTime.Now;
            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand("update ERSClaim set ERSClaimProcessDate=@var1 ,ERSClaimStatus=@var2 ,ERSClaimApproverRemarks=@var3 where ERSClaimID='" + claimId.Text + "'", con1);
            myCommand.Parameters.AddWithValue("@var1", CPdate);
            myCommand.Parameters.AddWithValue("@var2", ActionDropDown.SelectedValue);
            myCommand.Parameters.AddWithValue("@var3", remarks.Text);
            myReader = myCommand.ExecuteReader();

            // Triggering a Mail to Employee Regarding Claim Status
            string subject = "ERS-Claim Request Process Acknowledgement";
            string body = "Dear \t" + " " + ename + ",<br /><br /> Your Claim request with the following details has been Approved by Approver\n\n<br /><br /> Claim ID: \t" + claimid
                + "\n<br />Date: \t" + Date + "\n<br />Type: \t" + Type +
                "\n<br />Description: \t" + Description + "\n<br />Amount: \t" + BillAmount +
                "\n<br />Status: \t" + ActionDropDown.SelectedValue + "\n\n<br /><br />" + "\n<br />Approver Remarks: \t" + remarks.Text +
                "\n<br />Process Date: \t" + CPdate + "\n<br /> Approver ID: \t" + Approverid +
                "<br />Please click here to  <a href=\"http://192.168.0.105/Login.aspx\">login</a>" +
                "\n\n\n<br /><br />Thanks And Regards<br />\nERS Team<br />\nSNPL";
            sendMailToEmployee(subject, body, mailid);
            ActionDropDown.ClearSelection();
            remarks.Text = " ";
            // suc.Visible = true;
            alertmodal.Style.Add("background-color", "#d7ecc6");
            Alert.Style.Add("background-color", "#d7ecc6");
            Label7.ForeColor = System.Drawing.ColorTranslator.FromHtml("green");
            Label9.ForeColor = System.Drawing.ColorTranslator.FromHtml("black");
            //Label2.Text = "Success!";
            //Label5.Text = "Employee has been Deleted";
            //alert.Visible = true;
            Label7.Text = "Success!";
            Label9.Text = "Claim has been Approved";
            Alert.Visible = true;
            grid();
            // ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Claim has been Approved');", true);

            //Response.Redirect("ApproverDashboard.aspx");//Redirecting to Dashboard After Action Done

        }
        else if (ActionDropDown.SelectedValue == "Rejected")
        {
            //Start ...... Code for find Approver Action Level//
            //if (Session["ApproverID2"].ToString() == "SNPL-HYD-2048")
            //{
            //    actionlevel = "Level 1";
            //}
            //else if (Session["ApproverID2"].ToString() == "SNPL-HYD-2016")
            //{
            //    actionlevel = "Level 2";
            //}
            //else if (Session["ApproverID2"].ToString() == "SNPL-HYD-1107")
            //{
            //    actionlevel = "Level 3";
            //}
            //End .... Code for find Approver Action Level//

            DateTime CPdate = DateTime.Now;
            SqlDataReader myReader = null;
            //string Remarks = Request.Form["TextArea1"];
            //Label3.Text = "UnAuthorized Employee";
            SqlCommand myCommand = new SqlCommand("update ERSClaim set ERSClaimProcessDate=@var1 ,ERSClaimStatus=@var2 ,ERSClaimApproverRemarks=@var3 where ERSClaimID='" + claimId.Text + "'", con1);
            myCommand.Parameters.AddWithValue("@var1", CPdate);
            myCommand.Parameters.AddWithValue("@var2", ActionDropDown.SelectedValue);
            myCommand.Parameters.AddWithValue("@var3", remarks.Text);
            myReader = myCommand.ExecuteReader();
            //Triggering a Mail to Employee Regarding Claim Status
            // Triggering a Mail to Employee Regarding Claim Status
            string subject = "ERS-Claim Request Process Acknowledgement ";
            string body = "Dear \t" + " " + ename + ",\n\n<br /><br /> Your Claim request with the following details has been Rejected by Approver\n\n<br /><br /> Claim ID:\t" + claimid
                + "\n<br />Date: \t" + Date + "\n<br />Type: \t" + Type +
                "\n<br />Description: \t" + Description + "\n<br />Amount: \t" + BillAmount + "\n<br />Status: \t" + ActionDropDown.SelectedValue + "\n\n<br />" + "\n<br />Approver Remarks: \t" + remarks.Text +
                "\n<br />Process Date: \t" + CPdate + "\n<br /> Approver ID: \t" + Approverid +
                "<br /><br />Please click here to  <a href=\"http://192.168.0.105/Login.aspx\">login</a>" +
                "\n\n\n<br /><br />Thanks And Regards\n<br />ERS Team<br />\nSNPL";
            sendMailToEmployee(subject, body, mailid);
            ActionDropDown.ClearSelection();
            remarks.Text = " ";
            // suc.Visible = true;
            alertmodal.Style.Add("background-color", "#d7ecc6");
            Alert.Style.Add("background-color", "#d7ecc6");
            Label7.ForeColor = System.Drawing.ColorTranslator.FromHtml("green");
            Label9.ForeColor = System.Drawing.ColorTranslator.FromHtml("black");
            //Label3.Text = "Success!";
            //Label5.Text = "Employee has been Deleted";
            //alert.Visible = true;
            Label7.Text = "Success!";
            Label9.Text = "Claim has been Rejected";
            Alert.Visible = true;
            grid();
            // ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Claim has been Rejected');", true);
            //Response.Redirect("ApproverDashboard.aspx");//Redirecting to Dashboard After Action Done
        }
        else if (ActionDropDown.SelectedValue == "Forward to Next Level")
        {
            DateTime CPdate = DateTime.Now;
            SqlDataReader myReader = null;
            //Label3.Text = "UnAuthorized Employee";
            // string Remarks = Request.Form["TextArea1"];

            //Start ... Code for find from Approver Action level and To Approver Action level while forwarding claim//
            if (Session["SessionEmployeeID"].ToString() == "SNPL-HYD-2048")
            {
                //Appname = "V S N Murthy";
                Appid = "SNPL-HYD-2016";
                fromlevel = "Level 1";
                tolevel = "Level 2";
            }
            else if (Session["SessionEmployeeID"].ToString() == "SNPL-HYD-2016")
            {
                //Appname = "B V Chowdary";
                Appid = "SNPL-HYD-1107";
                fromlevel = "Level 2";
                tolevel = "Level 3";
            }

            //End ... Code for find from Approver Action level and To Approver Action level while forwarding claim//

            SqlCommand myCommand = new SqlCommand("update ERSClaim set ERSClaimProcessDate=@var1 ,ERSClaimStatus=@var2 ,ERSClaimApproverRemarks=@var3,ERSApproverID=@var4 where ERSClaimID='" + claimId.Text + "'", con1);
            myCommand.Parameters.AddWithValue("@var1", date);
            myCommand.Parameters.AddWithValue("@var2", "Pending");
            myCommand.Parameters.AddWithValue("@var3", remarks.Text);
            myCommand.Parameters.AddWithValue("@var4", Appid);
            //myCommand.Parameters.AddWithValue("@var5", Appname);
            myReader = myCommand.ExecuteReader();
            //Triggering a Mail to Employee Regarding Claim Status
            string subject = "ERS-Claim Request Process Acknowledgement";
            string body = "Dear \t" + " " + ename + ",<br /><br />\n\n Your Claim request with the following details has been Forwarded by Approver\n\n<br /><br /> Claim ID:\t" + claimid
                + "\n<br />Date: " + Date + "\n<br />Type: " + Type +
                "\n<br />Description: " + Description + "\n<br />Amount: " + BillAmount + "\n<br />Status: \t" + ActionDropDown.SelectedValue + "\n\n" + "\n<br />Approver Remarks: \t" + remarks.Text +
                "\n<br />Process Date: \t" + CPdate + "\n <br />Approver ID: \t" + Approverid +
               "<br /><br />Please click here to  <a href=\"http://192.168.0.105/Login.aspx\">login</a>" +
                "\n\n\n<br /><br />Thanks & Regards\n<br />ERS Team<br />\nSNPL";
            sendMailToEmployee(subject, body, mailid);
            ActionDropDown.ClearSelection();
            remarks.Text = " ";
            alertmodal.Style.Add("background-color", "#d7ecc6");
            Alert.Style.Add("background-color", "#d7ecc6");
            Label7.ForeColor = System.Drawing.ColorTranslator.FromHtml("green");
            Label9.ForeColor = System.Drawing.ColorTranslator.FromHtml("black");
            //Label3.Text = "Success!";
            //Label5.Text = "Employee has been Deleted";
            //alert.Visible = true;
            Label7.Text = "Success!";
            Label9.Text = "Claim has been forwarded to next level";
            Alert.Visible = true;
            grid();
            // ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Claim has been farwoded to next level');", true);
            //Response.Redirect("ApproverDashboard.aspx");//Redirecting to Dashboard After Action Done
        }
        else
        {
            //alertmod.Style.Add("background-color", "#ee7600");
            //Label1.ForeColor = System.Drawing.Color.White;
            //Label5.ForeColor = System.Drawing.Color.White;
            //fai.Visible = true;
            //Label2.Text = "Failure!";
            //Label5.Text = "Please select Action type";
            //alert.Visible = true;
            grid();
            //ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please select Action type');", true);
            claimId.Focus();
        }
        con1.Close();
        BindGrid();

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
            MailAddress copy = new MailAddress("chakri_snpl@yahoo.com");
            mm.Bcc.Add(copy);
            mm.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            NetworkCredential NetworkCred = new NetworkCredential("snplhelpdesk@gmail.com", "cmtoovnzlmtoiwja");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;
            smtp.Send(mm);
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GridView1.Rows[index];
            claimId.Text = row.Cells[0].Text;
            claimtype.Text = row.Cells[1].Text;
            type1 = claimtype.Text;
            applieddate.Text = row.Cells[2].Text;
            claimstatus.Text = row.Cells[3].Text;
            Label8.Text = row.Cells[4].Text;
            if (Employee.Text != null)
            {
                //SqlConnection con1 = new SqlConnection(@"Data Source=HI-PC\SQLEXPRESS; Database =ERSDB;Integrated Security=true");
                SqlConnection con1 = new SqlConnection(constring);
                con1.Open();
                DataTable dt25 = new DataTable();
                DataRow dr25;
                int i = 0;
                SqlDataAdapter sda25 = new SqlDataAdapter("select ERSEmployeeID from ERSClaim where ERSClaimID = '" + claimId.Text + "'", con1);
                sda25.Fill(dt25);
                if (dt25.Rows.Count > 0)
                {
                    dr25 = dt25.Rows[i];
                    employee123 = Convert.ToString(dr25[0]);
                    select(type1);
                }

                SqlCommand cmd1 = new SqlCommand("select EmployeeName from Employee where EmployeeID = '" + employee123 + "'", con1);
                SqlDataReader myReader1 = cmd1.ExecuteReader();
                if (myReader1.Read())
                {
                    Employee.Text = myReader1["EmployeeName"].ToString();
                    myReader1.Close();
                }

                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                con1.Close();
            }
            SqlConnection con2 = new SqlConnection(constring);
            con2.Open();
            SqlCommand cmd = new SqlCommand("select ERSClaimStatus,ERSBillImage,ERSDescription from ERSClaim where ERSClaimID = '" + claimId.Text + "'", con2);
            SqlDataReader myReader = cmd.ExecuteReader();
            if (myReader.Read())
            {
                claimstatus.Text = myReader["ERSClaimStatus"].ToString();
                Label3.Text = myReader["ERSDescription"].ToString();
                byte[] imagedata = (byte[])myReader["ERSBillImage"];
                string img = Convert.ToBase64String(imagedata, 0, imagedata.Length);
                Image1.ImageUrl = "data:image/png;base64," + img;
                Image2.ImageUrl = "data:image/png;base64," + img;
                myReader.Close();
                con2.Close();
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con2.Close();
            //Logic to Disable FOrwarded to Next Approver Option in DropDownlist for Level 3 Approver

            if (Session["SessionEmployeeID"].ToString() == "SNPL-HYD-1107")
            {

                //this.ActionDropDown.Items.FindByValue("1").Enabled = false;
                ActionDropDown.Items.Remove("Forward to Next Level");

            }
            if (Session["SessionEmployeeID"].ToString() == "SNPL-HYD-2016")
            {
                string claimid3 = claimId.Text;
               // AppRemarks.Text = "Level1 Remarks";
				premarks.Visible = true;
                LevelApproverRemarks.Visible = true;
                AppRemarks.Visible = true;
                AssignDetails(claimid3);
                // lreason.Text = "Remarks";
            }
            else if (Session["SessionEmployeeID"].ToString() == "SNPL-HYD-1107")
            {
                string claimid3 = claimId.Text;
               // AppRemarks.Text = "Level2 Remarks";
				premarks.Visible = true;
                LevelApproverRemarks.Visible = true;
               // AppRemarks.Visible = true;
                AssignDetails(claimid3);
              
            }
            if (claimstatus.Text == "Pending")
            {
                ActionDropDown.Visible = true;
                dropdownaction.Visible = true;
                remarks.Visible = true;
                remarks.Text = string.Empty;
                remarks.ReadOnly = false;
                cprocessdate.Visible = false;
                ClaimProcessDate.Visible = false;
                Submit.Visible = true;
                remarks.BorderStyle = BorderStyle.Solid;
                remarks.BorderColor = System.Drawing.Color.LightGray;
                remarks.BackColor = System.Drawing.Color.White;
            }
            else if (claimstatus.Text == "Approved")
            {
                remarks.BorderStyle = BorderStyle.None;
                remarks.BorderColor = System.Drawing.Color.WhiteSmoke;
                remarks.BackColor = System.Drawing.Color.WhiteSmoke;
                SqlConnection apprcon = new SqlConnection(constring);
                apprcon.Open();
                int i = 0;
                DataTable dt12 = new DataTable();
                DataRow dr12;
                SqlDataAdapter sda12 = new SqlDataAdapter("select ERSClaimApproverRemarks,ERSClaimProcessDate from ERSClaim where ERSClaimID = '" + claimId.Text + "'and ERSApproverID='" + Session["SessionEmployeeID"] + "'", apprcon);
                sda12.Fill(dt12);
                if (dt12.Rows.Count > 0)
                {
                    dr12 = dt12.Rows[i];
                    // ActionDropDown.DataTextField = Convert.ToString(dr12[0]);
                    remarks.Text = Convert.ToString(dr12[0]);
                    ClaimProcessDate.Text = Convert.ToString(dr12[1]);
                    string s = ClaimProcessDate.Text;
                    if (String.IsNullOrEmpty(s))
                    {
                        ClaimProcessDate.Text = s;
                    }
                    else
                    {
                        string r = s.Remove(s.Length - 11);
                        ClaimProcessDate.Text = r;

                    }
                }
                ActionDropDown.Visible = false;
                dropdownaction.Visible = false;
                ClaimProcessDate.Visible = true;
                cprocessdate.Visible = true;
                remarks.Visible = true;
                remarks.ReadOnly = true;
                Submit.Visible = false;
                apprcon.Close();
            }
            else if (claimstatus.Text == "Rejected")
            {
                 SqlConnection rejcon = new SqlConnection(constring);
                rejcon.Open();
                int i = 0;
                DataTable dt12 = new DataTable();
                DataRow dr12;
                SqlDataAdapter sda12 = new SqlDataAdapter("select ERSClaimApproverRemarks,ERSClaimProcessDate from ERSClaim where ERSClaimID = '" + claimId.Text + "'and ERSApproverID='" + Session["SessionEmployeeID"] + "'", rejcon);
                sda12.Fill(dt12);
                if (dt12.Rows.Count > 0)
                {
                    dr12 = dt12.Rows[i];
                    //ctionDropDown.DataTextField = Convert.ToString(dr12[0]);
                    remarks.Text = Convert.ToString(dr12[0]);
                    ClaimProcessDate.Text = Convert.ToString(dr12[1]);
                }
                // laction.Visible = false;
                ActionDropDown.Visible = false;
                dropdownaction.Visible = false;
                //ActionDropDown.Enabled = false;
                // lreason.Visible = true;
                ClaimProcessDate.Visible = true;
                cprocessdate.Visible = true;
                remarks.Visible = true;
                remarks.ReadOnly = true;
                Submit.Visible = false;
                remarks.BorderColor = System.Drawing.Color.Transparent;
                remarks.BorderStyle = BorderStyle.None;
                remarks.BorderColor = System.Drawing.Color.WhiteSmoke;
                remarks.BackColor = System.Drawing.Color.WhiteSmoke;
                rejcon.Close();
            }
            if (claimstatus.Text == "Need Clarification")
            {
                // drop.Visible = true;
                //down.Visible = true;
                // laction.Visible = true;
                ActionDropDown.Visible = true;
                dropdownaction.Visible = true;
                // lreason.Visible = true;
                remarks.Visible = true;
                remarks.Text = string.Empty;
                remarks.ReadOnly = false;
                cprocessdate.Visible = false;
                ClaimProcessDate.Visible = false;
                Submit.Visible = true;

                remarks.BorderStyle = BorderStyle.Solid;
                remarks.BorderColor = System.Drawing.Color.LightGray;
                remarks.BackColor = System.Drawing.Color.White;
            }
            if (e.CommandName == "Edit_Click")
            {
                MPE.Enabled = true;
                updatePanel.Visible = true;
                MPE.Show();
            }
        }
        catch (Exception ex)
        {
            throw ex;
            //Response.Write(ex.Message);
        }
    }

    protected void GridView1_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "Click to select the Claim";
        }
    }
}