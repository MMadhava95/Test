using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mycourses : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionUserMailID"] != null)
        {
            VideosBindGrid();
            VideosBindGrid1();
            VideosBindGrid2();
        }
        else
        {
            Response.Redirect("../Login.aspx");
        }

    }

    void VideosBindGrid()
    {
        int count = 0;
        SqlConnection con = new SqlConnection(constr);
        SqlCommand cmd = new SqlCommand("select * from LMSCourseregistration where EmployeeMailID = '" + Session["SessionUserMailID"].ToString() + "'",con);
       // cmd.CommandText = "select * from LMSCourseregistration where EmployeeMailID = '"+ Session["SessionUserMailID"].ToString()+ "'";
        con.Open();
        count =  Convert.ToInt32(cmd.ExecuteScalar());
        //count = 0;
        con.Close();
        if (count == 0)
        {
            ClientScript.RegisterStartupScript(typeof(Page), "alertMessage",
                "<script type='text/javascript'>alert('You did not enrolled to any Course!');window.location.replace('courselist.aspx');</script>");
        }
        else
        {
            
            con.Open();
            SqlCommand cmd1 = new SqlCommand("select * from CreateCourseTable as cct join LMSCourseregistration as lmscr on cct.CourseID = lmscr.CourseID where EmployeeMailID = '" + Session["SessionUserMailID"] + "' and coursetype ='Business' ", con);
            SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
            DataTable videodt = new DataTable();
            videodt.Load(dr);
            dlCourseList2.DataSource = videodt;
            dlCourseList2.DataBind();
            con.Close();
        }
    }
    void VideosBindGrid1()
    {
        SqlConnection con = new SqlConnection(constr);
        con.Open();
        SqlCommand cmd = new SqlCommand("select * from CreateCourseTable as cct join LMSCourseregistration as lmscr on cct.CourseID = lmscr.CourseID where EmployeeMailID = '"+Session["SessionUserMailID"]+"' and coursetype='Technical' ", con);
        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        DataTable videodt = new DataTable();
        videodt.Load(dr);
        dlCourseList1.DataSource = videodt;
        dlCourseList1.DataBind();
        con.Close();
    }
    void VideosBindGrid2()
    {
        SqlConnection con = new SqlConnection(constr);
        con.Open();
        SqlCommand cmd = new SqlCommand("select * from CreateCourseTable as cct join LMSCourseregistration as lmscr on cct.CourseID = lmscr.CourseID where EmployeeMailID = '" + Session["SessionUserMailID"] + "' and coursetype ='Development' ", con);
        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        DataTable videodt = new DataTable();
        videodt.Load(dr);
        dlCourseList3.DataSource = videodt;
        dlCourseList3.DataBind();
        con.Close();
    }


}