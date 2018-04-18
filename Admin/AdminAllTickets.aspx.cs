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

public partial class Admin_Default : System.Web.UI.Page
{
    private SqlConnection con1;
    private SqlCommand com;
    private string constr, query;
    DataTable dt;
    private void connection()
    {
        constr = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ToString();
        con1 = new SqlConnection(constr);
        con1.Open();

    }
    string emailID;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        int i = 0;
        
        string conString = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
        SqlConnection con = new SqlConnection(conString);

        //SqlConnection con = new SqlConnection(@"Data Source=HP-PC\SQLEXPRESS;Integrated Security=true;Initial Catalog=TicketManagementSystemDB");
        DataTable dt3 = new DataTable();
        DataRow dr3;
        SqlDataAdapter da3 = new SqlDataAdapter("Select count(TMSTicketID) from TMSTicketMaster where TMSTicketPriority='Critical' and TMSTicketOwnership is null", con);
        con.Open();
        da3.Fill(dt3);
        dr3 = dt3.Rows[i];
        Label2.Text = Convert.ToString(dr3[0]);

        DataTable dt4 = new DataTable();
        DataRow dr4;
        SqlDataAdapter da4 = new SqlDataAdapter("Select count(TMSTicketID) from TMSTicketMaster where TMSTicketPriority='High' and TMSTicketOwnership is null", con);
        da4.Fill(dt4);
        dr4 = dt4.Rows[i];
        Label3.Text = Convert.ToString(dr4[0]);

        DataTable dt5 = new DataTable();
        DataRow dr5;
        SqlDataAdapter da5 = new SqlDataAdapter("Select count(TMSTicketID) from TMSTicketMaster where TMSTicketPriority='Moderate' and TMSTicketOwnership is null", con);
        da5.Fill(dt5);
        dr5 = dt5.Rows[i];
        Label1.Text = Convert.ToString(dr5[0]);


        DataTable dt6 = new DataTable();
        DataRow dr6;
        SqlDataAdapter da6 = new SqlDataAdapter("Select count(TMSTicketID) from TMSTicketMaster where TMSTicketPriority='Low' and TMSTicketOwnership is null", con);
        da6.Fill(dt6);
        dr6 = dt6.Rows[i];
        Label4.Text = Convert.ToString(dr6[0]);
        con.Close();

        if (!this.IsPostBack)
        {
            GridviewBind();
            Dropdown();
        }

    }
    protected void Dropdown()
    {
        string conString = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
        SqlConnection con = new SqlConnection(conString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select distinct(EmployeeID) from Employee where EmployeeDepartment='" + DropDownList1.SelectedValue + "'", con);
        SqlDataAdapter sa = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sa.Fill(dt);
        DropDownList2.DataSource = dt;
        // DropDownList2.DataValueField = "ValueFieldFromDatabaseResults";

        //DropDownList2.DataTextField = "EmployeeID";
        DropDownList2.DataBind();

        con.Close();

    }
    protected void GridviewBind()
    {
        connection();
        query = "select TMSTicketID as TicketID,TMSTicketSubject as Subject,TMSTicketDepartment as Department,TMSTicketPriority as Priority,TMSTicketStatus as TicketStatus,TMSTicketDate as Date from TMSTicketMaster where TMSTicketOwnership is null order by TMSTicketDate desc";//not recommended this i have written just for example,write stored procedure for security
        com = new SqlCommand(query, con1);
        SqlDataAdapter da = new SqlDataAdapter(com);
        dt = new DataTable();
        da.Fill(dt);
        //ViewState["Paging"] = dt;
        GridView1.DataSource = dt;
        GridView1.DataBind();
        con1.Close();

        // grid styles
        if (GridView1.Rows.Count > 0)
        {
            GridView1.CssClass = "table table-bordered table-striped  table-inverse nomargin";
            GridView1.UseAccessibleHeader = true;
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            GridView1.FooterRow.TableSection = TableRowSection.TableFooter;
        }

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        GridviewBind1();
    }
    protected void GridviewBind1()
    {
        connection();
        query = "select TMSTicketID as TicketID,TMSTicketSubject as Subject,TMSTicketDepartment as Department,TMSTicketPriority as Priority,TMSTicketStatus as TicketStatus,TMSTicketDate as Date from TMSTicketMaster where TMSTicketPriority='Critical' and TMSTicketOwnership is null order by TMSTicketDate desc";//not recommended this i have written just for example,write stored procedure for security
        com = new SqlCommand(query, con1);
        SqlDataAdapter da = new SqlDataAdapter(com);
        dt = new DataTable();
        da.Fill(dt);
       // ViewState["Paging"] = dt;
        GridView1.DataSource = dt;
        GridView1.DataBind();
        con1.Close();

    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        GridviewBind2();
    }
    protected void GridviewBind2()
    {
        connection();
        query = "select TMSTicketID as TicketID,TMSTicketSubject as Subject,TMSTicketDepartment as Department,TMSTicketPriority as Priority,TMSTicketStatus as TicketStatus,TMSTicketDate as Date from TMSTicketMaster where TMSTicketPriority='High' and TMSTicketOwnership is null order by TMSTicketDate desc"; ;//not recommended this i have written just for example,write stored procedure for security
        com = new SqlCommand(query, con1);
        SqlDataAdapter da = new SqlDataAdapter(com);
        dt = new DataTable();
        da.Fill(dt);
        //ViewState["Paging"] = dt;
        GridView1.DataSource = dt;
        GridView1.DataBind();
        con1.Close();

    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        GridviewBind3();
    }
    protected void GridviewBind3()
    {
        connection();
        query = "select TMSTicketID as TicketID,TMSTicketSubject as Subject,TMSTicketDepartment as Department,TMSTicketPriority as Priority,TMSTicketStatus as TicketStatus,TMSTicketDate as Date from TMSTicketMaster where TMSTicketPriority='Moderate' and TMSTicketOwnership is null order by TMSTicketDate desc";//not recommended this i have written just for example,write stored procedure for security
        com = new SqlCommand(query, con1);
        SqlDataAdapter da = new SqlDataAdapter(com);
        dt = new DataTable();
        da.Fill(dt);
        //ViewState["Paging"] = dt;
        GridView1.DataSource = dt;
        GridView1.DataBind();
        con1.Close();

    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        GridviewBind4();
    }
    protected void GridviewBind4()
    {
        connection();
        query = "select TMSTicketID as TicketID,TMSTicketSubject as Subject,TMSTicketDepartment as Department,TMSTicketPriority as Priority,TMSTicketStatus as TicketStatus,TMSTicketDate as Date from TMSTicketMaster where TMSTicketPriority='Low' and TMSTicketOwnership is null order by TMSTicketDate desc";//not recommended this i have written just for example,write stored procedure for security
        com = new SqlCommand(query, con1);
        SqlDataAdapter da = new SqlDataAdapter(com);
        dt = new DataTable();
        da.Fill(dt);
       // ViewState["Paging"] = dt;
        GridView1.DataSource = dt;
        GridView1.DataBind();
        con1.Close();

    }
   

    protected void Button1_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow grow in GridView1.Rows)
        {
            //Searching CheckBox("chkDel") in an individual row of Grid
            CheckBox ticketSelect = (CheckBox)grow.FindControl("ticketSelect");
            //If CheckBox is checked than delete the record with particular empid
            if (ticketSelect.Checked)
            {
                string tktid = grow.Cells[1].Text;
                UpdateRecord(tktid);

            }
        }
        Response.Redirect("AdminAllTickets.aspx");


    }
    protected void UpdateRecord(string tktid)
    {
        string conString = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;

        using (SqlConnection con = new SqlConnection(conString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select EmployeeMailID from Employee where EmployeeID='" + DropDownList2.SelectedValue + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                emailID = reader["EmployeeMailID"].ToString();
                reader.Close();

                con.Close();

            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
        }





        string conString1 = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
        SqlConnection con1 = new SqlConnection(conString1);

        //SqlConnection con1 = new SqlConnection(@"Data Source=HP-PC\SQLEXPRESS;Initial Catalog=TicketManagementSystemDB;Integrated Security=True");
        SqlCommand com = new SqlCommand("update TMSTicketMaster set TMSTicketOwnership='" + DropDownList1.SelectedValue + "' where TMSTicketID=@ID", con1);
        com.Parameters.AddWithValue("@ID", tktid);
        con1.Open();
        com.ExecuteNonQuery();
        SendMail1(emailID, tktid);
        con1.Close();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Table1.Visible = true;
        string conString1 = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
        SqlConnection con = new SqlConnection(conString1);

        //SqlConnection con = new SqlConnection(@"Data Source=HP-PC\SQLEXPRESS;Integrated Security=true;Initial Catalog=TicketManagementSystemDB");
        con.Open();

        SqlCommand cmd = new SqlCommand("select count(TMSTicketID) from TMSTicketMaster where TMSTicketOwnership='" + DropDownList1.SelectedValue + "' and TMSTicketStatus='New'", con);
        SqlCommand cmd1 = new SqlCommand("select count(TMSTicketID) from TMSTicketMaster where TMSTicketOwnership='" + DropDownList1.SelectedValue + "' and TMSTicketStatus='Pending'", con);
        SqlCommand cmd2 = new SqlCommand("select count(TMSTicketID) from TMSTicketMaster where TMSTicketOwnership='" + DropDownList1.SelectedValue + "' and TMSTicketStatus='Waiting Customer Reply'", con);
        SqlCommand cmd3 = new SqlCommand("select count(TMSTicketID) from TMSTicketMaster where TMSTicketOwnership='" + DropDownList1.SelectedValue + "' and TMSTicketStatus='Resolved'", con);
        SqlCommand cmd4 = new SqlCommand("select count(TMSTicketID) from TMSTicketMaster where TMSTicketOwnership='" + DropDownList1.SelectedValue + "' and TMSTicketStatus='Closed'", con);
        SqlCommand cmd5 = new SqlCommand("select EmployeeDepartment from Employee where EmployeeID='" + DropDownList2.SelectedValue + "'", con);

        int s1 = (int)cmd.ExecuteScalar();
        int s2 = (int)cmd1.ExecuteScalar();
        int s3 = (int)cmd2.ExecuteScalar();
        int s4 = (int)cmd3.ExecuteScalar();
        int s5 = (int)cmd4.ExecuteScalar();
        string dep = (string)cmd5.ExecuteScalar();

        newTKTCount.Text = s1.ToString();
        pendingTKTCount.Text = s2.ToString();
        wcrTKTCount.Text = s3.ToString();
        resolvedTKTCount.Text = s4.ToString();
        closedTKTCount.Text = s5.ToString();
        empID.Text = DropDownList2.SelectedValue;
        depart.Text = dep;

        con.Close();
    }
    protected void SendMail1(string emailID, string tktid)
    {
       using (MailMessage mm = new MailMessage("helpdesk.snpl@gmail.com", emailID))
        {
            mm.Subject = "Reg : TMS  Assigned Tickets";
            mm.Body = "Hello Analyst," + "<br/>" + " Here is your Assigned Ticket." + "<br>" + " Please Check and Resolve Ticket Issue. " + "<br>" + " Your Assigned  TicketID is :" + tktid +

           "This is autogenerated mail. Please donot reply to this mail." + "<br>" + "Thanks&Regards" + "<br>" + "TMS Team";

            mm.IsBodyHtml = true;
            string str = mm.Body + @"<html><body>" + "<br /><img src=\"cid:logo\"></body></html>";
            AlternateView av = AlternateView.CreateAlternateViewFromString(str, null, System.Net.Mime.MediaTypeNames.Text.Html);
            LinkedResource lr = new LinkedResource("images/logo.png", MediaTypeNames.Image.Jpeg);
            lr.ContentId = "image1";
            av.LinkedResources.Add(lr);
            mm.AlternateViews.Add(av);

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            NetworkCredential NetworkCred = new NetworkCredential("helpdesk.snpl@gmail.com", "jdzydiyrrvfnrjbf");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;
            LinkedResource LinkedImage = new LinkedResource(@"Images\logo.png");
            LinkedImage.ContentId = "MyPic";
            // Added the patch for Thunderbird as suggested by Jorge
            LinkedImage.ContentType = new ContentType(MediaTypeNames.Image.Jpeg);
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mm.Body + "<br>" + " <img src=cid:MyPic>", null, "text/html");

            htmlView.LinkedResources.Add(LinkedImage);
            mm.AlternateViews.Add(htmlView);
            smtp.Send(mm);
            //ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Email sent Successfull.');", true);
        }
    }
   
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string conString1 = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
        SqlConnection con = new SqlConnection(conString1);
        con.Open();

        SqlCommand cmd = new SqlCommand("select EmployeeID from Employee where SecondaryPosition ='Approver'and EmployeeDepartment='"+DropDownList1.SelectedValue+"' ", con);
        SqlDataAdapter sa = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sa.Fill(dt);
        DropDownList2.DataSource = dt;
        // DropDownList2.DataValueField = "ValueFieldFromDatabaseResults";

        DropDownList2.DataTextField = "EmployeeID";
        DropDownList2.DataBind();

        con.Close();
    }
}






  