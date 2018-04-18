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
        if (Session["SessionUserMailID"] != null && Session["SessionUserPosition"].ToString() == "Employee" && Session["SessionUserSecondryPosition"].ToString() != "Approver")
        {
            //True
            empprofile.Text = Session["SessionEmployeeName"].ToString();
            Approver.Visible = false;
            Employee.Visible = true;
        }
        else if (Session["SessionUserMailID"] != null && Session["SessionUserPosition"].ToString() == "Employee" && Session["SessionUserSecondryPosition"].ToString() == "Approver")
        {
            //for approver connections
            if(Session["SessionUserSecondryPositioncheck"].ToString() == "true")
            {
                empprofile.Text = Session["SessionEmployeeName"].ToString();
                Approver.Visible = true;
                Employee.Visible = false;
            }
            else
            {
                empprofile.Text = Session["SessionEmployeeName"].ToString();
                Approver.Visible = false;
                Employee.Visible = true;
            }
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
