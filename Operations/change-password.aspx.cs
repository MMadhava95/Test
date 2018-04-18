using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class changepassword : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
    string Encryptpwd;
    protected void Page_Load(object sender, EventArgs e)
    {
        //ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        if (Session["SessionUserMailID"] != null && ((Session["SessionUserPosition"].ToString() == "Employee") || (Session["SessionUserSecondryPosition"].ToString() == "Approver")))
        {
            //true
        }
        else
        {
            //false
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void currentPassword_TextChanged(object sender, EventArgs e)
    {
        if ((Session["SessionUserPosition"].ToString() == "Employee" && Session["SessionUserSecondryPosition"].ToString() != "Approver") || (Session["SessionUserPosition"].ToString() == "Employee" && Session["SessionUserSecondryPosition"].ToString() == "Approver" && Session["SessionUserSecondryPositioncheck"].ToString() == "false"))
        {
            currentPassword.Attributes.Add("onblur", "this.type='password';");
            encryptpwdfun(currentPassword.Text);
            if (Page.IsPostBack == true)
            {
                SqlConnection checkoldpwdcon = new SqlConnection(constr);
                checkoldpwdcon.Open();
                SqlCommand checkoldpwdcmd = new SqlCommand("select EmployeePassword from Employee where EmployeePassword = '" + Encryptpwd + "'", checkoldpwdcon);
                SqlDataReader checkoldpwddr = checkoldpwdcmd.ExecuteReader();
                checkoldpwddr.Read();
                if (checkoldpwddr.HasRows)
                {
                    //currentPassword.Attributes.Add("onfocus", "this.type='text';");
                    //currentPassword.Attributes.Add("onfocus", "this.type='password';");
                    //currentPassword.Attributes["TextMode"] = "Password";
                    newpassword.Focus();
                }
                else
                {
                    //Response.Write("<script>alert('Please Enter correct password');</script>");
                    currentPasswordRequiredFieldValidator.Text = "Please Enter Correct Password";
                    currentPasswordRequiredFieldValidator.IsValid = false;
                    currentPassword.Text = string.Empty.ToString();
                    currentPassword.Focus();
                }
                checkoldpwdcon.Close();
            }
        }
        else if (Session["SessionUserPosition"].ToString() == "Employee" && Session["SessionUserSecondryPosition"].ToString() == "Approver" && Session["SessionUserSecondryPositioncheck"].ToString() == "true")
        {
            currentPassword.Attributes.Add("onblur", "this.type='password';");
            encryptpwdfun(currentPassword.Text);
            if (Page.IsPostBack == true)
            {
                SqlConnection checkoldpwdcon = new SqlConnection(constr);
                checkoldpwdcon.Open();
                SqlCommand checkoldpwdcmd = new SqlCommand("select ERSApproverPassword from ERSApprover where ERSApproverPassword = '" + Encryptpwd + "'", checkoldpwdcon);
                SqlDataReader checkoldpwddr = checkoldpwdcmd.ExecuteReader();
                checkoldpwddr.Read();
                if (checkoldpwddr.HasRows)
                {
                    //currentPassword.Attributes.Add("onfocus", "this.type='text';");
                    //currentPassword.Attributes.Add("onfocus", "this.type='password';");
                    //currentPassword.Attributes["TextMode"] = "Password";
                    newpassword.Focus();
                }
                else
                {
                    //Response.Write("<script>alert('Please Enter correct password');</script>");
                    currentPasswordRequiredFieldValidator.Text = "Please Enter Correct Password";
                    currentPasswordRequiredFieldValidator.IsValid = false;
                    currentPassword.Text = string.Empty.ToString();
                    currentPassword.Focus();
                }
                checkoldpwdcon.Close();
            }
        }
    }

    protected void pwdSavebtn_Click(object sender, EventArgs e)
    {
        try
        {
            if ((Session["SessionUserPosition"].ToString() == "Employee" && Session["SessionUserSecondryPosition"].ToString() != "Approver") || (Session["SessionUserPosition"].ToString() == "Employee" && Session["SessionUserSecondryPosition"].ToString() == "Approver" && Session["SessionUserSecondryPositioncheck"].ToString() == "false"))
            {
                var UpdatepwdCmd = "UPDATE Employee SET EmployeePassword = @var1 " +
                "WHERE  (Position='Employee' AND EmployeeMailID = '" + Session["SessionUserMailID"].ToString() + "')";
                using (SqlConnection Updatepwdcon = new SqlConnection(constr))
                {
                    using (SqlCommand Updatepwd = new SqlCommand(UpdatepwdCmd, Updatepwdcon))
                    {
                        encryptpwdfun(newpassword.Text);
                        Updatepwd.Parameters.AddWithValue("@var1", Encryptpwd);
                        Updatepwdcon.Open();
                        Updatepwd.ExecuteNonQuery();
                        Updatepwdcon.Close();
                        currentPassword.Text = newpassword.Text = confirmpassword.Text = string.Empty.ToString();
                    }
                }
                SentEmailforpwdchanging();
                //Response.Write("<script>alert('Password Update was successful.');</script>");
            }
            else if (Session["SessionUserPosition"].ToString() == "Employee" && Session["SessionUserSecondryPosition"].ToString() == "Approver" && Session["SessionUserSecondryPositioncheck"].ToString() == "true")
            {
                var UpdatepwdCmd = "UPDATE ERSApprover SET ERSApproverPassword = @var1 " +
                "WHERE  (Position ='Approver' AND ERSApproverMailID = '" + Session["SessionUserMailID"].ToString() + "')";
                using (SqlConnection Updatepwdcon = new SqlConnection(constr))
                {
                    using (SqlCommand Updatepwd = new SqlCommand(UpdatepwdCmd, Updatepwdcon))
                    {
                        encryptpwdfun(newpassword.Text);
                        Updatepwd.Parameters.AddWithValue("@var1", Encryptpwd);
                        Updatepwdcon.Open();
                        Updatepwd.ExecuteNonQuery();
                        Updatepwdcon.Close();
                        currentPassword.Text = newpassword.Text = confirmpassword.Text = string.Empty.ToString();
                    }
                }
                SentEmailforpwdchanging();
                //Response.Write("<script>alert('Password Update was successful.');</script>");
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('Password Update was Unsuccessful.');</script>");
            throw ex;
        }
    }

    protected void SentEmailforpwdchanging()
    {
        try
        {
            using (MailMessage mm = new MailMessage("helpdesk.snpl@gmail.com", Session["SessionUserMailID"].ToString()))
            {
                mm.Subject = "Password Changing";
                mm.Body = pwdchangingmailContent();
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.UseDefaultCredentials = true;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = true;
                System.Net.NetworkCredential NetworkCred = new NetworkCredential("helpdesk.snpl@gmail.com", "jdzydiyrrvfnrjbf");
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Password Changed Successfully.');", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private string pwdchangingmailContent()
    {
        string textbody = string.Empty;
        using (StreamReader reader = new StreamReader(Server.MapPath("~/Mail Content Pages/Mail Content Password Update.html")))
        {
            textbody = reader.ReadToEnd();
        }
        return textbody;
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
}
