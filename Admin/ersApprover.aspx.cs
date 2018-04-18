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
		Div1.Visible = false;
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
			cmd.CommandText = "select ERSApproverID,ERSApproverName,ERSApproverMailID,ERSApproverLevel,ERSApproverRole,ERSEmployeeID from ERSApprover where isactive='" + 1 + "' ";
            cmd.Connection = con;
			// cmd.Parameters.AddWithValue("@ContactName", txtsearch.Text.Trim());
			DataTable dt = new DataTable();
			using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
			{
				sda.Fill(dt);
				ViewState["Paging"] = dt;
				GridView1.DataSource = dt;
				GridView1.DataBind();
			}
		}
		if (GridView1.Rows.Count > 0)
		{
			GridView1.CssClass = "table table-bordered table-striped  table-inverse nomargin";
			GridView1.UseAccessibleHeader = true;
			GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
			GridView1.FooterRow.TableSection = TableRowSection.TableFooter;
		}
	}

	protected void update_Click(object sender, EventArgs e)
	{

		//string cs = ConfigurationManager.ConnectionStrings["ERSDBConnectionString"].ConnectionString;
		SqlConnection con = new SqlConnection(cs);
		con.Open();
		SqlCommand cmd = new SqlCommand("update ERSApprover set ERSApproverLevel=@var1,ERSApproverRole=@var2 where ERSApproverID=@var3", con);
		cmd.Parameters.AddWithValue("@var1", Approverlevel.Text);
		cmd.Parameters.AddWithValue("@var2", ApproverRole.Text);
		cmd.Parameters.AddWithValue("@var3", approverid.Text);
		cmd.ExecuteNonQuery();
		con.Close();
		BindGrid();
		alertmod.Style.Add("background-color", "#d7ecc6");
		alert.Style.Add("background-color", "#d7ecc6");
		Label1.ForeColor = System.Drawing.ColorTranslator.FromHtml("green");
		Label5.ForeColor = System.Drawing.ColorTranslator.FromHtml("black");

		Label1.Text = "Success!";
		Label5.Text = "Approver details has been updated";
		alert.Visible = true;
		grid();
		// ClientScript.RegisterStartupScript(GetType(), "alert", "alert(' Approver details has been  updated ');", true);

	}

	public void OnConfirm(object sender, EventArgs e)
	{
		string confirmValue = Request.Form["confirm_value"];
		if (confirmValue == "Yes")
		{
			// this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked YES!')", true);
			hai();
		}

	}
	
	protected void hai()
	{
		try
		{
			//string cs = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
			SqlConnection con = new SqlConnection(cs);
			con.Open();
			SqlCommand cmd2 = new SqlCommand(" select count(ERSClaimID) from ERSClaim  where ([ERSClaimStatus]='Pending' or [ERSClaimStatus]='Need Clarification')and ERSApproverID='" + approverid.Text + "' ", con);
			int count = Convert.ToInt16(cmd2.ExecuteScalar());
			if (count > 0)
			{
				alertmod.Style.Add("background-color", "#d7ecc6");
				alert.Style.Add("background-color", "#d7ecc6");
				Label1.ForeColor = System.Drawing.ColorTranslator.FromHtml("green");
				Label5.ForeColor = System.Drawing.ColorTranslator.FromHtml("black");
				//Label1.Text = "Success!";
				Label5.Text = "Approver has claims to process";
				alert.Visible = true;
			}
			else
			{
				SqlCommand cmd = new SqlCommand(" update ERSApprover set isactive=@var1 where ERSApproverID='" + approverid.Text + "'", con);
				cmd.Parameters.AddWithValue("@var1", "0");
				cmd.ExecuteNonQuery();

                var UpdateCmd = "update Employee set SecondaryPosition = @parm1 where EmployeeMailID = '" + Approvermailid.Text + "' AND EmployeeID ='" + approverid.Text + "'";
                using (SqlConnection cnn = new SqlConnection(cs))
                {
                    using (SqlCommand cmd11 = new SqlCommand(UpdateCmd, cnn))
                    {
                        cmd11.Parameters.AddWithValue("@parm1", "NA");
                        cnn.Open();
                        cmd11.ExecuteNonQuery();
                        cnn.Close();
                    }
                }


                con.Close();
				BindGrid();
				alertmod.Style.Add("background-color", "#d7ecc6");
				alert.Style.Add("background-color", "#d7ecc6");
				Label1.ForeColor = System.Drawing.ColorTranslator.FromHtml("green");
				Label5.ForeColor = System.Drawing.ColorTranslator.FromHtml("black");
				Label1.Text = "Success!";
				Label5.Text = "Approver has been removed";
				alert.Visible = true;
				grid();
			}
			// ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Approver has been deleted');", true);
			// Response.Redirect("EmployeeDetails2.aspx");
		}
		catch (Exception ex)
		{
			alertmod.Style.Add("background-color", "#ffc2b3");
			alert.Style.Add("background-color", "#ffc2b3");
			Label1.ForeColor = System.Drawing.ColorTranslator.FromHtml("red");
			Label5.ForeColor = System.Drawing.ColorTranslator.FromHtml("black");
			Label5.Text = "Approver has claims (or) an approver";
			Label1.Text = "Failure!";
			alert.Visible = true;
			grid();
			//  ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Approver has Claims (or) an approver ');", true);

		}
	}
	private void grid()
	{
		if (Session.IsNewSession)
		{
			//Label1.Text = "hia";
		}
		else
		{
			BindGrid();
		}
	}
	protected void GridView1_RowDataBound1(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + e.Row.RowIndex);
			e.Row.ToolTip = "Click to know the Approver details";

		}
	}




	protected void Button1_Click1(object sender, EventArgs e)
	{
		BindGrid();

		if (GridView1.Rows.Count == 0)
		{
			GridView1.Visible = true;
		}
	}


	protected void GridView1_PageIndexChanged1(object sender, EventArgs e)
	{
		try
		{
			approverid.Text = this.GridView1.SelectedRow.Cells[0].Text.ToString();
			Approvername.Text = this.GridView1.SelectedRow.Cells[1].Text.ToString();
			Approvermailid.Text = this.GridView1.SelectedRow.Cells[2].Text.ToString();
			Approverlevel.Text = this.GridView1.SelectedRow.Cells[3].Text.ToString();
			ApproverRole.Text = this.GridView1.SelectedRow.Cells[4].Text.ToString();

		}
		catch (Exception ex)
		{
			Response.Write(ex.Message);
		}

	}


	protected void yes_Click1(object sender, EventArgs e)
	{
		Label6.Text = "yes";

		Delete_Claim();
	}

	protected void no_Click1(object sender, EventArgs e)
	{
		Label6.Text = "no";
	}

	protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
	{
		int index = Convert.ToInt32(e.CommandArgument);
		GridViewRow row = GridView1.Rows[index];
		approverid.Text = row.Cells[0].Text;
		Approvername.Text = row.Cells[1].Text;

		Approvermailid.Text = row.Cells[2].Text;
		ApproverRole.Text = row.Cells[3].Text;
		Approverlevel.Text = row.Cells[4].Text;

		if (e.CommandName == "Edit_Click")
		{
			MPE.Enabled = true;
			updatePanel.Visible = true;
			MPE.Show();
		}
		if (e.CommandName == "Delete_Click")
		{
			Delete_Claim();
		}
	}
	protected void Delete_Claim()
	{
		alert.Visible = false;
		Div1.Visible = true;
		if (Label6.Text == "yes")
		{
			Div1.Visible = false;
			hai();
			Label6.Text = string.Empty;
		}
	}
}
