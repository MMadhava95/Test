using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ApplyClaim : System.Web.UI.Page
{
    public string AdvanceID;
    public string EmployeeID;
    public string mailid;
    public string Appid;
    DateTime FourDay;
    DateTime FiveDay;
    int i = 0;
    public string employeename;
    public string status;
    //public SqlConnection con = new SqlConnection(@"Data Source=SNPL-4007;Database=ERSDB;Integrated Security=True;");
    public string cs = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
	{
        if (Session["SessionUserMailID"] != null && Session["SessionUserPosition"].ToString() == "Employee")
        {
            // ImgPrv.Visible = false;
            //lnkbtnEdit.Attributes.Add("onClick", "return false;");
            Div1.Visible = false;
            alert.Visible = false;
            //UpdatePannel.Visible = false;
            //  ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            // Session["EmployeeID2"] = "SNPL-VIJ-4007";
            //if (Session["EmployeeID2"] != null)
            //{
            //    EmployeeID = Session["EmployeeID2"].ToString();
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
            try
            {
                //SqlConnection con = new SqlConnection(@"Data Source=SNPL-4007;Database=ERSDB;Integrated Security=True;");
                SqlConnection con = new SqlConnection(cs);
                con.Open();
                string str = "select * from Eligibility2";
                SqlCommand com = new SqlCommand(str, con);
                SqlDataReader reader = com.ExecuteReader();
                reader.Read();
                lblmjuniour.Text = reader["Medical"].ToString();
                reader.Read();
                lblmmiddle.Text = reader["Medical"].ToString();
                reader.Read();
                lblmseniour.Text = reader["Medical"].ToString();


                reader.Close();

                SqlDataReader reader1 = com.ExecuteReader();
                reader1.Read();
                lbltjuniour.Text = reader1["TravelOutStation"].ToString();
                reader1.Read();
                lbltmiddle.Text = reader1["TravelOutStation"].ToString();
                reader1.Read();
                lbltseniour.Text = reader1["TravelOutStation"].ToString();
                reader.Close();
                con.Close();
                con.Open();
                SqlDataReader reader2 = com.ExecuteReader();
                reader2.Read();
                lblmiscjuniour.Text = reader2["MiscellaneousAndEntertainmentExpenses"].ToString();
                reader2.Read();
                lblmiscmiddle.Text = reader2["MiscellaneousAndEntertainmentExpenses"].ToString();
                reader2.Read();
                lblmiscseniour.Text = reader2["MiscellaneousAndEntertainmentExpenses"].ToString();
                reader.Close();
                con.Close();
                con.Open();
                SqlDataReader reader3 = com.ExecuteReader();
                reader3.Read();
                lblrjuniour.Text = reader3["RepairsAndMaintenance"].ToString();
                reader3.Read();
                lblrmiddle.Text = reader3["RepairsAndMaintenance"].ToString();
                reader3.Read();
                lblrseniour.Text = reader3["RepairsAndMaintenance"].ToString();
                reader.Close();
                con.Close();
                con.Open();
                SqlDataReader reader4 = com.ExecuteReader();
                reader4.Read();
                lblljuniour.Text = reader4["LocalTravel"].ToString();
                reader4.Read();
                lbllmiddle.Text = reader4["LocalTravel"].ToString();
                reader4.Read();
                lbllseniour.Text = reader4["LocalTravel"].ToString();
                reader.Close();
                con.Close();

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }

            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "openModal();", true);
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
        
    }
    public void BindGrid()
    {
        //SqlConnection con = new SqlConnection(@"Data Source=SNPL-4007; Database=ERSDB; Integrated Security=True");
        SqlConnection con = new SqlConnection(cs);
        DataTable dt = new DataTable();
        SqlDataAdapter sda = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand("sp_griddropdownlist1");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Filter", ViewState["Filter"].ToString());
        cmd.Parameters.AddWithValue("@EmployeeID", Session["SessionEmployeeID"].ToString());
        cmd.Parameters.AddWithValue("@EmployeeName", Session["SessionEmployeeName"].ToString());
        cmd.Connection = con;
        sda.SelectCommand = cmd;
        sda.Fill(dt);
        GridView1.DataSource = dt;
        GridView1.DataBind();
        //DropDownList ddlStatus = (DropDownList)GridView1.HeaderRow.FindControl("ddlStatus");
        //this.BindStatuslist(ddlStatus);
        if (GridView1.Rows.Count > 0)
        {
            GridView1.CssClass = "table table-bordered table-striped  table-inverse nomargin";
            GridView1.UseAccessibleHeader = true;
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            GridView1.FooterRow.TableSection = TableRowSection.TableFooter;
        }

    }
    private void BindStatuslist(DropDownList ddlStatus)
    {
        //SqlConnection con = new SqlConnection(@"Data Source=SNPL-4007; Database=ERSDB; Integrated Security=True");
        SqlConnection con = new SqlConnection(cs);
        DataTable dt = new DataTable();
        SqlDataAdapter sda = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand("select distinct ERSClaimStatus from ERSClaim");

        cmd.Connection = con;
        con.Open();
        ddlStatus.DataTextField = "ERSClaimStatus";
        ddlStatus.DataValueField = "ERSClaimStatus";
        ddlStatus.DataBind();
        con.Close();
        ddlStatus.Items.FindByValue(ViewState["Filter"].ToString()).Selected = true;

    }

    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlStatus = (DropDownList)sender;
        ViewState["Filter"] = ddlStatus.SelectedValue;
        this.BindGrid();
    }


    private void select()
    {
        //SqlConnection con = new SqlConnection(@"Data Source = SNPL-4007 ; Database = ERSDB ; Integrated Security = True;");
        SqlConnection con = new SqlConnection(cs);
        con.Open();

        SqlCommand cmd = new SqlCommand("select ERSDescription,ERSClaimStatus,ERSClaimApproverRemarks,ERSClaimUserRemarks,ERSClaimProcessDate,ERSClaimStatus,ERSApproverID from ERSClaim where ERSClaimID = '" + claimID.Text + "'", con);
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {

            claimDescription.Text = reader["ERSDescription"].ToString();
            ApproverRemarks.Text = reader["ERSClaimApproverRemarks"].ToString();
            UserRemarks.Text = reader["ERSClaimUserRemarks"].ToString();
            ClaimProcessDate.Text = reader["ERSClaimProcessDate"].ToString();
            claimStatus.Text = reader["ERSClaimStatus"].ToString();

            reader.Close();

            con.Close();

        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
    }

    protected void grid1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "Click to select the Claim";

        }

    }


    private void greater(DateTime date, string desc, string claimid)
    {
        SqlConnection con1 = new SqlConnection(cs);
        con1.Open();
        SqlCommand myCommand = new SqlCommand("update ERSClaim  set ERSApproverID='SNPL-HYD-2016'  where ERSEmployeeID='" + Session["SessionEmployeeID"].ToString() + "'", con1);
        myCommand.ExecuteNonQuery();
        con1.Close();

        string subject = "Expenses Claim Acknowledgement";
        string body = "Dear \t" + name.Text + ",<br /><br />\n\n The below  Claim request has been forwarded to  your approver. <br />\n\n Claim ID: \t" + claimid
+ "<br />\n Date :\t" + date + "<br >\n Type :\t" + ClaimDropDown.SelectedValue +
"<br />\n Description: \t" + desc + "<br />\n Amount :\t" + txtBillAmount.Text + "<br />\n Status: \tPending\n\n" +
"\n\n<br /><br />Note :\tPlease Mention ClaimID in all Communications with us regarding the Claim" +
"<br />Please click here to  <a href=\"http://192.168.0.105/Login.aspx\" > login</a>" +
        "\n\n\n<br /><br />Thanks And Regards\n<br />ERS Team\n<br />SNPL";
        sendMailToEmployee(subject, body, mailid);
        alertmod.Style.Add("background-color", "#d7ecc6");
        alert.Style.Add("background-color", "#d7ecc6");
        Label5.ForeColor = System.Drawing.ColorTranslator.FromHtml("green");
        Label6.ForeColor = System.Drawing.ColorTranslator.FromHtml("black");
        Label5.Text = "Success!";
        Label6.Text = "your Claim Eligiblity Exceeded, Forwarded To Next Level";
        alert.Visible = true;
        //  ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' your Claim Eligiblity Exceeded, Forwarded To Next Level');", true);
        TextBox2.Text = string.Empty;

        txtBillAmount.Text = string.Empty;
        txtRemarks.Text = string.Empty;
        //FileUpload1.FileContent.Flush();
        Label2.Text = string.Empty;
        Label9.Text = string.Empty;
        ClaimDropDown.ClearSelection();
        currencydropdown.ClearSelection();
    }
    private void lesser(DateTime date, string desc, string claimid)
    {
        string subject = "Expenses Claim Acknowledgement";
        string body = "Dear\t" + name.Text + ",<br /><br />\n\n The below Claim request has been forwarded to your approver <br />\n\n Claim ID: \t" + claimid
+ "<br />\nClaim Date: \t" + date + "<br >\n Type: \t" + ClaimDropDown.SelectedValue +
"<br />\n Description: \t" + desc + "<br />\n Amount: \t" + txtBillAmount.Text + "<br />\n Status: \tPending\n\n" +
"\n\n<br /><br />Note :\tPlease Mention ClaimID in all Communications with us regarding the Claim" +
"<br />Please click here to  <a href=\"http://192.168.0.105/Login.aspx\" > login</a>" +
           "\n\n\n<br /><br />Thanks And Regards\n<br />ERS Team<br />\nSNPL";
        sendMailToEmployee(subject, body, mailid);

        alertmod.Style.Add("background-color", "#d7ecc6");
        alert.Style.Add("background-color", "#d7ecc6");
        Label5.ForeColor = System.Drawing.ColorTranslator.FromHtml("green");
        Label6.ForeColor = System.Drawing.ColorTranslator.FromHtml("black");
        Label5.Text = "Success!";
        Label6.Text = "Your claim details are submitted";
        alert.Visible = true;
        // ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Your claim details are submitted');", true);

        TextBox2.Text = string.Empty;
        txtBillAmount.Text = string.Empty;
        txtRemarks.Text = string.Empty;
        //FileUpload1.FileContent.Flush();
        currencydropdown.ClearSelection();
        ClaimDropDown.ClearSelection();
        Label2.Text = string.Empty;
        Label9.Text = string.Empty;
    }


    protected void submit_Click(object sender, EventArgs e)
    {
        if (ClaimDropDown.SelectedValue == "Select Claim Type")
        {

            //ClaimDropDown;
            // Button2.Enabled = false;

            // ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Please Select Expense type');", true);

        }
        else
        {
            ClaimDropDown.Attributes.Add("onblur", "javascript:validate()");
            TextBox2.Attributes.Add("onblur", "javascript:validate()");
            txtBillAmount.Attributes.Add("onblur", "javascript:validate()");
            FileUpload1.Attributes.Add("onblur", "javascript:validate()");
            //SqlConnection con = new SqlConnection(@"Data Source=SNPL-4007; Database=ERSDB; Integrated Security=True");
            SqlConnection con = new SqlConnection(cs);
			con.Open();
            //Copy your connection String here from the properties of the connection
            //SqlCommand cmd;
            try
            {


                HttpPostedFile postedFile = FileUpload1.PostedFile;
                string FileName = Path.GetFileName(postedFile.FileName);
                string FileExtension = Path.GetExtension(FileName);
                double billamount1 = Convert.ToDouble(txtBillAmount.Text);//BillAmount

                if (FileExtension.ToLower() == ".jpg" || FileExtension.ToLower() == ".bmp" ||
                    FileExtension.ToLower() == ".gif" || FileExtension.ToLower() == ".png" || billamount1 > 0)
                {
                    //BillMandatory();
                    Stream Stream = postedFile.InputStream;
                    BinaryReader binaryReader = new System.IO.BinaryReader(Stream);
                    Byte[] bytes = binaryReader.ReadBytes((int)Stream.Length);
                    // imgDetail.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(bytes, 0, bytes.Length);
                    //imgDetail.Visible = true;
                    //btnupload_Click( bytes,e);

                    mailid = Session["SessionUserMailID"].ToString();
                        DateTime date = DateTime.Now;
                        // date = Convert.ToDateTime(date.ToShortDateString());
                        string desc = TextBox2.Text;

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

                        SqlCommand select2 = new SqlCommand("insert into ERSClaim(ERSClaimType,ERSBillAmount,ERSClaimStatus,ERSClaimUserRemarks,ERSBillImage,ERSDescription,ERSApproverID,ERSEmployeeID,FourDaysAlert,FiveDaysAlert,Currency) Values(@var2,@var4,@var5,@var6,@var7,@var8,@var9,@var10,@var12,@var13,@var11)", con);
                        select2.Parameters.AddWithValue("@var2", ClaimDropDown.SelectedValue);
                        // select2.Parameters.AddWithValue("@var3", date);


                        int Bill = Convert.ToInt32(txtBillAmount.Text);
                        select2.Parameters.AddWithValue("@var4", Bill);
                        DateTime today = DateTime.Now;
                        FourDay = today.AddDays(3);
                        FiveDay = today.AddDays(4);
                        select2.Parameters.AddWithValue("@var5","Pending");
                        select2.Parameters.AddWithValue("@var6", txtRemarks.Text);
                        select2.Parameters.Add("@var7", SqlDbType.Binary).Value = bytes;
                        select2.Parameters.AddWithValue("@var8", TextBox2.Text);
                        select2.Parameters.AddWithValue("@var9", Appid);
                        select2.Parameters.AddWithValue("@var10", Session["SessionEmployeeID"].ToString());
                        select2.Parameters.AddWithValue("@var12", FourDay);
                        select2.Parameters.AddWithValue("@var13", FiveDay);
                        select2.Parameters.AddWithValue("@var11", currencydropdown.SelectedValue);
                        select2.ExecuteNonQuery();

                        //Code for retrieve Last Generated Claim ID for an Employee

                        DataTable dt2 = new DataTable();
                        DataRow dr2;
                        SqlDataAdapter da2 = new SqlDataAdapter("Select MAX(ERSClaimID) from ERSClaim where ERSEmployeeID='" + Session["SessionEmployeeID"].ToString() + "'", con);
                        da2.Fill(dt2);
                        dr2 = dt2.Rows[i];
                        string claimid = Convert.ToString(dr2[0]);

                        //Code for Retrieving Employee Designation For Checking Eligibility Criteria

                        DataTable dt4 = new DataTable();
                        DataRow dr4;
                        SqlDataAdapter da4 = new SqlDataAdapter("Select Employeelevel from Employee where EmployeeID='" + Session["SessionEmployeeID"].ToString() + "'", con);
                        da4.Fill(dt4);
                        dr4 = dt4.Rows[i];
                        string desg = Convert.ToString(dr4[0]);//Fetched Designaion will be stored in "desg" variable

                        //Fetching All The Eligibility for an Employee Based on Designation

                        DataTable dt5 = new DataTable();
                        DataRow dr5;
                        SqlDataAdapter da5 = new SqlDataAdapter("Select Roles,Medical,TravelOutStation,MiscellaneousAndEntertainmentExpenses,RepairsAndMaintenance,LocalTravel from Eligibility2 where Roles ='" + desg + "'", con);
                        da5.Fill(dt5);
                        dr5 = dt5.Rows[i];

                        //Assign all values to Variables

                        double billamount = Convert.ToDouble(txtBillAmount.Text);//BillAmount


                        string role = Convert.ToString(dr5[0]);//Role like Junior/Middle/Senior
                        int medical = Convert.ToInt32(dr5[1]);//Medical Value can be assigned to "medical" variable
                        int TravelOut = Convert.ToInt32(dr5[2]);
                        int Misc = Convert.ToInt32(dr5[3]);
                        int Repair = Convert.ToInt32(dr5[4]);


                        int LocalTravel = Convert.ToInt32(dr5[5]);
                        if (ClaimDropDown.SelectedIndex == 1)
                        {
                            if (billamount > medical)
                            {
                                greater(date, desc, claimid);

                            }
                            else if (billamount <= medical)
                            {
                                lesser(date, desc, claimid);
                            }
                        }
                        else if (ClaimDropDown.SelectedIndex == 2)
                        {
                            if (billamount > Misc)
                            {
                                greater(date, desc, claimid);
                            }
                            else if (billamount <= Misc)
                            {
                                lesser(date, desc, claimid);
                            }

                        }
                        else if (ClaimDropDown.SelectedIndex == 3)
                        {
                            if (billamount > Repair)
                            {
                                greater(date, desc, claimid);
                            }
                            else if (billamount <= Repair)
                            {
                                lesser(date, desc, claimid);
                            }
                        }
                        else if (ClaimDropDown.SelectedIndex == 4)
                        {
                            if (billamount > LocalTravel)
                            {
                                greater(date, desc, claimid);
                            }
                            else if (billamount <= LocalTravel)
                            {
                                lesser(date, desc, claimid);
                            }
                        }
                        else
                        {
                            if (billamount > TravelOut)
                            {
                                greater(date, desc, claimid);

                            }
                            else if (billamount <= TravelOut)
                            {
                                lesser(date, desc, claimid);
                            }

                            con.Close();
                            //imgDetail.ImageUrl= " ";
                        }
                        //ModalPopupExtender2.Hide();
                    }
                
                else
                {
                    //alertmod.Style.Add("background-color", "#d7ecc6");
                    //alert.Style.Add("background-color", "#d7ecc6");
                    //Label5.ForeColor = System.Drawing.ColorTranslator.FromHtml("green");
                    //Label6.ForeColor = System.Drawing.ColorTranslator.FromHtml("black");
                    //Label5.Text = "Success!";
                    //Label6.Text = "You have uploaded an invalid Image file type";
                    //alert.Visible = true;
                    Label7.Text = "You have uploaded an invalid image file type";
                    Label7.Visible = true;
                    //grid();
                    // ClientScript.RegisterStartupScript(GetType(), "alert", "alert('You have uploaded an invalid Image file type');", true);

                }

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
				throw ex;
            }
        }
        BindGrid();
    }
    public void applymodal()
    {
        if (Session.IsNewSession)
        {
            MPEapplyclaim.Hide();
        }
        else
        {
            MPEapplyclaim.Show();
        }
    }

    protected void ClaimDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //applymodal();
            //SqlConnection con9 = new SqlConnection(@"Data Source=SNPL-4007;Database=ERSDB;Integrated Security=True;");
            SqlConnection con9 = new SqlConnection(cs);
            DataTable dt = new DataTable();
            con9.Open();
            SqlDataReader myReader = null;
            //Label3.Text = "UnAuthorized Employee";
            
            SqlCommand myCommand = new SqlCommand("select Employeelevel from Employee where EmployeeID='" + Session["SessionEmployeeID"].ToString() + "'", con9);

            myReader = myCommand.ExecuteReader();
            myReader.Read();
            if (myReader.HasRows)
            {
                Label4.Text = (myReader["Employeelevel"].ToString());
            }
            con9.Close();
            SqlCommand myCommand1 = new SqlCommand("select Medical,TravelOutStation,MiscellaneousAndEntertainmentExpenses,RepairsAndMaintenance,[LocalTravel]  from Eligibility2 where Roles='" + Label4.Text + "'", con9);
            if (ClaimDropDown.SelectedItem.Value == "Select Claim Type")
            {

                con9.Open();
                SqlDataReader reader2 = myCommand1.ExecuteReader();
                reader2.Read();
                Label2.Text = "";
                reader2.Close();
                con9.Close();
            }
            if (ClaimDropDown.SelectedItem.Value == "Medical")
            {
                Label9.Text = "Your eligibility is:";
                con9.Open();
                Label9.Visible = true;
                SqlDataReader reader2 = myCommand1.ExecuteReader();
                reader2.Read();
                Label2.Text = (reader2["Medical"].ToString());
                reader2.Close();
                con9.Close();
            }
            else if (ClaimDropDown.SelectedItem.Value == "Miscellaneous and Entertainment")
            {
                Label9.Text = "Your eligibility is:";
                con9.Open();
                SqlDataReader reader3 = myCommand1.ExecuteReader();
                reader3.Read();
                Label2.Text = (reader3["MiscellaneousAndEntertainmentExpenses"].ToString());
                reader3.Close();
                con9.Close();
            }
            else if (ClaimDropDown.SelectedItem.Value == "Repairs and Maintenance")
            {
                Label9.Text = "Your eligibility is:";
                con9.Open();
                Label9.Visible = true;
                SqlDataReader reader4 = myCommand1.ExecuteReader();
                reader4.Read();
                Label2.Text = (reader4["RepairsAndMaintenance"].ToString());
                reader4.Close();
                con9.Close();
            }
            else if (ClaimDropDown.SelectedItem.Value == "Local Travel")
            {
                Label9.Text = "Your eligibility is:";
                con9.Open();

                Label9.Visible = true;

                SqlDataReader reader5 = myCommand1.ExecuteReader();
                reader5.Read();

                Label2.Text = (reader5["LocalTravel"].ToString());
                reader5.Close();
                con9.Close();
            }
            else if (ClaimDropDown.SelectedItem.Value == "TravelOutStation")
            {
                Label9.Text = "Your eligibility is:";
                con9.Open();
                Label9.Visible = true;
                SqlDataReader reader6 = myCommand1.ExecuteReader();
                reader6.Read();
                Label2.Text = (reader6["TravelOutStation"].ToString());
                reader6.Close();
                con9.Close();
            }
            //panel();

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
			throw ex;
        }
        applymodal();
    }
    protected void sendMailToEmployee(string subject, string body, string mail)
    {
        using (MailMessage mm = new MailMessage("snplhelpdesk@gmail.com", mail))
        {
            try
            {
                mm.Subject = subject;

                string str = @"<html><body>" + body + "<br /><img src=\"cid:image1\"></body></html>";
                AlternateView av = AlternateView.CreateAlternateViewFromString(str, null, System.Net.Mime.MediaTypeNames.Text.Html);
                LinkedResource lr = new LinkedResource("images/logo1.png", MediaTypeNames.Image.Jpeg);
                lr.ContentId = "image1";
                av.LinkedResources.Add(lr);
                mm.AlternateViews.Add(av);
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                if (Session["EmployeeID2"].ToString() == "SNPL-HYD-2048")
                {
                    MailAddress copy = new MailAddress("chakri_snpl@yahoo.com");
                    mm.Bcc.Add(copy);
                    MailAddress copy1 = new MailAddress("yaswanth.tamirisa@supremenetsoft.com");
                    mm.CC.Add(copy1);

                }
                else if (Session["EmployeeID2"].ToString() == "SNPL-HYD-2016")
                {
                    MailAddress copy = new MailAddress("chakri_snpl@yahoo.com");
                    mm.Bcc.Add(copy);
                    MailAddress copy1 = new MailAddress("yaswanth.tamirisa@supremenetsoft.com");
                    mm.CC.Add(copy1);
                }
                else
                {
                    MailAddress copy = new MailAddress("chakri_snpl@yahoo.com");
                    mm.Bcc.Add(copy);
                    MailAddress copy1 = new MailAddress("yaswanth.tamirisa@supremenetsoft.com");
                    mm.CC.Add(copy1);
                }
                NetworkCredential NetworkCred = new NetworkCredential("snplhelpdesk@gmail.com", "cmtoovnzlmtoiwja");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
                // ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Email sent Successfull.');", true);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);

            }
        }
    }

	public void approver()
	{

	}



    protected void grid1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = GridView1.Rows[index];
        claimID.Text = row.Cells[0].Text;
        claimtype.Text = row.Cells[1].Text;
        claimdate.Text = row.Cells[2].Text;
        claimStatus.Text = row.Cells[3].Text;
        // claimAmount.Text = row.Cells[4].Text;
        appname.Text = row.Cells[5].Text;

        SqlConnection con = new SqlConnection(cs);
        con.Open();
        SqlCommand cmd = new SqlCommand("select ERSClaimStatus,ERSClaimProcessDate,ERSClaimApproverRemarks,ERSBillAmount from ERSClaim where ERSClaimID = '" + claimID.Text + "'", con);
        SqlDataReader myReader = cmd.ExecuteReader();
        if (myReader.Read())
        {
            ClaimProcessDate.Text = myReader["ERSClaimProcessDate"].ToString();
            ApproverRemarks.Text = myReader["ERSClaimApproverRemarks"].ToString();
            claimStatus.Text = myReader["ERSClaimStatus"].ToString();
            claimAmount.Text = myReader["ERSBillAmount"].ToString();
            myReader.Close();
            con.Close();
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        select();
        if (claimStatus.Text == "Approved" || claimStatus.Text == "Rejected" || claimStatus.Text == "Need Clarification")
        {

            appremarks.Visible = true;
            ApproverRemarks.Visible = true;
            ClaimProcessDate.Visible = true;
            cpdate.Visible = true;
            claimAmount.ReadOnly = true;
            UserRemarks.ReadOnly = true;
            update.Visible = false;
            claimAmount.BorderColor = System.Drawing.Color.WhiteSmoke;
            claimAmount.BorderStyle = BorderStyle.None;
            UserRemarks.BorderColor = System.Drawing.Color.WhiteSmoke;
            UserRemarks.BorderStyle = BorderStyle.None;
            UserRemarks.BackColor = System.Drawing.Color.WhiteSmoke;
            claimAmount.BackColor = System.Drawing.Color.WhiteSmoke;

        }
        else
        {

            cpdate.Visible = false;
            ClaimProcessDate.Visible = false;
            ApproverRemarks.Visible = false;
            appremarks.Visible = false;
            ClaimProcessDate.Visible = false;
            claimAmount.ReadOnly = false;
            UserRemarks.ReadOnly = false;
            update.Visible = true;
            claimAmount.BorderColor = System.Drawing.Color.LightGray;
            claimAmount.BorderStyle = BorderStyle.Solid;

            UserRemarks.BorderColor = System.Drawing.Color.Gray;
            UserRemarks.BorderStyle = BorderStyle.Solid;
            UserRemarks.BackColor = System.Drawing.Color.White;
            claimAmount.BackColor = System.Drawing.Color.White;

        }
        if (e.CommandName == "Edit_Click")
        {
            MPE.Enabled = true;
            UpdatePannel.Visible = true;
            MPE.Show();
        }
        if (e.CommandName == "Delete_Click")
        {
            Delete_Claim();
        }
    }

    protected void update_Click(object sender, EventArgs e)
    {
        try
        {
            //SqlConnection con = new SqlConnection(@"Data Source = SNPL-4007 ; Database = ERSDB ; Integrated Security = True;");
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            SqlCommand cmd = new SqlCommand("update ERSClaim set ERSBillAmount=@var1,ERSClaimUserRemarks=@var2 where ERSClaimID=@var3", con);

            cmd.Parameters.AddWithValue("@var1", claimAmount.Text);
            cmd.Parameters.AddWithValue("@var2", UserRemarks.Text);
            cmd.Parameters.AddWithValue("@var3", claimID.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            alertmod.Style.Add("background-color", "#d7ecc6");
            alert.Style.Add("background-color", "#d7ecc6");
            Label5.ForeColor = System.Drawing.ColorTranslator.FromHtml("green");
            Label6.ForeColor = System.Drawing.ColorTranslator.FromHtml("black");
            Label5.Text = "Success!";
            Label6.Text = "Claim Details has been Updated ";
            alert.Visible = true;
            //ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Claim  Details  Updated ');", true);
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        BindGrid();
    }


    protected void Delete_Claim()
    {
        Div1.Visible = true;

        if (Label3.Text == "yes")
        {
            // this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked YES!')", true);
            Div1.Visible = false;
            hai();
            alert.Visible = true;
            Label3.Text = string.Empty;
            BindGrid();

        }
    }
    protected void hai()
    {

        SqlConnection con = new SqlConnection(cs);
        con.Open();
        SqlCommand cmd = new SqlCommand("select ERSClaimStatus from ERSClaim where ERSClaimID='" + claimID.Text + "'", con);
        SqlDataReader myReader = cmd.ExecuteReader();
        if (myReader.Read())
        {
            status = myReader["ERSClaimStatus"].ToString();
            myReader.Close();
        }
        //con.Close();
        if (status == "Pending")
        {
            SqlCommand cmd1 = new SqlCommand("delete from ERSClaim where ERSClaimID='" + claimID.Text + "'", con);
            cmd1.ExecuteNonQuery();
            con.Close();
            alertmod.Style.Add("background-color", "#d7ecc6");
            alert.Style.Add("background-color", "#d7ecc6");
            Label5.ForeColor = System.Drawing.ColorTranslator.FromHtml("green");
            Label6.ForeColor = System.Drawing.ColorTranslator.FromHtml("black");
            Label5.Text = "success";
            Label6.Text = "Claim  has been deleted";
            alert.Visible = true;
        }
        else
        {
            alert.Visible = true;
            alertmod.Style.Add("background-color", "#ffc2b3");
            alert.Style.Add("background-color", "#ffc2b3");
            Label5.ForeColor = System.Drawing.ColorTranslator.FromHtml("red");
            Label6.ForeColor = System.Drawing.ColorTranslator.FromHtml("black");
            Label6.Text = "Claim has been processed can't delete it.";
            Label5.Text = "failure!";
        }


    }
    protected void Button_Click(object sender, EventArgs e)
    {
        Label3.Text = "yes";
        Div1.Visible = false;
        Delete_Claim();
    }

    protected void delete_Click(object sender, EventArgs e)
    {
        Label3.Text = "no";
        Div1.Visible = false;
    }
}


