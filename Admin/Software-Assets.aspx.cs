using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

public partial class SoftwareAssets : System.Web.UI.Page
{

    string constr = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        //ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        //string userrole = Session["SessionUserRole"].ToString();
    
        if (Session["SessionUserMailID"] != null && Session["SessionUserPosition"].ToString() == "Admin")
        {
            Session["SessionSWClick"] = "Asset Insert";
            if (!Page.IsPostBack)
            {
                bindAdds();
                if (SWgvList.Rows.Count > 0)
                {
                    SWgvList.CssClass = "table table-bordered table-striped  table-inverse nomargin";
                    SWgvList.UseAccessibleHeader = true;
                    SWgvList.HeaderRow.TableSection = TableRowSection.TableHeader;
                    SWgvList.FooterRow.TableSection = TableRowSection.TableFooter;
                }
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    void bindAdds()
    {
        DataTable swdt = new DataTable();
        using (SqlConnection swcon = new SqlConnection(constr))
        {
            swcon.Open();
            SqlCommand swcmd = new SqlCommand("SELECT * FROM [SoftwareAssetsView]", swcon);
            SqlDataReader swdr = swcmd.ExecuteReader(CommandBehavior.CloseConnection);
            swdt.Load(swdr);
            SWgvList.DataSource = swdt;
            SWgvList.DataBind();
            swcon.Close();
            SWgvList.HeaderRow.Cells[0].Attributes["data-class"] = "expand";
        }

        if (SWgvList.Rows.Count > 0)
        {
            SWgvList.CssClass = "table table-bordered table-striped  table-inverse nomargin";
            SWgvList.UseAccessibleHeader = true;
            SWgvList.HeaderRow.TableSection = TableRowSection.TableHeader;
            SWgvList.FooterRow.TableSection = TableRowSection.TableFooter;
        }
    }

    protected void OnRowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(SWgvList, "Select$" + e.Row.RowIndex);
                e.Row.Attributes["style"] = "cursor:pointer";
            }
        }

        catch (Exception ex)
        {
            //string message = string.Format("Message: {0}\\n\\n", ex.Message);
            //message += string.Format("StackTrace: {0}\\n\\n", ex.StackTrace.Replace(Environment.NewLine, string.Empty));
            //message += string.Format("Source: {0}\\n\\n", ex.Source.Replace(Environment.NewLine, string.Empty));
            //message += string.Format("TargetSite: {0}", ex.TargetSite.ToString().Replace(Environment.NewLine, string.Empty));
            ////ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(\"" + message + "\");", true);
            //Session["eswar"] = message;

            //Session["pagename"] = "SoftwareAssets.aspx";
            //Response.Redirect("Errorpage.aspx");
            throw ex;
        }
    }

    protected void OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblID.Text = SWgvList.SelectedRow.Cells[0].Text;
            lblName.Text = SWgvList.SelectedRow.Cells[1].Text;
            lblVersion.Text = SWgvList.SelectedRow.Cells[2].Text;
            lblVendor.Text = SWgvList.SelectedRow.Cells[3].Text;
            lblCategory.Text = SWgvList.SelectedRow.Cells[4].Text;
            lblLicense.Text = SWgvList.SelectedRow.Cells[5].Text;
            lblLocation.Text = SWgvList.SelectedRow.Cells[6].Text;
            lblDOP.Text = SWgvList.SelectedRow.Cells[7].Text;
            lblMail.Text = SWgvList.SelectedRow.Cells[8].Text;
            swmp.Show();
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$('#SWdetailspopup').modal('modal');</script>", false);
        }

        catch (Exception ex)
        {
            //string message = string.Format("Message: {0}\\n\\n", ex.Message);
            //message += string.Format("StackTrace: {0}\\n\\n", ex.StackTrace.Replace(Environment.NewLine, string.Empty));
            //message += string.Format("Source: {0}\\n\\n", ex.Source.Replace(Environment.NewLine, string.Empty));
            //message += string.Format("TargetSite: {0}", ex.TargetSite.ToString().Replace(Environment.NewLine, string.Empty));
            //// ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(\"" + message + "\");", true);
            //Session["eswar"] = message;

            //Session["pagename"] = "SoftwareAssets.aspx";
            //Response.Redirect("Errorpage.aspx");

        }
    }

    protected void SWgvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = SWgvList.Rows[index];
            Session["SessionSWAssetID"] = int.Parse(row.Cells[0].Text);
            if (e.CommandName == "Edit_Click")
            {
                Session["SessionSWClick"] = "Asset Update";
                Session["Sessiondefault"] = "yes";
                Response.Redirect("../Admin/Add-Software-Assets.aspx");
            }
            else if (e.CommandName == "Delete_Click")
            {
                SqlConnection con = new SqlConnection(constr);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "master_sw_proc_delete";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Confirm", "<script>confirm('are you sure?');</script>");
                try
                {
                    cmd.Parameters.Add("@AMS_AssetID", SqlDbType.Int).Value = Session["SessionSWAssetID"];
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('Software Asset deleted successfully');window.location.href='../Admin/Software-Assets.aspx';</script>");
                }
                catch (Exception ex)
                {
                    throw ex; // return error message  
                }
                finally
                {
                    con.Close();
                }
            }
        }

        catch
        {
            //    string message = string.Format("Message: {0}\\n\\n", ex.Message);
            //    message += string.Format("StackTrace: {0}\\n\\n", ex.StackTrace.Replace(Environment.NewLine, string.Empty));
            //    message += string.Format("Source: {0}\\n\\n", ex.Source.Replace(Environment.NewLine, string.Empty));
            //    message += string.Format("TargetSite: {0}", ex.TargetSite.ToString().Replace(Environment.NewLine, string.Empty));
            //    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(\"" + message + "\");", true);
        }
    }
}