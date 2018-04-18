using System;
using System.Activities.Validation;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EmployeePenReqAssets : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        //ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        if (Session["SessionUserMailID"] != null && Session["SessionUserPosition"].ToString() == "Employee")
        {
            if (!Page.IsPostBack)
            {
                //hwassets();
                bindAdds();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    void bindAdds()
	{
        try
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand swcmd = new SqlCommand("SELECT * " +
                "FROM EmployeeAssetRequestTable " +
                "where (RequestedEmployeeMailId='" + Session["SessionUserMailID"].ToString() + "' AND RequestStatus != 'Approved')", con);
            SqlDataReader swdr = swcmd.ExecuteReader(CommandBehavior.CloseConnection);

            DataTable swdt = new DataTable();
            swdt.Load(swdr);
            gvList.DataSource = swdt;
            gvList.DataBind();
            con.Close();
            if (gvList.Rows.Count > 0)
            {
                gvList.CssClass = "table table-bordered table-striped  table-inverse nomargin";
                gvList.UseAccessibleHeader = true;
                gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvList.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

    protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvList, "Select$" + e.Row.RowIndex);
            e.Row.Attributes["style"] = "cursor:pointer";
        }
    }

    protected void gvList_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label1.Text = gvList.SelectedRow.Cells[0].Text;
        lblheading.Text = Label2.Text = gvList.SelectedRow.Cells[1].Text;
        Label3.Text = gvList.SelectedRow.Cells[2].Text;
        Label4.Text = gvList.SelectedRow.Cells[3].Text;
        Label5.Text = gvList.SelectedRow.Cells[4].Text;
        Label6.Text = gvList.SelectedRow.Cells[5].Text;
        Label7.Text = gvList.SelectedRow.Cells[6].Text;
        string str = gvList.SelectedRow.Cells[7].Text;
        //lblrejdate.InnerText = Label8.Text.Length.ToString();
        if (str.ToString().Length < 10)
        {
            //lblrejdate.InnerText = ""; 
            divrejectdate.Visible = false;
        }
        else
        {
            divrejectdate.Visible = true;
            lblrejdate.InnerText = "Rejected Date";
            Label8.Text = gvList.SelectedRow.Cells[7].Text;
        }
        EmpPenReqMP.Show();
    }
}