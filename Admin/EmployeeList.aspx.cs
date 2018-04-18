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

public partial class EmployeeList : System.Web.UI.Page
{
    public string str;
    string lastFourDigits;
    public int i = 0;
    public string Employeeid;
    public string cs = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
	{
        
        //ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        Div1.Visible = false;
        //if (Session["Admin"] != null)
        //{
        //    Session["Admin"].ToString();
        //}
        //else
        //{
        //    Response.Redirect("Login.aspx");
        //}
        if (!this.IsPostBack)
        {
            this.BindGrid();

        }
        alert.Visible = false;
    }

    private void select()
    {

        SqlConnection con = new SqlConnection(cs);
        con.Open();

        SqlCommand cmd = new SqlCommand("select EmployeeDesignation,EmployeeDepartment,EmployeeLocation from Employee where EmployeeID = '" + EmployeeID2.Text + "'", con);
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            EmployeeDesignation.Text = reader["EmployeeDesignation"].ToString();
            EmployeeDepartment.Text = reader["EmployeeDepartment"].ToString();
            Label7.Text = reader["EmployeeLocation"].ToString();

            reader.Close();

        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt3 = new DataTable();
        DataRow dr3;
        SqlDataAdapter sda1 = new SqlDataAdapter("select ERSEmployeeID from ERSApprover where ERSEmployeeID ='" + EmployeeID2.Text + "' and isactive='" + 1 + "'", con);
        sda1.Fill(dt3);

        if (dt3.Rows.Count > 0)
        {
            Label2.Text = "Yes";
            MakeApprover.Visible = false;
        }
        else if (dt3.Rows.Count == 0)
        {
            Label2.Text = "No";
            MakeApprover.Visible = true;
        }

        con.Close();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "Click to know the employee details";

        }

    }

    //method for Approver Action
    protected void MakeApprover_Click(object sender, EventArgs e)
    {
        //SqlCommand newcmd = new SqlCommand("select ERSApproverMailID from ERSApprover where ERSApproverMailID='"+empmailid.Text+"'", con);
        //newcmd.ExecuteNonQuery();
        try
        {
            string AppID = EmployeeID2.Text;
            string ApproverID = "SNPL" + AppID.Substring(AppID.Length - 4);
            string ApproverName = EmployeeName.Text;
            string ApproverMailID = EmployeeMailID.Text;
            string ApproverLevel = "level3";
            string EmployeeID = EmployeeID2.Text;
            string ApproverPassword = "Supreme@bza";
            string Position = "Approver";
            int attemptCount = 0;
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            SqlCommand cmd1 = new SqlCommand("select count(ERSApproverID) from ERSApprover where ERSEmployeeID='" + EmployeeID2.Text + "'", con);
            int count = Convert.ToInt16(cmd1.ExecuteScalar());
            if (count > 0)
            {
                SqlCommand cmd2 = new SqlCommand("update ERSApprover set isactive=@var1 where ERSEmployeeID='" + EmployeeID2.Text + "'", con);
                cmd2.Parameters.AddWithValue("@var1", "1");
                cmd2.ExecuteNonQuery();
            }
            else
            {

                SqlCommand cmd = new SqlCommand("insert into ERSApprover(ERSApproverID,ERSApproverName,ERSApproverMailID,ERSApproverLevel,ERSApproverRole,ERSEmployeeID,ERSApproverPassword,Position,attemptcount) values(@var1,@var2,@var3,@var4,@var5,@var6,@var7,@var8,@var9)", con);
                cmd.Parameters.AddWithValue("@var1", ApproverID);
                cmd.Parameters.AddWithValue("@var2", ApproverName);
                cmd.Parameters.AddWithValue("@var3", ApproverMailID);
                cmd.Parameters.AddWithValue("@var4", ApproverLevel);
                cmd.Parameters.AddWithValue("@var5", EmployeeDepartment.Text);
                cmd.Parameters.AddWithValue("@var6", EmployeeID);
                cmd.Parameters.AddWithValue("@var7", ApproverPassword);
                cmd.Parameters.AddWithValue("@var8", Position);
                cmd.Parameters.AddWithValue("@var9", attemptCount);
                cmd.ExecuteNonQuery();
                alertmod.Style.Add("background-color", "#d7ecc6");
                alert.Style.Add("background-color", "#d7ecc6");
                Label3.ForeColor = System.Drawing.ColorTranslator.FromHtml("green");
                Label5.ForeColor = System.Drawing.ColorTranslator.FromHtml("black");


                Label5.Text = "Employee promoted as  approver";
                Label3.Text = "Success!";
                alert.Visible = true;
                grid();
            }
            // ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Employee promoted as  approver');", true);
            con.Close();
            string subject = " ERS-Approver Position Acknowledgement";
            string body = "Dear\t" + ApproverName + ",<br />\n\n We Acknowledge you are promoted as Approver.<br /><br/>Please Login with the following Credentials\n\n<br />" +
                "Login ID :\t" + ApproverID + "<br />\nPassword : \t" + ApproverPassword + "<br /><br/>Please click here to login <a href=\"http://localhost:49348/Login.aspx\" >login</a>" + " <br /><br/>\n\n\nThanks & Regards\n<br />ERS Team<br />\nSNPL";
            sendMailToEmployee(subject, body, ApproverMailID);

            // ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Claim Applied Successfully');", true);

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
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
    protected void gridsearch_Click(object sender, EventArgs e)
    {
        BindGrid();

        if (GridView1.Rows.Count == 0)
        {
            GridView1.Visible = true;
        }

    }

    protected void Cancelsearch_Click(object sender, EventArgs e)
    {
        txtsearch.Text = "";
        // GridData();
        // Label49.Text = "";
        if (GridView1.Rows.Count != 0)
        {
            BindGrid();
        }
        else
        {
            BindGrid();
        }

    }
    private void BindGrid()
    {

        SqlConnection con = new SqlConnection(cs);

        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "select EmployeeID, EmployeeName, EmployeeMailID FROM Employee where (EmployeeID like '" + txtsearch.Text + "%' or EmployeeName like '" + txtsearch.Text + "%' or EmployeeMailID like '" + txtsearch.Text + "%') and isactive='" + 1 + "'";
            cmd.Connection = con;
            DataTable dt = new System.Data.DataTable();
            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
            {
                sda.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }


        }
        if (GridView1.Rows.Count > 0)
        {
            GridView1.CssClass = "table table-bordered table-striped  table-inverse nomargin";
            GridView1.UseAccessibleHeader = true;
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            GridView1.FooterRow.TableSection = TableRowSection.TableFooter;
        }
    }
    protected void sendMailToEmployee(string subject, string body, string mail)
    {
        using (MailMessage mm = new MailMessage("helpdesk.snpl@gmail.com", mail))
        {
            try
            {
                mm.Subject = subject;
                string str = @"<html><body>" + body + "<br /><img src=\"cid:image1\"></body></html>";
                AlternateView av = System.Net.Mail.AlternateView.CreateAlternateViewFromString(str, null, System.Net.Mime.MediaTypeNames.Text.Html);
                LinkedResource lr = new System.Net.Mail.LinkedResource("images/logo1.png", MediaTypeNames.Image.Jpeg);
                lr.ContentId = "image1";
                av.LinkedResources.Add(lr);
                mm.AlternateViews.Add(av);
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("helpdesk.snpl@gmail.com", "jdzydiyrrvfnrjbf");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
                alertmod.Style.Add("background-color", "#d7ecc6");
                alert.Style.Add("background-color", "#d7ecc6");
                Label3.ForeColor = System.Drawing.ColorTranslator.FromHtml("green");
                Label5.ForeColor = System.Drawing.ColorTranslator.FromHtml("black");
                Label3.Text = "Success!";
                Label5.Text = "Employee promoted as an approver";
                alert.Visible = true;
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
    protected void LogOut_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Session.RemoveAll();
        System.Web.Security.FormsAuthentication.SignOut();
        Response.Redirect("LoginPage.aspx");

    }
    protected void update_Click(object sender, EventArgs e)
    {
        try
        {
            // string cs = ConfigurationManager.ConnectionStrings["SNPL_ERSConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            SqlCommand cmd = new SqlCommand("update Employee set EmployeeDesignation=@var1,EmployeeDepartment=@var2  where EmployeeID='" + EmployeeID2.Text + "'", con);
            cmd.Parameters.AddWithValue("@var1", EmployeeDesignation.Text);
            cmd.Parameters.AddWithValue("@var2", EmployeeDepartment.Text);
            cmd.ExecuteNonQuery();
            con.Close();


            BindGrid();
            alertmod.Style.Add("background-color", "#d7ecc6");
            alert.Style.Add("background-color", "#d7ecc6");
            Label3.ForeColor = System.Drawing.ColorTranslator.FromHtml("green");
            Label5.ForeColor = System.Drawing.ColorTranslator.FromHtml("black");
            Label3.Text = "Success!";
            Label5.Text = "Employee details has been updated";

            alert.Visible = true;
            grid();

        }
        catch (Exception ex)
        {
            alertmod.Style.Add("background-color", "#ffc2b3");
            alert.Style.Add("background-color", "#ffc2b3");
            Label3.ForeColor = System.Drawing.ColorTranslator.FromHtml("red");
            Label5.ForeColor = System.Drawing.ColorTranslator.FromHtml("black");
            Label3.Text = "Failure!";
            Label5.Text = "Employee has claims (or) an Approver";
            alert.Visible = true;
            grid();

        }
    }
    public void OnConfirm(object sender, EventArgs e)
    {

        // this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked YES!')", true);
        Delete_Employee(sender, e);

    }

    protected void Delete_Employee(object sender, EventArgs e)
    {
        alert.Visible = false;
        Div1.Visible = true;
        if (Label6.Text == "yes")
        {
            Div1.Visible = false;
            hai(sender, e);
            Label6.Text = string.Empty;
        }


    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        this.BindGrid();
        //this.Grid();
    }

    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {


    }
    protected void hai(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(cs);
        con.Open();
        try
        {
            DataTable dt = new DataTable();
            SqlCommand cmd2 = new SqlCommand(" select count(ERSClaimID) from ERSClaim  where ([ERSClaimStatus]='Pending' or [ERSClaimStatus]='Need Clarification')and ERSEmployeeID='" + EmployeeID2.Text + "' ", con);

            int Count = Convert.ToInt16(cmd2.ExecuteScalar());
            if (Count > 0 && Label2.Text == "Yes")
            {
                alertmod.Style.Add("background-color", "#d7ecc6");
                alert.Style.Add("background-color", "#d7ecc6");
                Label3.ForeColor = System.Drawing.ColorTranslator.FromHtml("green");
                Label5.ForeColor = System.Drawing.ColorTranslator.FromHtml("black");
                Label5.Text = "Employee has claims as well as approver";
                alert.Visible = true;
            }
            else if (Count > 0 && Label2.Text == "No")
            {
                //employe has claims
                alertmod.Style.Add("background-color", "#d7ecc6");
                alert.Style.Add("background-color", "#d7ecc6");
                Label3.ForeColor = System.Drawing.ColorTranslator.FromHtml("green");
                Label5.ForeColor = System.Drawing.ColorTranslator.FromHtml("black");
                //Label3.Text = "Success!";
                Label5.Text = "Employee has claims";
                alert.Visible = true;
            }
            else if (Count <= 0 && Label2.Text == "Yes")
            {
                //employee is an approver
                alertmod.Style.Add("background-color", "#d7ecc6");
                alert.Style.Add("background-color", "#d7ecc6");
                Label3.ForeColor = System.Drawing.ColorTranslator.FromHtml("green");
                Label5.ForeColor = System.Drawing.ColorTranslator.FromHtml("black");
                Label5.Text = "Employee is an Approver";
                alert.Visible = true;
            }
            else
            {
                //delete employee
                SqlCommand cmd = new SqlCommand("update employee set isactive=@var1 where EmployeeID='" + EmployeeID2.Text + "'", con);
                cmd.Parameters.AddWithValue("@var1", "0");
                cmd.ExecuteNonQuery();
                SqlCommand cmd1 = new SqlCommand("update ERSApprover set isactive=@var1 where ERSEmployeeID='" + EmployeeID2.Text + "'", con);
                cmd1.Parameters.AddWithValue("@var1", "0");
                cmd1.ExecuteNonQuery();
                con.Close();

                alertmod.Style.Add("background-color", "#d7ecc6");
                alert.Style.Add("background-color", "#d7ecc6");
                Label3.ForeColor = System.Drawing.ColorTranslator.FromHtml("green");
                Label5.ForeColor = System.Drawing.ColorTranslator.FromHtml("black");
                Label3.Text = "Success!";
                Label5.Text = "Employee has been Deleted";
                alert.Visible = true;
                grid();

                BindGrid();
            }

        }
        catch (Exception ex)
        {

            Response.Write(ex.Message);

        }
    }
    private void Grid()
    {
        SqlConnection con = new SqlConnection(@"Data Source=HP-PC\SQLEXPRESS;Database =ERSDB;Integrated Security=True;");

        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.CommandText = "select EmployeeID, EmployeeName, EmployeeMailID FROM Employee where isactive='" + 1 + "' ";
            cmd.Connection = con;
            // cmd.Parameters.AddWithValue("@ContactName", txtsearch.Text.Trim());
            DataTable dt = new System.Data.DataTable();
            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
            {
                sda.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }
        if (GridView1.Rows.Count > 0)
        {
            GridView1.CssClass = "table table-bordered table-striped  table-inverse nomargin";
            GridView1.UseAccessibleHeader = true;
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            GridView1.FooterRow.TableSection = TableRowSection.TableFooter;
        }

    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        Label6.Text = "yes";

        Delete_Employee(sender, e);

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Label6.Text = "no";
    }

    protected void add_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddEmployee.aspx");
    }

    protected void lnkbtnEdit_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = GridView1.Rows[index];
        EmployeeID2.Text = row.Cells[0].Text;
        EmployeeName.Text = row.Cells[1].Text;

        EmployeeMailID.Text = row.Cells[2].Text;
        Employeeid = EmployeeID2.Text;
        select();
        if (e.CommandName == "Edit_Click")
        {
            MPE.Enabled = true;
            updatePanel.Visible = true;
            MPE.Show();
        }
    }
}


