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
		
		if (!this.IsPostBack)
		{
			this.BindGrid();
		}
	}

	protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvList, "Select$" + e.Row.RowIndex);
			e.Row.ToolTip = "Click to know the Claim details";

		}
	}

	protected void gvList_SelectedIndexChanged(object sender, EventArgs e)
	{

	}
	
	private void BindGrid()
	{
		//SqlConnection con = new SqlConnection(@"Data Source=HP-PC\SQLEXPRESS;Database =ERSDB;Integrated Security=True;");
		string cs = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
		SqlConnection con = new SqlConnection(cs);

		using (SqlCommand cmd = new SqlCommand())
		{
			cmd.CommandText = "Select ERSClaimID,ERSClaimType,ERSClaimDate,ERSClaimStatus,(Currency+'.' + ' '+CAST(ERSBillAmount as varchar(30))) as ERSBillAmount,ERSClaimApproverRemarks,ERSClaimProcessDate,ERSClaimUserRemarks,ERSApproverName,EmployeeName from ERSClaim left join ERSApprover on ERSClaim.ERSApproverID = ERSApprover.ERSApproverID left join Employee on Employee.EmployeeID = ERSClaim.ERSEmployeeID  order by ERSClaimID desc ";

			cmd.Connection = con;
			// cmd.Parameters.AddWithValue("@ContactName", txtsearch.Text.Trim());
			DataTable dt = new DataTable();
			using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
			{
				sda.Fill(dt);
				gvList.DataSource = dt;
				gvList.DataBind();
			}


		}
		if (gvList.Rows.Count > 0)
		{
			gvList.CssClass = "table table-bordered table-striped  table-inverse nomargin";
			gvList.UseAccessibleHeader = true;
			gvList.HeaderRow.TableSection = TableRowSection.TableHeader;
			gvList.FooterRow.TableSection = TableRowSection.TableFooter;
		}
	}

	protected void lnkbtnEdit_Click(object sender, EventArgs e)
	{

	}

	protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
	{
		int index = Convert.ToInt32(e.CommandArgument);
		GridViewRow row = gvList.Rows[index];
		claimId.Text = row.Cells[0].Text;
		type.Text = row.Cells[1].Text;

		date.Text = row.Cells[2].Text;
		amount.Text = row.Cells[3].Text;
		status.Text = row.Cells[4].Text;

		empname.Text = row.Cells[5].Text;
		appname.Text = row.Cells[6].Text;
		SqlConnection con2 = new SqlConnection(cs);
		con2.Open();
		SqlCommand cmd = new SqlCommand("select ERSClaimUserRemarks,ERSBillImage,ERSClaimApproverRemarks from ERSClaim where ERSClaimID = '" + claimId.Text + "'", con2);
		SqlDataReader myReader = cmd.ExecuteReader();
		if (myReader.Read())
		{
			userrm.Text = myReader["ERSClaimUserRemarks"].ToString();
			apprm.Text = myReader["ERSClaimApproverRemarks"].ToString();
			byte[] imagedata = (byte[])myReader["ERSBillImage"];
			string img = Convert.ToBase64String(imagedata, 0, imagedata.Length);
			Image1.ImageUrl = "data:image/png;base64," + img;
			Image2.ImageUrl = "data:image/png;base64," + img;
			myReader.Close();

			con2.Close();
		}
		SqlDataAdapter da = new SqlDataAdapter(cmd);
		con2.Close();



		if (e.CommandName == "Edit_Click")
		{
			MPE.Enabled = true;
			updatePanel.Visible = true;
			MPE.Show();
		}

	}
}