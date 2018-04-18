using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Operations_Default : System.Web.UI.Page
{
    public string dept;
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


    string emailID, dep;

    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["SessionEmployeeID"]==null)
        {
            Response.Redirect("~/Login.aspx");
        }

        string conString1 = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;

        SqlConnection con = new SqlConnection(conString1);
        try
        {
            con.Open();
            DataTable dtdep = new DataTable();
            DataRow drdep;
            int k = 0;
            SqlDataAdapter sddep = new SqlDataAdapter("select EmployeeDepartment from Employee where EmployeeID = '" + Session["SessionEmployeeID"] + "'", con);
            sddep.Fill(dtdep);
            if (dtdep.Rows.Count > 0)
            {
                drdep = dtdep.Rows[k];
                dep = Convert.ToString(drdep[0]);
                department.Text = dep;
            }

            con.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }

        int i = 0;


        DataTable dt3 = new DataTable();
        DataRow dr3;
        SqlDataAdapter da3 = new SqlDataAdapter("Select count(TMSTicketID) from TMSTicketMaster where TMSTicketPriority='Critical' and TMSTicketOwnership is null and TMSTicketDepartment='" + dep + "'", con);
        con.Open();
        da3.Fill(dt3);
        dr3 = dt3.Rows[i];
        Label1.Text = Convert.ToString(dr3[0]);

        DataTable dt4 = new DataTable();
        DataRow dr4;
        int j = 0;
        SqlDataAdapter da4 = new SqlDataAdapter("Select count(TMSTicketID) from TMSTicketMaster where TMSTicketDepartment='" + dep + "' and TMSTicketOwnership is null", con);
        da4.Fill(dt4);
        if (dt4.Rows.Count > 0)
        {
            dr4 = dt4.Rows[j];
            Label2.Text = Convert.ToString(dr4[0]);
        }
        DataTable dt5 = new DataTable();
        DataRow dr5;
        SqlDataAdapter da5 = new SqlDataAdapter("Select count(TMSTicketID) from TMSTicketMaster where GetDate()>=Level2Escalation and GetDate()<=Level3Escalation and TMSTicketOwnership is null and TMSTicketDepartment='" + dep + "'", con);
        da5.Fill(dt5);
        dr5 = dt5.Rows[i];
        Label3.Text = Convert.ToString(dr5[0]);
        DataTable dt6 = new DataTable();
        DataRow dr6;
        SqlDataAdapter da6 = new SqlDataAdapter("Select count(TMSTicketID) from TMSTicketMaster where Level3Escalation<=GETDATE() and TMSTicketOwnership is null and TMSTicketDepartment='" + dep + "'", con);
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
        string conString1 = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
        SqlConnection con = new SqlConnection(conString1);
        con.Open();

        SqlCommand cmd = new SqlCommand("select EmployeeID from Employee where (EmployeeDepartment='" + department.Text + "') and (SecondaryPosition ='Approver') ", con);
        SqlDataAdapter sa = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sa.Fill(dt);
        DropDownList1.DataSource = dt;
        // DropDownList2.DataValueField = "ValueFieldFromDatabaseResults";

        DropDownList1.DataTextField = "EmployeeID";
        DropDownList1.DataBind();

        con.Close();
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
                Response.Redirect("~/Operations/AllTickets.aspx");

            }
            else
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select atleast one checkbox ')", true);

        }

    }
    protected void UpdateRecord(string tktid)
    {
        string conString1 = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;

        using (SqlConnection con = new SqlConnection(conString1))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select EmployeeMailID from Employee where EmployeeID='" + DropDownList1.SelectedValue + "'", con);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                emailID = reader["EmployeeMailID"].ToString();
                reader.Close();

                con.Close();

            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
        }
        SqlConnection con1 = new SqlConnection(conString1);
        SqlCommand com = new SqlCommand("update TMSTicketMaster set TMSTicketOwnership='" + DropDownList1.SelectedValue + "' where TMSTicketID=@ID", con1);
        com.Parameters.AddWithValue("@ID", tktid);
        con1.Open();
        com.ExecuteNonQuery();
        SendMail(emailID, tktid);
        con1.Close();
    }
    protected void GridviewBind()
    {
        try
        {
            string conString1 = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;

            SqlConnection con = new SqlConnection(conString1);
            query = "select TMSTicketID as TicketID,TMSTicketSubject as Subject,TMSTicketDepartment as Department ,TMSTicketPriority as Priority ,TMSTicketStatus as Status,TMSTicketDate as Date  from TMSTicketMaster where TMSTicketOwnership is null and TMSTicketDepartment = '" + dep + "' order by TMSTicketDate desc";//not recommended this i have written just for example,write stored procedure for security
            com = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(com);

            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            ViewState["Paging"] = dt1;
            GridView1.DataSource = dt1;
            GridView1.DataBind();
            con.Close();

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        GridviewBind1();
    }
    protected void GridviewBind1()
    {
        connection();
        query = "select TMSTicketID as TicketID,TMSTicketSubject as Subject,TMSTicketDepartment as Department,TMSTicketPriority as Priority ,TMSTicketStatus as Status,TMSTicketDate as Date  from TMSTicketMaster where TMSTicketPriority='Critical' and TMSTicketOwnership is null and TMSTicketDepartment='" + dep + "' order by TMSTicketDate desc";//not recommended this i have written just for example,write stored procedure for security
        com = new SqlCommand(query, con1);
        SqlDataAdapter da = new SqlDataAdapter(com);
        dt = new DataTable();
        da.Fill(dt);
        ViewState["Paging"] = dt;
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
        query = "select TMSTicketID as TicketID,TMSTicketSubject as Subject,TMSTicketDepartment as Department,TMSTicketPriority as Priority,TMSTicketStatus as Status,TMSTicketDate as Date from TMSTicketMaster where TMSTicketDepartment='" + dep + "' and TMSTicketOwnership is null and  TMSTicketDepartment='" + dep + "' order by TMSTicketDate desc";//not recommended this i have written just for example,write stored procedure for security
        com = new SqlCommand(query, con1);
        SqlDataAdapter da = new SqlDataAdapter(com);
        dt = new DataTable();
        da.Fill(dt);
        ViewState["Paging"] = dt;
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
        query = "select TMSTicketID as TicketID,TMSTicketSubject as Subject,TMSTicketDepartment as Department,TMSTicketPriority as Priority ,TMSTicketStatus as Status,TMSTicketDate as Date,Level2Escalation as ResolutinTime,DeviationTime from TMSTicketMaster where GetDate()>=Level2Escalation and GetDate()<=Level3Escalation and TMSTicketOwnership is null and TMSTicketDepartment='" + dep + "' order by TMSTicketDate desc";//not recommended this i have written just for example,write stored procedure for security
        com = new SqlCommand(query, con1);
        SqlDataAdapter da = new SqlDataAdapter(com);
        dt = new DataTable();
        da.Fill(dt);
        ViewState["Paging"] = dt;
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
        query = "select TMSTicketID as TicketID,TMSTicketSubject as Subject,TMSTicketDepartment as Department,TMSTicketPriority as Priority ,TMSTicketStatus as Status,TMSTicketDate as Date,TMSSLAStatus as Priority,Level2Escalation as ResolveTime,DeviationTime as DeviationTime from TMSTicketMaster where Level3Escalation<=GETDATE() and TMSTicketOwnership is null and TMSTicketDepartment='" + dep + "'order by TMSTicketDate desc";//not recommended this i have written just for example,write stored procedure for security
        com = new SqlCommand(query, con1);
        SqlDataAdapter da = new SqlDataAdapter(com);
        dt = new DataTable();
        da.Fill(dt);
        ViewState["Paging"] = dt;
        GridView1.DataSource = dt;
        GridView1.DataBind();
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
        SqlCommand cmd5 = new SqlCommand("select EmployeeDepartment from Employee where EmployeeID='" + DropDownList1.SelectedValue + "'", con);

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
        empID.Text = DropDownList1.SelectedValue;
        depart.Text = dep;

        con.Close();
    }
    protected void SendMail(string emailID, string tktid)
    {

        var fromAddress = "akhihoney734@gmail.com";// Gmail Address from where you send the mail

        var toAddress = emailID;// any address where the email will be sending
        const string fromPassword = "kgrheduekiwkzqjz";//Password of your gmail address
        string subject = "Assigned Ticket";
        string body = "Hello Analyst, \n \n Here is your Assigned Ticket.\n\n Please Check and Resolve Ticket Issue. \n \n Your TicketID is :" + tktid + "\n\n";

        body += "This is autogenerated mail. Please donot reply to this mail." + "\n";

        // smtp settings
        var smtp = new System.Net.Mail.SmtpClient();
        {
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
            smtp.Timeout = 20000;
        }
        // Passing values to smtp object
        smtp.Send(fromAddress, toAddress, subject, body);
        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Email sent Successfull.');", true);
    }
}
   


