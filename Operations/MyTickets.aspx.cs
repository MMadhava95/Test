using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Operations_Default : System.Web.UI.Page
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
    protected void Page_Load(object sender, EventArgs e)
    { 
        if(Session["SessionEmployeeID"]==null)
        {
            Response.Redirect("~/Login.aspx");
        }
       
        if (!this.IsPostBack)
        {
            GridviewBind();
        }
        int i = 0;
        DataTable dt3 = new DataTable();
        DataRow dr3;
        string conString1 = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
        SqlConnection con = new SqlConnection(conString1);
        string str = Session["SessionEmployeeID"].ToString();
        SqlDataAdapter da3 = new SqlDataAdapter("Select count(TMSTicketID) from TMSTicketMaster where TMSTicketPriority='Critical' and TMSTicketOwnership='" + Session["SessionEmployeeID"] + "'", con);
        con.Open();
        da3.Fill(dt3);
        dr3 = dt3.Rows[i];
        Label2.Text = Convert.ToString(dr3[0]);

        DataTable dt4 = new DataTable();
        DataRow dr4;
        int j = 0;
        SqlDataAdapter da4 = new SqlDataAdapter("Select count(TMSTicketID) from TMSTicketMaster where TMSTicketPriority='High' and TMSTicketOwnership='" + Session["SessionEmployeeID"] + "'", con);
        da4.Fill(dt4);
        if (dt4.Rows.Count > 0)
        {
            dr4 = dt4.Rows[j];
            Label3.Text = Convert.ToString(dr4[0]);
        }

        DataTable dt5 = new DataTable();
        DataRow dr5;
        SqlDataAdapter da5 = new SqlDataAdapter("Select count(TMSTicketID) from TMSTicketMaster where TMSTicketPriority='Moderate' and TMSTicketOwnership='" + Session["SessionEmployeeID"] + "'", con);
        da5.Fill(dt5);
        dr5 = dt5.Rows[i];
        Label1.Text = Convert.ToString(dr5[0]);

        DataTable dt6 = new DataTable();
        DataRow dr6;
        SqlDataAdapter da6 = new SqlDataAdapter("Select count(TMSTicketID) from TMSTicketMaster where TMSTicketPriority='Low' and TMSTicketOwnership='" + Session["SessionEmployeeID"] + "'", con);
        da6.Fill(dt6);
        dr6 = dt6.Rows[i];
        Label4.Text = Convert.ToString(dr6[0]);
        con.Close();
    }
    protected void OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = GridView1.SelectedRow;
        TextBox1.Text = row.Cells[0].Text;
        //sending ticketID through session to TicketDetails page
        Session["TicketID"] = TextBox1.Text;
        Response.Redirect("TicketInfo-Analyst.aspx");
    }

    protected void GridviewBind()
    {
        connection();
        query = "select TMSTicketID as TicketID,TMSTicketSubject as Subject,TMSTicketDepartment as Department,TMSTicketPriority as Priority,TMSTicketStatus as TicketStatus,TMSTicketDate as Date from TMSTicketMaster where TMSTicketOwnership='" + Session["SessionEmployeeID"] + "'";//not recommended this i have written just for example,write stored procedure for security
        com = new SqlCommand(query, con1);
        SqlDataAdapter da = new SqlDataAdapter(com);
        dt = new DataTable();
        da.Fill(dt);
        ViewState["Paging"] = dt;
        GridView1.DataSource = dt;
        GridView1.DataBind();
        con1.Close();

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        GridviewBind1();
    }
    protected void GridviewBind1()
    {
        connection();
        query = "select TMSTicketID as TicketID,TMSTicketSubject as Subject,TMSTicketDepartment as Department,TMSTicketPriority as Priority,TMSTicketStatus as TicketStatus,TMSTicketDate as Date from TMSTicketMaster where TMSTicketPriority='Critical' and TMSTicketOwnership='" + Session["SessionEmployeeID"] + "'";//not recommended this i have written just for example,write stored procedure for security
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
        query = "select TMSTicketID as TicketID,TMSTicketSubject as Subject,TMSTicketDepartment as Department,TMSTicketPriority as Priority,TMSTicketStatus as TicketStatus,TMSTicketDate as Date from TMSTicketMaster where TMSTicketPriority='High' and TMSTicketOwnership='" + Session["SessionEmployeeID"] + "'";//not recommended this i have written just for example,write stored procedure for security
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
        query = "select TMSTicketID as TicketID,TMSTicketSubject as Subject,TMSTicketDepartment as Department,TMSTicketPriority as Priority,TMSTicketStatus as TicketStatus,TMSTicketDate as Date from TMSTicketMaster where TMSTicketPriority='Moderate' and TMSTicketOwnership='" + Session["SessionEmployeeID"] + "'";//not recommended this i have written just for example,write stored procedure for security
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
        query = "select TMSTicketID as TicketID,TMSTicketSubject as Subject,TMSTicketDepartment as Department,TMSTicketPriority as Priority,TMSTicketStatus as TicketStatus,TMSTicketDate as Date from TMSTicketMaster where TMSTicketPriority='Low' and TMSTicketOwnership='" + Session["SessionEmployeeID"] + "'";//not recommended this i have written just for example,write stored procedure for security
        com = new SqlCommand(query, con1);
        SqlDataAdapter da = new SqlDataAdapter(com);
        dt = new DataTable();
        da.Fill(dt);
        ViewState["Paging"] = dt;
        GridView1.DataSource = dt;
        GridView1.DataBind();
        con1.Close();

    }



    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "Click to select a ticket.";
            e.Row.Attributes["style"] = "cursor:pointer";

        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Style["background-color"] = "White";
            e.Row.Style["fore-color"] = "FFE8C4";
        }

    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GridView1.Rows[index];
            TextBox1.Text = row.Cells[1].Text;
            //sending ticketID through session to TicketInfo page
            Session["TicketID"] = TextBox1.Text;
            Response.Redirect("TicketInfo-Analyst.aspx");

        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception is :" + ex);
        }
        if (e.CommandName == "Edit_Click")
        {
            Response.Redirect("TicketInfo-Analyst.aspx");
        }
    }
}

