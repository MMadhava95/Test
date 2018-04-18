using System;
using System.Collections.Generic;
using System.Configuration;
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
		if (!IsPostBack)
		{
			SqlConnection con = new SqlConnection(cs);
			con.Open();
			SqlCommand cmd1 = new SqlCommand("select EmployeeID,EmployeeName,EmployeeDesignation,EmployeeDepartment,EmployeeMailID,EmployeePhoneNumber,EmployeePassword,EmployeeLocation from Employee where EmployeeID='" + Session["SessionEmployeeID"] + "'", con);
			SqlDataReader dr1 = cmd1.ExecuteReader();

			while (dr1.Read())
			{
				//Label3.Text = "Authorized Employee";
				empid.Text = (dr1["EmployeeID"].ToString());
				empname.Text = (dr1["EmployeeName"].ToString());
				empdepartment.Text = (dr1["EmployeeDesignation"].ToString());
				empdesignation.Text = (dr1["EmployeeDepartment"].ToString());
				empmailid.Text = (dr1["EmployeeMailID"].ToString());
				emplocation.Text = (dr1["EmployeeLocation"].ToString());
				emphone.Text = (dr1["EmployeePhoneNumber"].ToString());
			}
			string s = emphone.Text;
		}
	}

	protected void update_Click(object sender, EventArgs e)
	{
		try
		{
			var UpdateCmd = "UPDATE Employee SET EmployeePhoneNumber = @var1 ,EmployeeName = @var2 , EmployeeLocation = @var3,EmployeeDesignation = @var4 ,EmployeeDepartment = @var5 WHERE EmployeeID=  '" + Session["SessionEmployeeID"]+ "'";
			using (SqlConnection cnn = new SqlConnection(cs))
			{
				using (SqlCommand cmd11 = new SqlCommand(UpdateCmd, cnn))
				{
					cmd11.Parameters.AddWithValue("@var1", emphone.Text);
					cmd11.Parameters.AddWithValue("@var2", empname.Text);
					cmd11.Parameters.AddWithValue("@var3", emplocation.Text);
					cmd11.Parameters.AddWithValue("@var4", empdesignation.Text);
					cmd11.Parameters.AddWithValue("@var5", empdepartment.Text);
					cnn.Open();
					cmd11.ExecuteNonQuery();
					cnn.Close();
					alertmod.Style.Add("background-color", "#d7ecc6");
					alert.Style.Add("background-color", "#d7ecc6");
					Label5.ForeColor = System.Drawing.ColorTranslator.FromHtml("green");
					Label6.ForeColor = System.Drawing.ColorTranslator.FromHtml("black");
					Label5.Text = "Success!";
					Label6.Text = " Mobilenumber has been updated";
					alert.Visible = true;
				}
			}

		}
		catch (Exception ex)
		{
			Response.Write(ex.Message);
		}

	}

	protected void back_Click(object sender, EventArgs e)
	{
		Response.Redirect("AdminDashboard.aspx");
	}
}