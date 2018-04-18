<%@ WebHandler Language="C#" Class="HW_Asset_Name_Handler" %>

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

public class HW_Asset_Name_Handler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        string term = context.Request["term"] ?? "";
        List<string> listofassetnames = new List<string>();
        string constr = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            //SqlCommand cmd = new SqlCommand("Hardware_Asset_Retrieve", con);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = "Hardware_Asset_Retrieve";
            //cmd.Parameters.Add(new SqlParameter()
            //{
            //    ParameterName = "@AssetName",
            //    Value = term,
            //});
            con.Open();
            SqlCommand cmd = new SqlCommand("select AssetName from HardwareDbTable where AssetName like @Assetname + '%' group by AssetName", con);
            cmd.Parameters.Add(new SqlParameter()
            {
                ParameterName = "@AssetName",
                Value = term,
            });
            SqlDataReader rdr = cmd.ExecuteReader();
            //context.Session["hwname"] = rdr["AssetName"].ToString();
            while (rdr.Read())
            {
                listofassetnames.Add(rdr["AssetName"].ToString());
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