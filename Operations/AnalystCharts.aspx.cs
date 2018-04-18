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

public partial class Operations_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        {
            if (Session["SessionEmployeeID"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
        }
    }
        [WebMethod(EnableSession = true)]
    public static List<Status> GetAnalystTicketStatusData()
    {
        DataTable dt = new DataTable();
        string conString1 = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
        string empID = HttpContext.Current.Session["SessionEmployeeID"].ToString();
        using (SqlConnection con = new SqlConnection(conString1))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select TMSTicketStatus, count(TMSTicketID) from TMSTicketmaster where TMSTicketOwnership = '" + empID + "' group by TMSTicketStatus", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
        }
        List<Status> dataList = new List<Status>();
        foreach (DataRow dtrow in dt.Rows)
        {
            Status details = new Status();
            details.StatusType = dtrow[0].ToString();
            details.StatusTotal = Convert.ToInt32(dtrow[1]);
            dataList.Add(details);
        }
        return dataList;
    }

    [WebMethod(EnableSession = true)]
    public static List<Priority> GetAnalystTicketPriorityData()
    {
        DataTable dt = new DataTable();
        string conString1 = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
        string empID = HttpContext.Current.Session["SessionEmployeeID"].ToString();
        using (SqlConnection con = new SqlConnection(conString1))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select TMSTicketPriority, count(TMSTicketID) from TMSTicketmaster where TMSTicketOwnership ='" + empID + "'  group by TMSTicketPriority", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
        }
        List<Priority> dataList = new List<Priority>();
        foreach (DataRow dtrow in dt.Rows)
        {
            Priority details = new Priority();
            details.PriorityType = dtrow[0].ToString();
            details.PriorityTotal = Convert.ToInt32(dtrow[1]);
            dataList.Add(details);
        }
        return dataList;
    }


    public class Status
    {
        public string StatusType { get; set; }
        public int StatusTotal { get; set; }
    }

    public class Priority
    {
        public string PriorityType { get; set; }
        public int PriorityTotal { get; set; }
    }

}
