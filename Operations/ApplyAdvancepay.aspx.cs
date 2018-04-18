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

public partial class ApplyAdvancepay : System.Web.UI.Page
{

    public string LatestAdvanceID;
    public string Name;
    public string mailid;
    public string EmployeeID;
    public int i;
    DateTime TodayDate = DateTime.Now;
    DateTime ADVCompletedDate;
    DateTime RequestProcessdate;
    public string Appid;
    public string status;
    public string appname;
    public string appmail;
    public string cs = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
    double amount;
    double jhansi;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["SessionUserMailID"] != null && Session["SessionUserPosition"].ToString() == "Employee")
        {
            txtAmount.ReadOnly = true;
            txtgrosspay.ReadOnly = true;
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            SqlCommand cmd1 = new SqlCommand("Select salary from Employee where EmployeeID = '" + Session["SessionEmployeeID"].ToString() + "'", con);
            SqlDataReader dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                txtgrosspay.Text = (dr1["salary"].ToString());
            }
            dr1.Close();
            con.Close();
            amount = Convert.ToDouble(txtgrosspay.Text) * 0.2;
            jhansi = amount;
            txtAmount.Text = Amount.Text = amount.ToString();
            //RangeValidator1.MaximumValue = txtgrosspay.Text;
            //RangeValidator2.MaximumValue = 1.ToString();
            //maxvalue = Convert.ToInt32(grosspay.Text);
            Div1.Visible = false;
            UpdatePanel.Visible = false;
            alert.Visible = false;
            CalendarExtender1.StartDate = DateTime.Now.AddDays(3);


            //CalendarExtender1.EndDate = DateTime.Now.AddDays(3);
            // ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            //  RangeValidator3.MinimumValue = System.DateTime.Now.ToShortDateString();
            // RangeValidator3.MaximumValue = System.DateTime.Now.AddDays(3).ToShortDateString();
            Session["EmployeeID2"] = Session["SessionEmployeeID"];
            //if (Session["EmployeeID2"] != null)
            //{
            //    EmployeeID = Session["EmployeeID2"].ToString();
            //    //name.Text = EmployeeID;
            //}
            //else
            //{
            //    Response.Redirect("Login.aspx");
            //}

            Name = Session["SessionEmployeeName"].ToString();
            mailid = Session["SessionUserMailID"].ToString();

            if (!IsPostBack)
            {
                ViewState["Filter"] = "All";
                BindGrid();
            }

            //Copy your connection String here from the properties of the connection


            //SqlConnection con1 = new SqlConnection(@"Data Source=SNPL-4007; Database=ERSDB; Integrated Security=True");
            //string cs = ConfigurationManager.ConnectionStrings["ERSDBConnectionString"].ConnectionString;
            SqlConnection con1 = new SqlConnection(cs);
            con1.Open();
            int k = 0;
            string AdvanceRequestStatus1;
            DataTable dt63 = new DataTable();
            //  DataRow dr63;
            int j = 0;
            DataTable dt23 = new DataTable();
            DataRow dr23;
            int r = 0;
            SqlDataAdapter sda23 = new SqlDataAdapter("select max(ERSAdvanceID) from ERSAdvancePay where ERSEmployeeID = '" + Session["SessionEmployeeID"].ToString() + "'", con1);
            sda23.Fill(dt23);
            if (dt23.Rows.Count >= 1)
            {
                dr23 = dt23.Rows[r];
                LatestAdvanceID = Convert.ToString(dr23[0]);
                SqlDataAdapter sda3 = new SqlDataAdapter("Select AdvanceApproverActionDate from ERSAdvancePay where ERSEmployeeID='" + Session["SessionEmployeeID"] + "'and ERSAdvanceID='" + LatestAdvanceID + "'", con1);
                sda3.Fill(dt63);
                if (dt63.Rows.Count > 0)
                {

                    DataTable dt64 = new DataTable();
                    DataRow dr64;
                    SqlDataAdapter sda4 = new SqlDataAdapter("Select ERSAdavanceRequestStatus from ERSAdvancePay where ERSEmployeeID='" + Session["SessionEmployeeID"] + "'and ERSAdvanceID='" + LatestAdvanceID + "'", con1);
                    sda4.Fill(dt64);
                    if (dt64.Rows.Count > 0)
                    {
                        dr64 = dt64.Rows[k];
                        AdvanceRequestStatus1 = Convert.ToString(dr64[0]);
                        if (AdvanceRequestStatus1 == "Pending")
                        {
                            submit.Visible = false;
                            // NoteText.Text = "Note :";
                            DataTable dt65 = new DataTable();
                            DataRow dr65;
                            SqlDataAdapter sda5 = new SqlDataAdapter("Select AdvanceCompletedDate from ERSAdvancePay where ERSEmployeeID='" + Session["SessionEmployeeID"] + "'and ERSAdvanceID='" + LatestAdvanceID + "'", con1);
                            sda4.Fill(dt65);
                            if (dt65.Rows.Count > 0)
                            {
                                dr65 = dt65.Rows[i];
                                AdvancePayInfo.Visible = true;
                                AdvancePayInfo.Text = "You have already applied for Advance Pay";
                                //h4.Visible = true;
                                //ha.Visible = false;

                                txtgrosspay.Visible = false;
                                //one.Visible = false;
                                //two.Visible = false;
                                //three.Visible = false;
                                //four.Visible = false;
                                txtDescription.Visible = false;
                                txtAmount.Visible = false;
                                Label2.Visible = false;
                                advancegrosspay.Visible = false;
                                NoteLabel.Visible = false;
                                NoteText.Visible = false;
                                advdate.Visible = false;
                                //advdescription.Visible = false;
                                date.Visible = false;
                                advanceamount.Visible = false;
                                advgrosspay.Visible = false;
                            }
                        }
                        else
                        {
                            AdvancePaymethod();
                        }

                    }
                    else
                    {

                        // NoteText.Text = "Note:";
                        AdvancePayInfo.Text = "You are eligible for AdvancePay";
                    }
                }
                else if (dt63.Rows.Count == 1)
                {
                    //submit.Visible = false;
                    //NoteText.Text = "Note :";
                    //AdvancePayInfo.Text = "You Are Eligible for AdvancePay";
                }


            }
            else
            {
                submit.Visible = true;
                // NoteText.Text = "Note :";
                AdvancePayInfo.Text = "You Are Eligible for AdvancePay";
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }
    private void BindGrid()
    {
        //SqlConnection con = new SqlConnection(@"Data Source=SNPL-4007; Database=ERSDB; Integrated Security=True");
        //string cs = ConfigurationManager.ConnectionStrings["ERSDBConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(cs);
        DataTable dt = new DataTable();
        SqlDataAdapter sda = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand("sp_EmployeeAdavancePayGridview");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Filter", ViewState["Filter"].ToString());
        cmd.Parameters.AddWithValue("@EmployeeID", Session["SessionEmployeeID"].ToString());
        cmd.Parameters.AddWithValue("@EmployeeName", Name);
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


    private void Select()
    {
        //SqlConnection con = new SqlConnection(@"Data Source=SNPL-4007; DataBase=ERSDB;Integrated Security=true");
        SqlConnection con = new SqlConnection(cs);
        con.Open();
        SqlCommand cmd = new SqlCommand("select ERSAdavanceRequestStatus,ERSAdvanceApproverRemarks,ERSApplyDate from ERSAdvancePay where ERSAdvanceID = '" + advanceid.Text + "'", con);
        SqlDataReader myReader = cmd.ExecuteReader();
        if (myReader.Read())
        {
            advancestatus.Text = myReader["ERSAdavanceRequestStatus"].ToString();
            advanceremarks.Text = myReader["ERSAdvanceApproverRemarks"].ToString();
            advanceapplydate.Text = myReader["ERSApplyDate"].ToString();
            string s = advanceapplydate.Text;
            if (String.IsNullOrEmpty(s))
            {
                advanceapplydate.Text = s;
            }
            else
            {
                string r = s.Remove(s.Length - 11);
                advanceapplydate.Text = r;

            }
            myReader.Close();
            con.Close();
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);

    }

    protected void Unnamed_Click(object sender, EventArgs e)
    {

    }
    protected void submit_Click(object sender, EventArgs e)
    {
        try
        {
            //RangeValidator1.MaximumValue = txtgrosspay.Text;
            double a = Convert.ToDouble(txtgrosspay.Text);
            double b = Convert.ToDouble(txtAmount.Text);
            //RangeValidator1.Validate();
            if (b <= a)
            {
                if (Session["SessionEmployeeID"].ToString() == "SNPL-HYD-2048")
                {
                    Appid = "SNPL-HYD-2016";
                }
                else if (Session["SessionEmployeeID"].ToString() == "SNPL-HYD-2016")
                {
                    Appid = "SNPL-HYD-1107";
                }
                else
                {
                    Appid = "SNPL-HYD-2048";
                }
                DateTime ApplyDate = DateTime.Today;
                //SqlConnection con = new SqlConnection(@"Data Source =SNPL-4007;Database = ERSDB;Integrated Security = True;");
                SqlConnection con = new SqlConnection(cs);
                con.Open();
                SqlCommand cmd1 = new SqlCommand("Select ERSApproverName,ERSApproverMailID from ERSApprover where ERSApproverID='" + Appid + "'", con);

                SqlDataReader appdr = null;
                appdr = cmd1.ExecuteReader();
                if (appdr.Read())
                {
                    appname = appdr["ERSApproverName"].ToString();
                    appmail = appdr["ERSApproverMailID"].ToString();
                    appdr.Close();
                }
                SqlCommand cmd = new SqlCommand("Insert into ERSAdvancePay(ERSAdvanceReason,ERSGrossPay,ERSAdvanceAmount,ERSApproverName,ERSApproverMailID,RequestDate,ERSApplyDate,ERSAdavanceRequestStatus,ERSEmployeeID,ERSApproverID) Values(@var1, @var2, @var3,@var4,@var5, @var6, @var7, @var8, @var9,@var10)", con);
                cmd.Parameters.AddWithValue("@var1", txtDescription.Text);
                cmd.Parameters.AddWithValue("@var2", txtgrosspay.Text);
                cmd.Parameters.AddWithValue("@var3", txtAmount.Text);
                cmd.Parameters.AddWithValue("@var4", appname);
                cmd.Parameters.AddWithValue("@var5", appmail);
                cmd.Parameters.AddWithValue("@var6", date.Text);
                cmd.Parameters.AddWithValue("@var7", ApplyDate);
                cmd.Parameters.AddWithValue("@var8", "Pending");
                cmd.Parameters.AddWithValue("@var9", Session["SessionEmployeeID"].ToString());
                cmd.Parameters.AddWithValue("@var10", Appid);
                cmd.ExecuteNonQuery();
                //Fetching Advance Pay ID
                DataTable dt9 = new DataTable();
                DataRow dr9;
                int i = 0;
                SqlDataAdapter sd = new SqlDataAdapter("select max(ERSAdvanceID) from ERSAdvancePay where ERSEmployeeID='" + Session["SessionEmployeeID"].ToString() + "'", con);
                sd.Fill(dt9);
                if (dt9.Rows.Count >= 0)
                {

                    dr9 = dt9.Rows[i];
                    advanceid.Text = Convert.ToString(dr9[0]);
                    //Triggering Mail After Insertion 
                    string subject = "ERS-Advance Pay Acknowledgement";
                    string body = "Dear " + Name + ",\n\n<br /><br /> We Acknowledge your request for AdvancePay  with the following details\n\n<br /><br /> Advance Pay ID: \t" + advanceid.Text
+ "\n<br /> Apply Date: \t" + ApplyDate + "\n<br />Reason: \t " + txtDescription.Text +
"\n<br />Required Date : \t" + date.Text + "<br /><br />\n Amount: \t" + txtAmount.Text + "<br />\nAdvance Pay Status: \tPending\n\n" +
"\n<br />Request has been forwarded to the Approver<br /><br />\n\n\nNote:Please Mention Advance Pay ID in all Communications with us regarding this request" +
"\n\n\n <br /><br />please click here to <a href=\"http://192.168.0.105/Login.aspx\" > login</a><br /><br />Thanks And Regards\n<br />ERS Team\n<br />SNPL";
                    sendMailToEmployee(subject, body, mailid);

                    // ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Advancepay request is submitted');", true);
                    txtgrosspay.Text = string.Empty;
                    txtDescription.Text = string.Empty;
                    txtAmount.Text = string.Empty;
                    date.Text = string.Empty;
                    submit.Visible = false;
                    NoteText.Visible = false;
                    AdvancePayInfo.Visible = false;
                    con.Close();
                    alert.Visible = true;
                    alertmod.Style.Add("background-color", "#d7ecc6");
                    alert.Style.Add("background-color", "#d7ecc6");
                    Label5.ForeColor = System.Drawing.ColorTranslator.FromHtml("green");
                    Label6.ForeColor = System.Drawing.ColorTranslator.FromHtml("black");
                    Label5.Text = "success";
                    Label6.Text = "Advancepay request is submitted";
                    Response.Redirect("~/Operations/ApplyAdvancepay.aspx");
                    //BindGrid();
                    // Page_Load(sender, e);
                    //Response.Redirect("ClaimForm1.aspx");

                }
                else
                {
                    alertmod.Style.Add("background-color", "#d7ecc6");
                    alert.Style.Add("background-color", "#d7ecc6");
                    Label6.ForeColor = System.Drawing.ColorTranslator.FromHtml("green");
                    Label5.ForeColor = System.Drawing.ColorTranslator.FromHtml("black");
                    Label5.Text = "success";
                    Label6.Text = "Eligible for applying advancepay";

                    // ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Eligible for applying advancepay');", true);

                }



            }
            else
            {
                //Modalpopupextender1.Show();
                Modalpopupextender1.Focus();
                Label2.Text = "Amount should less than (or) equal to Grosspay";
                Label2.ForeColor = System.Drawing.ColorTranslator.FromHtml("Red");
                Label2.Visible = true;
                // ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Amount should less than (or) equal to Grosspay');", true);


                txtAmount.Text = string.Empty;
                txtAmount.Focus();

            }
        }

        catch (Exception ex)
        {
            Response.Write(ex.Message);
            throw ex;
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
            if (Session["EmployeeID2"].ToString() == "SNPL-HYD-2048")
            {
                MailAddress copy = new MailAddress("jhansilg_snpl@yahoo.com");
                mm.Bcc.Add(copy);
                //MailAddress copy1 = new MailAddress("vsn.murthy@supremenetsoft.com");
                //mm.CC.Add(copy1);

            }
            else if (Session["EmployeeID2"].ToString() == "SNPL-HYD-2016")
            {
                MailAddress copy = new MailAddress("jhansilg_snpl@yahoo.com");
                mm.Bcc.Add(copy);
                //MailAddress copy1 = new MailAddress("chowdary@supremesoft.net");
                //mm.CC.Add(copy1);
            }
            else
            {
                MailAddress copy = new MailAddress("jhansilg_snpl@yahoo.com");
                mm.Bcc.Add(copy);
                //MailAddress copy1 = new MailAddress("vsn.murthy@supremenetsoft.com");
                //mm.CC.Add(copy1);
            }
            NetworkCredential NetworkCred = new NetworkCredential("snplhelpdesk@gmail.com", "cmtoovnzlmtoiwja");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;
            smtp.Send(mm);
            //ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Email sent Successfull.');", true);

        }
    }
    private void AdvancePaymethod()
    {

        //SqlConnection con1 = new SqlConnection(@"Data Source=SNPL-4007; Database=ERSDB; Integrated Security=True");
        SqlConnection con1 = new SqlConnection(cs);
        int j = 0;
        DataTable dt22 = new DataTable();
        DataRow dr22;
        int k = 0;
        SqlDataAdapter sda22 = new SqlDataAdapter("select max(ERSAdvanceID) from ERSAdvancePay where ERSEmployeeID = '" + Session["EmployeeID2"].ToString() + "'", con1);
        sda22.Fill(dt22);
        if (dt22.Rows.Count > 1)
        {
            dr22 = dt22.Rows[k];
            LatestAdvanceID = Convert.ToString(dr22[0]);

            string AdvanceRequestStatus;
            DataTable dt60 = new DataTable();
            DataRow dr60;
            SqlDataAdapter sda = new SqlDataAdapter("Select AdvanceApproverActionDate from ERSAdvancePay where ERSEmployeeID='" + Session["EmployeeID2"] + "'and ERSAdvanceID='" + LatestAdvanceID + "'", con1);
            sda.Fill(dt60);
            if (dt60.Rows.Count > 0)
            {
                dr60 = dt60.Rows[j];
                DataTable dt61 = new DataTable();
                DataRow dr61;
                SqlDataAdapter sda1 = new SqlDataAdapter("Select ERSAdavanceRequestStatus from ERSAdvancePay where ERSEmployeeID='" + Session["SessionEmployeeID"] + "'and ERSAdvanceID='" + LatestAdvanceID + "'", con1);
                sda1.Fill(dt61);
                if (dt61.Rows.Count > 0)
                {
                    dr61 = dt61.Rows[j];
                    AdvanceRequestStatus = Convert.ToString(dr61[0]);
                    if (AdvanceRequestStatus == "Approved")
                    {
                        DataTable dt62 = new DataTable();
                        DataRow dr62;
                        SqlDataAdapter sda2 = new SqlDataAdapter("Select AdvanceCompletedDate from ERSAdvancePay where ERSEmployeeID='" + Session["SessionEmployeeID"] + "'and ERSAdvanceID='" + LatestAdvanceID + "'", con1);
                        sda2.Fill(dt62);
                        if (dt62.Rows.Count > 0)
                        {
                            dr62 = dt62.Rows[i];
                            if (dr62[0] != null)
                            {
                                AdvancePayInfo.Text = Convert.ToString(dr62[0]);
                                ADVCompletedDate = DateTime.Parse(AdvancePayInfo.Text);

                                NoteText.Text = "You are not eligible for AdvancePay ";
                                //  AdvancePayInfo.Text = dr62[0];
                            }
                            if (TodayDate >= ADVCompletedDate)
                            {
                                // NoteText.Text = "Note :";
                                AdvancePayInfo.Text = "You are Eligible for AdvancePay";

                            }
                            else if (TodayDate < ADVCompletedDate)
                            {
                                NoteText.Text = "Note :";
                                AdvancePayInfo.Text = "You have already taken AdvancePay";

                                submit.Visible = false;
                                //lgross.Enabled = false;
                                //AdvGrossPay.Enabled = false;
                                AdvancePayInfo.Enabled = false;
                                AdvAmount.Enabled = false;
                                advancegrosspay.Enabled = false;
                                advdate.Visible = false;
                                //advdescription.Visible = false;
                                date.Visible = false;
                                advanceamount.Visible = false;
                                advgrosspay.Visible = false;

                            }
                        }
                    }
                    else if (AdvanceRequestStatus == "Rejected")
                    {
                        NoteText.Text = "Note :";
                        AdvancePayInfo.Text = "You are Eligible  to make another request";
                    }
                }
                else
                {
                    NoteText.Text = "Note :";
                    AdvancePayInfo.Text = "You are Eligible for AdvancePay";
                }
            }
            else
            {
                NoteText.Visible = false;
                AdvancePayInfo.Visible = false;
                submit.Visible = false;
            }
        }
        else
        {
            // NoteText.Text = "Note :";
            AdvancePayInfo.Text = "You are Eligible for AdvancePay";
        }

    }
    protected void yes_click(object sender, EventArgs e)
    {
        Label1.Text = "yes";
        Div1.Visible = false;
        Delete_Advancepay();

    }
    protected void no_click(object sender, EventArgs e)
    {
        Label1.Text = "no";
		BindGrid();
    }
    protected void Delete_Advancepay()
    {
        Div1.Visible = true;

        if (Label1.Text == "yes")
        {
            // this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked YES!')", true);
            Div1.Visible = false;
            hai();
            alert.Visible = true;
            Label1.Text = string.Empty;
            BindGrid();

        }
    }
    protected void hai()
    {

        SqlConnection con = new SqlConnection(cs);
        con.Open();
        SqlCommand cmd = new SqlCommand("select ERSAdavanceRequestStatus from ERSAdvancepay where ERSAdvanceID='" + advanceid.Text + "'", con);
        SqlDataReader myReader = cmd.ExecuteReader();
        if (myReader.Read())
        {
            status = myReader["ERSAdavanceRequestStatus"].ToString();
            myReader.Close();
        }
        //con.Close();
        if (status == "Pending")
        {
            SqlCommand cmd1 = new SqlCommand("delete from ERSAdvancepay where ERSAdvanceID='" + advanceid.Text + "'", con);
            cmd1.ExecuteNonQuery();
            con.Close();
            alertmod.Style.Add("background-color", "#d7ecc6");
            alert.Style.Add("background-color", "#d7ecc6");
            Label5.ForeColor = System.Drawing.ColorTranslator.FromHtml("green");
            Label6.ForeColor = System.Drawing.ColorTranslator.FromHtml("black");
            Label5.Text = "success";
            Label6.Text = "Advancepay has been deleted";
            alert.Visible = true;
        }
        else
        {
            alert.Visible = true;
            alertmod.Style.Add("background-color", "#ffc2b3");
            alert.Style.Add("background-color", "#ffc2b3");
            Label5.ForeColor = System.Drawing.ColorTranslator.FromHtml("red");
            Label6.ForeColor = System.Drawing.ColorTranslator.FromHtml("black");
            Label6.Text = "Advancepay has been processed can't delete it.";
            Label5.Text = "failure!";
        }


    }
    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlStatus = (DropDownList)sender;
        ViewState["Filter"] = ddlStatus.SelectedValue;
        this.BindGrid();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = GridView1.Rows[index];
        advanceid.Text = row.Cells[0].Text;
        advreason.Text = row.Cells[1].Text;
        advancegrosspay.Text = row.Cells[2].Text;
        //  AdvAmount.Text = row.Cells[3].Text;
        advancestatus.Text = row.Cells[4].Text;
        SqlConnection con = new SqlConnection(cs);
        con.Open();
        SqlCommand cmd = new SqlCommand("select ERSAdavanceRequestStatus,ERSApplyDate,ERSAdvanceAmount,ERSAdvanceApproverRemarks from ERSAdvancePay where ERSAdvanceID = '" + advanceid.Text + "'", con);
        SqlDataReader myReader = cmd.ExecuteReader();
        if (myReader.Read())
        {
            advancestatus.Text = myReader["ERSAdavanceRequestStatus"].ToString();
            AdvAmount.Text = myReader["ERSAdvanceAmount"].ToString();
            myReader.Close();
            con.Close();
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        Select();

        if (advancestatus.Text == "Approved" || advancestatus.Text == "Rejected")
        {
            // AdvanceStatus.Text = this.GridView1.SelectedRow.Cells[4].Text.ToString();
            advreason.ReadOnly = true;
            // AdvanceReason.Visible = false;
            advanceapplydate.Visible = true;
            advancestatus.Visible = true;
            // delete.Visible = false;
            AdvAmount.ReadOnly = true;
            update.Visible = false;
            advancegrosspay.Visible = true;
            advanceremarks.Visible = true;
            advremarks.Visible = true;
        }
        else
        {
            advreason.ReadOnly = false;
            AdvAmount.Visible = true;
            // AdvanceReason.Visible = false;
            advanceremarks.Visible = false;
            advanceapplydate.Visible = true;
            AdvAmount.ReadOnly = false;
            update.Visible = true;
            advancegrosspay.Visible = true;
            //delete.Visible = true;
            advanceremarks.Visible = false;
            advremarks.Visible = false;

        }
        if (e.CommandName == "Edit_Click")
        {
            MPE.Enabled = true;
            UpdatePanel.Visible = true;
            MPE.Show();
        }
        if (e.CommandName == "Delete_Click")
        {
            Delete_Advancepay();
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "Click to select the Claim";

        }
    }

    protected void update_Click(object sender, EventArgs e)
    {
        SqlConnection con1 = new SqlConnection(cs);
        //SqlConnection con = new SqlConnection(cs);
        con1.Open();
        SqlCommand cmd1 = new SqlCommand("Select ERSGrossPay from ERSAdvancePay where ERSAdvanceID='" + advanceid.Text + "'", con1);
        SqlDataReader myReader = cmd1.ExecuteReader();
        //RangeValidator2.MaximumValue = advancegrosspay.Text;
        if (myReader.Read())
        {
            Label7.Text = myReader["ERSGrossPay"].ToString();

            myReader.Close();
            // con.Close();
        }
        cmd1.ExecuteNonQuery();
        con1.Close();
        try
        {
            double c = Convert.ToDouble(txtAmount.Text);
            double d = Convert.ToDouble(AdvAmount.Text);
            if (d <= c)
            {
                //SqlConnection con = new SqlConnection(@"Data Source =SNPL-4007 ; Database = ERSDB ; Integrated Security = True;");
                SqlConnection con = new SqlConnection(cs);
                con.Open();
                SqlCommand cmd = new SqlCommand("update ERSAdvancePay set ERSAdvanceReason=@var1,ERSAdvanceAmount=@var2 where ERSAdvanceID=@var3", con);
                cmd.Parameters.AddWithValue("@var1", advreason.Text);
                cmd.Parameters.AddWithValue("@var2", AdvAmount.Text);
                cmd.Parameters.AddWithValue("@var3", advanceid.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                alertmod.Style.Add("background-color", "#d7ecc6");
                alert.Style.Add("background-color", "#d7ecc6");
                Label5.ForeColor = System.Drawing.ColorTranslator.FromHtml("green");
                Label6.ForeColor = System.Drawing.ColorTranslator.FromHtml("black");
                Label5.Text = "Success!";
                Label6.Text = "Advnacepay Details has been Updated";
                alert.Visible = true;
                //  ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Details  Updated ');", true);
                BindGrid();
            }
            else
            {
                if (Page.IsPostBack)
                {
                    MPE.Show();
                    Label3.Visible = true;
                    Label3.Text = "Amount should less than (or) equal to Grosspay";
                    AdvAmount.Focus();
                }

                //  MPE.Show();


            }
        }

        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    private void panelu()
    {
        if (Session.IsNewSession)
        {
            MPE.Hide();
        }
        else
        {
            MPE.Show();
        }
    }
    private void panel1()
    {
        if (Session.IsNewSession)
        {
            Modalpopupextender1.Hide();
        }
        else
        {
            Modalpopupextender1.Show();
        }
    }

    protected void txtAmount_TextChanged(object sender, EventArgs e)
    {
        if (Page.IsPostBack == true)
        {
            Modalpopupextender1.Show();
            if (Convert.ToInt32(txtgrosspay.Text) > Convert.ToInt32(txtAmount.Text))
            {
                Label2.Text = "Please Enter valid amount";
                txtAmount.Text = string.Empty.ToString();
                txtAmount.Focus();
            }
            else if (Convert.ToInt32(txtgrosspay.Text) < Convert.ToInt32(txtAmount.Text))
            {
                date.Focus();
            }
        }
    }


    protected void txtAmount_TextChanged1(object sender, EventArgs e)
    {

    }

    //protected void AdvAmount_TextChanged(object sender, EventArgs e)
    //{
        
    //        MPE.Show();
    //        double Amounr1 = Convert.ToDouble(AdvAmount.Text);
    //        if (Amounr1 > jhansi)
    //        {
    //            Label3.Text = "*";
    //        }
       


    //}
}