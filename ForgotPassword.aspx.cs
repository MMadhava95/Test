using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ForgotPassword : System.Web.UI.Page
{
    public string constr = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
    public string mailcontentpwd;
    public string Encryptpwd;
    protected void Page_Load(object sender, EventArgs e)
    {


    }

    protected void register_Click(object sender, EventArgs e)
    {
        SqlConnection checkcon = new SqlConnection(constr);
        checkcon.Open();
        SqlCommand checkcmd = new SqlCommand("Select * from Employee where EmployeeID='" + empid.Text + "'AND EmployeeMailID='" + emailid.Text + "' AND isactive = '1' ", checkcon);
        SqlDataReader checkdr = (checkcmd.ExecuteReader());
        checkdr.Read();

        if (checkdr.HasRows && empapprvradiobtnlist.SelectedIndex == 1 && checkdr["Position"].ToString() == "Employee" && checkdr["SecondaryPosition"].ToString() == "Approver")
        {
            tempsessions();
            mailcontentpwd = "snpl" + GenerateRandomNo();
            updatingApprvrpwdfun();
        }
        else if (checkdr.HasRows && empapprvradiobtnlist.SelectedIndex == 0 && checkdr["Position"].ToString() == "Employee")
        {
            tempsessions();
            mailcontentpwd = "snpl" + GenerateRandomNo();
            updatingEmppwdfun();
        }
        else if (checkdr.HasRows && empapprvradiobtnlist.SelectedIndex == 1 && checkdr["Position"].ToString() == "Employee" && checkdr["SecondaryPosition"].ToString() != "Approver")
        {
            Response.Write("<script>alert('Invalid Atempt. Please select your Valid position Employee / Approver');</script>");
        }
        else
        {
            Response.Write("<script>alert('Please Enter valid Data');</script>");
        }
        checkcon.Close();
        checkcon.Dispose();
    }

    protected void updatingEmppwdfun()
    {
        encryptpwdfun(mailcontentpwd);
        try
        {
            var UpdatepwdCmd = "UPDATE Employee SET EmployeePassword = @var1 WHERE  (Position='Employee' AND EmployeeMailID = '" + Session["tempSessionUserMailID"].ToString() + "' AND isactive = '1')";
            using (SqlConnection Updatepwdcon = new SqlConnection(constr))
            {
                using (SqlCommand Updatepwd = new SqlCommand(UpdatepwdCmd, Updatepwdcon))
                {
                    encryptpwdfun(mailcontentpwd);
                    Updatepwd.Parameters.AddWithValue("@var1", Encryptpwd);
                    Updatepwdcon.Open();
                    Updatepwd.ExecuteNonQuery();
                    Updatepwdcon.Close();
                }
            }
            SentEmailforpwdreset();
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('Password Update was Unsuccessful.');</script>");
            throw ex;
        }
    }

    protected void updatingApprvrpwdfun()
    {
        encryptpwdfun(mailcontentpwd);
        try
        {
            var UpdatepwdCmd = "UPDATE ERSApprover SET ERSApproverPassword = @var1 WHERE  (Position='Approver' AND ERSApproverMailID = '" + Session["tempSessionUserMailID"].ToString() + "' AND isactive = '1')";
            using (SqlConnection Updatepwdcon = new SqlConnection(constr))
            {
                using (SqlCommand Updatepwd = new SqlCommand(UpdatepwdCmd, Updatepwdcon))
                {
                    encryptpwdfun(mailcontentpwd);
                    Updatepwd.Parameters.AddWithValue("@var1", Encryptpwd);
                    Updatepwdcon.Open();
                    Updatepwd.ExecuteNonQuery();
                    Updatepwdcon.Close();
                }
            }
            SentEmailforpwdreset();
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('Password Update was Unsuccessful.');</script>");
            throw ex;
        }
    }

    public Int32 GenerateRandomNo()
    {
        Int32 _min = 100000;
        Int32 _max = 999999;
        Random _rdm = new Random();
        return _rdm.Next(_min, _max);
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

    protected void SentEmailforpwdreset()
    {
        try
        {
            using (MailMessage mm = new MailMessage("helpdesk.snpl@gmail.com", emailid.Text))
            {
                mm.Subject = "Employee Forgot Password";
                //Label8.Text = GenerateRandomNo().ToString();
                //mm.Body = "Your Verification Code is: " + Label8.Text + "\n\nPlease Do not reply to this mail";
                mm.Body = forgotpwdMailContent();
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
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Password Resetting was successful. Check your Registered Mail ID and for the new password.');", true);
            }
        }
        catch (Exception ex)
        {
            //string message = string.Format("Message: {0}\\n\\n", ex.Message);
            //message += string.Format("StackTrace: {0}\\n\\n", ex.StackTrace.Replace(Environment.NewLine, string.Empty));
            //message += string.Format("Source: {0}\\n\\n", ex.Source.Replace(Environment.NewLine, string.Empty));
            //message += string.Format("TargetSite: {0}", ex.TargetSite.ToString().Replace(Environment.NewLine, string.Empty));
            //// ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert(\"" + message + "\");", true);
            //Session["eswar"] = message;

            //Session["pagename"] = "~/AuthenticationPages/Register.aspx";
            //Response.Redirect("~/Admin/Errorpage.aspx");
            throw ex;
        }
    }

    private string forgotpwdMailContent()
    {
        string textbody = string.Empty;
        using (StreamReader reader = new StreamReader(Server.MapPath("~/Mail Content Pages/Mail Content Forgot Password.html")))
        {
            textbody = reader.ReadToEnd();
        }
        textbody = textbody.Replace("[pwd]", mailcontentpwd);
        return textbody;
    }

    protected void tempsessions()
    {
        SqlConnection Sessioncon = new SqlConnection(constr);
        Sessioncon.Open();
        SqlCommand Sessioncmd = new SqlCommand("select * from EmployeeMaster where EmployeeMailID='" + emailid.Text + "'", Sessioncon);
        SqlDataReader Sessiondr = Sessioncmd.ExecuteReader();
        Sessiondr.Read();
        Session["tempSessionUserMailID"] = emailid.Text;
        Session["tempSessionEmployeeDesignation"] = Sessiondr["EmployeeDesignation"].ToString();
        Session["tempSessionEmployeeID"] = Sessiondr["EmployeeID"].ToString();
        Session["tempSessionEmployeeName"] = Sessiondr["EmployeeName"].ToString();
        Sessiondr.Close();
        Sessiondr.Dispose();
    }

    
}
