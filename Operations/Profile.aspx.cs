using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Operations_Default : System.Web.UI.Page
{
	public string cs = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;

	protected void Page_Load(object sender, EventArgs e)
	{

		if (!IsPostBack)
		{
			SqlConnection con = new SqlConnection(cs);
			con.Open();
			SqlCommand cmd1 = new SqlCommand("select EmployeeID,EmployeeName,EmployeeDesignation,EmployeeDepartment,EmployeeMailID,EmployeePhoneNumber,EmployeePassword,EmployeeLocation from Employee where EmployeeID='" + Session["SessionEmployeeID"].ToString() + "'", con);
			SqlDataReader dr1 = cmd1.ExecuteReader();

			while (dr1.Read())
			{
				//Label3.Text = "Authorized Employee";
				empid.Text = (dr1["EmployeeID"].ToString());
				name.Text = (dr1["EmployeeName"].ToString());
				designation.Text = (dr1["EmployeeDesignation"].ToString());
				Dept.Text = (dr1["EmployeeDepartment"].ToString());
				emailid.Text = (dr1["EmployeeMailID"].ToString());
				loc.Text = (dr1["EmployeeLocation"].ToString());
				mobile.Text = (dr1["EmployeePhoneNumber"].ToString());
			}
			string s = mobile.Text;
		}
	}

	protected void btnSignIn_Click1(object sender, EventArgs e)
	{
		try
		{
			var UpdateCmd = "UPDATE Employee SET EmployeePhoneNumber = @var1 WHERE EmployeeID=  '" + Session["SessionEmployeeID"].ToString() + "'";
			using (SqlConnection cnn = new SqlConnection(cs))
			{
				using (SqlCommand cmd11 = new SqlCommand(UpdateCmd, cnn))
				{
					cmd11.Parameters.AddWithValue("@var1", mobile.Text);
					cnn.Open();
					cmd11.ExecuteNonQuery();
					cnn.Close();
					
				}
			}
			alertmod.Style.Add("background-color", "#d7ecc6");
			alert.Style.Add("background-color", "#d7ecc6");
			Label5.ForeColor = System.Drawing.ColorTranslator.FromHtml("green");
			Label2.ForeColor = System.Drawing.ColorTranslator.FromHtml("black");
			Label5.Text = "Success!";
			Label2.Text = " Mobilenumber has been updated";
			alert.Visible = true;

		}
		catch (Exception ex)
		{
			Response.Write(ex.Message);
		}
	}
}
