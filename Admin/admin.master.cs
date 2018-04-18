using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class emp : System.Web.UI.MasterPage
{
	protected void Page_Load(object sender, EventArgs e)
	{
        if (Session["SessionUserMailID"] != null && Session["SessionUserPosition"].ToString() == "Admin")
        {
            //True
            adminprofile.Text = Session["SessionEmployeeName"].ToString();
            
            //Response.Redirect("~/Operations/Dashboard.aspx");
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }
    protected void btnLogout_Click(object Source, EventArgs e)
    {
        Response.Redirect("~/Login.aspx");
    }
}
