using System;
using System.Activities.Validation;
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

public partial class Registration : System.Web.UI.Page
{
    public string constr = ConfigurationManager.ConnectionStrings["SNPLDBSTRING"].ConnectionString;
    public string mailcontentpwd;
    public string Encryptpwd;
    protected void Page_Load(object sender, EventArgs e)
    {
        //ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        alert.Visible = false;
        pwdtemp.Text = string.Empty.ToString();
    }

    protected void register_Click(object sender, EventArgs e)
    {
		try
		{
			SqlConnection checkcon = new SqlConnection(constr);
			checkcon.Open();
			SqlCommand checkcmd = new SqlCommand("Select EmployeeID, EmployeeMailID from Employee where EmployeeID='" + Empid.Text + "'and EmployeeMailID='" + Emailid.Text + "' ", checkcon);
			SqlDataReader checkdr = (checkcmd.ExecuteReader());

			if (checkdr.HasRows)
			{
				alertmod.Style.Add("background-color", "#ffc2b3");
				alert.Style.Add("background-color", "#ffc2b3");
				fai.Visible = true;
				Label1.Text = "Failure!";
				Label1.ForeColor = System.Drawing.Color.Red;
				Label5.ForeColor = System.Drawing.Color.Black;
				Label5.Text = "Employee Already Registered";
				alert.Visible = true;
			}
			else
			{
				checkdr.Close();
				SqlConnection retrievecon = new SqlConnection(constr);
				retrievecon.Open();
				SqlCommand retrievecmd = new SqlCommand("Select EmployeeID, EmployeeName, EmployeeMailID, " +
								"EmployeePhoneNumber, EmployeeDesignation, EmployeeDepartment,EmployeeLocation " +
								"from EmployeeMaster " +
								"where EmployeeID ='" + Empid.Text + "'AND EmployeeMailID ='" + Emailid.Text + "'  ", retrievecon);
				SqlDataReader retrievedr = (retrievecmd.ExecuteReader());
				retrievedr.Read();
				string str1 = (string)retrievedr["EmployeeID"];
				string str2 = (string)retrievedr["EmployeeName"];
				Session["SessionEmpName"] = str2;
				string str3 = (string)retrievedr["EmployeeMailID"];
				string str4 = Convert.ToInt64(retrievedr["EmployeePhoneNumber"]).ToString();
				string str5 = (string)retrievedr["EmployeeDesignation"];
				string str6 = (string)retrievedr["EmployeeDepartment"];
				mailcontentpwd = "snpl" + GenerateRandomNo();
				string str7 = mailcontentpwd;
				encryptpwd(str7);
				string str8 = (string)retrievedr["EmployeeLocation"];
				retrievedr.Close();
				//retrievedr.Read();
				if (str7 != String.Empty.ToString())
				{
					SqlCommand insertcmd = new SqlCommand("insert into Employee " +
						"(EmployeeID, EmployeeName, EmployeeMailID, EmployeePhoneNumber, EmployeeDesignation, EmployeeDepartment, EmployeePassword, EmployeeLocation, Position, attemptcount, isactive, Employeelevel ) " +
						"values(@var1,@var2,@var3,@var4,@var5,@var6,@var7,@var8, @var9, @var10, @var11, @var12)", retrievecon);
					insertcmd.Parameters.AddWithValue("@var1", str1);
					insertcmd.Parameters.AddWithValue("@var2", str2);
					insertcmd.Parameters.AddWithValue("@var3", str3);
					insertcmd.Parameters.AddWithValue("@var4", str4);
					insertcmd.Parameters.AddWithValue("@var5", str5);
					insertcmd.Parameters.AddWithValue("@var6", str6);
					//pwdtemp.Text = "snpl" + GenerateRandomNo().ToString();
					insertcmd.Parameters.AddWithValue("@var7", Encryptpwd);
					insertcmd.Parameters.AddWithValue("@var8", str8);
                    if(str5 == "Admin")
                    {
                        insertcmd.Parameters.AddWithValue("@var9", str5);
                    }
                    else if(str5 != "Admin")
                    {
                        insertcmd.Parameters.AddWithValue("@var9", "Employee");
                    }
					insertcmd.Parameters.AddWithValue("@var10", "0");
					insertcmd.Parameters.AddWithValue("@var11", "1");
					insertcmd.Parameters.AddWithValue("@var12", "Junior");
					retrievedr.Close();
					insertcmd.ExecuteNonQuery();
					SentEmailforRegistration();
					suc.Visible = true;
					alertmod.Style.Add("background-color", "#d7ecc6");
					alert.Style.Add("background-color", "#d7ecc6");
					Label1.ForeColor = System.Drawing.ColorTranslator.FromHtml("green");
					Label5.ForeColor = System.Drawing.ColorTranslator.FromHtml("black");
					Label1.Text = "Success!";
					Label5.Text = "Registered successfully";
					alert.Visible = true;

				}
				retrievecon.Close();
				retrievecon.Dispose();
			}
			checkcon.Close();
			checkcon.Dispose();
		}
        catch(Exception ex)
		{
			Response.Write("<script>alert('Enter Valid Details.');</script>");
		}
    }
    
    public Int32 GenerateRandomNo()
    {
        Int32 _min = 100000;
        Int32 _max = 999999;
        Random _rdm = new Random();
        return _rdm.Next(_min, _max);
    }

    protected void encryptpwd(string pwd)
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

    protected void SentEmailforRegistration()
    {
        try
        {
            using (MailMessage mm = new MailMessage("helpdesk.snpl@gmail.com", Emailid.Text))
            {
                mm.Subject = "Employee Registration";
                //Label8.Text = GenerateRandomNo().ToString();
                //mm.Body = "Your Verification Code is: " + Label8.Text + "\n\nPlease Do not reply to this mail";
                mm.Body = RegMailContent();
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.UseDefaultCredentials = true;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = true;
                System.Net.NetworkCredential NetworkCred = new NetworkCredential("helpdesk.snpl@gmail.com", "jdzydiyrrvfnrjbf");
                //System.Net.NetworkCredential NetworkCred = new NetworkCredential("amsamshelpdesk.snpl@gmail.com", "bjrlnpztszwauuhk");
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Check your Registered Mail ID for credentials.');", true);
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

    private string RegMailContent()
    {
        string textbody = string.Empty;
        using (StreamReader reader = new StreamReader(Server.MapPath("~/Mail Content Pages/Mail Content Registration Successful.html")))
        {
            textbody = reader.ReadToEnd();
        }
        textbody = textbody.Replace("[mailid]", Emailid.Text);
        textbody = textbody.Replace("[passwd]", mailcontentpwd);
        textbody = textbody.Replace("[Empname]", Session["SessionEmpName"].ToString());
        return textbody;
    }
    
    protected void Empid_TextChanged(object sender, EventArgs e)
    {
        if(Page.IsPostBack==true)
        {
            SqlConnection funcon = new SqlConnection(constr);
            funcon.Open();
            SqlCommand funcmd = new SqlCommand("Select EmployeeID from EmployeeMaster " +
                            "where EmployeeID ='" + Empid.Text + "'", funcon);
            SqlDataReader fundr = (funcmd.ExecuteReader());
            fundr.Read();
            if (!fundr.HasRows)
            {
                //Response.Write("<sctipt>alert('Invalid Entry.');</sctipt>");
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Invalid Entry.');", true);
            }
            fundr.Close();
            fundr.Dispose();
            funcon.Close();
            funcon.Dispose();
        }
    }
}