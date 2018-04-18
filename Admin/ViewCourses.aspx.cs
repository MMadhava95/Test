using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Default : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
    int str;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["SessionUserMailID"] != null)
        {
            if (!Page.IsPostBack)
            {
                bindAdds();
            }
        }
        else
        {
            Response.Redirect("../Login.aspx");
        }
        
    }
    void bindAdds()
    {
        SqlConnection con = new SqlConnection(constr);
        con.Open();
        SqlCommand swcmd = new SqlCommand("SELECT CourseID,CourseName,CourseDuration,Faculty,Prerequisites from CreateCourseTable", con);
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

    protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = gvList.Rows[index];
        str = int.Parse(row.Cells[0].Text);
    }



    protected void gvList_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtCourseId.Text = gvList.SelectedRow.Cells[0].Text.ToString();
        txtName.Text = gvList.Rows[str].Cells[1].Text.ToString();
        txtDuraiton.Text = gvList.Rows[str].Cells[2].Text.ToString();
        txtfaculty.Text = gvList.SelectedRow.Cells[3].Text.ToString();
        txtprerereq.Text = gvList.SelectedRow.Cells[4].Text.ToString();
        coursesMP.Show();
    }

    protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvList, "Select$" + e.Row.RowIndex);
            e.Row.Attributes["style"] = "cursor:pointer";
        }
    }

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        SqlConnection con1 = new SqlConnection(constr);

        con1.Open();
        SqlCommand cmd = new SqlCommand("update CreateCourseTable set CourseName=@var1,CourseDuration=@var2,Faculty=@var3,Prerequisites=@var4  where CourseID=@var6", con1);
        cmd.Parameters.AddWithValue("@var1", txtName.Text);
        cmd.Parameters.AddWithValue("@var2", txtDuraiton.Text);
        cmd.Parameters.AddWithValue("@var3", txtfaculty.Text);
        cmd.Parameters.AddWithValue("@var4", txtprerereq.Text);

        cmd.Parameters.AddWithValue("@var6", txtCourseId.Text);

        cmd.ExecuteNonQuery();
        con1.Close();
        bindAdds();
        ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Course details updated Successfully!! ');", true);
    }
}
