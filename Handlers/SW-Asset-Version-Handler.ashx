<%@ WebHandler Language="C#" Class="SW_Asset_Version_Handler" %>

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

public class SW_Asset_Version_Handler : IHttpHandler,IReadOnlySessionState {
    
    public void ProcessRequest(HttpContext context)
        {
            string term = context.Request["term"] ?? "";
            List<string> listofassetnames = new List<string>();
            string constr = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("select top 10 SWVersion from SoftwareDbTable where SWAssetName ='" + context.Session["AssetName"].ToString() + "' AND SWVersion like @SWVersion + '%' group by SWVersion", con);
                //SqlCommand cmd = new SqlCommand("select SWVersion from SoftwareDbTable where SWVersion like @SWVersion + '%'", con);
                cmd.Parameters.Add(new SqlParameter()
                {
                    ParameterName = "@SWVersion",
                    Value = term,
                });

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    listofassetnames.Add(rdr["SWVersion"].ToString());
                }
                con.Close();
                //context.Session["Name"] = "@AssetName";
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            context.Response.Write(jss.Serialize(listofassetnames));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

}