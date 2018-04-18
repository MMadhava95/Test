using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;

public partial class Operations_Default : System.Web.UI.Page
{
    public string cs = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
	{
        alert.Visible = false;
        CalendarExtender1.EndDate = DateTime.Now;
        CalendarExtender2.EndDate = DateTime.Now;
        // dateValidator.ValueToCompare = DateTime.Now.ToShortDateString();
       
        //if (Session["ApproverID2"] != null)
        //{
        //    Session["ApproverID2"].ToString();
        //}
        //else
        //{
        //    Response.Redirect("Login.aspx");
        //}

    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        ClaimDataGrid();
    }

    private void ClaimDataGrid()
    {
        SqlConnection con = new SqlConnection(cs);
        using (con)
        {
            using (SqlCommand cmd = new SqlCommand("select ERSClaimID,ERSApproverID,ERSEmployeeID,ERSClaimType,(Currency+'.' + ' '+CAST(ERSBillAmount as varchar(30))) as ERSBillAmount,ERSClaimStatus,ERSClaimProcessDate from ERSClaim Where (ERSClaimDate>='" + TextBox1.Text + "'and ERSClaimDate<='" + TextBox2.Text + "') and ERSApproverID='"+ Session["SessionEmployeeID"].ToString() + "' "))
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

                    GridClaimData.DataSource = dTable;
                    GridClaimData.DataBind();
                    //if (GridClaimData.Columns.Count > 0)
                    //{
                    //    GridClaimData.Columns[0].Visible = false;
                    //    GridClaimData.Columns[6].Visible = false;

                    //}
                    //else
                    //{
                    //    GridClaimData.HeaderRow.Cells[0].Visible = false;
                    //    GridClaimData.HeaderRow.Cells[6].Visible = false;
                    //    foreach (GridViewRow gvr in GridClaimData.Rows)
                    //    {
                    //        gvr.Cells[0].Visible = false;
                    //    }

                    //}
                }
                catch (Exception ex)
                {
                    // Error msg display here  
                    Response.Write(ex.Message);
                }
            }
        }
    }
    protected void Button2_Click1(object sender, EventArgs e)
    {
        if (GridClaimData.Rows.Count > 0)
        {
            ExportGridToExcel();
            TextBox1.Text = string.Empty;
            TextBox2.Text = string.Empty;
            GridClaimData.Visible = false;
        }
        else
        {
            alertmod.Style.Add("background-color", "#ffc2b3");
            alert.Style.Add("background-color", "#ffc2b3");
            Label1.ForeColor = System.Drawing.ColorTranslator.FromHtml("red");
            Label5.ForeColor = System.Drawing.ColorTranslator.FromHtml("black");
            //Label1.Text = "Failure!";
            Label5.Text = "No records found to export to excel";
            alert.Visible = true;
            // ClientScript.RegisterStartupScript(GetType(), "alert", "alert('No data found');", true);

        }

    }


    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    private void ExportGridToExcel()
    {
        //GridClaimData.AllowPaging = true;
        //this.ClaimDataGrid();
        //GridClaimData.ForeColor = System.Drawing.Color.Black;
        GridClaimData.RowStyle.ForeColor = System.Drawing.Color.Black;
        GridClaimData.HeaderStyle.ForeColor = System.Drawing.Color.Black;

        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=ExportGridData.xls");
        Response.ContentType = "File/Data.xls";

        StringWriter StringWriter = new System.IO.StringWriter();
        HtmlTextWriter HtmlTextWriter = new HtmlTextWriter(StringWriter);

        GridClaimData.RenderControl(HtmlTextWriter);
        Response.Write(StringWriter.ToString());
        Response.End();
    }

    protected void GridClaimData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "Claim ID";
            e.Row.Cells[1].Text = "Approver ID";
            e.Row.Cells[2].Text = "Employee ID";
            e.Row.Cells[3].Text = "Claim Type";
            e.Row.Cells[4].Text = "Bill Amount";
            e.Row.Cells[5].Text = "Claim Status";
            e.Row.Cells[6].Text = "Claim Process Date";
        }

    }

    protected void GridClaimData_SelectedIndexChanged(object sender, EventArgs e)
    {


    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            // dateValidator.Text = "Result: Valid!";
            ClaimDataGrid();

        }
        else
        {

            //dateValidator.Text = "Should be Lessthan or Equal to current Date";
            // TextBox1.Text = string.Empty;
            TextBox2.Text = string.Empty;
        }

        // ClaimDataGrid();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        int row = GridClaimData.Rows.Count;
        if (row > 0)
        {
            ExportGridToExcel();
            TextBox1.Text = string.Empty;
            TextBox2.Text = string.Empty;
            GridClaimData.Visible = false;

        }
        else
        {
            alertmod.Style.Add("background-color", "#ffc2b3");
            alert.Style.Add("background-color", "#ffc2b3");
            Label1.ForeColor = System.Drawing.ColorTranslator.FromHtml("red");
            Label5.ForeColor = System.Drawing.ColorTranslator.FromHtml("black");
            // Label1.Text = "Failure!";
            Label5.Text = "No records found to export to excel ";
            alert.Visible = true;
            // ClientScript.RegisterStartupScript(GetType(), "alert", "alert('No data found to export');", true);
        }

    }
}