using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class EmployeeHardwareAssets : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        //ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;

        if (Session["SessionUserMailID"] != null && Session["SessionUserPosition"].ToString() == "Employee")
        {
            if (!Page.IsPostBack)
            {
                HwAssets();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }

    void HwAssets()
    {
        SqlConnection con = new SqlConnection(constr);
        con.Open();
        SqlCommand swcmd = new SqlCommand("SELECT * FROM EmployeeAssetRequestTable as emp " +
            "join Asset_Master as mas  on emp.AssetID = mas.AMS_AssetID " +
            "join Hardware_Physical_Assets as HPA on HPA.AMSHW_PH_AssetID = emp.AssetID " +
            "where RequestStatus = 'Approved' AND AssetType = 'Hardware Assets' AND RequestedEmployeeMailId = '" + Session["SessionUserMailID"].ToString() + "'", con);
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
        Label8.Text = gvList.SelectedRow.Cells[7].Text;
        Label9.Text = gvList.SelectedRow.Cells[8].Text;
        Label10.Text = gvList.SelectedRow.Cells[9].Text;
        Label11.Text = gvList.SelectedRow.Cells[10].Text;
        Label12.Text = gvList.SelectedRow.Cells[11].Text;
        Label13.Text = gvList.SelectedRow.Cells[12].Text;
        Label14.Text = gvList.SelectedRow.Cells[13].Text;
        Label15.Text = gvList.SelectedRow.Cells[14].Text;
        Label16.Text = gvList.SelectedRow.Cells[15].Text;
        EmpHAMP.Show();
    }
}