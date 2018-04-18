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
        
        //if (Session["Admin"] != null)
        //{
        //    Session["Admin"].ToString();
        //}
        //else
        //{
        //    Response.Redirect("Login.aspx");
        //}
        alert.Visible = false;
       // ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;

    }
    protected void submit_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(cs);

            SqlDataAdapter GetempID = new SqlDataAdapter("select EmployeeID from EmployeeDuplicate where EmployeeID='"+empid.Text+"'", con);
            DataTable dt = new DataTable();
            DataRow dr3;
            GetempID.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                alertmod.Style.Add("background-color", "#ffc2b3");
                alert.Style.Add("background-color", "#ffc2b3");
                Label5.ForeColor = System.Drawing.ColorTranslator.FromHtml("Red");
                Label6.ForeColor = System.Drawing.ColorTranslator.FromHtml("black");
                Label5.Text = "Failure!";
                Label6.Text = "EmployeeID already exists";
                alert.Visible = true;
                empid.Text = string.Empty;
                empname.Text = string.Empty;
                empmailid.Text = string.Empty;
                emphone.Text = string.Empty;
                empdesignation.Text = string.Empty;
                empdepartment.Text = string.Empty;
                emplocation.Text = string.Empty;

            }
            else
            {
                con.Open();

                SqlCommand select2 = new SqlCommand("insert into EmployeeDuplicate(EmployeeID ,EmployeeName ,EmployeeMailID,EmployeePhoneNumber,EmployeeDesignation,EmployeeDepartment,EmployeeLocation) Values(@var1,@var2,@var3,@var4,@var5,@var6,@var7)", con);
                select2.Parameters.AddWithValue("@var1", empid.Text);
                select2.Parameters.AddWithValue("@var2", empname.Text);
                select2.Parameters.AddWithValue("@var3", empmailid.Text);
                select2.Parameters.AddWithValue("@var4", emphone.Text);
                select2.Parameters.AddWithValue("@var5", empdesignation.Text);
                select2.Parameters.AddWithValue("@var6", empdepartment.Text);
                select2.Parameters.AddWithValue("@var7", emplocation.Text);
                select2.ExecuteNonQuery();
                alertmod.Style.Add("background-color", "#d7ecc6");
                alert.Style.Add("background-color", "#d7ecc6");
                Label5.ForeColor = System.Drawing.ColorTranslator.FromHtml("green");
                Label6.ForeColor = System.Drawing.ColorTranslator.FromHtml("black");
                Label5.Text = "Success!";
                Label6.Text = "New employee added";
                alert.Visible = true;
                con.Close();
                empid.Text = string.Empty;
                empname.Text = string.Empty;
                empmailid.Text = string.Empty;
                emphone.Text = string.Empty;
                empdesignation.Text = string.Empty;
                empdepartment.Text = string.Empty;
                emplocation.Text = string.Empty;
            }
        }
        catch (Exception ex)
        {
            
        }
    }
    protected void back_Click(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeList.aspx");
    }
}
