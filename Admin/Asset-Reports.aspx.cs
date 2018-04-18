using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Globalization;
using System.Configuration;
using System.Drawing;

public partial class AssetReports : System.Web.UI.Page
{
    DateTime value1 = DateTime.Now;
    DateTime value2 = DateTime.Now;
    string constr = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        //ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        if (Session["SessionUserMailID"] != null && Session["SessionUserPosition"].ToString() == "Admin")
        {
            eswar.StartDate = DateTime.Now.AddYears(-200);
            CalendarExtender2.StartDate = DateTime.Now.AddYears(-200);
            eswar.EndDate = DateTime.Now;
            CalendarExtender2.EndDate = DateTime.Now;
            Dateorotherlist.Visible = true;
            exportToExcel.Visible = false;

            for (int i = 0; i < 3; i++)
            {
                ReportAssetType.Items[i].Attributes.Add("style", "color:black");
                Dateorotherlist.Items[i].Attributes.Add("style", "color:black");
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }
    public DateTime conv(string var1)
    {
        value1.ToString("");
        return value1;
    }
    private void TicketDataGrid()
    {
        SqlConnection con = new SqlConnection(constr);
        con.Open();

        using (con)
        {
            using (SqlCommand cmd = new SqlCommand("select * from Asset_Master Where AMS_Date_of_Purchase between'" + string.Format("{0:yyyy-MM-dd}", DateTime.Parse(fromDate.Text, new CultureInfo("gu-IN", false))) + "'AND '" + string.Format("{0:yyyy-MM-dd}", DateTime.Parse(todate.Text, new CultureInfo("gu-IN", false))) + "' AND AMS_AssetType Like'" + '%' + ReportAssetType.SelectedValue + '%' + "'"))
            {
                SqlDataAdapter dt = new SqlDataAdapter();
                try
                {
                    cmd.Connection = con;

                    dt.SelectCommand = cmd;

                    DataTable dTable = new DataTable();
                    dt.Fill(dTable);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    GridTicketData.DataSource = dTable;
                    GridTicketData.DataBind();

                    if (GridTicketData.Rows.Count != 0)
                    {
                        exportToExcel.Visible = true;

                        GridTicketData.CssClass = "table table-bordered table-striped  table-inverse nomargin";
                        GridTicketData.UseAccessibleHeader = true;
                        //GridTicketData.HeaderRow.TableSection = TableRowSection.TableHeader;
                        //GridTicketData.FooterRow.TableSection = TableRowSection.TableFooter;
                    }
                    else
                    {
                        exportToExcel.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('There is no data');", true);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        con.Close();
    }

    protected void GridTicketData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridTicketData.PageIndex = e.NewPageIndex;
        TicketDataGrid(); //bindgridview will get the data source and bind it again
        exportToExcel.Visible = true;
    }

    private void ExportGridToExcel()
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=ExportGridData.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            using (System.IO.StringWriter StringWriter = new System.IO.StringWriter())
            {
                HtmlTextWriter HtmlTextWriter = new HtmlTextWriter(StringWriter);
                GridTicketData.AllowPaging = false;
                this.TicketDataGrid();
                GridTicketData.CssClass = "rounded_corners";
                foreach (GridViewRow row in GridTicketData.Rows)
                {
                    foreach (TableCell cell in row.Cells)
                    {
                        cell.Attributes.CssStyle["text-align"] = "center";
                        List<Control> controls = new List<Control>();
                        //Add controls to be removed to Generic List
                        foreach (Control control in cell.Controls)
                        {
                            controls.Add(control);
                        }
                        foreach (Control control in controls)
                        {
                            switch (control.GetType().Name)
                            {
                                case "Label":
                                    cell.Controls.Add(new Literal { Text = (control as Label).Text });
                                    break;
                            }
                            cell.Controls.Remove(control);
                        }
                    }
                }
                GridTicketData.RenderControl(HtmlTextWriter);
                Response.Write(StringWriter.ToString());
                Response.End();

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }

    protected void exportToExcel_Click1(object sender, EventArgs e)
    {
        ExportGridToExcel();
    }

    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        DateTime From_Date = DateTime.Parse(fromDate.Text, new CultureInfo("gu-IN", false));
        DateTime To_Date = DateTime.Parse(todate.Text, new CultureInfo("gu-IN", false));
        if (From_Date < To_Date)
        {
            exportToExcel.Visible = true;
            TicketDataGrid();
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('From Date should be Valid.');", true);
            fromDate.Text = todate.Text = "";
            fromDate.Focus();
        }
    }
    
    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        divDate.Visible = true;
    }
    
    protected void Dateorotherlist_SelectedIndexChanged(object sender, EventArgs e)
    {
        Dateorotherlist.Attributes["value"] = "this.value";
        Dateorotherlist.Visible = true;
        if (Page.IsPostBack == true)
        {
            if (ReportAssetType.SelectedIndex == 0)
            {
                ReportAssetTypeRequiredFieldValidator.IsValid = false;
            }
            else if (ReportAssetType.SelectedIndex != 0 && Dateorotherlist.SelectedIndex == 1)
            {
                divDate.Visible = true;
                LinkButton3.Visible = true;
                searchdiv.Visible = false;
                Dateorotherlist.Visible = true;
                Othersgridview.Visible = false;
                GridTicketData.Visible = true;
                Otherexporttoexcell.Visible = false;
                LinkButton3.Visible = true;
            }
            else if (ReportAssetType.SelectedIndex != 0 && Dateorotherlist.SelectedIndex == 2)
            {
                divDate.Visible = false;
                searchdiv.Visible = true;
                Dateorotherlist.Visible = true;
                Othersgridview.Visible = true;
                GridTicketData.Visible = false;
                exportToExcel.Visible = false;
                LinkButton3.Visible = false;
            }
        }
    }



    private void OtherTextexportToexcell()
    {
        //try
        //{
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=ExportGridData.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (System.IO.StringWriter StringWriter = new System.IO.StringWriter())
        {
            HtmlTextWriter HtmlTextWriter = new HtmlTextWriter(StringWriter);


            Othersgridview.AllowPaging = false;

            this.oThertext();

            Othersgridview.CssClass = "rounded_corners";
            foreach (GridViewRow row in Othersgridview.Rows)
            {

                foreach (TableCell cell in row.Cells)
                {
                    cell.Attributes.CssStyle["text-align"] = "center";
                    List<Control> controls = new List<Control>();
                    //Add controls to be removed to Generic List
                    foreach (Control control in cell.Controls)
                    {
                        controls.Add(control);
                    }
                    //Loop through the controls to be removed and replace then with Literal
                    foreach (Control control in controls)
                    {
                        switch (control.GetType().Name)
                        {
                            case "Label":
                                cell.Controls.Add(new Literal { Text = (control as Label).Text });
                                break;
                        }
                        cell.Controls.Remove(control);
                    }
                }

            }
            Othersgridview.RenderControl(HtmlTextWriter);
            Response.Write(StringWriter.ToString());
            Response.End();

        }
        //}
        //catch (Exception ex)
        //{
        //   throw ex;
        //}
    }

    protected void LinkButton1_Click1(object sender, EventArgs e)
    {
        OtherTextexportToexcell();
    }

    private void oThertext()
    {
        SqlConnection cono = new SqlConnection(constr);
        cono.Open();

        try
        {

            string str = "select AMS_AssetID,AMS_AssetName,AMS_AssetType,AMS_Company_Location,AMS_Date_of_Purchase,AMS_Remarks from Asset_Master where (AMS_AssetID Like '%' + @AMS_AssetID +'%' or " + "AMS_AssetName Like '%' + @AMS_AssetName + '%'  or " + " AMS_AssetType = @AMS_AssetType or " + " AMS_Company_Location Like '%' + @AMS_Company_Location )";
            SqlCommand cmd = new SqlCommand(str, cono);
            cmd.Parameters.Add("@AMS_AssetID", SqlDbType.NVarChar).Value = txtFindVendor.Text;
            cmd.Parameters.Add("@AMS_AssetName", SqlDbType.NVarChar).Value = txtFindVendor.Text;
            cmd.Parameters.Add("@AMS_AssetType", SqlDbType.NVarChar).Value = ReportAssetType.SelectedValue.Trim();
            cmd.Parameters.Add("@AMS_Company_Location", SqlDbType.NVarChar).Value = txtFindVendor.Text.Trim();
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            da.Fill(ds, "@AMS_AssetID");
            da.Fill(ds, "@AMS_AssetName");
            da.Fill(ds, "@AMS_AssetType");
            da.Fill(ds, "@AMS_Company_Location");

            Othersgridview.DataSource = ds;
            Othersgridview.DataBind();
        }
        catch (Exception)
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('There is no approved Data.');", true);

        }
        finally
        {
            cono.Close();
        }
    }
    protected void txtFindVendor_TextChanged(object sender, EventArgs e)
    {
        if (Page.IsPostBack == true)
        {
            searchdiv.Visible = true;
            if (txtFindVendor.Text != string.Empty.ToString() && ReportAssetType.SelectedIndex == 1)
            {
                Othersgridview.DataSourceID = "SWSqlDataSource";
                Othersgridview.DataBind();
                if (Othersgridview.Rows.Count == 0)
                {
                    txtFindVendor.Text = string.Empty;
                    Othersgridview.Visible = true;
                    Othersgridview.EmptyDataText = "No Records Found / Invalid Search";
                    Otherexporttoexcell.Visible = false;
                }
                else
                {
                    Otherexporttoexcell.Visible = true;
                }
            }
            else if (txtFindVendor.Text != string.Empty.ToString() && ReportAssetType.SelectedIndex == 2)
            {
                Othersgridview.DataSourceID = "HWSqlDataSource";
                Othersgridview.DataBind();
                if (Othersgridview.Rows.Count == 0)
                {
                    txtFindVendor.Text = string.Empty;
                    Othersgridview.Visible = true;
                    Othersgridview.EmptyDataText = "No Records Found / Invalid Search";
                    Otherexporttoexcell.Visible = false;
                }
                else
                {
                    Otherexporttoexcell.Visible = true;
                }
            }
            else
            {
                Response.Redirect("~/Admin/Asset-Reports.aspx");
            }
        }
    }

    //Other text export code
    private void BindGrid()
    {
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM HardwareDbTable"))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        GridTicketData.DataSource = dt;
                        GridTicketData.DataBind();
                    }
                }
            }
        }
    }

    protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridTicketData.PageIndex = e.NewPageIndex;
        this.BindGrid();
    }

    protected void ExportToExcel(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //To Export all pages
            GridTicketData.AllowPaging = false;
            this.BindGrid();

            foreach (TableCell cell in GridTicketData.HeaderRow.Cells)
            {
                cell.BackColor = GridTicketData.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in GridTicketData.Rows)
            {
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = GridTicketData.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = GridTicketData.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }
            GridTicketData.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }

    protected void Otherexporttoexcell_Click(object sender, EventArgs e)
    {
        Othergrid();
    }

    private void Othergrid()
    {
        //try
        //{
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=ExportGridData.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (System.IO.StringWriter StringWriter = new System.IO.StringWriter())
        {
            HtmlTextWriter HtmlTextWriter = new HtmlTextWriter(StringWriter);


            Othersgridview.AllowPaging = false;

            this.oThertext();

            Othersgridview.CssClass = "rounded_corners";
            foreach (GridViewRow row in Othersgridview.Rows)
            {

                foreach (TableCell cell in row.Cells)
                {
                    cell.Attributes.CssStyle["text-align"] = "center";
                    List<Control> controls = new List<Control>();
                    //Add controls to be removed to Generic List
                    foreach (Control control in cell.Controls)
                    {
                        controls.Add(control);
                    }
                    //Loop through the controls to be removed and replace then with Literal
                    foreach (Control control in controls)
                    {
                        switch (control.GetType().Name)
                        {
                            case "Label":
                                cell.Controls.Add(new Literal { Text = (control as Label).Text });
                                break;
                        }
                        cell.Controls.Remove(control);
                    }
                }

            }
            Othersgridview.RenderControl(HtmlTextWriter);
            Response.Write(StringWriter.ToString());
            Response.End();

        }
        //}
        //catch (Exception ex)
        //{
        //   throw ex;
        //}
    }

    protected void back_Click(object sender, EventArgs e)
    {
        txtFindVendor.Visible = true;

    }

    protected void ReportAssetType_SelectedIndexChanged1(object sender, EventArgs e)
    {
        ReportAssetType.Attributes["value"] = "this.value";
        if (Page.IsPostBack == true)
        {
            txtFindVendor.Text = null;
        }
        if (ReportAssetType.SelectedIndex == 1 || ReportAssetType.SelectedIndex == 2)
        {
            txtFindVendor.Text = null;
        }
    }

    protected void Othersgridview_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header && ReportAssetType.SelectedIndex == 1)
        {
            e.Row.Cells[0].Text = "Asset ID";
            e.Row.Cells[1].Text = "Name";
            e.Row.Cells[2].Text = "Version";
            e.Row.Cells[3].Text = "Vendor";
            e.Row.Cells[4].Text = "Category";
            e.Row.Cells[5].Text = "License Type";
            e.Row.Cells[6].Text = "Company Location";
            e.Row.Cells[7].Text = "Purchased Date";
            e.Row.Cells[8].Text = "Vendor Mail ID";
            e.Row.Cells[9].Text = "Asset Type";
        }
        else if (e.Row.RowType == DataControlRowType.Header && ReportAssetType.SelectedIndex == 2)
        {
            e.Row.Cells[0].Text = "Asset ID";
            e.Row.Cells[1].Text = "Name";
            e.Row.Cells[2].Text = "Manufacturer";
            e.Row.Cells[3].Text = "Model";
            e.Row.Cells[4].Text = "Cost";
            e.Row.Cells[5].Text = "Purchased Date";
            e.Row.Cells[6].Text = "Characteristics";
            e.Row.Cells[7].Text = "Company Location";
            e.Row.Cells[8].Text = "Warranty Status";
            e.Row.Cells[9].Text = "Asset Type";
            e.Row.Cells[10].Text = "Serial Number";
        }
    }
}