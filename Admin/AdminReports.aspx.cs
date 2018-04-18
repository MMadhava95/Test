using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;


public partial class Admin_Default : System.Web.UI.Page
{
    string conString = System.Configuration.ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        CalendarExtender1.EndDate = DateTime.Now;
        CalendarExtender2.EndDate = DateTime.Now;
        if (Session["SessionEmployeeID"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }

    }

    protected void generateReport_Click(object sender, EventArgs e)
    {
        exportToExcel.Visible = true;
        exportToWord.Visible = true;
        if (fromDate.Text == string.Empty || todate.Text == string.Empty)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select dates')", true);
        }
        else
        {
            TicketDataGrid();
        }
    }

    private void TicketDataGrid()
    {
        if (DropDownList1.SelectedValue == "All")
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand("select  TMSTicketID as TicketID ,TMSTicketSubject as Subject,TMSTicketDepartment as Department,TMSTicketDescription as Description,TMSTicketPriority as Priority,TMSTicketStatus as Status,TMSTicketDate as Date,TMSTicketOwnership as Ownership from TMSTicketMaster Where TMSTicketDate>='" + fromDate.Text + "'and TMSTicketDate<='" + todate.Text + "'"))
                {
                    SqlDataAdapter dt = new SqlDataAdapter();
                    try
                    {
                        cmd.Connection = con;
                        con.Open();
                        dt.SelectCommand = cmd;

                        DataTable dTable = new DataTable();
                        dt.Fill(dTable);
                        SqlDataReader sdr = cmd.ExecuteReader();
                        GridTicketData.DataSource = dTable;
                        GridTicketData.DataBind();
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.Message);
                        // Error msg display here  
                    }
                }
            }
        }
        else if (DropDownList1.SelectedValue == "Sla")
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand("select  TMSTicketID as TicketID ,TMSTicketSubject as Subject,TMSTicketDepartment as Department,TMSTicketDescription as Description,TMSTicketPriority as Priority,TMSTicketStatus as Status,TMSTicketDate as SubmittedDate,TMSTicketOwnership as Ownership,DeviationTime from TMSTicketMaster Where (TMSTicketDate>='" + fromDate.Text + "'and TMSTicketDate<='" + todate.Text + "') and  (Level2Escalation<GETDATE() or Level3Escalation<GETDATE()) "))
                {
                    SqlDataAdapter dt = new SqlDataAdapter();
                    try
                    {
                        cmd.Connection = con;
                        con.Open();
                        dt.SelectCommand = cmd;

                        DataTable dTable = new DataTable();
                        dt.Fill(dTable);
                        SqlDataReader sdr = cmd.ExecuteReader();
                        GridTicketData.DataSource = dTable;
                        GridTicketData.DataBind();
                    }
                    catch (Exception)
                    {
                        // Error msg display here  
                    }
                }
            }
        }

        else
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand("select TMSTicketID as TicketID ,TMSTicketSubject as Subject,TMSTicketDepartment as Department,TMSTicketDescription as Description,TMSTicketPriority as Priority,TMSTicketStatus as Status,TMSTicketDate as SubmittedDate,TMSTicketOwnership as Ownership from TMSTicketMaster Where TMSTicketDate>='" + fromDate.Text + "'and TMSTicketDate<='" + todate.Text + "' and TMSTicketPriority='" + DropDownList1.SelectedValue + "'"))
                {
                    SqlDataAdapter dt = new SqlDataAdapter();
                    try
                    {
                        cmd.Connection = con;
                        con.Open();
                        dt.SelectCommand = cmd;

                        DataTable dTable = new DataTable();
                        dt.Fill(dTable);
                        SqlDataReader sdr = cmd.ExecuteReader();
                        GridTicketData.DataSource = dTable;
                        GridTicketData.DataBind();
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.Message);
                        // Error msg display here  
                    }
                }

            }
        }
    }

    protected void GridTicketData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridTicketData.PageIndex = e.NewPageIndex;
        TicketDataGrid();
    }

    protected void exportToExcel_Click(object sender, EventArgs e)
    {
        int row = GridTicketData.Rows.Count;
        if (row > 0)
        {
            ExportGridToExcel();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No data found to export.')", true);
        }
    }

    private void ExportGridToExcel()
    {
        GridTicketData.AllowPaging = true;
        this.TicketDataGrid();
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename= " + DropDownList1.SelectedValue + "" + "Data.xls");
        Response.ContentType = "File/Data.xls";
        StringWriter StringWriter = new System.IO.StringWriter();
        HtmlTextWriter HtmlTextWriter = new HtmlTextWriter(StringWriter);

        GridTicketData.RenderControl(HtmlTextWriter);
        Response.Write(StringWriter.ToString());
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // controller   
    }

    protected void exportToWord_Click(object sender, EventArgs e)
    {

        int row = GridTicketData.Rows.Count;
        if (row > 0)
        {
            ExportGridToword();

        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No data found to export.')", true);
        }
    }

    private void ExportGridToword()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "Report" + DateTime.Now + ".doc";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/msword";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        GridTicketData.GridLines = GridLines.Both;
        GridTicketData.HeaderStyle.Font.Bold = true;
        GridTicketData.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();
    }
}
