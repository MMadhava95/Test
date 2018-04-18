<%@ WebHandler Language="C#" Class="SW_Asset_Name_Handler" %>

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

public class SW_Asset_Name_Handler : IHttpHandler {
    
    public void ProcessRequest(HttpContext context)
        {

            string term = context.Request["term"] ?? "";
            List<string> listofassetnames = new List<string>();
            string constr = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
              
                con.Open();
                SqlCommand cmd = new SqlCommand("select top 10 SWAssetName from SoftwareDbTable where SWAssetName like @SWAssetName + '%' group by SWAssetName", con);
                cmd.Parameters.Add(new SqlParameter()
                {
                    ParameterName = "@SWAssetName",
                    Value = term,
                });
                SqlDataReader rdr = cmd.ExecuteReader();
                //context.Session["hwname"] = rdr["AssetName"].ToString();
                while (rdr.Read())
                {
                    listofassetnames.Add(rdr["SWAssetName"].ToString());
                }
                con.Close();

            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            context.Response.Write(jss.Serialize(listofassetnames));


            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

}