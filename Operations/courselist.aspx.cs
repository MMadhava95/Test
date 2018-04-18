using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class courselist : System.Web.UI.Page
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
        SqlConnection con = new SqlConnection(constr);
        con.Open();
        SqlCommand cmd = new SqlCommand("select * from CreateCourseTable where coursetype='Business' ", con);
        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        DataTable videodt = new DataTable();
        videodt.Load(dr);
        dlCourseList.DataSource = videodt;
        dlCourseList.DataBind();
        con.Close();
    }
    void VideosBindGrid1()
    {
        SqlConnection con = new SqlConnection(constr);
        con.Open();
        SqlCommand cmd = new SqlCommand("select * from CreateCourseTable where coursetype='Technical' ", con);
        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        DataTable videodt = new DataTable();
        videodt.Load(dr);
        DataList1.DataSource = videodt;
        DataList1.DataBind();
        con.Close();
    }
    void VideosBindGrid2()
    {
        SqlConnection con = new SqlConnection(constr);
        con.Open();
        SqlCommand cmd = new SqlCommand("select * from CreateCourseTable where coursetype='development' ", con);
        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        DataTable videodt = new DataTable();
        videodt.Load(dr);
        DataList2.DataSource = videodt;
        DataList2.DataBind();
        con.Close();
    }


}