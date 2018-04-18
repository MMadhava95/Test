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

public partial class Admin_Default : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{

	}
    [WebMethod]
    public static List<object> GetChartData()
    {
        string query = "SELECT LmsCourseName, COUNT(LmsCourseName) TotalOrders";
        query += " FROM LMSCourseregistration group by LmsCourseName ";
        string constr = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
        List<object> chartData = new List<object>();
        chartData.Add(new object[]
        {
                "LmsCourseName", "TotalNumberOfUsersRegistered"
        });
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        chartData.Add(new object[]
                        {
                        sdr["LmsCourseName"], sdr["TotalOrders"]
                        });
                    }
                }
                con.Close();
                return chartData;
            }
        }
    }
    protected void btnlogout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Response.Redirect("..\\Login.aspx");
    }
}