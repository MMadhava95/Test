using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
    string Encryptpwd;
    protected void Page_Load(object sender, EventArgs e)
    {
        //ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        Session.Clear();
        Session.RemoveAll();
        alert.Visible = false;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        encryptpwdfun(UserPassword.Text);
        SqlConnection Logincon = new SqlConnection(constr);
        Logincon.Open();
		//SqlCommand ses = new SqlCommand("Select SecondaryPosition From Employee where EmployeeMailID='" + UserMailID.Text + "' AND EmployeePassword='" + Encryptpwd + "' AND isactive = '1'", Logincon);
		//SqlDataReader dr = ses.ExecuteReader();
		//string ss = dr["SecondaryPosition"].ToString();
		//dr.Close();
		SqlCommand Logincmd = new SqlCommand("Select * From Employee where EmployeeMailID='" + UserMailID.Text + "' AND EmployeePassword='" + Encryptpwd + "' AND isactive = '1'", Logincon);
        SqlDataReader Logindr = Logincmd.ExecuteReader();
        Logindr.Read();
		

        if (Logindr.HasRows && (Logindr["Position"].ToString() == "Employee" && Logindr["SecondaryPosition"].ToString() == "NA"))
        {
            // Session["EmployeeID"] = Logindr["EmployeeID"].ToString();

            Session["SessionUserPosition"] = "Employee";
            Session["SessionUserSecondryPosition"] = "NA";
            Logindr.Close();
            AMSsessions();
            Response.Redirect("~/Operations/Dashboard.aspx");
        }
        else if (Logindr.HasRows && (Logindr["Position"].ToString() == "Admin" && Logindr["SecondaryPosition"].ToString() == "NA"))
        {
            Session["SessionUserPosition"] = "Admin";
            Session["SessionUserSecondryPosition"] = "NA";
            Logindr.Close();
            AMSsessions();
            Response.Redirect("~/Admin/AdminDashboard.aspx");
        }
        else if (Logindr.HasRows && (Logindr["Position"].ToString() == "Employee" && Logindr["SecondaryPosition"].ToString() == "Approver" && Apprvckeck.Checked == false))
        {
            Session["SessionUserPosition"] = "Employee";
            Session["SessionUserSecondryPosition"] = "Approver";
            Session["SessionUserSecondryPositioncheck"] = "false";
            Logindr.Close();
            AMSsessions();
            Response.Redirect("~/Operations/Dashboard.aspx");
        }
        else if ((!Logindr.HasRows) || (Logindr["Position"].ToString() == "Employee" && Logindr["SecondaryPosition"].ToString() == "Approver" && Apprvckeck.Checked == true))
        {
            Logindr.Close();
            Logindr.Dispose();
            try
            {
                //Checking for Valid Approver or not?
                SqlCommand checkcmd = new SqlCommand("select SecondaryPosition, isactive  " +
                    "from Employee where EmployeeMailID = '" + UserMailID.Text + "' AND isactive = '1' ", Logincon);
                SqlDataReader checkdr = checkcmd.ExecuteReader();
                checkdr.Read();
                string secondaryposition = checkdr["SecondaryPosition"].ToString();
                checkdr.Close();
                checkdr.Dispose();

                //Aprover command
                SqlCommand Loginapprvcmd = new SqlCommand("select * from ERSApprover where ERSApproverMailID = '" + UserMailID.Text + "' AND ERSApproverPassword ='" + Encryptpwd + "' " +
                    "AND isactive = '1'", Logincon);
                SqlDataReader Loginapprvdr = Loginapprvcmd.ExecuteReader();
                Loginapprvdr.Read();
                if (secondaryposition == "Approver" && Loginapprvdr.HasRows && Apprvckeck.Checked == true)
                {
                    Session["SessionUserPosition"] = "Employee";
                    Session["SessionUserSecondryPosition"] = "Approver";
                    Session["SessionUserSecondryPositioncheck"] = "true";
                    Loginapprvdr.Close();
                    AMSsessions();
                    Response.Redirect("~/Operations/ApproverDashboard.aspx");
                }
                else if (secondaryposition == "Approver" && Loginapprvdr.HasRows && Apprvckeck.Checked == false)
                {
                  //  ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Please Check the Approver Checkbox');", true);
                    alertmod.Style.Add("background-color", "#ffc2b3");
                    alert.Style.Add("background-color", "#ffc2b3");
                    Label5.ForeColor = System.Drawing.ColorTranslator.FromHtml("red");
                   
                    Label5.Text = "Please Check the Approver Checkbox";
                   
                    alert.Visible = true;
                }
                else
                {
                    Session["SessionUserSecondryPosition"] = "NA";
                    alertmod.Style.Add("background-color", "#ffc2b3");
                    alert.Style.Add("background-color", "#ffc2b3");
                    Label5.ForeColor = System.Drawing.ColorTranslator.FromHtml("red");

                    Label5.Text = "Incorrect User Name/Password";

                    alert.Visible = true;
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Incorrect User Name / Password!');", true);
                    UserMailID.Text = string.Empty.ToString();
                    UserMailID.Focus();
                }
                Loginapprvdr.Close();
                // Loginapprvdr.Dispose();
            }
            catch (Exception ex)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Incorrect User Name / Password!');", true);
                //UserMailID.Text = string.Empty.ToString();
                //UserMailID.Focus();
                throw ex;
            }

        }
        else
        {
            Logindr.Close();
            alertmod.Style.Add("background-color", "#ffc2b3");
            alert.Style.Add("background-color", "#ffc2b3");
            Label5.ForeColor = System.Drawing.ColorTranslator.FromHtml("red");

            Label5.Text = "Incorrect User Name/Password";

            alert.Visible = true;
          //  ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Incorrect User Name / Password!');", true);
        }

        Logincon.Close();
        Logincon.Dispose();
    }

    //Retrieving Employee Data using Sessions (AMS functionality need this function)
    protected void AMSsessions()
    {
        SqlConnection Sessioncon = new SqlConnection(constr);
        Sessioncon.Open();
        SqlCommand Sessioncmd = new SqlCommand("select * from EmployeeMaster where EmployeeMailID='" + UserMailID.Text + "'", Sessioncon);
        SqlDataReader Sessiondr = Sessioncmd.ExecuteReader();
        Sessiondr.Read();
        if (Sessiondr.HasRows)
        {
            Session["SessionUserMailID"] = UserMailID.Text;
            //Session["SessionUserPwd"] = UserPassword.Text;
            Session["SessionEmployeeDesignation"] = Sessiondr["EmployeeDesignation"].ToString();
            Session["SessionEmployeeDepartment"] = Sessiondr["EmployeeDepartment"].ToString();
            Session["SessionEmployeeID"] = Sessiondr["EmployeeID"].ToString();
            string raja = Session["SessionEmployeeID"].ToString();
            string raj = Sessiondr["EmployeeID"].ToString();
            Session["SessionEmployeeName"] = Sessiondr["EmployeeName"].ToString();
            Session["SessionSWClick"] = "Asset Insert";
            Session["SessionHWClick"] = "Asset Insert";
            Session["Sessiondefault"] = "no";
            Sessiondr.Close();
            //Sessioncon.Close();
            //Sessiondr.Dispose();
        }
        else
        {
            Response.Write("<script>alert('problem occured when reading sessions');</script>");
        }

    }

    protected void encryptpwdfun(string pwd)
    {
        byte[] hs = new byte[50];
        //string pass = pwd;
        MD5 md5 = MD5.Create();
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(pwd);
        byte[] hash = md5.ComputeHash(inputBytes);
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < hash.Length; i++)
        {
            hs[i] = hash[i];
            sb.Append(hs[i].ToString("x2"));
        }
        var hash_pass = sb.ToString();
        Encryptpwd = hash_pass;
    }

    protected void UserMailID_TextChanged(object sender, EventArgs e)
    {
        if (Page.IsPostBack == true && UserMailID.Text != String.Empty.ToString())
        {
            SqlConnection tempcon = new SqlConnection(constr);
            tempcon.Open();
            SqlCommand tempcmd = new SqlCommand("Select * From Employee where EmployeeMailID='" + UserMailID.Text + "' AND isactive = '1'", tempcon);
            SqlDataReader tempdr = tempcmd.ExecuteReader();
            tempdr.Read();
            if (tempdr.HasRows && tempdr["SecondaryPosition"].ToString() != "Approver" && tempdr["Position"].ToString() == "Employee")
            {
                Apprvckeck.Visible = false;
                UserPassword.Focus();
            }
            else if (tempdr.HasRows && tempdr["SecondaryPosition"].ToString() == "Approver" && tempdr["Position"].ToString() == "Employee")
            {
                Apprvckeck.Visible = true;
                UserPassword.Focus();
            }
            else if (tempdr.HasRows && tempdr["SecondaryPosition"].ToString() != "Approver" && tempdr["Position"].ToString() == "Admin")
            {
                Apprvckeck.Visible = false;
                UserPassword.Focus();
            }
            else
            {
                Apprvckeck.Visible = false;
                Response.Write("<script>alert('Invalid Mail ID');</script>");
                UserMailID.Focus();
            }
            //UserPassword.Focus();
        }
        else
        {
            UserMailID.Focus();
        }
    }
}
