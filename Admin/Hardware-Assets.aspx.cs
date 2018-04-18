using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HardwareAssets : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        //ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        if (Session["SessionUserMailID"] != null && Session["SessionUserPosition"].ToString() == "Admin")
        {
            Session["SessionHWClick"] = "Asset Insert";
            if (!Page.IsPostBack)
            {
                if (!Page.IsPostBack)
                {
                    HWdatabind();
                }
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    void HWdatabind()
    {
        DataTable hwdt = new DataTable();

        using (SqlConnection hwcon = new SqlConnection(constr))
        {
            hwcon.Open();
            SqlCommand hwcmd = new SqlCommand("SELECT * FROM [HardwareAssetsView]", hwcon);
            SqlDataReader hwdr = hwcmd.ExecuteReader(CommandBehavior.CloseConnection);
            hwdt.Load(hwdr);
            HWgvList.DataSource = hwdt;
            HWgvList.DataBind();
            hwcon.Close();

            //SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            //DataTable dt = new DataTable();
            //dt.Load(dr);

            //GridView1.DataSource = dt;
            //GridView1.DataBind();
        }

        if (HWgvList.Rows.Count > 0)
        {
            HWgvList.CssClass = "table table-bordered table-striped  table-inverse nomargin";
            HWgvList.UseAccessibleHeader = true;
            HWgvList.HeaderRow.TableSection = TableRowSection.TableHeader;
            HWgvList.FooterRow.TableSection = TableRowSection.TableFooter;
        }
    }

    protected void OnRowDataBound1(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(HWgvList, "Select$" + e.Row.RowIndex);
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

            //Session["pagename"] = "HardwareAssets.aspx";
            //Response.Redirect("Errorpage.aspx");
        }
    }

    protected void OnSelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            //lbldetails.Text = lblAssetName.Text;
            lblID.Text = HWgvList.SelectedRow.Cells[0].Text;
            lblName.Text = HWgvList.SelectedRow.Cells[1].Text;
            lblMake.Text = HWgvList.SelectedRow.Cells[2].Text;
            lblModel.Text = HWgvList.SelectedRow.Cells[3].Text;
            lblCost.Text = (HWgvList.SelectedRow.FindControl("Label1") as Label).Text;
            lblDOP.Text = HWgvList.SelectedRow.Cells[5].Text;
            lblSpecs.Text = HWgvList.SelectedRow.Cells[6].Text;
            lblLocation.Text = HWgvList.SelectedRow.Cells[7].Text;
            lblwsts.Text = HWgvList.SelectedRow.Cells[8].Text;
            lblSno.Text = HWgvList.SelectedRow.Cells[10].Text;
            hwmp.Show();
        }

        catch (Exception ex)
        {
            //string message = string.Format("Message: {0}\\n\\n", ex.Message);
            //message += string.Format("StackTrace: {0}\\n\\n", ex.StackTrace.Replace(Environment.NewLine, string.Empty));
            //message += string.Format("Source: {0}\\n\\n", ex.Source.Replace(Environment.NewLine, string.Empty));
            //message += string.Format("TargetSite: {0}", ex.TargetSite.ToString().Replace(Environment.NewLine, string.Empty));
            ////ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(\"" + message + "\");", true);
            //Session["eswar"] = message;

            //Session["pagename"] = "HardwareAssets.aspx";
            //Response.Redirect("Errorpage.aspx");
        }
    }

    protected void HWgvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = HWgvList.Rows[index];
            Session["SessionSWAssetID"] = int.Parse(row.Cells[0].Text);
            if (e.CommandName == "Edit_Click")
            {
                Session["SessionHWClick"] = "Asset Update";
                Session["Sessiondefault"] = "yes";
                Response.Redirect("../Admin/Add-Hardware-Assets.aspx");
            }
            else if (e.CommandName == "Delete_Click")
            {
                SqlConnection con = new SqlConnection(constr);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "master_hw_proc_delete";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Confirm", "<script>confirm('are you sure?');</script>");
                try
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = Session["SessionSWAssetID"];
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "<script>alert('Hardware Asset deleted successfully');window.location.href='../Admin/Hardware-Assets.aspx';</script>");
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

        catch //(Exception ex)
        {
            //string message = string.Format("Message: {0}\\n\\n", ex.Message);
            //message += string.Format("StackTrace: {0}\\n\\n", ex.StackTrace.Replace(Environment.NewLine, string.Empty));
            //message += string.Format("Source: {0}\\n\\n", ex.Source.Replace(Environment.NewLine, string.Empty));
            //message += string.Format("TargetSite: {0}", ex.TargetSite.ToString().Replace(Environment.NewLine, string.Empty));
            ////ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(\"" + message + "\");", true);
            //Session["eswar"] = message;

            //Session["pagename"] = "HardwareAssets.aspx";
            //Response.Redirect("Errorpage.aspx");
        }
    }
}