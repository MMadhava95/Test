using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Default : System.Web.UI.Page
{
	public string cs = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
	public SqlCommand cmd;
	protected void Page_Load(object sender, EventArgs e)
	{
		alert.Visible = false;
		//if (Session["Admin"] != null)
		//{
		//	Session["Admin"].ToString();
		//}
		//else
		//{
		//	Response.Redirect("Login.aspx");
		//}

		for (int i = 0; i <= 3; i++) { DropDownList1.Items[i].Attributes.Add("style", "color:Black"); }
		for (int i = 0; i <= 3; i++) { ClaimStatusDropDown.Items[i].Attributes.Add("style", "color:Black"); }
		alert.Visible = false;
		pieChartPanel.Visible = true;
		employeePanel.Visible = false;
		claimDatePanel.Visible = false;
		claimStatusPanel.Visible = false;
		GridClaimData.Visible = false;


	}
	[WebMethod]
	public static List<Claims> GetChartData()
	{
		DataTable dt = new DataTable();
		string cs = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
		SqlConnection con = new SqlConnection(cs);
		using (con)
		{
			con.Open();
			SqlCommand cmd = new SqlCommand("select ERSClaimType,COUNT(ERSClaimID) as total from ERSClaim group by ERSClaimType", con);
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			da.Fill(dt);
			con.Close();
		}
		List<Claims> dataList = new List<Claims>();
		foreach (DataRow dtrow in dt.Rows)
		{
			Claims details = new Claims();
			details.ClaimType = dtrow[0].ToString();
			details.Total = Convert.ToInt32(dtrow[1]);
			dataList.Add(details);
		}
		return dataList;
	}

	public class Claims
	{
		public string ClaimType { get; set; }
		public int Total { get; set; }
	}

	[WebMethod]
	public static List<Status> GetChartData2()
	{
		DataTable dt = new DataTable();
		string cs = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
		SqlConnection con = new SqlConnection(cs);
		using (con)
		{
			con.Open();
			SqlCommand cmd = new SqlCommand("select ERSClaimStatus,count(ERSClaimID) from ERSClaim group by ERSClaimStatus", con);
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			da.Fill(dt);
			con.Close();
		}
		List<Status> dataListStatus = new List<Status>();
		foreach (DataRow dtrow in dt.Rows)
		{
			Status statusDetails = new Status();
			statusDetails.StatusType = dtrow[0].ToString();
			statusDetails.StatusTotal = Convert.ToInt32(dtrow[1]);
			dataListStatus.Add(statusDetails);
		}
		return dataListStatus;
	}

	public class Status
	{
		public string StatusType { get; set; }
		public int StatusTotal { get; set; }
	}

	protected void DropDownList1_SelectedIndexChanged1(object sender, EventArgs e)
	{
		if (DropDownList1.SelectedIndex == 0)
		{
			pieChartPanel.Visible = true;
			employeePanel.Visible = false;
			claimDatePanel.Visible = false;
			claimStatusPanel.Visible = false;
		}
		else if (DropDownList1.SelectedIndex == 1)
		{
			pieChartPanel.Visible = false;
			employeePanel.Visible = true;
			claimDatePanel.Visible = false;
			claimStatusPanel.Visible = false;

		}
		else if (DropDownList1.SelectedIndex == 2)
		{
			CalendarExtender1.EndDate = DateTime.Now;
			CalendarExtender3.EndDate = DateTime.Now;

			pieChartPanel.Visible = false;
			claimDatePanel.Visible = true;
			employeePanel.Visible = false;
			claimStatusPanel.Visible = false;

		}
		else if (DropDownList1.SelectedIndex == 3)
		{
			pieChartPanel.Visible = false;
			claimStatusPanel.Visible = true;
			claimDatePanel.Visible = false;
			employeePanel.Visible = false;

		}
	}

	protected void btnEmployeeGenerateReports_Click(object sender, EventArgs e)
	{
		pieChartPanel.Visible = false;
		employeePanel.Visible = true;
		GridClaimData.Visible = true;
		
		SqlConnection con = new SqlConnection(cs);
		SqlCommand cmd;
		using (con)
		{
			using (cmd = new SqlCommand("select ERSClaimID,ERSApproverName,EmployeeName,ERSClaimType,(Currency+'.' + ' '+CAST(ERSBillAmount as varchar(30))) as ERSBillAmount,ERSClaimStatus from ERSClaim left join Employee on Employee.EmployeeID = ERSClaim.ERSEmployeeID left join ERSApprover on ERSClaim.ERSApproverID=ERSApprover.ERSApproverID where EmployeeName='" + DropDownList2.SelectedValue + "'", con))

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
				}
				catch (Exception ex)
				{
					Response.Write(ex.Message);
				}

			}
			if (GridClaimData.Rows.Count > 0)
			{
				GridClaimData.CssClass = "table table-bordered table-striped  table-inverse nomargin";
				GridClaimData.UseAccessibleHeader = true;
				GridClaimData.HeaderRow.TableSection = TableRowSection.TableHeader;
				GridClaimData.FooterRow.TableSection = TableRowSection.TableFooter;
			}
		}

	}

	protected void btnExportExcel_Click(object sender, EventArgs e)
	{
		try
		{
			if (GridClaimData.Rows.Count > 0)
			{
				//btnExportExcel.Visible = true;
				Response.Clear();
				Response.Buffer = true;
				Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
				Response.Charset = "";
				Response.ContentType = "application/vnd.ms-excel";
				using (StringWriter sw = new StringWriter())
				{
					HtmlTextWriter hw = new HtmlTextWriter(sw);
					GridClaimData.RowStyle.ForeColor = System.Drawing.Color.Black;
					GridClaimData.HeaderStyle.ForeColor = System.Drawing.Color.Black;
					//To Export all pages
					GridClaimData.AllowPaging = false;
					//this.BindGrid();
					if (DropDownList1.SelectedIndex == 1)
					{
						btnEmployeeGenerateReports_Click(sender, e);
					}
					else if (DropDownList1.SelectedIndex == 2)
					{
						btnclaimDateGenerateReports_Click(sender, e);
					}
					else if (DropDownList1.SelectedIndex == 3)
					{
						btnStatusGenerateReport_Click(sender, e);
					}

					foreach (TableCell cell in GridClaimData.HeaderRow.Cells)
					{
						cell.BackColor = GridClaimData.HeaderStyle.BackColor;
					}
					foreach (GridViewRow row in GridClaimData.Rows)
					{
						row.BackColor = Color.White;
						foreach (TableCell cell in row.Cells)
						{
							if (row.RowIndex % 2 == 0)
							{
								cell.BackColor = GridClaimData.AlternatingRowStyle.BackColor;
							}
							else
							{
								cell.BackColor = GridClaimData.RowStyle.BackColor;
							}
							cell.CssClass = "textmode";
						}
					}

					GridClaimData.RenderControl(hw);

					//style to format numbers to string
					string style = @"<style> .textmode { } </style>";
					Response.Write(style);
					Response.Output.Write(sw.ToString());
					Response.Flush();
					Response.End();
				}

			}
			else
			{

				if (DropDownList1.SelectedIndex == 1)
				{
					pieChartPanel.Visible = false;
					employeePanel.Visible = true;
					claimDatePanel.Visible = false;
					claimStatusPanel.Visible = false;

				}
				else if (DropDownList1.SelectedIndex == 2)
				{
					pieChartPanel.Visible = false;
					employeePanel.Visible = false;
					claimDatePanel.Visible = true;
					claimStatusPanel.Visible = false;
				}
				else if (DropDownList1.SelectedIndex == 3)
				{
					pieChartPanel.Visible = false;
					employeePanel.Visible = false;
					claimDatePanel.Visible = false;
					claimStatusPanel.Visible = true;
				}
				alertmod.Style.Add("background-color", "#ffc2b3");
				alert.Style.Add("background-color", "#ffc2b3");
				Label5.ForeColor = System.Drawing.ColorTranslator.FromHtml("black");
				Label5.Text = "No records to export";
				alert.Visible = true;
				TextBox1.Text = string.Empty;
				TextBox3.Text = string.Empty;

			}
		}
		catch (Exception ex)
		{
			alertmod.Style.Add("background-color", "#ffc2b3");
			alert.Style.Add("background-color", "#ffc2b3");
			//Label2.ForeColor = System.Drawing.ColorTranslator.FromHtml("Red");
			Label5.ForeColor = System.Drawing.ColorTranslator.FromHtml("black");
			//Label2.Text = "Failure!";
			Label5.Text = "Employee has no claims";
			alert.Visible = true;

		}
	}

	protected void btnStatusGenerateReport_Click(object sender, EventArgs e)
	{
		pieChartPanel.Visible = false;
		claimStatusPanel.Visible = true;
		GridClaimData.Visible = true;
		SqlConnection con1 = new SqlConnection(cs);
		using (con1)
		{
			using (cmd = new SqlCommand("select ERSClaimID,ERSApproverName,EmployeeName,ERSClaimType,(Currency+'.' + ' '+CAST(ERSBillAmount as varchar(30))) as ERSBillAmount,ERSClaimStatus from ERSClaim left join Employee on Employee.EmployeeID = ERSClaim.ERSEmployeeID left join ERSApprover on ERSClaim.ERSApproverID=ERSApprover.ERSApproverID  Where ERSClaimStatus='" + ClaimStatusDropDown.SelectedValue + "'"))
			{
				SqlDataAdapter dt = new SqlDataAdapter();
				try
				{
					cmd.Connection = con1;
					con1.Open();
					dt.SelectCommand = cmd;

					DataTable dTable = new DataTable();
					dt.Fill(dTable);
					SqlDataReader sdr = cmd.ExecuteReader();
					GridClaimData.DataSource = dTable;
					GridClaimData.DataBind();
				}
				catch (Exception ex)
				{
					// Error msg display here  
					Response.Write(ex.Message);
				}
			}
		}

	}

	protected void btnclaimDateGenerateReports_Click(object sender, EventArgs e)
	{
		pieChartPanel.Visible = false;
		claimDatePanel.Visible = true;
		GridClaimData.Visible = true;

		ClaimDataGrid2();
	}

	private void ClaimDataGrid2()
	{
		if (DropDownList1.SelectedIndex == 2)
		{
			SqlConnection con = new SqlConnection(cs);
			using (con)
			{
				using (cmd = new SqlCommand("select ERSClaimID,ERSApproverName,EmployeeName,ERSClaimType,(Currency+'.' + ' '+CAST(ERSBillAmount as varchar(30))) as ERSBillAmount,ERSClaimStatus from ERSClaim left join Employee on Employee.EmployeeID = ERSClaim.ERSEmployeeID left join ERSApprover on ERSClaim.ERSApproverID=ERSApprover.ERSApproverID Where ERSClaimDate>='" + TextBox1.Text + "'and ERSClaimDate<='" + TextBox3.Text + "'", con))
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
					}
					catch (Exception ex)
					{
						// Error msg display here  
						Response.Write(ex.Message);
					}
				}
			}
			if (GridClaimData.Rows.Count > 0)
			{
				GridClaimData.CssClass = "table table-bordered table-striped  table-inverse nomargin";
				GridClaimData.UseAccessibleHeader = true;
				GridClaimData.HeaderRow.TableSection = TableRowSection.TableHeader;
				GridClaimData.FooterRow.TableSection = TableRowSection.TableFooter;
			}
		}
	}
	public override void VerifyRenderingInServerForm(Control control)
	{

	}
}