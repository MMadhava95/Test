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
   
 public string conString1 = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {

        
    }

    [WebMethod]
    public static List<Status> GetTicketStatusData()
    {
        DataTable dt = new DataTable();


        string conString1 = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;

        using (SqlConnection con = new SqlConnection(conString1))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(" select TMSTicketStatus,count(TMSTicketID) from TMSTicketMaster group by TMSTicketStatus", con);
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


    [WebMethod]
    public static List<Priority> GetTicketPriorityData()
    {
        DataTable dt = new DataTable();
        string conString1 = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;

        using (SqlConnection con = new SqlConnection(conString1))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select TMSTicketPriority,count(TMSTicketID) from TMSTicketMaster where Level2Escalation<GETDATE() group by TMSTicketPriority;", con);

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

    [WebMethod]
    public static List<Location> GetTicketLocationData()
    {
        DataTable dt = new DataTable();
        string conString1 = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;

        using (SqlConnection con = new SqlConnection(conString1))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select TMSTicketLoaction,COUNT(TMSTicketID) from TMSTicketMaster group by TMSTicketLoaction", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
        }
        List<Location> dataList = new List<Location>();
        foreach (DataRow dtrow in dt.Rows)
        {
            Location details = new Location();
            details.LocationName = dtrow[0].ToString();
            details.LocationTotal = Convert.ToInt32(dtrow[1]);
            dataList.Add(details);
        }
        return dataList;
    }

    [WebMethod]
    public static List<Department> GetTicketDepartmentData()
    {
        DataTable dt = new DataTable();
        string conString1 = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;

        using (SqlConnection con = new SqlConnection(conString1))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select TMSTicketDepartment,COUNT(TMSTicketID) from TMSTicketMaster group by tmsticketdepartment", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
        }
        List<Department> dataList = new List<Department>();
        foreach (DataRow dtrow in dt.Rows)
        {
            Department details = new Department();
            details.DepartmentName = dtrow[0].ToString();
            details.DepartmentTotal = Convert.ToInt32(dtrow[1]);
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

    public class Location
    {
        public string LocationName { get; set; }
        public int LocationTotal { get; set; }
    }

    public class Department
    {
        public string DepartmentName { get; set; }
        public int DepartmentTotal { get; set; }
    }

}
