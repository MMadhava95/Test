<%@ WebHandler Language="C#" Class="HW_Asset_Model_Handler" %>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Web.SessionState;

public class HW_Asset_Model_Handler : IHttpHandler, IReadOnlySessionState {
    
    public void ProcessRequest(HttpContext context)
        {
            string term = context.Request["term"] ?? "";
            List<string> listofassetnames = new List<string>();
            string constr = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("select top 10 Model from HardwareDbTable where Manufacturer ='" + context.Session["hwmake"].ToString() + "' and Model like @Model + '%' group by Model", con);

                cmd.Parameters.Add(new SqlParameter()
                {
                    ParameterName = "@Model",
                    Value = term,
                });

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    listofassetnames.Add(rdr["Model"].ToString());
                }
                con.Close();
                //context.Session["Name"] = "@AssetName";
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            context.Response.Write(jss.Serialize(listofassetnames));
        }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}