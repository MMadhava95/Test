using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class history : System.Web.UI.Page
{
    private SqlConnection con1;
    private SqlCommand com;
    private string constr, query;
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    DataTable dt;
    private void connection()
    {
        constr = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ToString();
        con1 = new SqlConnection(constr);

    }
    
    protected void Page_Load(object sender, EventArgs e)
	{
        if (Session["SessionEmployeeID"]==null)
        {
            Response.Redirect("~/Login.aspx");
        }

            if (!IsPostBack)
        {
            BindGridview();
        }
    }
  

    
    public void BindGridview()
    {
        connection();
        query = "SELECT TMSTicketID,TMSTicketSubject, TMSTicketPriority, TMSTicketStatus, TMSTicketDate FROM TMSTicketMaster WHERE EmployeeID = '" + Session["SessionEmployeeID"] + "' ORDER BY TMSTicketDate DESC";
        //not recommended this i have written just for example,write stored procedure for security
        con1.Open();
        com = new SqlCommand(query, con1);
        SqlDataAdapter da = new SqlDataAdapter(com);
        dt = new DataTable();
        da.Fill(dt);
        ViewState["Paging"] = dt;
        gvList.DataSource = dt;
        gvList.DataBind();
        con1.Close();

        // grid styles
        if (gvList.Rows.Count > 0)
        {
            gvList.CssClass = "table table-bordered table-striped  table-inverse nomargin";
            gvList.UseAccessibleHeader = true;
            gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
            gvList.FooterRow.TableSection = TableRowSection.TableFooter;
        }
    }
    protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        try
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvList.Rows[index];
            TextBox1.Text = row.Cells[0].Text;
            //sending ticketID through session to TicketInfo page
            Session["TicketID"] = TextBox1.Text;
            Response.Redirect("ticket-info.aspx");

        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception is :" + ex);
        }
        if (e.CommandName == "Edit_Click")
        {
            Response.Redirect("ticket-info.aspx");
        }
    }
    protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvList, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "Click to select a ticket.";

        }
       
    }

}
