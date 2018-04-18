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
	public string cs = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
	protected void Page_Load(object sender, EventArgs e)
	{
		alert.Visible = false;
		if (!Page.IsPostBack)
		{
			BindGrid();
		}

	}
	private void BindGrid()
	{
		SqlConnection con = new SqlConnection(cs);

		using (SqlCommand cmd = new SqlCommand())
		{
			cmd.CommandText = "select Roles,Medical,TravelOutStation,MiscellaneousAndEntertainmentExpenses,RepairsAndMaintenance,LocalTravel from Eligibility2";
			cmd.Connection = con;
			// cmd.Parameters.AddWithValue("@ContactName", txtsearch.Text.Trim());
			DataTable dt = new DataTable();
			using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
			{
				sda.Fill(dt);
				GridView1.DataSource = dt;
				GridView1.DataBind();
			}

			if (GridView1.Rows.Count > 0)
			{
				GridView1.CssClass = "table table-bordered table-striped  table-inverse nomargin";
				GridView1.UseAccessibleHeader = true;
				GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
				GridView1.FooterRow.TableSection = TableRowSection.TableFooter;
			}
		}
	}
	protected void Update_Click(object sender, EventArgs e)
	{
		//string cs = ConfigurationManager.ConnectionStrings["ERSDBConnectionString"].ConnectionString;
		SqlConnection con = new SqlConnection(cs);


		try
		{


			con.Open();
			SqlCommand cmd = new SqlCommand("update Eligibility2 set Medical=@var1,TravelOutStation=@var2,MiscellaneousAndEntertainmentExpenses=@var4,RepairsAndMaintenance=@var3,LocalTravel=@var5 where Roles=@var6 ", con);
			cmd.Parameters.AddWithValue("@var1", medical.Text);
			cmd.Parameters.AddWithValue("@var2", travelout.Text);
			cmd.Parameters.AddWithValue("@var3", randm.Text);
			cmd.Parameters.AddWithValue("@var4", misc.Text);
			cmd.Parameters.AddWithValue("@var5", local.Text);
			cmd.Parameters.AddWithValue("@var6", designation.Text);
			cmd.ExecuteNonQuery();
			con.Close();
			BindGrid();
			alertmod.Style.Add("background-color", "#d7ecc6");
			alert.Style.Add("background-color", "#d7ecc6");
			Label5.ForeColor = System.Drawing.ColorTranslator.FromHtml("green");
			Label6.ForeColor = System.Drawing.ColorTranslator.FromHtml("black");
			Label5.Text = "Success!";
			Label6.Text = "Eligibility has been updated";
			alert.Visible = true;
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message);
		}
	}

	protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
	{
		int index = Convert.ToInt32(e.CommandArgument);
		GridViewRow row = GridView1.Rows[index];
		designation.Text = row.Cells[0].Text;
		medical.Text = row.Cells[1].Text;

		travelout.Text = row.Cells[2].Text;
		misc.Text = row.Cells[3].Text;
		randm.Text = row.Cells[4].Text;

		local.Text = row.Cells[5].Text;




		if (e.CommandName == "Edit_Click")
		{
			MPE.Enabled = true;
			updatePanel.Visible = true;
			MPE.Show();
		}
	}

	protected void randm_TextChanged(object sender, EventArgs e)
	{

	}
}